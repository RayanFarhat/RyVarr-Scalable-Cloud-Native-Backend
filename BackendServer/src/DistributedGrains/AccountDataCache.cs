using Orleans.Runtime;
using BackendServer.DTOs;
using BackendServer.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BackendServer.Authentication;

namespace BackendServer.DistributedGrains;

public class AccountDataCache
{
    private readonly IClusterClient _clusterClient;
    private readonly RyvarrDb _db;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountDataCache(IClusterClient clusterClient, RyvarrDb db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _clusterClient = clusterClient;
        _userManager = userManager;
    }

    // use write-through
    public async Task AddOrUpdate(AccountData entity)
    {
        var grain = _clusterClient.GetGrain<IAccountDataGrain>(entity.Id);
        var value = await grain.Get();
        if (value.Id != "")
        {
            await grain.Set(entity);
        }
        AccountData? row = _db.AccountData.Where(row => row.Id == entity.Id).FirstOrDefault();
        if (row == null)
            _db.AccountData.Add(entity);
        else
        {
            _db.Entry(row).State = EntityState.Detached;
            _db.AccountData.Update(entity);
        }

        await _db.SaveChangesAsync();
    }
    public async Task<AccountData?> Get(string UserId)
    {
        var grain = _clusterClient.GetGrain<IAccountDataGrain>(UserId);
        //if userid is not cached then get from database and update on cache
        var value = await grain.Get();
        if (value.Id == "")
        {
            value = _db.AccountData.Where(row => row.Id == UserId).FirstOrDefault();
            if (value == null)
                return null;

            await grain.Set(value);
        }

        if (NeedToCancelPro(value))
        {
            value.IsPro = false;
            await AddOrUpdate(value);
            //also change role
            await RemoveRoleUserPro(value.Id);
        }
        return value;
    }
    public async Task<bool> RemoveIncludeUserManager(string UserId)
    {
        AccountData? row = _db.AccountData.Where(row => row.Id == UserId).FirstOrDefault();
        if (row == null)
            return false;

        // delete from my userManager Tables
        var user = await _userManager.FindByIdAsync(UserId);
        if (user == null)
        {
            return false;
        }
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded == false)
        {
            return false;
        }
        // delete from my accountData Table
        var grain = _clusterClient.GetGrain<IAccountDataGrain>(UserId);
        await grain.Clear();
        _db.AccountData.Remove(row);
        _db.SaveChanges();
        return true;
    }

    private static bool NeedToCancelPro(AccountData entity)
    {
        DateTime d = DateTime.ParseExact(entity.ProEndingDate, "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        if (d < DateTime.Now && entity.IsPro == true)
        {
            return true;
        }
        return false;
    }
    private async Task RemoveRoleUserPro(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.RemoveFromRoleAsync(user!, UserRoles.UserPro);
        await _userManager.AddToRoleAsync(user!, UserRoles.User);
    }
}
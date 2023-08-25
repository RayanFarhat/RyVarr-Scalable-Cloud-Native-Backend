using Orleans.Runtime;
using BackendServer.DTOs;
using BackendServer.DB;

namespace BackendServer.DistributedGrains;

public class AccountDataCache
{
    private readonly IClusterClient _clusterClient;
    private readonly RyvarrDb _db;

    public AccountDataCache(IClusterClient clusterClient, RyvarrDb db)
    {
        _db = db;
        _clusterClient = clusterClient;
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
            _db.AccountData.Update(entity);
        _db.SaveChanges();
    }
    public async Task<AccountData?> Get(string UserId)
    {
        var grain = _clusterClient.GetGrain<IAccountDataGrain>(UserId);
        //if userid is not cached then get from database and update on cache
        var value = await grain.Get();
        if (value.Id == "")
        {
            AccountData? row = _db.AccountData.Where(row => row.Id == UserId).FirstOrDefault();
            if (row == null)
                return null;

            await grain.Set(row);
            return row;
        }
        return value;
    }
    public async Task<bool> Remove(string UserId)
    {
        AccountData? row = _db.AccountData.Where(row => row.Id == UserId).FirstOrDefault();
        if (row == null)
            return false;
        var grain = _clusterClient.GetGrain<IAccountDataGrain>(UserId);
        await grain.Clear();
        _db.AccountData.Remove(row);
        _db.SaveChanges();
        return true;
    }
}
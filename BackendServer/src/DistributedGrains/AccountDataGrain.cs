using Orleans.Runtime;
using BackendServer.DTOs;
namespace BackendServer.DistributedGrains;

public interface IAccountDataGrain : IGrainWithStringKey
{
    Task Set(AccountData accountData);
    Task<AccountData> Get();
    Task Clear();
}

public class AccountDataGrain : Grain, IAccountDataGrain
{
    private readonly IPersistentState<AccountData> _state;

    public AccountDataGrain(
        [PersistentState(stateName: "AccountData")]
            IPersistentState<AccountData> state) => _state = state;

    public async Task Set(AccountData accountData)
    {
        _state.State = new AccountData(accountData.Id, accountData.Username, accountData.IsPro, accountData.ProEndingDate);

        await _state.WriteStateAsync();
    }

    public Task<AccountData> Get()
    {
        return Task.FromResult(_state.State);
    }

    public async Task Clear()
    {
        await _state.ClearStateAsync();
    }
}
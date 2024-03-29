﻿using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using WebSore_Edu.WebAPI;
using WebStore_Edu.Domain.Identity;
using WebStore_Edu.Interfaces.Identity;
using WebStore_Edu.WebAPI.Clients.Base;

namespace WebStore_Edu.WebAPI.Clients.Identity;

public class RolesClient : BaseClient , IRoleClient
{
    public RolesClient(HttpClient HttpHttp) : base(HttpHttp, ApiAddresses.Roles)
    {
    }

    #region IRoleStore<Role>

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel)
    {
        var response = await PostAsync(Address, role, cancel).ConfigureAwait(false);
        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

        return result
            ? IdentityResult.Success
            : IdentityResult.Failed();
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel)
    {
        var response = await PutAsync(Address, role, cancel).ConfigureAwait(false);
        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

        return result
            ? IdentityResult.Success
            : IdentityResult.Failed();
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/Delete", role, cancel).ConfigureAwait(false);
        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<bool>(cancellationToken: cancel).ConfigureAwait(false);

        return result
            ? IdentityResult.Success
            : IdentityResult.Failed();
    }

    public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/GetRoleId", role, cancel).ConfigureAwait(false);
        return await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadAsStringAsync(cancel)
           .ConfigureAwait(false);
    }

    public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/GetRoleName", role, cancel).ConfigureAwait(false);
        return await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadAsStringAsync(cancel)
           .ConfigureAwait(false);
    }

    public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/SetRoleName/{name}", role, cancel).ConfigureAwait(false);
        role.Name = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadAsStringAsync(cancel)
           .ConfigureAwait(false);
    }

    public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/GetNormalizedRoleName", role, cancel).ConfigureAwait(false);
        return await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadAsStringAsync(cancel)
           .ConfigureAwait(false);
    }

    public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
    {
        var response = await PostAsync($"{Address}/SetNormalizedRoleName/{name}", role, cancel).ConfigureAwait(false);
        role.NormalizedName = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadAsStringAsync(cancel)
           .ConfigureAwait(false);
    }

    public async Task<Role> FindByIdAsync(string id, CancellationToken cancel)
    {
        return (await GetAsync<Role>($"{Address}/FindById/{id}", cancel)
           .ConfigureAwait(false))!;
    }

    public async Task<Role> FindByNameAsync(string name, CancellationToken cancel)
    {
        return (await GetAsync<Role>($"{Address}/FindByName/{name}", cancel)
           .ConfigureAwait(false))!;
    }

    #endregion
}
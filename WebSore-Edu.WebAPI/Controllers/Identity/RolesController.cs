using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore_Edu.DAL.Context;
using WebStore_Edu.Domain.Identity;

namespace WebSore_Edu.WebAPI.Controllers.Identity
{
    [Route(ApiAddresses.Roles)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;

        public RolesController(WebStoreDb db)
        {
            _RoleStore = new RoleStore<Role>(db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAll() => await _RoleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(Role role)
        {
            var creationResult = await _RoleStore.CreateAsync(role);
            // Добавить логирование в случае Succeeded == false
            return creationResult.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role)
        {
            var uprateResult = await _RoleStore.UpdateAsync(role);
            return uprateResult.Succeeded;
        }

        [HttpDelete]
        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role)
        {
            var deleteResult = await _RoleStore.DeleteAsync(role);
            return deleteResult.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role) => await _RoleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role) => await _RoleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync(Role role, string name)
        {
            await _RoleStore.SetRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await _RoleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync(Role role, string name)
        {
            await _RoleStore.SetNormalizedRoleNameAsync(role, name);
            await _RoleStore.UpdateAsync(role);
            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await _RoleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await _RoleStore.FindByNameAsync(name);
    }
}

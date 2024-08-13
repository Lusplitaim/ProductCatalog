using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Services;

namespace ProductCatalog.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService m_UserService;
        public UsersController(IUserService userService)
        {
            m_UserService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await m_UserService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var result = await m_UserService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto model)
        {
            var result = await m_UserService.CreateAsync(model);
            return this.ResolveResult(result, () => Created(nameof(CreateUser), result.Result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto model)
        {
            var result = await m_UserService.UpdateAsync(id, model);
            return this.ResolveResult(result, () => Ok(result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await m_UserService.DeleteAsync(id);
            return this.ResolveResult(result, Ok);
        }
    }
}

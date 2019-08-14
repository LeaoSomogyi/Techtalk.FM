using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts.Services;
using DTO = Techtalk.FM.Domain.DTOs;
using Entities = Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> Login(DTO.User user)
        {
            try
            {
                DTO.Token token = await _loginService.Authenticate(new Entities.User(user));

                if (token == null)
                {
                    return BadRequest(new
                    {
                        type = "ERROR",
                        message = "Usuário ou senha incorretos."
                    });
                }
                else
                    return Ok(token);
            }
            catch (Exception ex)
            {
                return new UnprocessableEntityObjectResult(ex.Message);
            }
        }
    }
}
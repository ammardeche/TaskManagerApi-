using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Dtos;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]

        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.getUser();
            var userDto = new UserDto(user);
            return Ok(userDto);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        {
            try
            {

                // HERE WE USE THE DTO FOR THE PRESENTATION LAYER 
                var result = await _userService.UpdateUserPassword(
                    oldPassword: updateUserPasswordDto.CurrentPassword!,
                    newPassword: updateUserPasswordDto.NewPassword!,
                    confirmPassword: updateUserPasswordDto.ConfirmPassword!);

                if (result.Succeeded)
                    return Ok("Password has been updated successfully");
                return BadRequest(result.Errors);
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApi.Models;

namespace TaskApi.Dtos
{
    public class UserDto
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public UserDto(User user)
        {
            FullName = user.FullName;
            Email = user.Email;
        }
    }
}
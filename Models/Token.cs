using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public class Token
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
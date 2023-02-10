using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}

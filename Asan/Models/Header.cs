using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Header
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Svg { get; set; }
        public string Phone { get; set; }
        public string Text { get; set; }
        public bool IsDeactive { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}

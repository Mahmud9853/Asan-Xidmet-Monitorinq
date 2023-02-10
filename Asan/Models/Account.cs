using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Attachment { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public bool IsDeactive { get; set; }
    }
}

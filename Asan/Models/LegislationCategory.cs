using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class LegislationCategory
    {
        public int Id { get; set; }

        public string Title { get; set; }
    
        public string Image { get; set; }
        public bool IsDeactive { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<Legislation> Legislations { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneCenter { get; set; }
        public string EmailCenter { get; set; }
        public string EmailAsan { get; set; }
        public string LocationFirst { get; set; }
        public string LocationTwo { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string PhoneSvg { get; set; }
        public string PhoneCenterSvg { get; set; }
        public string EmailCenterSvg { get; set; }
        public string EmailAsanSvg { get; set; }
        public string LocationSvgFirst { get; set; }
        public string LocationSvgTwo { get; set; }
        public bool IsDeactive { get; set; }

    }
}

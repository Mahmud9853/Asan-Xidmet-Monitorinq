using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class MailVM
    {
            [Required]
            public string Message { get; set; }
    }
}

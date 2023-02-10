using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Connect
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDeactive { get; set; }
        public int Count { get; set; }
        public bool IsReading { get; set; }
    }
}

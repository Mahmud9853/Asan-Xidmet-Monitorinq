using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public bool IsDeactive { get; set; }
    }
}

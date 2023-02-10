using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Fine> Fines { get; set; }
        public bool IsDeactive { get; set; }
    }
}

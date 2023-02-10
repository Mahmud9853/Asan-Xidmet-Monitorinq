using Asan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class PenaltyVM
    {
        public List<Penalty> Penalty { get; set; }
        public List<PenaltyCategory> PenaltyCategory { get; set; }
        public List<Fine> Fines { get; set; }
        public List<Category> Category { get; set; }
    }
}

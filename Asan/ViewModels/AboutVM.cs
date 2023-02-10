using Asan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class AboutVM
    {
        public List<Legislation> Legislations { get; set; }
        public List<LegislationCategory> LegislationCategories { get; set; }
    }
}

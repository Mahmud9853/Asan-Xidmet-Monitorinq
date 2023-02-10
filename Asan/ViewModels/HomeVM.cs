using Asan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Contract> Contracts { get; set; }
        public List<Document> Documents { get; set; }
        public List<AboutSlider> AboutSliders { get; set; }
        //public List<Connect> Connects { get; set; }

    }
}

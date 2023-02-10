using Asan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.ViewModels
{
    public class StatisticVM
    {
        public List<Static> Statics { get; set; }
        public List<Statistic> Statistics { get; set; }
        public StatisticImg StatisticImg { get; set; }
    }
}


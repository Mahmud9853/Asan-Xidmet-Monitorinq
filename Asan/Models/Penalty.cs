using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Penalty
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDeactive { get; set; }
        public PenaltyCategory PenaltyCategory { get; set; }
        public int PenaltyCategoryId { get; set; }


    }
}

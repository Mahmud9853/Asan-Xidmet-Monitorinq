using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class PenaltyCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Penalty> Penalty { get; set; }
        public bool IsDeactive { get; set; }
    }
}

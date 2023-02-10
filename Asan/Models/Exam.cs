using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string SvgFirst { get; set; }
        public string SvgSecond { get; set; }
        public string TitleFirst { get; set; }
        public string TitleSecond { get; set; }
        public bool IsDeactive { get; set; }
    }
}

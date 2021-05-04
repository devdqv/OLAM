using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OLAM.Models
{
    public class PEELINGModel
    {
        public string ss_pressure { get; set; }
        public Nullable<double> value_pressure { get; set; }
        public Nullable<System.DateTime> time_update { get; set; }
        public string ss_speeddrum { get; set; }
        public Nullable<double> Value_speeddrum { get; set; }
        public Nullable<int> timer_action { get; set; }
    }
}
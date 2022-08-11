using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyoTestUnits
{
    public class ResponseMessage
    {
        public string Result { get; set; }

        public string Message { get; set; }

        public int Status { get; set; }
    }

    public class MetricUnit
    {
        public string UnitName { get; set; }
        public string UnitDesc { get; set; }
    }

    public class ImperialUnit
    {
        public string UnitName { get; set; }
        public string UnitDesc { get; set; }
    }
}

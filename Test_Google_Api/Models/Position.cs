using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Google_Api.Models
{
    public class Position
    {
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public string CustomLabel { get; set; }
        public string Review { get; set; }
        public DateTime? Date { get; set; }
        public int? Kitchen { get; set; }
        public int? Service { get; set; }
        public int? Interior { get; set; }
    }
}
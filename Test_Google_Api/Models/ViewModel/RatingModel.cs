using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Google_Api.Models.ViewModel
{
    public class RatingModel
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int? KitchenMark { get; set; }
        public int? InteriorMark { get; set; }
        public int? ServiceMark { get; set; }
        public int RestaurantId { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? Sum { get; set; }
    }
}
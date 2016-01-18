using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test_Google_Api.Models.SaveModels
{
    //This model will be used for sending new review, new network, new city and new district
    public class AdminSaveModel
    {
        //universal part of the model
        //used for creating city, district, network
        public int Id { get; set; }
        public int? CityId { get; set; }


        //review part of the model
       [Required]
        public string RestaurantName { get; set; }

       
        public string ReviewText { get; set; }
        [Required]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get {return DateTime.Now;}  }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Координата не может быть меньше 0")]
        public string Lat { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Координата не может быть меньше 0")]
        public string Lng { get; set; }

        public string KitchenType { get; set; }

        public string WorkHours { get; set; }

        public int? Amount { get; set; }

        public string Children { get; set; }

        public string Propositions { get; set; }

        public string Music { get; set; }

        public string Phones { get; set; }
        public int? NetworkId { get; set; }
        public int? DistrictId { get; set; }

        public int? KitchenMark { get; set; }
        public int? InteriorMark { get; set; }
        public int? ServiceMark { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test_Google_Api.Models.AdminModels
{
    public class District
    {
        [Required]
        public string Name { get; set; }
        public int? CityId { get; set; }
        public int Id { get; set; }
    }
}
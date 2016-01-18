using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test_Google_Api.Models.AdminModels
{
    public class Network
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
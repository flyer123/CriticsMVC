using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;

namespace Test_Google_Api.Models
{
    public class IdentityModels
    {
    }
    public class ApplicationUser : IdentityUser
    {
        //You can extend this class by adding additional fields like Birthday
    }
    
}
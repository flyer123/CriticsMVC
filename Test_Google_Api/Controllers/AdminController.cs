using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Google_Api.Models.ViewModel;
using Test_Google_Api.Models.DB;
using Test_Google_Api.Models.AdminModels;
using Test_Google_Api.Models.SaveModels;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using PagedList;

namespace Test_Google_Api.Controllers
{
    public class AdminController : Controller
    {

        [Authorize]
        public ActionResult CreateReview()
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                //get districts
                var districts = rdb.districts.Select(s => new District { Id = s.Id, CityId = s.CityId, Name = s.District }).ToList();

                //networks
                var networks = rdb.networks.Select(s => new Network { Id = s.Id, Name = s.Network }).ToList();

                //get cities
                var cities = rdb.cities.Select(s => new City { Id = s.Id, Name = s.City }).ToList();
                ViewBag.Districts = districts;
                ViewBag.Networks = networks;
                ViewBag.Cities = cities;

            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateReview(AdminSaveModel rmd)
        {
            int? netId;
            if (rmd.NetworkId == null)
                netId = 1;
            else
                netId = rmd.NetworkId;

            string message;
            string hashtag = "";
            if (rmd.ReviewText != null)
            {
                if (rmd.ReviewText.Contains('#'))
                {
                    //get hashtag expression
                    var regex = new Regex(@"(?<=#)\w+");
                    var matches = regex.Matches(rmd.ReviewText);
                    StringBuilder stb = new StringBuilder();
                    foreach (Match m in matches)
                    {
                        stb.Append(m.Value);
                        stb.Append(" ");
                    }
                    if (stb[stb.Length - 1].ToString().Equals(" "))
                    {
                        stb.Remove(stb.Length - 1, 1);
                    }
                    hashtag = stb.ToString();
                }
            }
            else
            {
                rmd.ReviewText = "";
            }
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                double ln = double.Parse(rmd.Lng.Replace('.', ','));
                double la = double.Parse(rmd.Lat.Replace('.', ','));
                rdb.restaurants.Add(new restaurants
                {
                    Address = rmd.Address,
                    Children = rmd.Children,
                    DateOfCreation = DateTime.Now,
                    DistrictId = rmd.DistrictId,
                    InteriorMark = rmd.InteriorMark,
                    KitchenMark = rmd.KitchenMark,
                    ServiceMark = rmd.ServiceMark,
                    NetworkId = netId,
                    Name = rmd.RestaurantName,
                    Phones = rmd.Phones,
                    Music = rmd.Music,
                    Longitude = ln,
                    Lattitude = la,
                    KitchenType = rmd.KitchenType,
                    Propositions = rmd.Propositions,
                    ReviewText = rmd.ReviewText,
                    SumAmount = rmd.Amount,
                    WorkTime = rmd.WorkHours,
                    CustomLabel = hashtag
                });
                rdb.SaveChanges();
                message = "Successfully Saved!";
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                else
                {
                    return null;
                }

            }
        }




        [Authorize]
        public ActionResult EditReview(string id)
        {
            int idR = Int32.Parse(id);
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var rest = rdb.restaurants.Where(s => s.Id == idR).Select(s => new AdminSaveModel
                {
                    Address = s.Address,
                    Amount = s.SumAmount,
                    Children = s.Children,
                    DistrictId = s.DistrictId,
                    Id = idR,
                    InteriorMark = s.InteriorMark,
                    KitchenMark = s.KitchenMark,
                    KitchenType = s.KitchenType,
                    Lat = s.Lattitude.ToString(),
                    Lng = s.Longitude.ToString(),
                    RestaurantName = s.Name,
                    Music = s.Music,
                    NetworkId = s.NetworkId,
                    Propositions = s.Propositions,
                    ReviewText = s.ReviewText,
                    Phones = s.Phones,
                    ServiceMark = s.ServiceMark,
                    WorkHours = s.WorkTime,
                    CityId = s.districts.CityId
                }).ToList()[0];
                var districts = rdb.districts.Select(s => new District { Id = s.Id, CityId = s.CityId, Name = s.District }).ToList();

                //networks
                var networks = rdb.networks.Select(s => new Network { Id = s.Id, Name = s.Network }).ToList();

                //get cities
                var cities = rdb.cities.Select(s => new City { Id = s.Id, Name = s.City }).ToList();

                ViewBag.Districts = districts;
                ViewBag.Networks = networks;
                ViewBag.Cities = cities;
                return View(rest);
            }
        }


        [HttpPost]
        public ActionResult EditReview(AdminSaveModel rmd)
        {
            int id = rmd.Id;
            string message;
            string hashtag = "";
            try
            {
                //get hashtag expression
                var regex = new Regex(@"(?<=#)\w+");
                var matches = regex.Matches(rmd.ReviewText);
                StringBuilder stb = new StringBuilder();
                foreach (Match m in matches)
                {
                    stb.Append(m.Value);
                    stb.Append(" ");
                }
                if (stb[stb.Length - 1].ToString().Equals(" "))
                {
                    stb.Remove(stb.Length - 1, 1);
                }
                hashtag = stb.ToString();
            }
            catch { }
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var rest = rdb.restaurants.Where(s => s.Id == id).FirstOrDefault();
                if (rest != null)
                {
                    rest.Address = rmd.Address;
                    rest.Children = rmd.Children;
                    rest.DateOfCreation = DateTime.Now;
                    rest.DistrictId = rmd.DistrictId;
                    rest.InteriorMark = rmd.InteriorMark;
                    rest.KitchenMark = rmd.KitchenMark;
                    rest.ServiceMark = rmd.ServiceMark;
                    rest.NetworkId = rmd.NetworkId;
                    rest.Name = rmd.RestaurantName;
                    rest.Phones = rmd.Phones;
                    rest.Music = rmd.Music;
                    rest.Longitude = double.Parse(rmd.Lng.Replace('.', ','));
                    rest.Lattitude = double.Parse(rmd.Lat.Replace('.', ','));
                    rest.KitchenType = rmd.KitchenType;
                    rest.Propositions = rmd.Propositions;
                    rest.ReviewText = rmd.ReviewText;
                    rest.SumAmount = rmd.Amount;
                    rest.WorkTime = rmd.WorkHours;
                    rest.CustomLabel = hashtag;
                }
                rdb.Entry(rest).State = System.Data.Entity.EntityState.Modified;
                rdb.SaveChanges();
                message = "Successfully Saved!";
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                else
                {
                    return null;
                }

            }
        }


        public ActionResult GetImages(int id)
        {

            List<Image> images = new List<Image>();
            if (Directory.Exists((Server.MapPath("~/images/Restaurants/rest_" + id.ToString()))))
            {
                string imageDirectoryPath = Server.MapPath("~/images/Restaurants/rest_" + id.ToString());
                var imageFiles = new DirectoryInfo(imageDirectoryPath).GetFiles();
                foreach (var imgFile in imageFiles)
                {
                    images.Add(new Image { Path = "/images/Restaurants/rest_" + id.ToString() + "/" + imgFile.Name, Name = "/images/Restaurants/rest_" + id.ToString() + "/" + imgFile.Name, URL = imgFile.DirectoryName + "\\" + imgFile.Name });
                }
            }

            return Json(new { Data = images }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateNetwork()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNetwork(Network net)
        {
            string message = "";
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    rdb.networks.Add(new networks { Network = net.Name });
                    rdb.SaveChanges();
                    var id = rdb.networks.Where(s => s.Network.Equals(net.Name)).Select(s => s.Id).ToList()[0];
                    message = "Successfully Saved!" + id.ToString();
                }
                catch
                {
                    message = "Error! Please try again!";
                }
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return PartialView();
                }
            }
        }

        public ActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCity(City city)
        {
            string message = "";
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    rdb.cities.Add(new cities { City = city.Name });
                    rdb.SaveChanges();
                    var id = rdb.cities.Where(s => s.City.Equals(city.Name)).Select(s => s.Id);
                    message = "Successfully Saved!" + id.ToString();
                }
                catch
                {
                    message = "Error! Please try again!";
                }
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return PartialView();
                }
            }
        }
        public ActionResult CreateDistrict()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDistrict(District ds)
        {
            string message = "";
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    rdb.districts.Add(new districts { District = ds.Name, CityId = ds.CityId });
                    rdb.SaveChanges();
                    var id = rdb.districts.Where(s => s.District.Equals(ds.Name)).Select(s => s.Id);
                    message = "Successfully Saved!" + id.ToString();
                }
                catch
                {
                    message = "Error! Please try again!";
                }
                if (Request.IsAjaxRequest())
                {
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return PartialView();
                }
            }
        }



        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            //id of newly created review
            string folderName = "";
            //here we will get the last id of restaurant
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                int id = rdb.restaurants.OrderByDescending(s => s.Id).FirstOrDefault().Id;
                folderName = id.ToString();
            }


            //here we will save images
            bool isSavedSuccessfully = true;
            string fName = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}images\\Restaurants\\" + "rest_" + folderName, Server.MapPath(@"\")));

                    string pathString = originalDirectory.ToString();

                    var fileName1 = Path.GetFileName(file.FileName);


                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = "Новая рецензия занесена в базу" });
            }
            else
            {
                return Json(new { Message = "Не получается сохранить фотографии" });
            }
        }


        [HttpPost]
        public ActionResult UpdateUploadedFile(string folder)
        {
            //id of newly created review
            string folderName = folder.ToString();
            //here we will get the last id of restaurant
            //using (RestaurantsDBEntities rdb = new RestaurantsDBEntities())
            //{
            //    int id = rdb.Restaurants.OrderByDescending(s => s.Id).FirstOrDefault().Id;
            //    folderName = id.ToString();
            //}


            //here we will save images
            bool isSavedSuccessfully = true;
            string fName = "";
            var originalDirectory = new DirectoryInfo(string.Format("{0}images\\Restaurants\\" + "rest_" + folderName, Server.MapPath(@"\")));
            //string[] filePaths = Directory.GetFiles(originalDirectory.ToString());
            //foreach (string filePath in filePaths)
            //{
            //    System.IO.File.Delete(filePath);
            //}
            //here remove all files in directory
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {



                    string pathString = originalDirectory.ToString();

                    var fileName1 = Path.GetFileName(file.FileName);


                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = "Новая рецензия занесена в базу" });
            }
            else
            {
                return Json(new { Message = "Не получается сохранить фотографии" });
            }
        }


        public void RemoveFile(Image file)
        {
            string filePath = file.URL;
            System.IO.File.Delete(filePath);
        }

        [Authorize]
        public ActionResult AdminMenu()
        {
            return View();
        }

        [Authorize]
        public ActionResult SearchReview()
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var restaurants = rdb.restaurants.Select(s => new Restaurant { Name = s.Name, Address = s.Address, Id = s.Id }).ToList();
                ViewBag.Restaurants = restaurants;
                return View();
            }
        }

        [Authorize]
        public ActionResult RemoveRestaurant()
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var networks = rdb.networks.Select(s => new Network { Id = s.Id, Name = s.Network }).ToList();
                ViewBag.Networks = networks;
                var restaurants = rdb.restaurants.Select(s => new Restaurant { Name = s.Name, Address = s.Address, Id = s.Id, NetworkId = s.NetworkId }).ToList();
                ViewBag.Restaurants = restaurants;
                return View();
            }
        }

        [HttpPost]
        public ActionResult RemoveReview(string id)
        {
            int idToCheck = Int32.Parse(id);
            string folderName = id;
            string message;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var rest = rdb.restaurants.Where(s => s.Id == idToCheck).FirstOrDefault();
                    rdb.restaurants.Remove(rest);
                    rdb.SaveChanges();
                    //return Json(true);
                    //return Content(Boolean.TrueString);
                    message = "Successfully removed!";
                    var originalDirectory = new DirectoryInfo(string.Format("{0}images\\Restaurants\\" + "rest_" + folderName, Server.MapPath(@"\")));
                    string pathString = originalDirectory.ToString();
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (isExists)
                    {
                        Directory.Delete(pathString, true);
                    }

                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                catch
                {//TODO: Log error	
                    //return Json(false);
                    //return Content(Boolean.FalseString);
                    message = "Error!";
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
            }
        }

        [Authorize]
        public ActionResult AdminPlaces()
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var cities = rdb.cities.Select(s => new City { Id = s.Id, Name = s.City }).ToList();
                var districts = rdb.districts.Select(s => new District { CityId = s.CityId, Id = s.Id, Name = s.District }).ToList();
                ViewBag.Cities = cities;
                ViewBag.Districts = districts;
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditCity(City cityToUpdate)
        {
            int id = cityToUpdate.Id;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var city = rdb.cities.Where(s => s.Id == id).FirstOrDefault();
                    city.City = cityToUpdate.Name;
                    rdb.Entry(city).State = System.Data.Entity.EntityState.Modified;
                    rdb.SaveChanges();
                    string message = "Successfully Saved!";
                    if (Request.IsAjaxRequest())
                    {
                        return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                    }
                    else
                    {
                        return null;
                    }
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    return Json(false);
                    //return Content(Boolean.FalseString);
                }
            }
        }

        [HttpPost]
        public ActionResult RemoveCity(string id)
        {
            int idToCheck = Int32.Parse(id);
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var city = rdb.cities.Where(s => s.Id == idToCheck).FirstOrDefault();
                    rdb.cities.Remove(city);
                    rdb.SaveChanges();
                    return Json(true);
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    return Json(false);
                    //return Content(Boolean.FalseString);
                }
            }
        }
        public ActionResult EditDistrict(District ds)
        {
            int id = ds.Id;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var district = rdb.districts.Where(s => s.Id == id).FirstOrDefault();
                    district.District = ds.Name;
                    rdb.Entry(district).State = System.Data.Entity.EntityState.Modified;
                    rdb.SaveChanges();
                    string message = "Successfully Saved!";
                    if (Request.IsAjaxRequest())
                    {
                        return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                    }
                    else
                    {
                        return null;
                    }
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    return Json(false);
                    //return Content(Boolean.FalseString);
                }
            }
        }

        [HttpPost]
        public ActionResult RemoveDistrict(string id)
        {
            int idToCheck = Int32.Parse(id);
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var district = rdb.districts.Where(s => s.Id == idToCheck).FirstOrDefault();
                    rdb.districts.Remove(district);
                    rdb.SaveChanges();
                    return Json(true);
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    return Json(false);
                    //return Content(Boolean.FalseString);
                }
            }
        }

        [HttpPost]
        public ActionResult EditNetwork(Network net)
        {
            int idToCheck = net.Id;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var netOld = rdb.networks.Where(s => s.Id == idToCheck).FirstOrDefault();
                    netOld.Network = net.Name;
                    rdb.Entry(netOld).State = System.Data.Entity.EntityState.Modified;
                    rdb.SaveChanges();
                    string message = "Successfully Saved!";
                    if (Request.IsAjaxRequest())
                    {
                        return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                    }
                    else
                    {
                        return null;
                    }
                    //RedirectToAction("RemoveRestaurant", "Admin");
                    //return View();
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    //RedirectToAction("RemoveRestaurant", "Admin");
                    //return View();
                    return Content(Boolean.FalseString);
                }
            }
        }

        [HttpPost]
        public ActionResult RemoveNetwork(string id)
        {
            int idToCheck = Int32.Parse(id);
            string message;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var network = rdb.networks.Where(s => s.Id == idToCheck).FirstOrDefault();
                    rdb.networks.Remove(network);
                    rdb.SaveChanges();
                    message = "Successfully removed!";
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //return View("AdminMenu");
                    //return RedirectToAction("RemoveRestaurant");
                    //return View();
                    //return Content(Boolean.TrueString);
                }
                catch
                {//TODO: Log error	
                    message = "Error";
                    return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //return View("AdminMenu");
                    //return RedirectToAction("RemoveRestaurant");
                    //return View();
                    //return Content(Boolean.FalseString);
                }
            }
        }

        [Authorize]
        public ActionResult EditComments(int? page)
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var comments = rdb.usercomments.Select(s => new CustomLabelModel { Date = s.DateOfCreation, LabelText = s.Comment, Name = s.UserName, Id = s.Id }).ToList();
                return (View("EditComments", comments.ToPagedList(pageNumber: page ?? 1, pageSize: 1)));
            }
        }

        [Authorize]
        public ActionResult EditComment(string id)
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                int idOfEditedComment = Int32.Parse(id);
                var comment = rdb.usercomments.Where(s => s.Id == idOfEditedComment).Select(x => new CustomLabelModel { Date = x.DateOfCreation, Id = x.Id, LabelText = x.Comment, E_mail = x.Email}).ToList()[0];
                return View(comment);
            }
        }
        public void RemoveComment(string id)
        {
            int idToCheck = Int32.Parse(id);
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var comment = rdb.usercomments.Where(s => s.Id == idToCheck).FirstOrDefault();
                    rdb.usercomments.Remove(comment);
                    rdb.SaveChanges();
                }
                catch
                {//TODO: Log error	
                }
            }
        }
        public void UpdateComment(CustomLabelModel clm)
        {
            int id = clm.Id;
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                try
                {
                    var comment = rdb.usercomments.Where(s => s.Id == id).FirstOrDefault();
                    comment.Comment = clm.LabelText;
                    rdb.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                    rdb.SaveChanges();
                }
                catch
                {//TODO: Log error	
                }
            }
        }

        
    }

}
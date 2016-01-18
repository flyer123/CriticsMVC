using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Test_Google_Api.Models;
using Test_Google_Api.Models.DB;
using Test_Google_Api.Models.AdminModels;
using Test_Google_Api.Models.ViewModel;
using Test_Google_Api.Models.SaveModels;
using PagedList;
using System.Net.Mail;
using System.Net;



namespace Test_Google_Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Index()
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                //выбираем из базы необходимые данные используя модель и передаем лист моделей position в view
                var position = rdb.restaurants.Select(s => new Position
                {
                    Lat = s.Lattitude,
                    Lng = s.Longitude,
                    Name = s.Name,
                    AddressId = s.Id,
                    CustomLabel = s.CustomLabel,
                    Review = s.ReviewText,
                    Kitchen = s.KitchenMark,
                    Service = s.ServiceMark,
                    Interior = s.InteriorMark,
                    Date = s.DateOfCreation
                }).ToList();
                return View(position);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        //вывод рецензии
        public ViewResult Review(string num)
        {
            int addressId = Int32.Parse(num);
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var review = rdb.restaurants.Where(s => s.Id == addressId).Select(n => new ReviewModel
                {
                    Id = addressId,
                    Name = n.Name,
                    KitchenMark = n.KitchenMark,
                    InteriorMark = n.InteriorMark,
                    ServiceMark = n.ServiceMark,
                    Text = n.ReviewText,
                    Label = n.CustomLabel,
                    Address = n.Address,
                    Date = n.DateOfCreation,
                    Lat = n.Lattitude,
                    Lng = n.Longitude,
                    KitchenType = n.KitchenType,
                    WorkHours = n.WorkTime,
                    Amount = n.SumAmount,
                    Children = n.Children,
                    Propositions = n.Propositions,
                    Music = n.Music,
                    Phone = n.Phones
                }).ToList()[0];
                return View(review);
            }
        }

        //вывод страницы рейтинга
        public ViewResult Rating(int? page)
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var networks = rdb.networks.Select(s => new Network {Id = s.Id, Name = s.Network }).ToList();
                var rating = rdb.restaurants.Select(s => new RatingModel
                {
                    Date = s.DateOfCreation,
                    InteriorMark = s.InteriorMark,
                    KitchenMark = s.KitchenMark,
                    ServiceMark = s.ServiceMark,
                    Name = s.Name,
                    RestaurantId = s.Id,
                    DistrictId = s.DistrictId
                }).ToList();
                var districts = rdb.districts.Select(s => new DistrictModel {Id = s.Id, District = s.District }).ToList();
                ViewBag.Networks = networks;
                ViewBag.Districts = districts;
                ViewBag.NetworkName = "";
                return (View("Rating", rating.ToPagedList(pageNumber: page ?? 1, pageSize: 1)));
            }
        }

        //вывод отсортированной страницы рейтинга
        [HttpPost]
        public ViewResult Rating(string district, string data, string criterion, string network, int? page)
        {
            //no network selected
            if (Int32.Parse(network) == 1)
            {
                DateTime dateOFSort;
                bool month = false;
                if (data.Equals("month"))
                    month = true;
                int c = Int32.Parse(criterion);
                using (restaurants_dbEntities rdb = new restaurants_dbEntities())
                {
                    var networks = rdb.networks.Select(s => new Network { Id = s.Id, Name = s.Network }).ToList();
                    ViewBag.Networks = networks;
                    //rating output without limitation by date
                    if (!month)
                    {//if there are limitations by district
                        if (!district.Equals("0"))
                        {
                            int distId = Int32.Parse(district);
                            var rating = rdb.restaurants.Where(x => x.DistrictId == distId).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = distId,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = "";
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                        else
                        {
                            //no district limitation
                            var rating = rdb.restaurants.Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                Sum = s.SumAmount
                            });
                            //sorting by filters
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = "";
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                    }
                    else
                    {//last month
                        dateOFSort = DateTime.Now.AddMonths(-1);
                        //as in previous case but limited by date
                        if (!district.Equals("0"))
                        {
                            int distId = Int32.Parse(district);
                            var rating = rdb.restaurants.Where(x => x.DistrictId == distId).Where(x => x.DateOfCreation >= dateOFSort).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = s.Id,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = "";
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                        else
                        {
                            //any district selected
                            var rating = rdb.restaurants.Where(x => x.DateOfCreation >= dateOFSort).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = s.Id,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = "";
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                    }
                }
            }
            else
            {
                //when network is selected
                int net = Int32.Parse(network);
                DateTime dateOFSort;
                bool month = false;
                if (data.Equals("month"))
                    month = true;
                int c = Int32.Parse(criterion);
                using (restaurants_dbEntities rdb = new restaurants_dbEntities())
                {
                    var networks = rdb.networks.Select(s => new Network { Id = s.Id, Name = s.Network }).ToList();
                    ViewBag.Networks = networks;
                    //get the name of selected network
                    var networkName = rdb.networks.Where(s => s.Id == net).Select(s => s.Network).ToList()[0];
                    //rating output without limitation by date
                    if (!month)
                    {//if there are limitations by district
                        if (!district.Equals("0"))
                        {
                            int distId = Int32.Parse(district);
                            var rating = rdb.restaurants.Where(x => x.DistrictId == distId).Where(s => s.NetworkId == net).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = distId,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = networkName;
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                        else
                        {
                            //no district limitation
                            var rating = rdb.restaurants.Where(s => s.NetworkId == net).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                Sum = s.SumAmount
                            });
                            //sorting by filters
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = networkName;
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                    }
                    else
                    {//last month
                        dateOFSort = DateTime.Now.AddMonths(-1);
                        //as in previous case but limited by date
                        if (!district.Equals("0"))
                        {
                            int distId = Int32.Parse(district);
                            var rating = rdb.restaurants.Where(x => x.DistrictId == distId).Where(s => s.NetworkId == net).Where(x => x.DateOfCreation >= dateOFSort).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = s.Id,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = networkName;
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 1)));
                        }
                        else
                        {
                            //any district selected
                            var rating = rdb.restaurants.Where(x => x.DateOfCreation >= dateOFSort).Where(s => s.NetworkId == net).Select(s => new RatingModel
                            {
                                Date = s.DateOfCreation,
                                InteriorMark = s.InteriorMark,
                                KitchenMark = s.KitchenMark,
                                ServiceMark = s.ServiceMark,
                                Name = s.Name,
                                RestaurantId = s.Id,
                                DistrictId = s.Id,
                                Sum = s.SumAmount
                            });
                            switch (c)
                            {
                                case 1:
                                    rating = rating.OrderBy(s => s.KitchenMark);
                                    break;
                                case 2:
                                    rating = rating.OrderBy(s => s.InteriorMark);
                                    break;
                                case 3:
                                    rating = rating.OrderBy(s => s.ServiceMark);
                                    break;
                                case 4:
                                    rating = rating.OrderBy(s => s.Sum);
                                    break;
                            }
                            var sortedRating = rating.ToList();
                            var districts = rdb.districts.Select(s => new DistrictModel { Id = s.Id, District = s.District }).ToList();
                            ViewBag.Districts = districts;
                            ViewBag.NetworkName = networkName;
                            return (View("Rating", sortedRating.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
                        }
                    }
                }

            }
        }


        public ViewResult Search()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Search(string word, int? page)
        {
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                //search the network
                var tempNetwork = rdb.networks.Where(s => s.Network.ToLower().Contains(word.ToLower())).Select(s => s.Id);
                if (tempNetwork.ToList().Count != 0)
                {
                    int networkId = tempNetwork.ToList()[0];
                    var netSearchResult = rdb.restaurants.Where(s => s.NetworkId == networkId).Select(s => new SearchModel { Id = s.Id, Name = s.Name }).ToList();
                }
                //the search by name
                var rating1 = rdb.restaurants.Where(s => s.Name.Contains(word.Substring(0))).Select(s => new SearchModel { Id = s.Id, Name = s.Name }).ToList();
                //here the search will be by kitchen type, for example "chinese food", and then we do intersection of lists
                var rating2 = rdb.restaurants.Where(s => s.KitchenType.Contains(word.Substring(0))).Select(s => new SearchModel { Id = s.Id, Name = s.Name }).ToList();
                //the search will by by propositions, for example "vegetarian food"
                var rating3 = rdb.restaurants.Where(s => s.Propositions.Contains(word.Substring(0))).Select(s => new SearchModel { Id = s.Id, Name = s.Name }).ToList();
                var rating4 = rdb.restaurants.Where(s => s.ReviewText.Contains(word.Substring(0))).Select(s => new SearchModel { Id = s.Id, Name = s.Name }).ToList();

                var hs12 = new HashSet<SearchModel>(rating1, new SearchModelComparer());
                hs12.UnionWith(rating2);
                var rating12 = hs12.ToList();

                var hs123 = new HashSet<SearchModel>(rating12, new SearchModelComparer());
                hs123.UnionWith(rating3);
                var rating123 = hs123.ToList();

                var hs1234 = new HashSet<SearchModel>(rating123, new SearchModelComparer());
                hs1234.UnionWith(rating4);
                var rating1234 = hs1234.ToList();

                return (View("Search", rating1234.ToPagedList(pageNumber: page ?? 1, pageSize: 10)));
            }
        }

        public ActionResult SaveLabel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveLabel(CustomLabelModel lb)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    using (restaurants_dbEntities rdb = new restaurants_dbEntities())
                    {
                        rdb.usercomments.Add(new usercomments { Comment = lb.LabelText, DateOfCreation = DateTime.Now, RestaurantId = lb.ReviewID, UserName = lb.Name, Email = lb.E_mail});
                        rdb.SaveChanges();
                        message = "Successfully Saved!";
                    }
                }
                catch (Exception ex)
                {
                    message = "Error. Please try egain!";
                }
            }
            else
            {
                message = "Please provide required fields value.";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return View();
            }   
        }

        public PartialViewResult LabelView(int num)
        {
            //creates partial label view
            using (restaurants_dbEntities rdb = new restaurants_dbEntities())
            {
                var labels = rdb.usercomments.Where(s => s.RestaurantId == num).Select(n => new CustomLabelModel { Date = n.DateOfCreation,
                E_mail = n.Email, LabelText = n.Comment, Name = n.UserName}).ToList();
                ViewBag.Users = labels;
                ViewBag.ID = num;
                return PartialView();
            }            
        }
        public ViewResult Contacts()
        {
            ViewBag.Title = "Contacts";
            return View();
        }


  

        [HttpPost]
        public ActionResult SendMessage(MailModel md)
        {
            const string SERVER = "relay-hosting.secureserver.net";
            const string TOEMAIL = "yuryneo2005@gmail.com.com";
            MailAddress from = new MailAddress(md.E_mail);
            MailAddress to = new MailAddress(TOEMAIL);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Web Site Contact Inquiry from " + md.Name;
            message.Body = "Message from: " + md.Name + " at " +
                           md.E_mail + "\n\n" + md.Message;
            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient(SERVER);
            client.Send(message);

            //commented part is for testing purposes
            
            //var fromAddress = new MailAddress("yuryneo2005@gmail.com", md.Name);
            //var toAddress = new MailAddress("yurijform@yahoo.com", "To Name");
            //const string fromPassword = "password";
            //const string subject = "test";
            //string body = md.Message;

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            //    Timeout = 20000
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(message);
            //}
            return null;
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
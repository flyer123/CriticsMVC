using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PagedList;
using Test_Google_Api.Controllers;
using Test_Google_Api.Models.ViewModel;
using Test_Google_Api.Controllers;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //arrange
            //create paged list collection of rating model
            List<RatingModel> listForTest_1 = new List<RatingModel>() { };
            List<RatingModel> listForTest_2 = new List<RatingModel>() { };
            List<RatingModel> listForTest_3 = new List<RatingModel>() { };

            for (int i = 0; i < 100; i++)
                listForTest_1.Add(new RatingModel { });
            for (int i = 0; i < 25; i++)
                listForTest_1.Add(new RatingModel { });
            for (int i = 0; i < 34; i++)
                listForTest_1.Add(new RatingModel { });





            //act
            //call the home controller rating method

            HomeController testController = new Test_Google_Api.Controllers.HomeController();
            testController.Rating(1);

            //assert
            //check the results
        }
    }
}

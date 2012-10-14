using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Public360;
using Public360.Controllers;
using System.Data.Entity;
using Public360.Models;
using System.Web.Script.Serialization;

namespace Public360.Tests.Controllers
{
    [TestClass]
    public class CityControllerTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            // to turn off entity framework auto creating stuff
            Database.SetInitializer<PublicEntities>(null);

            _controller = new CityController();

        }

        [TestMethod]
        public void SingleCityPopJSON()
        {
            JsonResult result = _controller.SingleCityPopJSON(3900142) as JsonResult;

            Assert.IsNotNull(result, "result is null");

            string jsonString = new JavaScriptSerializer().Serialize(result.Data);

            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<Dictionary<string,object>> cityList = ser.Deserialize<List<Dictionary<string, object>>>(jsonString);
     
            Assert.IsNotNull(cityList, "deserialized json is null");

            Assert.AreNotEqual(0, cityList.Count, "city count equal zero");

            Dictionary<string, object> cityItem = (Dictionary<string, object>)cityList[0];

            Assert.IsNotNull(cityItem, "city is null");

            string cityValue = cityItem["value"].ToString();

            Assert.AreEqual("3900142", cityValue);

            string cityText = cityItem["text"].ToString();

            Assert.AreEqual("Aberdeen", cityText.Substring(0,8));

        }

        [TestMethod]
        public void CitiesWithinPercentJSON()
        {
            int pop = 1630;
            double percent = 3;
            int cityID = 3900142;
            int stateID = 39;
            JsonResult result = _controller.CitiesWithinPercentJSON(cityID, stateID, pop, percent) as JsonResult;

            Assert.IsNotNull(result, "result is null");

            string jsonString = new JavaScriptSerializer().Serialize(result.Data);

            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<Dictionary<string,object>> cityList = ser.Deserialize<List<Dictionary<string, object>>>(jsonString);
     
            Assert.IsNotNull(cityList, "deserialized json is null");

            Assert.AreNotEqual(0, cityList.Count, "city count equal zero");

            Dictionary<string, object> cityItem = (Dictionary<string, object>)cityList[0];

            Assert.IsNotNull(cityItem, "city is null");

            string cityValue = cityItem["value"].ToString();

            string cityText = cityItem["text"].ToString();

            int beg = cityText.IndexOf("(pop ");
            int end = cityText.IndexOf(")");
            string test = cityText.Substring(beg + 5, end - beg - 5);
            int popIn = Convert.ToInt32(test);

            Assert.IsTrue((popIn >= pop * (1 - percent / 100) && popIn <= pop * (1 + percent / 100)));

        }

        [TestCleanup()]
        public void Cleanup()
        {
            _controller = null;
        }

        private CityController _controller;

    }
}

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
    public class HomeControllerTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            // to turn off entity framework auto creating stuff
            Database.SetInitializer<PublicEntities>(null);

            _controller = new HomeController();

        }

        [TestMethod]
        public void Index()
        {

            // Act
            ViewResult result = _controller.Index() as ViewResult;

            IEnumerable<Public360.Models.State> x = (IEnumerable<Public360.Models.State>)result.Model;

            int stateCount = x.Count();

            Assert.AreNotEqual(0, stateCount, "state count equals zero");

        }

        [TestMethod]
        public void CitiesJSON()
        {
            JsonResult result = _controller.CitiesJSON(39) as JsonResult;

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

            Assert.AreEqual("Aberdeen", cityText);

        }

        [TestCleanup()]
        public void Cleanup()
        {
            _controller = null;
        }

        private HomeController _controller;

    }
}

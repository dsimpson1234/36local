using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Public360.Models;
using System.Data.Entity;

namespace Public360.Controllers
{
    public class HomeController : Controller
    {
        PublicEntities publicDB = new PublicEntities();

        //
        // get: /Home/
        // returns home view passing states
        public ActionResult Index()
        {
            var states = publicDB.States.Where(x => x.ActiveCities == true).ToList();
            return View(states);
        }

        // .
        // get
        // AJAX: /City/CitiesJSON?state=39
        public ActionResult CitiesJSON(byte stateID)
        {
            var cities = publicDB.Cities.Where(c => c.StateID == stateID && c.Active == true).ToList();
            return Json(cities.Select(x => new { value = x.CityID, text = x.CityUniqueName }), JsonRequestBehavior.AllowGet);
        } 
    }
}

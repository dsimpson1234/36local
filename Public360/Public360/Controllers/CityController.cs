using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Public360.Models;
using System.Data.Entity;
using Public360.ViewModels;

namespace Public360.Controllers
{
    public class CityController : Controller
    {
        PublicEntities publicDB = new PublicEntities();

        // get: /city/Details?id=5
        public ActionResult Details(int id)
        {
            CityDetail cityDetail = new CityDetail();
            cityDetail.City = publicDB.Cities.Where(c => c.CityID == id).Single();
            cityDetail.UICityDataTypes = publicDB.UICityDataTypes.Where(cdt => cdt.CityDataTypeID > 0 && cdt.DataLevel == 1 && cdt.Active == true).OrderBy(cdt => cdt.SortOrder).ToList();
            cityDetail.UICityDataTypeSize = cityDetail.UICityDataTypes.Count() + 2;
            cityDetail.ComparisonCities = publicDB.Cities.Where(c => c.CityID != id && c.StateID == cityDetail.City.StateID).ToList();
            cityDetail.CityPopulation = publicDB.CensusCityPops.Where(p => p.CityID == id && p.Year == 10).Select(p => p.Population).Single(); 
            return View(cityDetail);
        }

        // 
        // get
        // AJAX: /City/SingleCityPopJSON?city=3900142
        public ActionResult SingleCityPopJSON(int cityID)
        {
            City singleCity = publicDB.Cities.Where(c => c.CityID == cityID).Single();

            int cityValue = singleCity.CityID;

            int? cityPop = publicDB.CensusCityPops.Where(p => p.CityID == cityID && p.Year == 10).Select(p => p.Population).Single();

            string showText = singleCity.CityUniqueName + " (pop " + cityPop + ")";

            CityNameList cityItem = new CityNameList();
            cityItem.CityID = cityValue;
            cityItem.CityText = showText;
            List<CityNameList> cities = new List<CityNameList>();
            cities.Add(cityItem);

            return Json(cities.Select(x => new { value = x.CityID, text = x.CityText }), JsonRequestBehavior.AllowGet);
        }

        // 
        // get
        // AJAX: /City/CitiesWithinPercentJSON?percent=3
        public ActionResult CitiesWithinPercentJSON(int excludeCityID, int stateID, int homePop, double percent)
        {

            var cities1 =  from c in publicDB.Cities join p in publicDB.CensusCityPops on c.CityID equals p.CityID into cp
                           from p in cp
                           where p.Year == 10 &&
                           p.Population >= homePop * (1 - percent/100) &&
                           p.Population <= homePop * (1 + percent/100) &&
                           c.StateID == stateID &&
                           c.CityID != excludeCityID
                           select new {p.CityID, c.CityUniqueName, p.Population};
                           
            List<CityNameList> cities = new List<CityNameList>();
            foreach (var x in cities1)
            {
                CityNameList cityItem = new CityNameList();
                cityItem.CityID = x.CityID;
                string showText = x.CityUniqueName + " (pop " + x.Population + ")";
                cityItem.CityText = showText;
                cities.Add(cityItem);
            }

            return Json(cities.Select(x => new { value = x.CityID, text = x.CityText }), JsonRequestBehavior.AllowGet);
        }

        // 
        // get
        // AJAX: /City/UICityDataTypeJSON?uiCityDataTypeID=1
        public ActionResult UICityDataTypeJSON(int uiCityDataTypeID)
        {
            List<UICityDataType> uiCityDataTypes = publicDB.UICityDataTypes.Where(u => u.ParentCityDataTypeID == uiCityDataTypeID && u.Active == true).OrderBy(u => u.SortOrder).ToList();

            return Json(uiCityDataTypes.Select(x => new { value = x.CityDataTypeID, text = x.DisplayText }), JsonRequestBehavior.AllowGet);

        }

        // 
        // get
        // AJAX: /City/UICityDataTypeFullJSON?uiCityDataTypeID=1
        public ActionResult UICityDataTypeFullJSON(int uiCityDataTypeID)
        {
            List<UICityDataType> uiCityDataTypes = publicDB.UICityDataTypes.Where(u => u.ParentCityDataTypeID == uiCityDataTypeID && u.Active == true).OrderBy(u => u.SortOrder).ToList();

            return Json(uiCityDataTypes.Select(x => new { uniqueID = x.CityDataTypeID, sortOrder = x.SortOrder, active = x.Active, displayText = x.DisplayText, dataLevel = x.DataLevel, parentUniqueID = x.ParentCityDataTypeID, isLeaf = x.IsLeaf, source = x.Source }), JsonRequestBehavior.AllowGet);
        
        }
        
        // 
        // get
        // AJAX: /City/UICityDataJSON?uiCityDataTypeID=1
        public ActionResult UICityDataJSON(int uiCityDataTypeID, int cityID)
        {
            switch (uiCityDataTypeID)
            {
                case 8: //population

                    CensusCityPop cityPop = publicDB.CensusCityPops.Where(p => p.CityID == cityID && p.Year == 10).Single();

                    CityData cityData = new CityData();
                    cityData.Text = "Total Population";
                    if (cityPop.Population != null)
                    {
                        cityData.Value = cityPop.Population.ToString();
                    }
                    else
                    {
                        cityData.Value = "n/a";
                    }
                    List<CityData> cityDatas = new List<CityData>();
                    cityDatas.Add(cityData);
                    return Json(cityDatas.Select(x => new { value = x.Value, text = x.Text }), JsonRequestBehavior.AllowGet);
                    break;

                case 9: // ages (male)
                    List<CensusCityAge> cityAges = publicDB.CityAges.Where(p => p.CityID == cityID && p.Gender == false).ToList();

                    List<CityData> cityDatas2 = AgeByGender(cityAges);
                    return Json(cityDatas2.Select(x => new { value = x.Value, text = x.Text }), JsonRequestBehavior.AllowGet);
                    break;

                case 10: // ages (female)
                    List<CensusCityAge> cityAges2 = publicDB.CityAges.Where(p => p.CityID == cityID && p.Gender == true).ToList();

                    List<CityData> cityDatas3 = AgeByGender(cityAges2);
                    return Json(cityDatas3.Select(x => new { value = x.Value, text = x.Text }), JsonRequestBehavior.AllowGet);
                    break;

                case 16: // households by type
                    break;

                case 17: // relationships
                    break;

                default:
                    break;

            }

            List<UICityDataType> uiCityDataTypes = publicDB.UICityDataTypes.Where(u => u.ParentCityDataTypeID == uiCityDataTypeID && u.Active == true).OrderBy(u => u.SortOrder).ToList();

            return Json(uiCityDataTypes.Select(x => new { value = x.CityDataTypeID, text = x.DisplayText }), JsonRequestBehavior.AllowGet);
        }

        private List<CityData> AgeByGender(List<CensusCityAge> cityAges)
        {
            List<CityData> cityDatas = new List<CityData>();
            foreach (CensusCityAge cityAge in cityAges)
            {
                CityData cityData = new CityData();
                var label = "";
                bool haveMin = false;
                if (cityAge.MinAge == 0)
                {
                    label = "Under ";
                }
                else
                {
                    label = cityAge.MinAge.ToString() + " ";
                    haveMin = true;
                }
                if (cityAge.MaxAge != null)
                {
                    if (haveMin)
                    {
                        label = label + "- " + cityAge.MaxAge.ToString() + " Years";
                    }
                    else
                    {
                        label = label + cityAge.MaxAge.ToString() + " Years";
                    }
                }
                else
                {
                    label = label + "and Older";
                }
                cityData.Text = label;
                if (cityAge.Population != null)
                {
                    cityData.Value = cityAge.Population.ToString();
                }
                else
                {
                    cityData.Value = "n/a";
                }
                cityDatas.Add(cityData);
            }
            return cityDatas;

        }

    }
}


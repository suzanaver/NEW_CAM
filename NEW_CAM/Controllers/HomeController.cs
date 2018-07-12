using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NEW_CAM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public void SaveEntrance(List<string> things)
        {
            Dal.writeToLog("userPhone=" + things[0]);
            Dal.writeToLog("nameG ="+KlaliController.nameG);
            string phone = Dal.getPhoneNum(KlaliController.nameG);
            phone = phone.Replace("-", "");
            Dal.writeToLog("phone =" + phone);
            try
            {
                Dal.sendSms("0"+things[0], "gama 066 " + phone + " gama");
            }
            catch (Exception ex) {
                Dal.writeToLog("error:"+ex.InnerException+"."+ ex.Message);
            }

        }
    }
}

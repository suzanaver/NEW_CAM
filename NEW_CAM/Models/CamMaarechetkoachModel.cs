using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NEW_CAM.Models
{
    public class CamMaarechetkoachModel
    {
        // values for dinamic dropdawnlist in HTML - db table - new_cam_dropdownlist
        //מערכת כח באתר 1
        public List<SelectListItem> Yaud { get; set; }
        public List<SelectListItem> sug { get; set; }
        public List<SelectListItem> godelmafsek { get; set; }
        public List<SelectListItem> kamutmeyashrim { get; set; }
        public List<SelectListItem> SugMzber { get; set; }
        public List<SelectListItem> kayamLVD { get; set; }
        public List<SelectListItem> GodelMafsek { get; set; }
        public List<SelectListItem> KamutMazberim { get; set; }
        public List<SelectListItem> Year { get; set; }
        public List<SelectListItem> Month { get; set; }

        //חיבור צרכנים למערכת
        public List<SelectListItem> HiburLeMaarecet { get; set; }


        //גודל מפסק DC
        public List<SelectListItem> GodelMafsekt { get; set; }



        // values for field from database  - db table GIS_CAMMaarechetkoachL
        //מערכת כח באתר 1
        public string str_Yaud_First { get; set; }
        public string str_sug_First { get; set; }
        public string str_godelmafsek_First { get; set; }
        public string str_kamutmeyashrim_First { get; set; }
        public string str_SugMzber_First { get; set; }
        public string str_kayamLVD_First { get; set; }
        public string str_ztichatzerem_First { get; set; }
        public string str_Year_First { get; set; }
        public string str_Month_First { get; set; }
        //חיבור צרכנים למערכת
        public string str_HiburLeMaarecetOne_First { get; set; }
        public string str_HiburLeMaarecetTwo_First { get; set; }
        public string str_HiburLeMaarecetThree_First { get; set; }
        public string str_HiburLeMaarecetFoor_First { get; set; }
        public string str_HiburLeMaarecetFive_First { get; set; }
        public string str_HiburLeMaarecetSix_First { get; set; }
        public string str_HiburLeMaarecetSeven_First { get; set; }
        public string str_HiburLeMaarecetEight_First { get; set; }
     
        //גודל מפסק DC
        public string str_GodelMafsekOne_First { get; set; }
        public string str_GodelMafsektwo_First { get; set; }
        public string str_GodelMafsekthree_First { get; set; }
        public string str_GodelMafsekFoor_First { get; set; }
        public string str_GodelMafsekFive_First { get; set; }
        public string str_GodelMafsekSix_First { get; set; }
        public string str_GodelMafsekSeven_First { get; set; }
        public string str_GodelMafsekEight_First { get; set; }

        //מערכת כח באתר 2
        public string str_Yaud_Second { get; set; }
        public string str_sug_Second { get; set; }
        public string str_godelmafsek_Second { get; set; }
        public string str_kamutmeyashrim_Second { get; set; }
        public string str_SugMazber_Second { get; set; }
        public string str_kayamLVD_Second { get; set; }
        public string str_ztichatzerem_Second { get; set; }
        public string str_Year_Second { get; set; }
        public string str_Month_Second { get; set; }

        //חיבור צרכנים למערכת
        public string str_HiburLeMaarecetOne_Second { get; set; }
        public string str_HiburLeMaarecetTwo_Second { get; set; }
        public string str_HiburLeMaarecetThree_Second { get; set; }
        public string str_HiburLeMaarecetFoor_Second { get; set; }
        public string str_HiburLeMaarecetFive_Second { get; set; }
        public string str_HiburLeMaarecetSix_Second { get; set; }
        public string str_HiburLeMaarecetSeven_Second { get; set; }
        public string str_HiburLeMaarecetEight_Second { get; set; }

        //גודל מפסק DC
        public string str_GodelMafsekOne_Second { get; set; }
        public string str_GodelMafsektwo_Second { get; set; }
        public string str_GodelMafsekthree_Second { get; set; }
        public string str_GodelMafsekFoor_Second { get; set; }
        public string str_GodelMafsekFive_Second { get; set; }
        public string str_GodelMafsekSix_Second { get; set; }
        public string str_GodelMafsekSeven_Second { get; set; }
        public string str_GodelMafsekEight_Second { get; set; }

        //מערכת כח באתר 3
        public string str_Yaud_Third { get; set; }
        public string str_sug_Third { get; set; }
        public string str_godelmafsek_Third { get; set; }
        public string str_kamutmeyashrim_Third { get; set; }
        public string str_SugMazber_Third { get; set; }
        public string str_kayamLVD_Third { get; set; }
        public string str_ztichatzerem_Third { get; set; }
        public string str_Year_Third { get; set; }
        public string str_Month_Third { get; set; }

        //חיבור צרכנים למערכת
        public string str_HiburLeMaarecetOne_Third { get; set; }
        public string str_HiburLeMaarecetTwo_Third { get; set; }
        public string str_HiburLeMaarecetThree_Third { get; set; }
        public string str_HiburLeMaarecetFoor_Third { get; set; }
        public string str_HiburLeMaarecetFive_Third { get; set; }
        public string str_HiburLeMaarecetSix_Third { get; set; }
        public string str_HiburLeMaarecetSeven_Third { get; set; }
        public string str_HiburLeMaarecetEight_Third { get; set; }

        //גודל מפסק DC
        public string str_GodelMafsekOne_Third { get; set; }
        public string str_GodelMafsektwo_Third { get; set; }
        public string str_GodelMafsekthree_Third { get; set; }
        public string str_GodelMafsekFoor_Third { get; set; }
        public string str_GodelMafsekFive_Third { get; set; }
        public string str_GodelMafsekSix_Third { get; set; }
        public string str_GodelMafsekSeven_Third { get; set; }
        public string str_GodelMafsekEight_Third { get; set; }

        //הערות
        public string str_Notes_maarechet { get; set; }
        //תאריך ביקורת 
        public string str_TaarihBikoret_AC { get; set; }
       
        ////כמות מערכות כח באתר
        //public string str_kamutmarehetcoahbeatar { get; set; }
        //public List<SelectListItem> kamutmarehetcoahbeatar { get; set; }
        // משספר אתר
        public string SITE_NAME_Maarechet { get; set; }

    }
}
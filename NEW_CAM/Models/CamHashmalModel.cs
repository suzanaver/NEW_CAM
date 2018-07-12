using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NEW_CAM.Models
{
    public class CamHashmalModel
    {
        // values for dinamic dropdawnlist in HTML - db table - new_cam_dropdownlist
        //AC הזנה חיצונית
        public List<SelectListItem> MakorHazanatHashmal { get; set; }
        public List<SelectListItem> GodelMafsek { get; set; }
        public List<SelectListItem> KriatMoneMerahok { get; set; }
        public List<SelectListItem> SugMone { get; set; }
        public List<SelectListItem> kaiamshataphashmal { get; set; }

        //AC הזנה בחדר
        public List<SelectListItem> SugLyahHashmalBeAtar { get; set; }
        public List<SelectListItem> GodelMafsekRashi { get; set; }
        public List<SelectListItem> YesNo { get; set; }
        public List<SelectListItem> SugShekaKayam { get; set; }
        public List<SelectListItem> MehubarLekriaMerahok { get; set; }

        //AC הספקה לגורם נוסף
     
        public List<SelectListItem> KriatMoneMeRahok_GoremNosaf { get; set; }
        
        // company manu for gorem nosaf
        public List<SelectListItem> CamHasmal_CompanyName { get; set; }

        // values for field from database  - db table GIS_CAMDATA_HASHMAL
        public string SITE_NAME_ID { get; set; }
        //AC הזנה חיצונית
        public string str_MakorHazanatHashmal { get; set; }
        public string str_GodelMafsek { get; set; }
        public string str_MikumMaazHizoniRashi { get; set; }
        public string str_KriatMoneMerahok { get; set; }
        public string str_SugMone_HazanaHizonit { get; set; }
        public string str_kaiamshataphashmal { get; set; }
        public string str_ShemHevra_HazanaHizonit { get; set; }
        public string str_MisparMone_HazanaHizonit { get; set; }
        public string str_MisparHoze_HazanaHizonit { get; set; }

        //AC הזנה בחדר
        public string str_SugLyahHashmalBeAtar { get; set; }
        public string str_GodelMafsekRashi { get; set; }
        public string str_SugMone_HazanaBeHeder { get; set; }
        public string str_MisparMone_HazanaBeHeder { get; set; }
        public string str_NitalLehaberGenerator { get; set; }
        public string str_SugShekaKayam { get; set; }
        public string str_MehubarLekriaMerahok { get; set; }

        ////values of check box of  HaspakatHashmalLeGoremNosaf
        //public string cbk_hot { get; set; }
        //public string cbk_cellcom { get; set; }
        //public string cbk_police { get; set; }
        //public string cbk_pelephone { get; set; }

        //AC הספקה לגורם נוסף
        // GOREMNOSAF_COMPANY1
        public string str_GOREMNOSAF_COMPANY_first { get; set; }
        public string str_GodelMafsek_first { get; set; }
        public string str_SugMone_first { get; set; }
        public string str_KriatMoneMeRahok_first { get; set; }
        public string str_KriatMoneAhrona_first { get; set; }
        public string str_MisparMone_first { get; set; }
        public string str_DATEKRIATMONEAHRONA_first { get; set; }

        // GOREMNOSAF_COMPANY 2
        public string str_GOREMNOSAF_COMPANY_second { get; set; }
        public string str_GodelMafsek_second { get; set; }
        public string str_SugMone_second { get; set; }
        public string str_KriatMoneMeRahok_second { get; set; }
        public string str_KriatMoneAhrona_second { get; set; }
        public string str_MisparMone_second { get; set; }
        public string str_DATEKRIATMONEAHRONA_second { get; set; }

        // GOREMNOSAF_COMPANY 3
        public string str_GOREMNOSAF_COMPANY_third { get; set; }
        public string str_GodelMafsek_third { get; set; }
        public string str_SugMone_third { get; set; }
        public string str_KriatMoneMeRahok_third { get; set; }
        public string str_KriatMoneAhrona_third { get; set; }
        public string str_MisparMone_third { get; set; }
        public string str_DATEKRIATMONEAHRONA_third { get; set; }
        // GOREMNOSAF_COMPANY 4
        public string str_GOREMNOSAF_COMPANY_quater { get; set; }
        public string str_GodelMafsek_quater { get; set; }
        public string str_SugMone_quater { get; set; }
        public string str_KriatMoneMeRahok_quater { get; set; }
        public string str_KriatMoneAhrona_quater { get; set; }
        public string str_MisparMone_quater { get; set; }
        public string str_DATEKRIATMONEAHRONA_quater { get; set; }

        //הערות
        public string str_Notes { get; set; }

        //תאריך ביקורת AC
        public string str_TaarihBikoretAC { get; set; }
    }
}
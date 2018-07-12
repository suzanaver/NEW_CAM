using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NEW_CAM.Models
{
    public class CamKlaliModel
    {
        public string USERNAME { get; set; }

        public string PERMISSION { get; set; }
        public string SITE_NAME_ID { get; set; }
        public string TXTADDRESS { get; set; }
        public string TXTCITY { get; set; }
        public string pramisSiteName { get; set; }

        public string cbk_HOT_CBK { get; set; }
        public string cbk_PARTNER { get; set; }
        public string cbk_CELLCOM { get; set; }
        public string cbk_PELEPHONE { get; set; }
        public string cbk_BEZEQ { get; set; }
        //public string CBK { get; set; }

        public string A_24_HOURS { get; set; }
        public string TIMEGENFROM { get; set; }
        public string TIMEGENTO { get; set; }
        public string CKBTECH24 { get; set; }
        public string TIME_REMARKS { get; set; }
        public string TXTPOLEOWNERSHIP1 { get; set; }
        public string MNUSITETYPE { get; set; }
        public string roomResponsibity { get; set; }        
        public string MNUSTRUCTURETYPE { get; set; }
        public string CHRSITENEEDCOORD { get; set; }
        public string ACCESS_INSTRUCTIONS { get; set; }
        public string CHRSITESENSITIVE { get; set; }
        public string CHRSITEENTRCODE { get; set; }
        public string CHRSITECARRESTR { get; set; }
        public string chrSiteNeedKey { get; set; }
        public string CHRSITESECURITY { get; set; }
        public string ELECTRICITY_SOURCE { get; set; }
        public string TXTCURRENTRF { get; set; }
        public string TXTCURRENTRFREPORT { get; set; }
        public string txtswitchsecundary { get; set; }
        public string txtswitch { get; set; }
        public string modifiedday { get; set; }

        public string MMARECHET_KOAH_NUM { get; set; }
        [RegularExpression("[^0-9]", ErrorMessage = "SIM_NUM must be numeric")]
        public string SIM_NUM { get; set; }
        public string JABER_LINK { get; set; }
        [RegularExpression("[^0-9]", ErrorMessage = "MONE_NUM must be numeric")]
        public string MONE_NUM { get; set; }
        public string CONTRUCT_NUM { get; set; }
        public string NEEDS_GENERATOR { get; set; }

        public string mnuPoleType { get; set; }

        public string MME { get; set; }
        public List<CamContactList> CamContactList { get; set; }
        public string RNC_ID_ { get; set; }
        public string HDBSC { get; set; }
        public string openIncidents { get; set; }
        public string lastTechAtSite { get; set; }
        public string GSM_1800 { get; set; }
        public string GSM_900 { get; set; }
        public string UMTS_900 { get; set; }
        public string UMTS_2100 { get; set; }
        public string LTE_1800 { get; set; }
        public string LTE_900 { get; set; }
        public string LTE_2100 { get; set; }
        public string IDEN { get; set; }

        public string EZOR_HALUKA { get; set; }
        public string RATZ_EZOR { get; set; }
        public string TECH_POLY { get; set; }
        public string HAS_TRANSMITION_BKP { get; set; }

        public bool trans_bk { get; set; }

        public string HOSTED { get; set; }
        public string SUPPLY_SIZE { get; set; }
        public string IsInit { get; set; }


        public List<SelectListItem> SelectionPolyTech { get; set; }


    


    }
}
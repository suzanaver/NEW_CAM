using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Web;
using System.Web.Mvc;
using NEW_CAM.Models;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace NEW_CAM.Controllers
{
   
    public class KlaliController : Controller
    {
        public static string nameG = "";

 

        public ActionResult ShowCam()
        {

            //if (Roles.IsUserInRole(User.Identity.Name, "PHI-NETWORKS\\Wifi_Corp"))
            //{

            //}

            string name = System.Web.HttpContext.Current.User.Identity.Name;
            if (name.Contains('\\')) { name = name.Split('\\')[1]; }
            nameG = name;
            string SITE_ID_param = "";
            var vm = new ContactListViewModel();
            if (!Request.QueryString.ToString().Contains("SITE"))
            {
                SITE_ID_param = Globals.site_id;
            }
            else { SITE_ID_param = Request.QueryString["SITE"]; }
            string SITE_ID = Regex.Match(SITE_ID_param, @"\d+").Value;
            int siteInt = int.Parse(SITE_ID);
            Dal dal = new Dal();
            string fiveDigits = dal.checkExistance(SITE_ID);
            //string siteNameAttoll = dal.getSiteName(SITE_ID);
            if (fiveDigits==string.Empty)
            {
                dal.insertRowCamPramisData(SITE_ID);
                dal.insertRowCamSiteDetails(SITE_ID);
                dal.insertRowCamDataMenual(SITE_ID);
            }
            //if (siteNameAttoll == String.Empty){
            //    return  Content("אתר לא קיים במערכת. אנא פנה למנהל המערכת");
            //}

            OracleCommand oCommand;
            OracleDataAdapter oDataAdapter;
            OracleConnection oConn = dal.Connect("gisOraConn");
            oConn.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "select * from GIS_CAM_CONTACTS_MENUAL_v2 where rownum<4 AND site_id = '" + SITE_ID + "'";
            oDataAdapter = new OracleDataAdapter(oCommand);
            var reader2 = oCommand.ExecuteReader();
            int i = 0;
            List<CamContactList> ccl = new List<CamContactList>();
            for (int j = 0; j < 3; j++)
            {
               if (!reader2.Read()) {
                    dal.insert3Rows(SITE_ID);
                }               
            }
            oConn.Close();
            oConn.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "select * from GIS_CAM_CONTACTS_MENUAL_v2 where rownum<4 AND site_id = '" + SITE_ID + "' order by contact1";
            oDataAdapter = new OracleDataAdapter(oCommand);            
            var reader5 = oCommand.ExecuteReader();
            while (reader5.Read())
            {
                CamContactList ccaa = new CamContactList();
                ccaa.contact1 = reader5["contact1"].ToString();
                ccaa.txtphonecellular = reader5["txtphonecellular"].ToString();
                ccaa.txtphonecellular2 = reader5["txtphonecellular2"].ToString();
                ccl.Add(ccaa);
                ccl[0].remarks += reader5["remarks"].ToString();
            }
            oConn.Close();
            string concatTechnologies = "";
            string RNC="", HDBSC = "";
            oConn = dal.Connect("gisOraConn");
            oConn.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "select RNC, HDBSC from V_GIS_RNC_HDBSC where NAME like '%" + SITE_ID + "%'";
            oDataAdapter = new OracleDataAdapter(oCommand);
            var reader4 = oCommand.ExecuteReader();
            while (reader4.Read())
            {
                RNC = reader4["RNC"].ToString();
                HDBSC = reader4["HDBSC"].ToString();
            }
            oConn.Close();

            CamKlaliModel camKlali = new CamKlaliModel();

            camKlali.PERMISSION = "HGHRRGH";

            OracleConnection oConn2 = dal.Connect("gisOraConn");
            oConn2.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn2;
            oCommand.CommandText = "select * from v_gis_cam_general_V2 where site_name_id like '%" + SITE_ID + "%'";
            oDataAdapter = new OracleDataAdapter(oCommand);
            var reader = oCommand.ExecuteReader();

            if (!reader.Read()) {
                return Content("בעיה בהצגת הנתונים. אנא פנה למנהל המערכת");
            }
            else
            {
                camKlali.pramisSiteName = reader["pramisSiteName"].ToString();
                camKlali.SITE_NAME_ID = reader["site_name_id"].ToString();
                camKlali.TXTADDRESS = reader["TXTADDRESS"].ToString();
                camKlali.txtswitch = reader["txtswitch"].ToString();
                camKlali.txtswitchsecundary = reader["txtswitchsecundary"].ToString();
                camKlali.TXTCITY = reader["TXTCITY"].ToString();
                camKlali.modifiedday = reader["MODIFIEDDAY"].ToString();
                camKlali.MME = reader["MME"].ToString();
                camKlali.MONE_NUM = reader["MONE_NUMBER"].ToString();
                camKlali.CONTRUCT_NUM = reader["CONTRUCT_NUM"].ToString();
                camKlali.mnuPoleType = reader["mnuPoleType"].ToString();
                camKlali.SIM_NUM = reader["SIM_NUMBER"].ToString();
                camKlali.JABER_LINK = "gama 066 ";
                camKlali.MMARECHET_KOAH_NUM = reader["MMARECHET_KOAH_NUM"].ToString();
                camKlali.cbk_PELEPHONE = reader["CBK"].ToString().Contains("Pelephone") ? "Pelephone" : "";
                camKlali.cbk_CELLCOM = reader["CBK"].ToString().Contains("Cellcom") ? "Cellcom" : "";
                camKlali.cbk_HOT_CBK = reader["CBK"].ToString().Contains("Mirs") ? "Mirs" : "";
                camKlali.cbk_BEZEQ = reader["CBK"].ToString().Contains("Bezeq") ? "Bezeq" : "";
                camKlali.cbk_PARTNER = reader["CBK"].ToString().Contains("Partner") ? "Partner" : "";
                camKlali.A_24_HOURS = reader["A_24_HOURS"].ToString();
                camKlali.NEEDS_GENERATOR = reader["NEEDS_GENERATOR"].ToString();
                camKlali.roomResponsibity = reader["roomResponsibity"].ToString();
                camKlali.TIMEGENFROM = reader["TIMEGENFROM"].ToString();
                camKlali.TIMEGENTO = reader["TIMEGENTO"].ToString();
                camKlali.TIME_REMARKS = reader["TIME_REMARKS"].ToString();
                camKlali.TXTPOLEOWNERSHIP1 = reader["TXTPOLEOWNERSHIP1"].ToString();
                camKlali.MNUSITETYPE = reader["MNUSITETYPE"].ToString();
                camKlali.MNUSTRUCTURETYPE = reader["MNUSTRUCTURETYPE"].ToString();
                camKlali.CHRSITENEEDCOORD = reader["CHRSITENEEDCOORD"].ToString();
                camKlali.ACCESS_INSTRUCTIONS = reader["ACCESS_INSTRUCTIONS"].ToString();
                camKlali.CHRSITESENSITIVE = reader["CHRSITESENSITIVE"].ToString();
                camKlali.CHRSITEENTRCODE = reader["CHRSITEENTRCODE"].ToString();
                camKlali.CHRSITECARRESTR = reader["CHRSITECARRESTR"].ToString();
                camKlali.CHRSITESECURITY = reader["CHRSITESECURITY"].ToString();
                camKlali.ELECTRICITY_SOURCE = reader["ELECTRICITY_SOURCE"].ToString();
                camKlali.TXTCURRENTRF = reader["TXTCURRENTRF"].ToString();
                camKlali.TXTCURRENTRFREPORT = reader["TXTCURRENTRF"].ToString().Contains("CR") ?"http://10.8.117.15/SiteReports/home/DisplayChaneRequestReports?crVersion="+ reader["TXTCURRENTRF"].ToString() :
                    "http://10.8.117.27/SiteReports/home/DisplayRfReport?RfVersion=" + reader["TXTCURRENTRF"].ToString();
                camKlali.CamContactList = ccl;

                camKlali.RNC_ID_ = RNC;
                camKlali.HDBSC = HDBSC;
                camKlali.openIncidents = reader["openIncidents"].ToString();
                camKlali.lastTechAtSite = reader["lastTechAtSite"].ToString();
                camKlali.HOSTED = reader["HOSTED"].ToString();
                camKlali.EZOR_HALUKA = reader["EZOR_HALUKA"].ToString();
                camKlali.RATZ_EZOR = reader["RATZ_EZOR"].ToString();

                camKlali.TECH_POLY = reader["TECH_POLY"].ToString();


                // טכנאי פולי MENU
                    DataSet ds_poly_tech = new DataSet();
                    DataTable dt_poly_tech = new DataTable();
                    oCommand = new OracleCommand();
                    oCommand.Connection = oConn2;
                    oCommand.CommandText = "select  distinct TECH_POLY from gis_camdata_menual";
                    oDataAdapter = new OracleDataAdapter(oCommand);
                    var poly_tech = oCommand.ExecuteReader();
                    oDataAdapter.Fill(ds_poly_tech, "poly_tech");
                    dt_poly_tech = ds_poly_tech.Tables["poly_tech"];

                    List<string> PoltTechlst = (from x in dt_poly_tech.AsEnumerable()
                                                select x.Field<string>("TECH_POLY")).ToList();

                    camKlali.SelectionPolyTech = PoltTechlst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();



                string TRANSMITTION_BK = reader["HAS_TRANSMITION_BKP"].ToString();
                if (TRANSMITTION_BK != null && TRANSMITTION_BK != " " && TRANSMITTION_BK != "")
                    camKlali.HAS_TRANSMITION_BKP = "כן";


                //Get Brodcast Method from pramis
                DataSet ds_mnuBrodcastMethod = new DataSet();
                DataTable dt_mnuBrodcastMethod = new DataTable();
                string sitename = "";
                sitename = camKlali.SITE_NAME_ID;
                string tec = "";
                oCommand = new OracleCommand();
                OracleConnection oConn7 = dal.Connect("aradmin");
                oConn7.Open();
                oCommand.Connection = oConn7;
                oCommand.CommandText = "select  mnuBrodcastMethod from ORGsysBrodcastMethod where  Status=5 AND mnuHistory=1 AND txtSiteNumber like '%" + sitename + "%'";
                oDataAdapter = new OracleDataAdapter(oCommand);
                oDataAdapter.Fill(ds_mnuBrodcastMethod, "mnuBrodcastMethod");
                dt_mnuBrodcastMethod = ds_mnuBrodcastMethod.Tables["mnuBrodcastMethod"];
                oConn7.Close();
                
                for (int b = 0; b < dt_mnuBrodcastMethod.Rows.Count; b++)
                {
                    tec += "***";
                    tec += dt_mnuBrodcastMethod.Rows[b]["MNUBRODCASTMETHOD"].ToString();
                    tec += "; "; 
                }
                camKlali.GSM_1800 = tec.Contains("***1800;") ? "כן" : "לא";
                camKlali.GSM_900 = tec.Contains("***900;") ? "כן" : "לא";
                camKlali.LTE_1800 = tec.Contains("***LTE1800;") ? "כן" : "לא";
                camKlali.LTE_2100 = tec.Contains("***LTE2100;") ? "כן" : "לא";
                camKlali.LTE_900 = tec.Contains("***LTE900;") ? "כן" : "לא";
                camKlali.IDEN = tec.Contains("***iDEN;") ? "כן" : "לא";
                camKlali.UMTS_2100 = tec.Contains("***UMTS;") ? "כן" : "לא";
                camKlali.UMTS_900 = tec.Contains("***UMTS_900;") ? "כן" : "לא";

             //Get Transmission from pramis
             oConn7.Open();
             oCommand = new OracleCommand();
             oCommand.Connection = oConn7;
             oCommand.CommandText = "select txtSwitch, txtSwitchSecundary from ORGsysTransmission where lstHistory = 1  and lstCRType = 1 and chrSiteNumber like '%" + sitename + "%'";
             oDataAdapter = new OracleDataAdapter(oCommand);            
             var Transmissionreader= oCommand.ExecuteReader();
             while (Transmissionreader.Read())
               {
                camKlali.txtswitch = Transmissionreader["TXTSWITCH"].ToString();
                camKlali.txtswitchsecundary= Transmissionreader["TXTSWITCHSECUNDARY"].ToString();
               }
                 oConn7.Close();


            //Get last tech
            DataSet ds_tech = new DataSet();
            DataTable dt_tech = new DataTable();
            OracleConnection oConntech = dal.Connect("gisOraConn");
            oConntech.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConntech;
            oCommand.CommandText = "select  First_name, last_name from ivr_technicians_ where mobile_phone = (select  tech_name from v_users_history_ivr_ WHERE  time = (select max(time)from v_users_history_ivr_ WHERE SITE_NAME like '%" + SITE_ID + "%'))";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_tech, "ds_tech");
            dt_tech = ds_tech.Tables["ds_tech"];

            if (dt_tech.Rows.Count == 1)
                camKlali.lastTechAtSite = dt_tech.Rows[0]["FIRST_NAME"].ToString() + " " + dt_tech.Rows[0]["LAST_NAME"].ToString();

            oConntech.Close();
         
            //Get Initial Site from pramis
            DataSet ds_Initial = new DataSet();
            DataTable dt_Initial = new DataTable();
            oConn7.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn7;
            oCommand.CommandText = "select distinct chrInitialSite from ORGsysLink where lstHistory = 1 and mnuStatus = 2 and  chrDestinationSite like '%" + sitename + "%'";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_Initial, "Initial");
            dt_Initial = ds_Initial.Tables["Initial"];
            int rowcount = 0;
            rowcount = dt_Initial.Rows.Count;
            string dest = "";

            if (rowcount == 1)
                camKlali.IsInit = dt_Initial.Rows[0]["CHRINITIALSITE"].ToString();
       
            if (rowcount > 1)
            {
                for (int b = 0; b < dt_Initial.Rows.Count; b++)
                {
                    dest += dt_Initial.Rows[b]["CHRINITIALSITE"].ToString();
                    dest += ", ";
                }
                camKlali.IsInit = dest;
            }
            oConn7.Close();


            }
            return View(camKlali);
        }
        [HttpPost]


    


        public ActionResult OpenCam(CamHashmalModel CamShowHashmal)
        {
            Globals.site_id = CamShowHashmal.SITE_NAME_ID;
            return RedirectToAction("ShowCam");
        }
        [HttpPost]

        public ActionResult OpenCamFromMaarechetKoah(CamMaarechetkoachModel CamMaarechet)
        {
            Globals.site_id = CamMaarechet.SITE_NAME_Maarechet;
            return RedirectToAction("ShowCam");
        }
        [HttpPost]


        public ActionResult OpenHashmal(CamKlaliModel camKlali)
        {
            Globals.site_id = camKlali.SITE_NAME_ID;
            return RedirectToAction("ShowHashmal");
        }
        [HttpPost]

        public async void SaveEntrance(List<string> things)
        {
            Dal.writeToLog("userPhone"+things[0]);
            string phone = Dal.getPhoneNum(nameG);
            Dal.writeToLog("phone ="+ phone);
            Dal.sendSms(phone, "Gamma 066 0" + things[0] + " gama");

        }
        public ActionResult ShowHashmal()
            
        {
                CamHashmalModel CamShowHashmal = new CamHashmalModel();
                Dal dal = new Dal();
                //Get Data from db for drop down list
                DataSet hashmalds = new DataSet();
                DataTable hashmaltbl = new DataTable();
                CamHashmalModel CamHashmal = new CamHashmalModel();
                OracleCommand oCommandHashmal;
                OracleDataAdapter oDataAdapterHashmal;
                OracleConnection oConnHashmal = dal.Connect("gisOraConn");
                oConnHashmal.Open();
                oCommandHashmal = new OracleCommand();
                oCommandHashmal.Connection = oConnHashmal;
                oCommandHashmal.CommandText = "SELECT dropdownlist_text,dropdownlist_name,dropdownlist_value FROM new_cam_dropdownlist";
                oDataAdapterHashmal = new OracleDataAdapter(oCommandHashmal);
                oDataAdapterHashmal.Fill(hashmalds, "drop");
                oConnHashmal.Close();
                hashmaltbl = hashmalds.Tables["drop"];

                //Get Data from GIS_CAMDATA_HASHMAL by siteid
                string SITE_ID_param = "";
                string HaspakatHashmalGoremNosaf = "";
                if (!Request.QueryString.ToString().Contains("SITE"))
                {
                    SITE_ID_param = Globals.site_id;
                }
                else { SITE_ID_param = Request.QueryString["SITE"]; }
                string SITE_ID = Regex.Match(SITE_ID_param, @"\d+").Value;
                int siteInt = int.Parse(SITE_ID);
                oCommandHashmal = new OracleCommand();
                OracleConnection ConnHashmal = dal.Connect("gisOraConn");
                ConnHashmal.Open();
                oCommandHashmal.Connection = ConnHashmal;
                oCommandHashmal.CommandText = "select * from GIS_CAMDATA_HASHMAL where SITEID like '%" + SITE_ID + "%'";
                oDataAdapterHashmal = new OracleDataAdapter(oCommandHashmal);
                var readerHashmal = oCommandHashmal.ExecuteReader();

                while (readerHashmal.Read())
                {
                    //AC הזנה חיצונית
                    CamShowHashmal.str_MakorHazanatHashmal = readerHashmal["MAKORHAZANATHASHMAL"].ToString();
                    CamShowHashmal.str_GodelMafsek = readerHashmal["GODELMAFSEK"].ToString();
                    CamShowHashmal.str_MikumMaazHizoniRashi = readerHashmal["MIKUMMAAZHIZONIRASHI"].ToString();
                    CamShowHashmal.str_KriatMoneMerahok = readerHashmal["KRIATMONEMERAHOK"].ToString();
                    CamShowHashmal.str_SugMone_HazanaHizonit = readerHashmal["SUGMONEHAZANAHIZONIT"].ToString();
                    CamShowHashmal.str_MisparMone_HazanaHizonit = readerHashmal["MISPARMONE_HIZONIT"].ToString();
                    CamShowHashmal.str_MisparHoze_HazanaHizonit = readerHashmal["MISPARHOZE_HIZONIT"].ToString();
                    CamShowHashmal.str_kaiamshataphashmal = readerHashmal["KAIAMSHATAPHASHMAL"].ToString();

                    //AC הזנה בחדר
                    CamShowHashmal.str_SugLyahHashmalBeAtar = readerHashmal["SUGLYAHHASHMALBEATAR"].ToString();
                    CamShowHashmal.str_GodelMafsekRashi = readerHashmal["GODELMAFSEKRASHI"].ToString();
                    CamShowHashmal.str_SugMone_HazanaBeHeder = readerHashmal["SUGMONEHAZANABEHEDER"].ToString();
                    CamShowHashmal.str_MisparMone_HazanaBeHeder = readerHashmal["MISPARMONEBEHEDER"].ToString();
                    CamShowHashmal.str_NitalLehaberGenerator = readerHashmal["NITALLEHABERGENERATOR"].ToString();
                    CamShowHashmal.str_SugShekaKayam = readerHashmal["SUGSHEKAKAYAM"].ToString();
                    CamShowHashmal.str_MehubarLekriaMerahok = readerHashmal["MEHUBARLEKRIAMERAHOK"].ToString();

                     //AC הספקה לגורם נוסף
                    //GOREMNOSAF_COMPANY FIRST
                    CamShowHashmal.str_GOREMNOSAF_COMPANY_first = readerHashmal["GOREMNOSAF_COMPANY_FIRST"].ToString();
                    CamShowHashmal.str_GodelMafsek_first = readerHashmal["GODELMAFSEK_FIRST"].ToString();
                    CamShowHashmal.str_SugMone_first = readerHashmal["SUGMONE_FIRST"].ToString();
                    CamShowHashmal.str_KriatMoneMeRahok_first = readerHashmal["KRIATMONEMERAHOK_FIRST"].ToString();
                    CamShowHashmal.str_KriatMoneAhrona_first = readerHashmal["KRIATMONEAHRONA_FIRST"].ToString();
                    CamShowHashmal.str_MisparMone_first = readerHashmal["MISPARMONE_FIRST"].ToString();
                    CamShowHashmal.str_DATEKRIATMONEAHRONA_first = readerHashmal["DATEKRIATMONEAHRONA_FIRST"].ToString();
                    //GOREMNOSAF_COMPANY SECOND
                    CamShowHashmal.str_GOREMNOSAF_COMPANY_second = readerHashmal["GOREMNOSAF_COMPANY_SECOND"].ToString();
                    CamShowHashmal.str_GodelMafsek_second = readerHashmal["GODELMAFSEK_SECOND"].ToString();
                    CamShowHashmal.str_SugMone_second = readerHashmal["SUGMONE_SECOND"].ToString();
                    CamShowHashmal.str_KriatMoneMeRahok_second = readerHashmal["KRIATMONEMERAHOK_SECOND"].ToString();
                    CamShowHashmal.str_KriatMoneAhrona_second = readerHashmal["KRIATMONEAHRONA_SECOND"].ToString();
                    CamShowHashmal.str_MisparMone_second = readerHashmal["MISPARMONE_SECOND"].ToString();
                    CamShowHashmal.str_DATEKRIATMONEAHRONA_second = readerHashmal["DATEKRIATMONEAHRONA_SECOND"].ToString();
                    //GOREMNOSAF_COMPANY THIRD
                    CamShowHashmal.str_GOREMNOSAF_COMPANY_third = readerHashmal["GOREMNOSAF_COMPANY_THIRD"].ToString();
                    CamShowHashmal.str_GodelMafsek_third = readerHashmal["GODELMAFSEK_THIRD"].ToString();
                    CamShowHashmal.str_SugMone_third = readerHashmal["SUGMONE_THIRD"].ToString();
                    CamShowHashmal.str_KriatMoneMeRahok_third = readerHashmal["KRIATMONEMERAHOK_THIRD"].ToString();
                    CamShowHashmal.str_KriatMoneAhrona_third = readerHashmal["KRIATMONEAHRONA_THIRD"].ToString();
                    CamShowHashmal.str_MisparMone_third = readerHashmal["MISPARMONE_THIRD"].ToString();
                    CamShowHashmal.str_DATEKRIATMONEAHRONA_third = readerHashmal["DATEKRIATMONEAHRONA_THIRD"].ToString();
                    //GOREMNOSAF_COMPANY_QUATER
                    CamShowHashmal.str_GOREMNOSAF_COMPANY_quater = readerHashmal["GOREMNOSAF_COMPANY_QUATER"].ToString();
                    CamShowHashmal.str_GodelMafsek_quater = readerHashmal["GODELMAFSEK_QUATER"].ToString();
                    CamShowHashmal.str_SugMone_quater = readerHashmal["SUGMONE_QUATER"].ToString();
                    CamShowHashmal.str_KriatMoneMeRahok_quater = readerHashmal["KRIATMONEMERAHOK_QUATER"].ToString();
                    CamShowHashmal.str_KriatMoneAhrona_quater = readerHashmal["KRIATMONEAHRONA_QUATER"].ToString();
                    CamShowHashmal.str_MisparMone_quater = readerHashmal["MISPARMONE_QUATER"].ToString();
                    CamShowHashmal.str_DATEKRIATMONEAHRONA_quater = readerHashmal["DATEKRIATMONEAHRONA_QUATER"].ToString();
                    //הערות
                    CamShowHashmal.str_Notes = readerHashmal["NOTES"].ToString();
                   //תאריך ביקורת AC
                    CamShowHashmal.str_TaarihBikoretAC = readerHashmal["TAARIHBIKORETAC"].ToString();
                  // מספר אתר
                    CamShowHashmal.SITE_NAME_ID = readerHashmal["SITEID"].ToString();
                }
                oConnHashmal.Close();
               //AC הזנה חיצונית dropdownlist values
               //מקור הזנת חשמל

                List<string> MakorHazanatHashmallist = (from x in hashmaltbl.AsEnumerable()
                                                        where x.Field<string>("dropdownlist_name") == "MakorHazanatHashmal"
                                                        select x.Field<string>("dropdownlist_text")).ToList();

                CamShowHashmal.MakorHazanatHashmal = MakorHazanatHashmallist.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
               //גודל מפסק הזנה
                List<string> GodelMafseklist = (from x in hashmaltbl.AsEnumerable()
                                                where x.Field<string>("dropdownlist_name") == "GodelMafsek"
                                                select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.GodelMafsek = GodelMafseklist.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
               //קריאת מונה מרחוק
                List<string> KriatMoneMerahoklst = (from x in hashmaltbl.AsEnumerable()
                                                    where x.Field<string>("dropdownlist_name") == "KriatMoneMerahok"
                                                    select x.Field<string>("dropdownlist_text")).ToList();

                CamShowHashmal.KriatMoneMerahok = KriatMoneMerahoklst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

               //סוג מונה
                List<string> SugMone = (from x in hashmaltbl.AsEnumerable()
                                        where x.Field<string>("dropdownlist_name") == "SugMone"
                                        select x.Field<string>("dropdownlist_text")).ToList();

                CamShowHashmal.SugMone = SugMone.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();



                 //AC הזנה בחדר
                //סוג לוח חשמל באתר
                List<string> SugLyahHashmalBeAtarlst = (from x in hashmaltbl.AsEnumerable()
                                                        where x.Field<string>("dropdownlist_name") == "SugLyahHashmalBeAtar"
                                                        select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.SugLyahHashmalBeAtar = SugLyahHashmalBeAtarlst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

                //גודל מפסק ראשי
                List<string> GodelMafsekRashilst = (from x in hashmaltbl.AsEnumerable()
                                                    where x.Field<string>("dropdownlist_name") == "GodelMafsekRashi"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.GodelMafsekRashi = GodelMafsekRashilst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

                //ניתן לחבר גנרטור
                List<string> YesNolst = (from x in hashmaltbl.AsEnumerable()
                                         where x.Field<string>("dropdownlist_name") == "YesNoMenu"
                                         select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.YesNo = YesNolst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
                //סוג שקע קיים
                List<string> SugShekaKayamlst = (from x in hashmaltbl.AsEnumerable()
                                                 where x.Field<string>("dropdownlist_name") == "SugShekaKayam"
                                                 select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.SugShekaKayam = SugShekaKayamlst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
                //מחובר לקריאה מרחוק
                List<string> MehubarLekriaMerahok = (from x in hashmaltbl.AsEnumerable()
                                                     where x.Field<string>("dropdownlist_name") == "YesNoMenu"
                                                     select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.MehubarLekriaMerahok = MehubarLekriaMerahok.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

                //קריאת מונה מרחוק 
                List<string> KriatMoneMeRahok_firstlst = (from x in hashmaltbl.AsEnumerable()
                                                          where x.Field<string>("dropdownlist_name") == "KriatMoneMerahok"
                                                          select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.KriatMoneMeRahok_GoremNosaf = KriatMoneMeRahok_firstlst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

                //שם חברה
                List<string> CamHasmal_CompanyNamelst = (from x in hashmaltbl.AsEnumerable()
                                                         where x.Field<string>("dropdownlist_name") == "CompanyName"
                                                         select x.Field<string>("dropdownlist_text")).ToList();
                CamShowHashmal.CamHasmal_CompanyName = CamHasmal_CompanyNamelst.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();


                return View(CamShowHashmal);
            //CamMaarechetkoachModel MaarechetkoachModel = new CamMaarechetkoachModel();
           // return View(MaarechetkoachModel);
        }
        public ActionResult ShowMaarechetkoach()
        {
            CamMaarechetkoachModel MaarechetkoachModel = new CamMaarechetkoachModel();
            Dal dal = new Dal();
            DataSet Maarechetkoachds = new DataSet();
            DataTable Maarechetkoachtbl = new DataTable();

            OracleCommand oCommandMaarechetkoach;
            OracleDataAdapter oDataAdapterMaarechetkoach;
            OracleConnection oConnMaarechetkoach = dal.Connect("gisOraConn");
            oConnMaarechetkoach.Open();
            oCommandMaarechetkoach = new OracleCommand();
            oCommandMaarechetkoach.Connection = oConnMaarechetkoach;
            oCommandMaarechetkoach.CommandText = "SELECT dropdownlist_text, dropdownlist_name, dropdownlist_value FROM new_cam_dropdownlist";
            oDataAdapterMaarechetkoach = new OracleDataAdapter(oCommandMaarechetkoach);
            oDataAdapterMaarechetkoach.Fill(Maarechetkoachds, "drop");
            oConnMaarechetkoach.Close();
            Maarechetkoachtbl = Maarechetkoachds.Tables["drop"];

            // Get Data from GIS_CAMDATA_MAARECHETKOAH by siteid
            string SITE_ID_param = "";
            if (!Request.QueryString.ToString().Contains("SITE"))
            {
                SITE_ID_param = Globals.site_id;
            }
            else { SITE_ID_param = Request.QueryString["SITE"]; }
            string SITE_ID = Regex.Match(SITE_ID_param, @"\d+").Value;
            int siteInt = int.Parse(SITE_ID);
            oCommandMaarechetkoach = new OracleCommand();
            OracleConnection ConnMaarechetkoach = dal.Connect("gisOraConn");
            ConnMaarechetkoach.Open();
            oCommandMaarechetkoach.Connection = ConnMaarechetkoach;
            oCommandMaarechetkoach.CommandText = "select * from GIS_CAMDATA_MAARECHETKOAH where SITEID like '%" + SITE_ID + "%'";
            oDataAdapterMaarechetkoach = new OracleDataAdapter(oCommandMaarechetkoach);
            var readerMaarechetkoach = oCommandMaarechetkoach.ExecuteReader();

            while (readerMaarechetkoach.Read())
            {
                //מערכת כוח 1
                MaarechetkoachModel.str_Yaud_First = readerMaarechetkoach["YAUDFIRST"].ToString();
                MaarechetkoachModel.str_sug_First = readerMaarechetkoach["SUGFIRST"].ToString();
                MaarechetkoachModel.str_godelmafsek_First = readerMaarechetkoach["GODELMAFSEKFIRST"].ToString();
                MaarechetkoachModel.str_kamutmeyashrim_First = readerMaarechetkoach["KAMUTMEYASHRIMFIRST"].ToString();
                MaarechetkoachModel.str_SugMzber_First = readerMaarechetkoach["SUGMZBERFIRST"].ToString();
                MaarechetkoachModel.str_kayamLVD_First = readerMaarechetkoach["KAYAMLVDFIRST"].ToString();
                MaarechetkoachModel.str_ztichatzerem_First = readerMaarechetkoach["ZTICHATZEREMFIRST"].ToString();
                MaarechetkoachModel.str_Year_First = readerMaarechetkoach["YearFIRST"].ToString();
                MaarechetkoachModel.str_Month_First = readerMaarechetkoach["MonthFIRST"].ToString();

                MaarechetkoachModel.str_HiburLeMaarecetOne_First = readerMaarechetkoach["HIBURLEMAARECETONEFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetTwo_First = readerMaarechetkoach["HIBURLEMAARECETTWOFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetThree_First = readerMaarechetkoach["HIBURLEMAARECETTHREEFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFoor_First = readerMaarechetkoach["HIBURLEMAARECETFOORFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFive_First = readerMaarechetkoach["HIBURLEMAARECETFIVEFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSix_First = readerMaarechetkoach["HIBURLEMAARECETSIXFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSeven_First = readerMaarechetkoach["HIBURLEMAARECETSEVENFIRST"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetEight_First = readerMaarechetkoach["HIBURLEMAARECETEIGHTFIRST"].ToString();

                MaarechetkoachModel.str_GodelMafsekOne_First = readerMaarechetkoach["GODELMAFSEKONEFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsektwo_First = readerMaarechetkoach["GODELMAFSEKTWOFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekthree_First = readerMaarechetkoach["GODELMAFSEKTHREEFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekFoor_First = readerMaarechetkoach["GODELMAFSEKFOORFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekFive_First = readerMaarechetkoach["GODELMAFSEKFIVEFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekSix_First = readerMaarechetkoach["GODELMAFSEKSIXFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekSeven_First = readerMaarechetkoach["GODELMAFSEKSEVENFIRST"].ToString();
                MaarechetkoachModel.str_GodelMafsekEight_First = readerMaarechetkoach["GODELMAFSEKEIGHTFIRST"].ToString();

                //מערכת כוח 2
                MaarechetkoachModel.str_Yaud_Second = readerMaarechetkoach["YAUDSECOND"].ToString();
                MaarechetkoachModel.str_sug_Second = readerMaarechetkoach["SUGSECOND"].ToString();
                MaarechetkoachModel.str_godelmafsek_Second = readerMaarechetkoach["GODELMAFSEKSECOND"].ToString();
                MaarechetkoachModel.str_kamutmeyashrim_Second = readerMaarechetkoach["KAMUTMEYASHRIMSECOND"].ToString();
                MaarechetkoachModel.str_SugMazber_Second = readerMaarechetkoach["SUGMZBERSECOND"].ToString();
                MaarechetkoachModel.str_kayamLVD_Second = readerMaarechetkoach["KAYAMLVDSECOND"].ToString();
                MaarechetkoachModel.str_ztichatzerem_Second = readerMaarechetkoach["ZTICHATZEREMSECOND"].ToString();
                MaarechetkoachModel.str_Year_Second = readerMaarechetkoach["YearSECOND"].ToString();
                MaarechetkoachModel.str_Month_Second = readerMaarechetkoach["MonthSECOND"].ToString();

                MaarechetkoachModel.str_HiburLeMaarecetOne_Second = readerMaarechetkoach["HIBURLEMAARECETONESECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetTwo_Second = readerMaarechetkoach["HIBURLEMAARECETTWOSECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetThree_Second = readerMaarechetkoach["HIBURLEMAARECETTHREESECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFoor_Second = readerMaarechetkoach["HIBURLEMAARECETFOORSECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFive_Second = readerMaarechetkoach["HIBURLEMAARECETFIVESECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSix_Second = readerMaarechetkoach["HIBURLEMAARECETSIXSECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSeven_Second = readerMaarechetkoach["HIBURLEMAARECETSEVENSECOND"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetEight_Second = readerMaarechetkoach["HIBURLEMAARECETEIGHTSECOND"].ToString();

                MaarechetkoachModel.str_GodelMafsekOne_Second = readerMaarechetkoach["GODELMAFSEKONESECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsektwo_Second = readerMaarechetkoach["GODELMAFSEKTWOSECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekthree_Second = readerMaarechetkoach["GODELMAFSEKTHREESECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekFoor_Second = readerMaarechetkoach["GODELMAFSEKFOORSECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekFive_Second = readerMaarechetkoach["GODELMAFSEKFIVESECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekSix_Second = readerMaarechetkoach["GODELMAFSEKSIXSECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekSeven_Second = readerMaarechetkoach["GODELMAFSEKSEVENSECOND"].ToString();
                MaarechetkoachModel.str_GodelMafsekEight_Second = readerMaarechetkoach["GODELMAFSEKEIGHTSECOND"].ToString();

                //מערכת כוח 3
                MaarechetkoachModel.str_Yaud_Third = readerMaarechetkoach["YAUDTHIRD"].ToString();
                MaarechetkoachModel.str_sug_Third = readerMaarechetkoach["SUGTHIRD"].ToString();
                MaarechetkoachModel.str_godelmafsek_Third = readerMaarechetkoach["GODELMAFSEKTHIRD"].ToString();
                MaarechetkoachModel.str_kamutmeyashrim_Third = readerMaarechetkoach["KAMUTMEYASHRIMTHIRD"].ToString();
                MaarechetkoachModel.str_SugMazber_Third = readerMaarechetkoach["SUGMZBERTHIRD"].ToString();
                MaarechetkoachModel.str_kayamLVD_Third = readerMaarechetkoach["KAYAMLVDTHIRD"].ToString();
                MaarechetkoachModel.str_ztichatzerem_Third = readerMaarechetkoach["ZTICHATZEREMTHIRD"].ToString();
                MaarechetkoachModel.str_Year_Third = readerMaarechetkoach["YearTHIRD"].ToString();
                MaarechetkoachModel.str_Month_Third = readerMaarechetkoach["MonthTHIRD"].ToString();

                MaarechetkoachModel.str_HiburLeMaarecetOne_Third = readerMaarechetkoach["HIBURLEMAARECETONETHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetTwo_Third = readerMaarechetkoach["HIBURLEMAARECETTWOTHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetThree_Third = readerMaarechetkoach["HIBURLEMAARECETTHREETHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFoor_Third = readerMaarechetkoach["HIBURLEMAARECETFOORTHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetFive_Third = readerMaarechetkoach["HIBURLEMAARECETFIVETHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSix_Third = readerMaarechetkoach["HIBURLEMAARECETSIXTHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetSeven_Third = readerMaarechetkoach["HIBURLEMAARECETSEVENTHIRD"].ToString();
                MaarechetkoachModel.str_HiburLeMaarecetEight_Third = readerMaarechetkoach["HIBURLEMAARECETEIGHTTHIRD"].ToString();

                MaarechetkoachModel.str_GodelMafsekOne_Third = readerMaarechetkoach["GODELMAFSEKONETHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsektwo_Third = readerMaarechetkoach["GODELMAFSEKTWOTHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekthree_Third = readerMaarechetkoach["GODELMAFSEKTHREETHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekFoor_Third = readerMaarechetkoach["GODELMAFSEKFOORTHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekFive_Third = readerMaarechetkoach["GODELMAFSEKFIVETHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekSix_Third = readerMaarechetkoach["GODELMAFSEKSIXTHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekSeven_Third = readerMaarechetkoach["GODELMAFSEKSEVENTHIRD"].ToString();
                MaarechetkoachModel.str_GodelMafsekEight_Third = readerMaarechetkoach["GODELMAFSEKEIGHTTHIRD"].ToString();

                //הערות
                MaarechetkoachModel.str_Notes_maarechet = readerMaarechetkoach["NOTES"].ToString();
                //תאריך ביקורת AC
                MaarechetkoachModel.str_TaarihBikoret_AC = readerMaarechetkoach["TAARIHBIKORETAC"].ToString();
                //מספר אתר
                MaarechetkoachModel.SITE_NAME_Maarechet = readerMaarechetkoach["SITEID"].ToString();
            }
            ConnMaarechetkoach.Close();

            //מערכות כח DC dropdownlist values

            //יעוד מערכת כח
            List<string> lastYaudMaarechetKoah = (from x in Maarechetkoachtbl.AsEnumerable()
                                                    where x.Field<string>("dropdownlist_name") == "YaudMaarechetKoah"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.Yaud = lastYaudMaarechetKoah.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();        

            //סוג מערכת כח
            List<string> lstSugMaarehetKoah = (from x in Maarechetkoachtbl.AsEnumerable()
                                      where x.Field<string>("dropdownlist_name") == "SugMaarehetKoah"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.sug = lstSugMaarehetKoah.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();        

            //גודל מפסק AC בלוח חשמל
            List<string> lstgodelmafsek = (from x in Maarechetkoachtbl.AsEnumerable()
                                              where x.Field<string>("dropdownlist_name") == "godelmafsekACBeLuahHashmal"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.godelmafsek = lstgodelmafsek.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();        

            //כמות מיישרים
            List<string> lstkamutmeyashrim = (from x in Maarechetkoachtbl.AsEnumerable()
                                                 where x.Field<string>("dropdownlist_name") == "kamutmeyashrim"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.kamutmeyashrim = lstkamutmeyashrim.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();       

            //סוג מצבר
            List<string> lstSugMzber = (from x in Maarechetkoachtbl.AsEnumerable()
                                              where x.Field<string>("dropdownlist_name") == "SugMzber"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.SugMzber = lstSugMzber.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();        
         
            // כמות מצברים
            List<string> lstKamutMazberim = (from x in Maarechetkoachtbl.AsEnumerable()
                                             where x.Field<string>("dropdownlist_name") == "KamutMazberim"
                                             orderby x.Field<string>("dropdownlist_value")
                                        select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.KamutMazberim = lstKamutMazberim.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

            //שנה
            List<string> lstYear = (from x in Maarechetkoachtbl.AsEnumerable()
                                    where x.Field<string>("dropdownlist_name") == "Year"
                                    orderby x.Field<string>("dropdownlist_text")
                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.Year = lstYear.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

            //חודש
            List<string> lstMonth = (from x in Maarechetkoachtbl.AsEnumerable()
                                     where x.Field<string>("dropdownlist_name") == "Month"
                                     orderby x.Field<string>("dropdownlist_value")
                                     select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.Month = lstMonth.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            //קיים עוקף LVD לתמסורת
            List<string> lstkayamLVD = (from x in Maarechetkoachtbl.AsEnumerable()
                                           where x.Field<string>("dropdownlist_name") == "kayamLVD"
                                                    select x.Field<string>("dropdownlist_text")).ToList();
            MaarechetkoachModel.kayamLVD = lstkayamLVD.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
          
            //חיבור צרכנים למערכת
            List<string> lstHiburLeMaarecet = (from x in Maarechetkoachtbl.AsEnumerable()
                                               where x.Field<string>("dropdownlist_name") == "HiburLeMaarecet"
                                               select x.Field<string>("dropdownlist_text")).ToList();
            //מערכת עבור 3 מערכות
            MaarechetkoachModel.HiburLeMaarecet = lstHiburLeMaarecet.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
 
            //גודל מפסק DC
            List<string> lstGodelMafsek = (from x in Maarechetkoachtbl.AsEnumerable()
                                           where x.Field<string>("dropdownlist_name") == "GodelMafsekDC"
                                           select x.Field<string>("dropdownlist_text")).ToList();
            //מערכת1
            MaarechetkoachModel.GodelMafsek = lstGodelMafsek.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

            return View(MaarechetkoachModel);
        }
        [HttpPost]

       
        public ActionResult ShowAirCond()
        {
            CamAirCondModel CamAir = new CamAirCondModel();
            Dal dal = new Dal();
            OracleCommand oCommand;
            OracleDataAdapter oDataAdapter;
            OracleConnection oConn = dal.Connect("tech_cm");
            oConn.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "SELECT room_number, room_type, has_private_equipment, length_2, width,location,owner,remarks,site_type FROM CM_ROOM  WHERE DOOR =7864 AND ROOM_NUMBER=1";
            oDataAdapter = new OracleDataAdapter(oCommand);
            var reader = oCommand.ExecuteReader();
            if (reader.Read())
            {
              
                CamAir.SITE_TYPE = reader["site_type"].ToString();
                CamAir.room_type = reader["room_type"].ToString();
                CamAir.owner = reader["owner"].ToString();
            }
            int conditionersNum = 0;
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "SELECT count(*) as cond_Num FROM CM_AIR_COND  WHERE DOOR =7864 and room = 1";
            oDataAdapter = new OracleDataAdapter(oCommand);
            reader = oCommand.ExecuteReader();
            if (reader.Read())
            {
                conditionersNum = int.Parse(reader["cond_Num"].ToString());
            }
            oConn.Close();
            oConn.Open();
            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "SELECT * FROM CM_AIR_COND  WHERE DOOR =7864 and room = 1";
            oDataAdapter = new OracleDataAdapter(oCommand);
            reader = oCommand.ExecuteReader();
            if (reader.Read())
            {
                CamAir.MANUFACTURER = reader["MANUFACTURER"].ToString();
                CamAir.TYPE_2 = reader["TYPE_2"].ToString();
                CamAir.MODEL = reader["MODEL"].ToString();
            }

            return View(CamAir);
        }
        [HttpPost]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SaveHashmal(CamHashmalModel CamShowHashmal)
        {

          Globals.site_id = CamShowHashmal.SITE_NAME_ID;
          Dal dal = new Dal();
          dal.saveHashmalData(CamShowHashmal);
          return RedirectToAction("ShowHashmal");
        }                       
        [HttpPost]

      
        public ActionResult UpdateMaarechetkoachModel(CamMaarechetkoachModel Mkoach)
        {
            Globals.site_id = Mkoach.SITE_NAME_Maarechet;
            Dal dal = new Dal();
            dal.saveMaarechetkoachData(Mkoach);
            return RedirectToAction("ShowMaarechetkoach");
        }
        [HttpPost]




        public ActionResult Save(CamKlaliModel camKlali)
        {
            Globals.site_id = camKlali.SITE_NAME_ID;
            Dal dal = new Dal();
            dal.saveData(camKlali);
          return  RedirectToAction("ShowCam");
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]


        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}

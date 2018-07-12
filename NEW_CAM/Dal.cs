using NEW_CAM.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.DirectoryServices;
using System.Threading.Tasks;


namespace NEW_CAM
{
    public class Dal
    {
        public OracleConnection Connect(string astrConnectionString)
        {
            return new OracleConnection(WebConfigurationManager.ConnectionStrings[astrConnectionString].ConnectionString);
        }

        public bool saveData(CamKlaliModel camKlali)
        {
            string concat = "";
            string[] camCell = { camKlali.cbk_PELEPHONE, camKlali.cbk_CELLCOM, camKlali.cbk_PARTNER, camKlali.cbk_HOT_CBK };
            foreach (string company in camCell)
            {
                if (company == null)
                {
                    continue;
                }
                else
                {
                    if (concat != "")
                    {
                        concat += "," + company;
                    }
                    else { concat += company; }
                }

            }
             try
            {

                updateGisCamPramis(camKlali);
                updateGisCamMenualFill(camKlali);
                updateContacts(camKlali.CamContactList, camKlali.SITE_NAME_ID);
                updateGisCamPramis2(camKlali, concat);
                updateModifyDate(camKlali);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SiteDownLogHistory:Error trying to create log on DBConnection : " + ex.Message);
            }
            return true;
        }

        public bool saveHashmalData(CamHashmalModel camHashmal)
        {
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            string site_num = Regex.Match(camHashmal.SITE_NAME_ID, @"\d+").Value;
            cmd.CommandText = " UPDATE GIS_CAMDATA_HASHMAL  SET MAKORHAZANATHASHMAL = '" + camHashmal.str_MakorHazanatHashmal + 
               "', GODELMAFSEK = '" + camHashmal.str_GodelMafsek + "', MIKUMMAAZHIZONIRASHI = '" + camHashmal.str_MikumMaazHizoniRashi +
               "', KRIATMONEMERAHOK  = '" + camHashmal.str_KriatMoneMerahok + "' , SUGMONEHAZANAHIZONIT = '" + camHashmal.str_SugMone_HazanaHizonit +
               "' , MISPARMONE_HIZONIT = '" + camHashmal.str_MisparMone_HazanaHizonit + "', MISPARHOZE_HIZONIT ='" + 
                camHashmal.str_MisparHoze_HazanaHizonit + "', KAIAMSHATAPHASHMAL='" + camHashmal.str_kaiamshataphashmal +
               "', SUGLYAHHASHMALBEATAR = '" + camHashmal.str_SugLyahHashmalBeAtar +
               "', GODELMAFSEKRASHI = '" + camHashmal.str_GodelMafsekRashi + "', SUGMONEHAZANABEHEDER = '" + camHashmal.str_SugMone_HazanaBeHeder +
               "', MISPARMONEBEHEDER = '" + camHashmal.str_MisparMone_HazanaBeHeder + "', NITALLEHABERGENERATOR = '" + camHashmal.str_NitalLehaberGenerator +
               "', GODELMAFSEK_FIRST = '" + camHashmal.str_GodelMafsek_first + "', SUGMONE_FIRST = '" + camHashmal.str_SugMone_first +
               "', KRIATMONEMERAHOK_FIRST = '" + camHashmal.str_KriatMoneMeRahok_first + "', KRIATMONEAHRONA_FIRST = '" + camHashmal.str_KriatMoneAhrona_first +
               "', MISPARMONE_FIRST = '" + camHashmal.str_MisparMone_first + "', DATEKRIATMONEAHRONA_FIRST = '" + camHashmal.str_DATEKRIATMONEAHRONA_first +
               "', GOREMNOSAF_COMPANY_FIRST = '" + camHashmal.str_GOREMNOSAF_COMPANY_first +
               "', GODELMAFSEK_SECOND = '" + camHashmal.str_GodelMafsek_second +
               "', SUGMONE_SECOND = '" + camHashmal.str_SugMone_second + "', KRIATMONEMERAHOK_SECOND = '" + camHashmal.str_KriatMoneMeRahok_second +
               "', KRIATMONEAHRONA_SECOND = '" + camHashmal.str_KriatMoneAhrona_second + "', MISPARMONE_SECOND = '" + camHashmal.str_MisparMone_second +
               "', DATEKRIATMONEAHRONA_SECOND = '" + camHashmal.str_DATEKRIATMONEAHRONA_second +
               "', GOREMNOSAF_COMPANY_SECOND = '" + camHashmal.str_GOREMNOSAF_COMPANY_second +
                "', GODELMAFSEK_THIRD = '" + camHashmal.str_GodelMafsek_second +
               "', SUGMONE_THIRD = '" + camHashmal.str_SugMone_third + "', KRIATMONEMERAHOK_THIRD = '" + camHashmal.str_KriatMoneMeRahok_third +
               "', KRIATMONEAHRONA_THIRD = '" + camHashmal.str_KriatMoneAhrona_third + "', MISPARMONE_THIRD = '" + camHashmal.str_MisparMone_third +
               "', DATEKRIATMONEAHRONA_THIRD = '" + camHashmal.str_DATEKRIATMONEAHRONA_third +
               "', GOREMNOSAF_COMPANY_THIRD = '" + camHashmal.str_GOREMNOSAF_COMPANY_third +
                "', GODELMAFSEK_QUATER = '" + camHashmal.str_GodelMafsek_second +
               "', SUGMONE_QUATER = '" + camHashmal.str_SugMone_quater + "', KRIATMONEMERAHOK_QUATER = '" + camHashmal.str_KriatMoneMeRahok_quater +
               "', KRIATMONEAHRONA_QUATER = '" + camHashmal.str_KriatMoneAhrona_quater + "', MISPARMONE_QUATER = '" + camHashmal.str_MisparMone_quater +
               "', DATEKRIATMONEAHRONA_QUATER = '" + camHashmal.str_DATEKRIATMONEAHRONA_quater +
               "', GOREMNOSAF_COMPANY_QUATER = '" + camHashmal.str_GOREMNOSAF_COMPANY_quater +

               "', NOTES = '" + camHashmal.str_Notes + "', TAARIHBIKORETAC = '" + camHashmal.str_TaarihBikoretAC + 
               "', SUGSHEKAKAYAM = '" + camHashmal.str_SugShekaKayam +
               "' WHERE SITEID like '%" + site_num + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }

        public bool saveMaarechetkoachData(CamMaarechetkoachModel CamShowMaarechetkoach)
        {
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            string site_num = Regex.Match(CamShowMaarechetkoach.SITE_NAME_Maarechet, @"\d+").Value;

            cmd.CommandText = "UPDATE GIS_CAMDATA_MAARECHETKOAH  SET YAUDFIRST = '" + CamShowMaarechetkoach.str_Yaud_First +
               "', SUGFIRST = '" + CamShowMaarechetkoach.str_sug_First + "', GODELMAFSEKFIRST = '" + CamShowMaarechetkoach.str_godelmafsek_First +
               "', KAMUTMEYASHRIMFIRST  = '" + CamShowMaarechetkoach.str_kamutmeyashrim_First + "' , SUGMZBERFIRST = '" + CamShowMaarechetkoach.str_SugMzber_First +
               "' , KAYAMLVDFIRST = '" + CamShowMaarechetkoach.str_kayamLVD_First + "', ZTICHATZEREMFIRST ='" +
                CamShowMaarechetkoach.str_ztichatzerem_First + "', YearFIRST='" + CamShowMaarechetkoach.str_Year_First +
               "', MonthFIRST = '" + CamShowMaarechetkoach.str_Month_First +
               "', HIBURLEMAARECETONEFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetOne_First + "', HIBURLEMAARECETTWOFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetTwo_First +
               "', HIBURLEMAARECETTHREEFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetThree_First + "', HIBURLEMAARECETFOORFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFoor_First +
               "', HIBURLEMAARECETFIVEFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFive_First + "', HIBURLEMAARECETSIXFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSix_First +
               "', HIBURLEMAARECETSEVENFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSeven_First + "', HIBURLEMAARECETEIGHTFIRST = '" + CamShowMaarechetkoach.str_HiburLeMaarecetEight_First +
               "', GODELMAFSEKONEFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekOne_First + "', GODELMAFSEKTWOFIRST = '" + CamShowMaarechetkoach.str_GodelMafsektwo_First +
               "', GODELMAFSEKTHREEFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekthree_First +
               "', GODELMAFSEKFOORFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekFoor_First +
               "', GODELMAFSEKFIVEFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekFive_First + "', GODELMAFSEKSIXFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekSix_First +
               "', GODELMAFSEKSEVENFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekSeven_First + "', GODELMAFSEKEIGHTFIRST = '" + CamShowMaarechetkoach.str_GodelMafsekEight_First +
               "',YAUDSECOND = '" + CamShowMaarechetkoach.str_Yaud_Second +
               "', SUGSECOND = '" + CamShowMaarechetkoach.str_sug_Second + "', GODELMAFSEKSECOND = '" + CamShowMaarechetkoach.str_godelmafsek_Second +
               "', KAMUTMEYASHRIMSECOND  = '" + CamShowMaarechetkoach.str_kamutmeyashrim_Second + "' , SUGMZBERSECOND = '" + CamShowMaarechetkoach.str_SugMazber_Second +
               "' , KAYAMLVDSECOND = '" + CamShowMaarechetkoach.str_kayamLVD_First + "', ZTICHATZEREMSECOND ='" +
                CamShowMaarechetkoach.str_ztichatzerem_Second + "', YearSECOND='" + CamShowMaarechetkoach.str_Year_Second +
               "', MonthSECOND = '" + CamShowMaarechetkoach.str_Month_Second +
               "', HIBURLEMAARECETONESECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetOne_Second + "', HIBURLEMAARECETTWOSECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetTwo_Second +
               "', HIBURLEMAARECETTHREESECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetThree_Second + "', HIBURLEMAARECETFOORSECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFoor_Second +
               "', HIBURLEMAARECETFIVESECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFive_Second + "', HIBURLEMAARECETSIXSECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSix_Second +
               "', HIBURLEMAARECETSEVENSECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSeven_Second + "', HIBURLEMAARECETEIGHTSECOND = '" + CamShowMaarechetkoach.str_HiburLeMaarecetEight_Second +
               "', GODELMAFSEKONESECOND = '" + CamShowMaarechetkoach.str_GodelMafsekOne_Second + "', GODELMAFSEKTWOSECOND = '" + CamShowMaarechetkoach.str_GodelMafsektwo_Second +
               "', GODELMAFSEKTHREESECOND = '" + CamShowMaarechetkoach.str_GodelMafsekthree_Second +
               "', GODELMAFSEKFOORSECOND = '" + CamShowMaarechetkoach.str_GodelMafsekFoor_Second +
               "', GODELMAFSEKFIVESECOND = '" + CamShowMaarechetkoach.str_GodelMafsekFive_Second + "', GODELMAFSEKSIXSECOND = '" + CamShowMaarechetkoach.str_GodelMafsekSix_Second +
               "', GODELMAFSEKSEVENSECOND = '" + CamShowMaarechetkoach.str_GodelMafsekSeven_Second + "', GODELMAFSEKEIGHTSECOND = '" + CamShowMaarechetkoach.str_GodelMafsekEight_Second +
              "',YAUDTHIRD = '" + CamShowMaarechetkoach.str_Yaud_Third +
               "', SUGTHIRD = '" + CamShowMaarechetkoach.str_sug_Third + "', GODELMAFSEKTHIRD = '" + CamShowMaarechetkoach.str_godelmafsek_Third +
               "', KAMUTMEYASHRIMTHIRD  = '" + CamShowMaarechetkoach.str_kamutmeyashrim_Third + "' , SUGMZBERTHIRD = '" + CamShowMaarechetkoach.str_SugMazber_Third +
               "' , KAYAMLVDTHIRD = '" + CamShowMaarechetkoach.str_kayamLVD_Third + "', ZTICHATZEREMTHIRD ='" +
                CamShowMaarechetkoach.str_ztichatzerem_Third + "', YearTHIRD='" + CamShowMaarechetkoach.str_Year_Third +
               "', MonthTHIRD = '" + CamShowMaarechetkoach.str_Month_Third +
               "', HIBURLEMAARECETONETHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetOne_Third + "', HIBURLEMAARECETTWOTHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetTwo_Third +
               "', HIBURLEMAARECETTHREETHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetThree_Third + "', HIBURLEMAARECETFOORTHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFoor_Third +
               "', HIBURLEMAARECETFIVETHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetFive_Third + "', HIBURLEMAARECETSIXTHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSix_Third +
               "', HIBURLEMAARECETSEVENTHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetSeven_Third + "', HIBURLEMAARECETEIGHTTHIRD = '" + CamShowMaarechetkoach.str_HiburLeMaarecetEight_Third +
               "', GODELMAFSEKONETHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekOne_Third + "', GODELMAFSEKTWOTHIRD = '" + CamShowMaarechetkoach.str_GodelMafsektwo_Third +
               "', GODELMAFSEKTHREETHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekthree_Third +
               "', GODELMAFSEKFOORTHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekFoor_Third +
               "', GODELMAFSEKFIVETHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekFive_Third + "', GODELMAFSEKSIXTHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekSix_Third +
               "', GODELMAFSEKSEVENTHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekSeven_Third + "', GODELMAFSEKEIGHTTHIRD = '" + CamShowMaarechetkoach.str_GodelMafsekEight_Third +
               "', NOTES = '" + CamShowMaarechetkoach.str_Notes_maarechet + "', TAARIHBIKORETAC = '" + CamShowMaarechetkoach.str_TaarihBikoret_AC +
               "' WHERE SITEID like '%" + site_num + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return true;
        }

        public string getRnc(int x, int y)
        {
            x = 162085;
            y = 592663;
            string res = "";
            OracleConnection cn = new OracleConnection(WebConfigurationManager.ConnectionStrings["gisOraConn"].ConnectionString);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_rnc_itm";
            cmd.Parameters.Add("results", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("p_x", OracleDbType.Decimal).Value = x;
            cmd.Parameters.Add("p_y", OracleDbType.Decimal).Value = y;
            cmd.Connection = cn;
            cmd.InitialLONGFetchSize = 1000;
            cmd.Connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                int j = 0;
                if (reader.Read())
                {
                    res = (readCellFromDb(reader, 0));
                }
            }
            return res;
        }

        public void updatateGisCamDetails(CamKlaliModel camKlali)
        {
            OracleConnection oConn = Connect("aradmin");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;

        }
        public static string getPhoneNum(string name) {

            var users = GetUsers();
            string tempName = "";
            if (name.Split('.').Length == 2)
            {
                tempName = name.Split('.')[0]+ " "+ name.Split('.')[1];
            }
            writeToLog("tempName Name:"+ tempName.ToUpper());
            foreach (var item in users)
            { 
                if (item.displayname.ToUpper() == tempName.ToUpper())
                {
                    return item.mobile;
                }
            }
            return string.Empty;

        }
        // todo:check to update
        public void updateGisCamMenualFill(CamKlaliModel camKlali)
        {
            string instuctions = camKlali.ACCESS_INSTRUCTIONS  == null ? "" : camKlali.ACCESS_INSTRUCTIONS.Replace("'","");

            string transbk = camKlali.HAS_TRANSMITION_BKP == null ? "" : camKlali.HAS_TRANSMITION_BKP.Replace("כן", "קיים בשטח");

            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            string site_name_ = Regex.Match(camKlali.SITE_NAME_ID, @"\d+").Value;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = " UPDATE gis_camdata_menual A SET a.electricity_source = '" + camKlali.ELECTRICITY_SOURCE + "', a.MONE_NUMBER = '" + camKlali.MONE_NUM + "', a.MMARECHET_KOAH_NUM = '" + camKlali.MMARECHET_KOAH_NUM + "', A.EZOR_HALUKA  = '" + camKlali.EZOR_HALUKA + "' , A.RATZ_EZOR = '" + camKlali.RATZ_EZOR +
                "' , TECH_POLY = '" + camKlali.TECH_POLY + "', A.SUPPLY_SIZE ='" + camKlali.SUPPLY_SIZE + "', MNUSTRUCTURETYPE='" + camKlali.MNUSTRUCTURETYPE +
              "', A.ACCESS_INSTRUCTIONS ='" + instuctions + "', A.HAS_TRANSMITION_BKP ='" + transbk + "' WHERE A.SITE_NAME_ID like '%" + site_name_ + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void updateContacts(List<CamContactList> camKlali, string site_name_id)
        {
            string SITE_ID = Regex.Match(site_name_id, @"\d+").Value;
            int siteInt = int.Parse(SITE_ID);
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = "delete from GIS_CAM_CONTACTS_MENUAL_v2 A where A.SITE_ID='" + siteInt + "'";
             if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            insertUpdatedContacts(camKlali, site_name_id);
        }
        public void insertUpdatedContacts(List<CamContactList> camKlali, string site_name_id)
        {
            string SITE_ID = Regex.Match(site_name_id, @"\d+").Value;
            int siteInt = int.Parse(SITE_ID);
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            foreach (var contact in camKlali)
            {
                string Remarks = contact.remarks == null ? "" : contact.remarks.Replace("'", "");
                cmd.CommandText = "insert into  GIS_CAM_CONTACTS_MENUAL_v2 A  (site_id, contact1, txtphonecellular , txtphonecellular2, remarks)" +
                     "values('" + siteInt + "','" + contact.contact1 + "','" + contact.txtphonecellular + "','" + contact.txtphonecellular2 + "','" + Remarks + "')";
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            cmd.Connection.Close();
        }
            public  string checkExistance(string site_id_) {
            string siteName = "";
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = "select site_name_id from v_gis_cam_general_V2 where site_name_id like '%" + site_id_ + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                try
                {
                    cmd.Connection.Open();
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
                //cmd.Connection.Open();
            }
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    siteName = reader["site_name_id"].ToString();
                }
            }
            cmd.Connection.Close();
            return siteName;
        }
        public string getSiteName(string site_id_)
        {
            string siteName = "";
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = "select substr(name, 2) as sname from v_orange_sites where substr(name, 3) like '%" + site_id_ + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    siteName = reader["sname"].ToString();
                }
            }
            cmd.Connection.Close();
            return siteName;
        }
        public async static Task sendSms(string name, string msg)
        {
           msg = "test message";
           name = "0545946090";
            string bk = "";
            writeToLog("name"+name);
            writeToLog("msg"+msg);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://bms.hotmobile.co.il/bms/httpSrv?message=00470061006D006D0061002000300036003600200030003500340035003900340036003000390030002000670061006D0061&to=0542074328&from=phi&language=2&period=100&channel=SRV&acknowledge=true&user=105707932227&password=Y35vs6MJ");
                    client.DefaultRequestHeaders.Accept.Clear();
                    var messageAsBytes = Encoding.BigEndianUnicode.GetBytes(msg);
                    string msgASbits = BitConverter.ToString(messageAsBytes).Replace("-", "");
                    try
                    {
                        string link = "?message=" + msgASbits + "&to=" + name +
                                      "&from=phi&language=2&period=100&channel=SRV&acknowledge=true&user=105707932227&password=Y35vs6MJ";
                        writeToLog(link);
                        HttpResponseMessage response = await client.GetAsync(link);
                        writeToLog("here");
                        if (response.IsSuccessStatusCode)
                        {
                            writeToLog("Sms sent msg:" + " to numbers: ");
                        }
                        else
                        {
                            writeToLog("Sms was not sent msg:" + " to numbers: " + " because :" + response.ReasonPhrase);
                        }
                    }
                    catch (Exception ex) {writeToLog("failed to send sms. inner exception:"+ex.InnerException+ ex.Message); }
                }
            }
            catch (Exception ex) { writeToLog("failed. inner ex:"+ex.Message); }
          
        }

       public void insertRowCamDataMenual(string SITE_ID)  // 4 digits
       {
           OracleConnection oConn = Connect("gisOraConn");
           OracleDataAdapter da = new OracleDataAdapter();
           OracleCommand cmd = new OracleCommand();
           cmd.Connection = oConn;
           cmd.CommandText = "select * from  gis_camdata_menual A where site_name_id like" +
              "'%" + SITE_ID + "%'";
           if (cmd.Connection.State == ConnectionState.Closed)
           {
               cmd.Connection.Open();
           }
           var reader = cmd.ExecuteReader();

           if (reader.Read())
           {
               cmd.Connection.Close();
               return;
           }
           cmd.CommandText = "insert into  gis_camdata_menual A  (site_name_id)" +
                  "values('" + SITE_ID + "')";
           if (cmd.Connection.State == ConnectionState.Closed)
           {
               cmd.Connection.Open();
           }
           cmd.ExecuteNonQuery();
           cmd.Connection.Close();
    }
         public void insertRowCamSiteDetails(string SITE_ID)// 4 digits
         {

             OracleConnection oConn = Connect("aradmin");
             OracleDataAdapter da = new OracleDataAdapter();
             OracleCommand cmd = new OracleCommand();
             cmd.Connection = oConn;
             cmd.CommandText = "select * from  GIS_CAM_SITE_DETAILS_CAM A where site_name_id like" +
                "'%" + SITE_ID + "%'";
             if (cmd.Connection.State == ConnectionState.Closed)
             {
                 cmd.Connection.Open();
             }
             var reader = cmd.ExecuteReader();
             if (reader.Read())
             {
                 cmd.Connection.Close();
                 return;
             }

             cmd.CommandText = "insert into  GIS_CAM_SITE_DETAILS_CAM A  (site_name_id)" +
                    "values('" + SITE_ID + "')";
             if (cmd.Connection.State == ConnectionState.Closed)
             {
                 cmd.Connection.Open();
             }
             cmd.ExecuteNonQuery();
             cmd.Connection.Close();

         }
        public void insertRowCamPramisData(string SITE_ID)// 4 digits
         {
             OracleConnection oConn = Connect("aradmin");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = "select * from  gis_cam_pramis_data_tbl A where site_name_id like" +
                  "'%" + SITE_ID + "%'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
           var reader  =  cmd.ExecuteReader();
           if (reader.Read()) {
               cmd.Connection.Close();
               return; 
           }

            cmd.CommandText = "insert into  gis_cam_pramis_data_tbl A  (site_name_id)" +
                   "values('" + SITE_ID+ "')";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        
        }
        public void insert3Rows(string siteInt)
        {
            OracleConnection oConn = Connect("gisOraConn");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            cmd.CommandText = "insert into  GIS_CAM_CONTACTS_MENUAL_v2 A  (site_id, contact1, txtphonecellular , txtphonecellular2, remarks)" +
                   "values('" + siteInt + "','','','','')";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public void updateGisCamPramis(CamKlaliModel camKlali)
        {
            OracleConnection oConn = Connect("aradmin");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            string site_num = Regex.Match(camKlali.SITE_NAME_ID, @"\d+").Value;
            cmd.CommandText = " UPDATE GIS_CAM_SITE_DETAILS_CAM A SET  HOSTED='" + camKlali.HOSTED + 
                "', A.NEEDS_GENERATOR ='" + camKlali.NEEDS_GENERATOR + "',A.SIM_NUMBER= '" + camKlali.SIM_NUM + "' WHERE A.SITE_NAME_ID like '%" + site_num + "%'";//"UPDATE LOG_SITE_DOWN SET removedMenual='yes' WHERE SITE_ID='" + siteid + "'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }

            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void updateGisCamPramis2(CamKlaliModel camKlali, string concat)
        {
            OracleConnection oConn = Connect("aradmin");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            string site_name_ = Regex.Match(camKlali.SITE_NAME_ID, @"\d+").Value;
            cmd.Connection = oConn;
            string CHRSITEENTRCODE = camKlali.CHRSITEENTRCODE == null ? "" : camKlali.CHRSITEENTRCODE.Replace("'", "");
            string TIME_REMARKS = camKlali.TIME_REMARKS == null ? "" : camKlali.TIME_REMARKS.Replace("'", "");
            cmd.CommandText = " UPDATE gis_cam_pramis_data_tbl A SET A.time_remarks='" + TIME_REMARKS + "', A.TIMEGENFROM  = '" + camKlali.TIMEGENFROM + "' , A.TIMEGENTO = '" + camKlali.TIMEGENTO + "' , A_24_HOURS = '" + camKlali.A_24_HOURS  +
            "', A.CHRSITESENSITIVE ='" + camKlali.CHRSITESENSITIVE + "', A.CHRSITENEEDKEY ='" + camKlali.chrSiteNeedKey
            + "', A.CHRSITENEEDCOORD ='" + camKlali.CHRSITENEEDCOORD + "', A.CHRSITEENTRCODE ='" + CHRSITEENTRCODE + "',A.CHRSITECARRESTR= '" + camKlali.CHRSITECARRESTR + "', A.CHRSITESECURITY= '" 
            + camKlali.CHRSITESECURITY + "', A.cbk= '" + concat + "',a.ROOMRESPONSIBITY='" +  camKlali.roomResponsibity + "' WHERE A.SITE_NAME_ID like '%" + site_name_ + "%'";//"UPDATE LOG_SITE_DOWN SET removedMenual='yes' WHERE SITE_ID='" + siteid + "'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        public void updateModifyDate(CamKlaliModel camKlali)
        {
            OracleConnection oConn = Connect("aradmin");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            string site_name_ = Regex.Match(camKlali.SITE_NAME_ID, @"\d+").Value;
            cmd.Connection = oConn;
            cmd.CommandText = "update  gis_cam_pramis_data_tbl A set A.MODIFIEDDAY = (TO_DATE(sysdate, 'dd/mm/yyyy hh24:mi:ss')) WHERE A.SITE_NAME_ID like '%" + site_name_ + "%'";//"UPDATE LOG_SITE_DOWN SET removedMenual='yes' WHERE SITE_ID='" + siteid + "'";
            if (cmd.Connection.State == ConnectionState.Closed)
            {
                cmd.Connection.Open();
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        // rnc
        public string getRncRacData()
        {
            OracleConnection oConn = Connect("OracleDatabase");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            string rnc = "";
            cmd.CommandText = "select rnc_id from v_orange_sites t where name ='CW0230'";//"UPDATE LOG_SITE_DOWN SET removedMenual='yes' WHERE SITE_ID='" + siteid + "'";
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rnc = readCellFromDb(reader, 0);
                    }
                }
            }
            catch (Exception ex) { writeToLog("getColorByIncidentNumber: failed to execute. command text: " + cmd.CommandText + ex.InnerException); }
            finally
            {
                cmd.Connection.Close();
            }
            return rnc;
        }

        public string getEmptyData()
        {
            //HDBSC_MME_TECH_MANGER_supply_hosted
            OracleConnection oConn = Connect("OracleDatabase");
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oConn;
            string rnc = "";
            cmd.CommandText = "select '' as HDBSC, '' as MME, '' as TECH, '' as MANGER, '' as supply, '' as hosted from dual where name ='CW0230'";//"UPDATE LOG_SITE_DOWN SET removedMenual='yes' WHERE SITE_ID='" + siteid + "'";
            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        rnc = readCellFromDb(reader, 0);
                    }
                }
            }
            catch (Exception ex) { writeToLog("getColorByIncidentNumber: failed to execute. command text: " + cmd.CommandText + ex.InnerException); }
            finally
            {
                cmd.Connection.Close();
            }
            return rnc;
        }
        public static void writeToLog(string logMsg)
        {
            System.IO.File.AppendAllText(@"C:\log_hii_beiar.txt", DateTime.Now + logMsg + Environment.NewLine);
        }
        public string readCellFromDb(OracleDataReader reader, int i)
        {
            string type = reader.GetFieldType(i).ToString();
            switch (type)
            {
                case "System.String":
                    return reader.GetOracleString(i).IsNull ? "" : reader.GetString(i);
                case "System.DateTime":
                    {
                        return reader.GetOracleDate(i).IsNull ? "" : reader.GetDateTime(i).ToString();
                    }
                case "System.Character":
                    {
                        return reader.GetChar(i).ToString();
                    }
                case "System.Decimal":
                    {
                        return reader.GetOracleDecimal(i).IsNull ? "" : System.Convert.ToString(reader.GetDecimal(i));
                    }
                case "System.Int64":
                    {
                        return System.Convert.ToString(reader.GetInt64(i));
                    }

            }
            return "";
        }
         public static List<UserDetails> GetUsers()
        {
                List<UserDetails> activeDirectoryUses = new List<UserDetails>();
                var SampleOU = Authenticate("shir.salhov", "Asdfqwer12", "phi-networks.co.il");
                DirectorySearcher searcher;
                SearchResultCollection results;
                searcher = new DirectorySearcher();
                searcher.Filter = "(&(objectCategory=CN=Person,CN=Schema,CN=Configuration,DC=phi-networks,DC=co,DC=il)(mail=*))";
                searcher.SearchRoot = SampleOU;
                using (searcher)
                {
                    results = searcher.FindAll();
                    string logger = "";
                    int errorCounter = 0;
                    int succeededUsers = 0;
                    foreach (SearchResult item in results)
                    {
                        ResultPropertyCollection myResultPropColl;
                        myResultPropColl = item.Properties;
                        List<string> tempList = new List<string>();
                        foreach (var directUser in myResultPropColl["directreports"])
                        {
                            tempList.Add(directUser.ToString());
                        }
                        tempList = tempList.Select(item1 => item1.Split(',').Where(q => q.Contains("CN=")).FirstOrDefault().Replace("CN=", "")).ToList();
                        try
                        {
                            activeDirectoryUses.Add(new UserDetails()
                            {
                                directreports = tempList,
                            
                                displayname = myResultPropColl["displayname"] != null ? myResultPropColl["displayname"][0].ToString() : "",
                                mobile = item.Properties["mobile"].Count > 0 ? item.Properties["mobile"][0].ToString() : ""
                            });
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        myResultPropColl = item.Properties;
                    }
                }

                // Fetch the file contents.
                return activeDirectoryUses;
        }

        private static DirectoryEntry Authenticate(string userName,
                     string password, string domain)
        {
            DirectoryEntry entry = null;
            bool authentic = false;
            try
            {
                entry = new DirectoryEntry("LDAP://" + domain,
                   userName, password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException e)
            {
                var message = e.Message;
            }
            return entry;
        }
    }
  }

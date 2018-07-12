using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.NewCamModel;

namespace NewCamDataAccess
{
    public interface ICamKlaliDataAccess
    {
      KlaliModel getKlaliData(string doorNumber);

    }
    public class CamKlaliDataAccess : ICamKlaliDataAccess {
      public KlaliModel getKlaliData(string doorNumber) {
            KlaliModel km = new KlaliModel();
            return km;
        
        
        }
    
    }  
}

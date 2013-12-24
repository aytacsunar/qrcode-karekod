using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KareKodDLL
{
    class RenkSinifi
    {
        string renkAdi;
        public string RenkAdi
        {
            get { return renkAdi; }
            set { renkAdi = value; }
        }
        string renkKodu;
        public string RenkKodu
        {
            get { return renkKodu; }
            set { renkKodu = value; }
        }
        public RenkSinifi(string renkadi, string renkkodu)
        {
            RenkAdi = renkadi;
            RenkKodu = renkkodu;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wpfvizsgagyak1
{
    internal class EsemenyKezelo
    {
        List<Esemeny> esemenyek;
        public void LoadFromData(string path)
        {
            esemenyek = File.ReadAllLines(path).Select(x => new Esemeny(x)).ToList();
        }

        public List<String> EbedlokKodjai()
        {
            List<Esemeny> ebedlok = GetEsemenyek("Könyvtár");
            List<String> ebedlokKodjai = new List<String>();
            foreach (var ebedlo in ebedlok)
            {
                ebedlokKodjai.Add(ebedlo.TanuloKod);
            }
            return ebedlokKodjai;
        }

        public List<Esemeny> GetEsemenyek(string esemenyFajta)
        {
            List<Esemeny> esemenyekTipusAlapjan = new List<Esemeny>();
            foreach (var esemeny in esemenyek)
            {
                if (esemeny.EsemenyKod == esemenyFajta)
                {
                    esemenyekTipusAlapjan.Add(esemeny);
                }
            }
            return esemenyekTipusAlapjan;
        }

        // A megadott időintervallumban az iskolában tartózkodók listája
        public List<String> KikVoltakIskolaban(int kezdoOra, int kezdoPerc, int zarOra, int zaroPerc)
        {
            List<Esemeny> iskolaban = new List<Esemeny>();
            foreach (var esemeny in esemenyek)
            {
                string[] idopont = esemeny.EsemenyIdopont.Split(':');
                int ora = int.Parse(idopont[0]);
                int perc = int.Parse(idopont[1]);
                if (ora >= kezdoOra && ora <= zarOra && perc >= kezdoPerc && perc <= zaroPerc)
                {
                    iskolaban.Add(esemeny);
                }
            }
            List<String> iskolabanKodjai = new List<String>();
            foreach (var tanulo in iskolaban)
            {
                iskolabanKodjai.Add(tanulo.TanuloKod);
            }
            return iskolabanKodjai;
        }

    }
}
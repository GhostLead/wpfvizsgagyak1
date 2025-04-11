using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfvizsgagyak1
{
    internal class Esemeny
    {
        string tanuloKod;
        string esemenyIdopont;
        string esemenyKod;

        public Esemeny(string sor)
        {

            string[] tomb = sor.Split();

            this.TanuloKod = tomb[0];
            this.EsemenyIdopont = tomb[1];
            if (int.Parse(tomb[2]) == 1)
            {
                this.esemenyKod = "Belépés";
            }
            if (int.Parse(tomb[2]) == 2)
            {
                this.esemenyKod = "Kilépés";
            }
            if (int.Parse(tomb[2]) == 3)
            {
                this.esemenyKod = "Ebéd";
            }
            if (int.Parse(tomb[2]) == 4)
            {
                this.esemenyKod = "Könyvtár";
            }
        }

        public string TanuloKod { get => tanuloKod; set => tanuloKod = value; }
        public string EsemenyIdopont { get => esemenyIdopont; set => esemenyIdopont = value; }
        public string EsemenyKod { get => esemenyKod; set => esemenyKod = value; }
    }
}
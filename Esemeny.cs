using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaWPFgyak1
{
    internal class Esemeny
    {
        string tanuloKod;
        string esemenyIdopont;
        int esemenyKod;

        public Esemeny(string sor)
        {

            string[] tomb = sor.Split();

            this.TanuloKod = tomb[0];
            this.EsemenyIdopont = tomb[1];
            this.EsemenyKod = int.Parse(tomb[2]);
        }

        public string TanuloKod { get => tanuloKod; set => tanuloKod = value; }
        public string EsemenyIdopont { get => esemenyIdopont; set => esemenyIdopont = value; }
        public int EsemenyKod { get => esemenyKod; set => esemenyKod = value; }
    }
}
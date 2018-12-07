using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LOLBR.classes
{
    public class Champion
    {
        public string name { get; set; }
        public string title { get; set; }
        public string id { get; set; }
        public int key { get; set; }
        public string lore { get; set; }
        public string partype { get; set; }
        public ImageSource Imagem { get; set; }
        public Information info { get; set; }
        public string[] tags { get; set; }
        public Estatos stats { get; set; }
        public Skins[] skins { get; set; }
        public string allytips { get; set; }
        public Spells[] spells { get; set; }
        public Passiva passive { get; set; }

        public class Passiva
        {
            public string name { get; set; }
            public string description { get; set; }
            public ImagemStatus image { get; set; }
            public ImageSource imagem { get; set; }
            public class ImagemStatus
            {
                public string full { get; set; }
                public string sprite { get; set; }
            }
        }
        public class Information
        {
            public int attack { get; set; }
            public int defense { get; set; }
            public int magic { get; set; }
            public int difficulty { get; set; }
        }
        public class Estatos
        {
            public double hp { get; set; }
            public double hpperlevel { get; set; }
            public double mp { get; set; }
            public double mpperlevel { get; set; }
            public double movespeed { get; set; }
            public double armor { get; set; }
            public double armorperlevel { get; set; }
            public double spellblock { get; set; }
            public double spellblockperlevel { get; set; }
            public double attackrange { get; set; }
            public double hpregen { get; set; }
            public double hpregenperlevel { get; set; }
            public double mpregen { get; set; }
            public double mpregenperlevel { get; set; }
            public double crit { get; set; }
            public double critperlevel { get; set; }
            public double attackdamage { get; set; }
            public double attackdamageperlevel { get; set; }
            public double attackspeedoffset { get; set; }
            public double attackspeedperlevel { get; set; }
        }
        public class Skins
        {
            public double id { get; set; }
            public double num { get; set; }
            public string name { get; set; }
            public bool chromas { get; set; }
        }
        public class Spells
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string tooltip { get; set; }
            public LevelTip leveltip { get; set; }
            public int maxrank { get; set; }
            public double[] cooldown { get; set; }
            public string cooldownBurn { get; set; }
            public double[] cost { get; set; }
            public string costBurn { get; set; }
            //public List<string> datavalues { get; set; }
            public List<List<double>> effect { get; set; }
            public List<string> effectBurn { get; set; }
            public List<Vars> vars { get; set; }
            public string costType { get; set; }
            public double maxammo { get; set; }
            public List<double> range { get; set; }
            public string rangeBurn { get; set; }
            public ImagemStatus image { get; set; }
            public string resource { get; set; }
            public ImageSource Imagem { get; set; }
            public class ImagemStatus
            {
                public string full { get; set; }
                public string sprite { get; set; }
            }
            public class LevelTip
            {
                public List<string> Label { get; set; }
                public List<string> effect { get; set; }
            }
            public class Vars
            {
                public string link { get; set; }
                public object coeff { get; set; }
                public string key { get; set; }
            }
        }
        public Champion Copy()
        {
            return (Champion)this.MemberwiseClone();
        }
    }
}

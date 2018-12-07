using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LOLBR.classes
{
    public class Summoner
    {
        public long ID { get; set; }
        public long Accountid { get; set; }
        public long RevisionData { get; set; }
        public long SummonerLevel { get; set; }
        public string Name { get; set; }
        public long ProfileIconId { get; set; }
        public ImageSource Image { get; set; }
        public string Version { get; set; }
    }
}

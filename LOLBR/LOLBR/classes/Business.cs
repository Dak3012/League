using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LOLBR.classes
{
    static class Business
    {
        private static List<Champion> _Champions;
        public static List<Champion> Champions
        {
            get
            {
                var objectArray = new List<Champion>();
                foreach (var i in _Champions)
                {
                    objectArray.Add(i.Copy());
                }
                return (objectArray);
            }
            private set { _Champions = value; }
        }
        static async public Task Start()
        {
            await LoLConnect.Update_version();
            Champions = await LoLConnect.LOL_GetList_Champions();
        }
        static public async Task<Summoner> GetSummonerDados()
        {
            var summoner = await LoLConnect.LOL_GetDados("Dak3012");
            return summoner;
        }
        public static async Task<List<Champion>> Get_Free_to_PlayAsync()
        {
            var champions = await LoLConnect.Get_ChampionsFreeToplayAsync();
            return champions;
        }
        public static async Task<Champion> ChampionGetDados(Champion Champion)
        {
            var Champ = await LoLConnect.GetChampionDados(Champion);
            return Champ;
        }
        public static ImageSource ImageBackGround(Champion champion)
        {
            var ImageSourceBackground = LoLConnect.BackGroundImageSkin(champion);
            return ImageSourceBackground;
        }
    }
}

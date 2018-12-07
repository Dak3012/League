using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LOLBR.classes
{
    public static class LoLConnect
    {
        private static readonly string uriddragon = "https://ddragon.leagueoflegends.com/cdn/";
        private static readonly string API_Key = "RGAPI-7e9469bc-e248-4ad5-84e2-c5db6c952c92"; //teste api
        private static List<Champion> Champions;
        private static Realms RealmVersion;
        public static async Task Update_version()
        {
            string url = "https://ddragon.leagueoflegends.com/realms/br.json";
            var request = await DataService.GetDataFromServerAsync(url);
            RealmVersion = request["n"].ToObject<Realms>();
        }
        public async static Task<List<Champion>> LOL_GetList_Champions()
        {
            string ImageURl = uriddragon + RealmVersion.champion + "/img/champion/";
            string url = uriddragon + RealmVersion.champion + "/data/pt_BR/champion.json";
            var request = await DataService.GetDataFromServerAsync(url);
            var champJobject = request["data"].ToList();
            Champions = champJobject.Select(p => p.Children().ToList()[0].ToObject<Champion>()).OrderBy(p => p.name).ToList();
            foreach (var i in Champions)
            {
                var uri = new Uri(ImageURl + i.id + ".png");
                var image = new Image();
                i.Imagem = new UriImageSource()
                {
                    Uri = uri,
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1, 0, 0, 0)
                };
            }
            return Champions;
        }
        public static ImageSource BackGroundImageSkin(Champion champion)
        {
            string url = uriddragon + "img/champion/loading/" + champion.id + "_" + champion.skins[0].num + ".jpg";
            var imagesource = ImageSource.FromUri(new Uri(url));
            return imagesource;
        }
        public static async Task<Champion> GetChampionDados(Champion champion)
        {
            string uri = uriddragon + RealmVersion.champion + "/data/pt_BR/champion/" + champion.id + ".json";
            string uriImagem = uriddragon + RealmVersion.champion + "/img/spell/";
            string uriPassive = uriddragon + RealmVersion.champion + "/img/passive/";
            var request = await DataService.GetDataFromServerAsync(uri);
            champion.skins = request["data"][champion.id]["skins"].ToObject<Champion.Skins[]>();
            champion.lore = request["data"][champion.id]["lore"].ToObject<string>();
            champion.spells = request["data"][champion.id]["spells"].ToObject<Champion.Spells[]>();
            champion.passive = request["data"][champion.id]["passive"].ToObject<Champion.Passiva>();
            foreach (var i in champion.spells)
            {
                champion.spells.First(p => p == i).Imagem = new UriImageSource()
                {
                    Uri = new Uri(uriImagem + i.image.full),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1, 0, 0, 0)
                };
            }
            champion.passive.imagem = new UriImageSource()
            {
                Uri = new Uri(uriPassive + champion.passive.image.full),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0)
            };
            return champion;
        }
        public static async Task<Summoner> LOL_GetDados(string usuario)
        {
            string user = "Dak3012";
            string url = "https://br1.api.riotgames.com/lol/summoner/v3/summoners/by-name/" + user + "?api_key=" + API_Key;
            var request = await DataService.GetDataFromServerAsync(url);
            var summoner = request.ToObject<Summoner>();
            var uri = new Uri("http://ddragon.leagueoflegends.com/cdn/" + RealmVersion.profileicon + "/img/profileicon/" + summoner.ProfileIconId + ".png");
            summoner.Image = new UriImageSource()
            {
                Uri = uri,
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0)
            };
            return summoner;
        }
        public static async Task<List<Champion>> Get_ChampionsFreeToplayAsync()
        {
            string url = "https://br1.api.riotgames.com/lol/platform/v3/champion-rotations?api_key=" + API_Key;
            var request = await DataService.GetDataFromServerAsync(url);
            var Campeoes = request["freeChampionIds"].ToObject<int[]>().ToList();
            var Lista_FreeToPlay = Champions.Where(p => (Campeoes.Exists(a => a.ToString() == p.key.ToString()))).ToList();
            return Lista_FreeToPlay;
        }

    }
}
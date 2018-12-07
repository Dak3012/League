using LOLBR.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LOLBR
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Start();
        }

        private async void Start()
        {
            await Business.Start();
            InitializeComponent();
        }
        private async void ButtonFreetoPlay_Clicked(object sender, EventArgs e)
        {
            var champ = (await Business.Get_Free_to_PlayAsync()).OrderBy(p => p.name).ToList();
            await Navigation.PushAsync(new Page.ChampionsPage(champ));
        }

        private async void Button_ChampionList_Clicked(object sender, EventArgs e)
        {
            var champ = Business.Champions.OrderBy(p => p.name);
            await Navigation.PushAsync(new Page.ChampionsPage(champ.ToList()));
        }

        private async void Button_Summoner_Clicked(object sender, EventArgs e)
        {
            var summoner = await Business.GetSummonerDados();
            await Navigation.PushAsync(new Page.SummonerPage());
        }
    }
}

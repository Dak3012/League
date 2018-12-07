using LOLBR.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LOLBR.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionsPage : ContentPage
    {
        public ObservableCollection<Champion> Champ { get; set; }
        public ChampionsPage(List<Champion> Champs)
        {
            InitializeComponent();
            Start(Champs);
        }
        public void Start(List<Champion> Champions)
        {
            Champ = new ObservableCollection<Champion>();

            foreach (var p in Champions)
            {
                Champ.Add(p);
            }
            Champions_List.ItemsSource = Champ;
        }
        private void Busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            var champions = Business.Champions.Where(p => p.name.ToUpper().StartsWith(Busca.Text.ToUpper())).ToList();
            var championsremove = Champ.Except(champions).ToList();
            foreach (var p in championsremove)
            {
                Champ.Remove(p);
            }
            var championsADD = champions.Except(Champ);
            foreach (var p in championsADD)
            {
                Champ.Add(p);
            }
            var campOrder = Champ.OrderBy(p => p.name).ToList();
            foreach (var p in campOrder)
            {
                Champ.Move(Champ.IndexOf(Champ.First(a => a.name == p.name)), campOrder.IndexOf(campOrder.First(a => a.name == p.name)));
            }
        }
        private async void Champions_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem as Champion != null)
            {
                await Navigation.PushAsync(new ChampionStatePage(e.SelectedItem as Champion));
                Champions_List.SelectedItem = 0;
            }
        }
    }
}
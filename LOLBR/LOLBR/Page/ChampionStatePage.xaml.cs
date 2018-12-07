using System;
using System.Linq;
using System.Text.RegularExpressions;
using LOLBR.classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LOLBR.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChampionStatePage : ContentPage
    {
        private Champion champion;

        public ChampionStatePage(Champion Champion)
        {
            InitializeComponent();
            ChampionDataAsync(Champion);
        }

        public object Image_BackGround { get; private set; }

        private async void ChampionDataAsync(Champion _champion)
        {
            var champ = await Business.ChampionGetDados(_champion);
            var image = Business.ImageBackGround(_champion);
            Label_Name.Text = champ.name;
            Label_Title.Text = champ.title;
            Label_lore.Text = Regex.Replace(champ.lore, "<[^>]*>", "");
            Label_Partype.Text = champ.partype;
            DefineStatus(champ);
            DefineSpells(champ);
        }
        private void DefineSpells(Champion champ)
        {
            Image_passive.Source = champ.passive.imagem;
            var item = Image_passive.Width;
            description_passive.Text = Regex.Replace(champ.passive.description, "<[^>]*>", "");
            foreach (var i in champ.spells)
            {
                var frameBackground = new Frame();
                frameBackground.Style = (Style)Application.Current.Resources["Frame_fundoOpacoPage"];
                var gridFrameBackground = new Grid();
                gridFrameBackground.Children.Add(frameBackground);
                var Childre_StackLayoult = new Grid();
                var Gridspell = new GridSpells(i);
                Childre_StackLayoult.RaiseChild(Gridspell);
                Childre_StackLayoult.Children.Add(gridFrameBackground);
                Childre_StackLayoult.Children.Add(Gridspell);
                Habiliti_Layoult.Children.Add(Childre_StackLayoult);
            }
        }
        private void DefineStatus(Champion champ)
        {
            Label_attackDamage.Text = "Dano de Ataque = " + FormatString(Math.Round(champ.stats.attackdamage, 2), Math.Round(champ.stats.attackdamageperlevel, 2), null);
            Label_moveSpeed.Text = "Velocidade de movimento = " + champ.stats.movespeed.ToString();
            Label_attackSpeed.Text = "Velocidade de ataque = " + Math.Round(0.625 / (1 + champ.stats.attackspeedoffset), 3).ToString() + " por segundo\n" + champ.stats.attackspeedperlevel + "% por level";
            Label_armor.Text = "Armadura = " + FormatString(Math.Round(champ.stats.armor, 2), Math.Round(champ.stats.armorperlevel, 2), null);
            Label_hp.Text = "HP = " + FormatString(champ.stats.hp, champ.stats.hpperlevel, null);
            if (champ.partype == "Mana")
            {
                Label_mpregen.Text = "Regeneração de " + champ.partype + " = " + FormatString(Math.Round(champ.stats.mpregen, 2), Math.Round(champ.stats.mpregenperlevel, 2), null);
                Label_mp.Text = champ.partype + " = " + FormatString(Math.Round(champ.stats.mp, 2), Math.Round(champ.stats.mpperlevel, 2), null);
            }
            else
            {
                Label_mp.IsVisible = false;
                Label_mp.IsEnabled = false;
                Label_mpregen.IsVisible = false;
                Label_mpregen.IsEnabled = false;
            }
            Label_hpregen.Text = "Regeneração de HP = " + FormatString(Math.Round(champ.stats.hpregen, 2), Math.Round(champ.stats.hpregenperlevel, 2), null);
            Label_mpResist.Text = "Resistência magica = " + FormatString(Math.Round(champ.stats.spellblock, 2), Math.Round(champ.stats.spellblockperlevel, 2), null);
            Label_crit.Text = "Dano Critico = " + FormatString(champ.stats.crit, champ.stats.critperlevel, null);

        }
        private string FormatString(double inicial, double perlevel, int? level)
        {
            string StringRetorno;
            if (level == null)
                StringRetorno = inicial.ToString() + "(+ " + perlevel + " por level) \n" + inicial.ToString() + " - " + (inicial + 17 * perlevel).ToString();
            else
                StringRetorno = (inicial + (level - 1) * perlevel).ToString() + " lvl:" + level.ToString();
            return StringRetorno;
        }
        private class GridSpells : Grid
        {
            public GridSpells(Champion.Spells Spells)
            {
                Margin = new Thickness(10);
                var Str = Application.Current.Resources["Tamanho"];
                var resource = Convert.ToInt16(Str);
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition{Width=resource}
                };
                Children.Add(new Image
                {
                    Source = Spells.Imagem,
                    Aspect = Aspect.AspectFit,
                    WidthRequest = resource,
                    HeightRequest = resource,
                    MinimumWidthRequest = resource,
                    MinimumHeightRequest = resource,
                });
                var stack = new StackLayout();
                Children.Add(stack);
                SetColumn(Children[0], 0);
                SetColumn(Children[1], 1);
                stack.Children.Add(new Label { Text = Regex.Replace(Spells.description, "<[^>]*>", "") });
                var tooltip = GetValueTooltip(Spells);
                stack.Children.Add(new Label { Text = "Descrição = " + tooltip });
                stack.Children.Add(new Label { Text = "Custo de conjuração = " + Spells.costBurn });
                stack.Children.Add(new Label { Text = "Tempo de recarga = " + Spells.cooldownBurn });
                stack.Children.Add(new Label { Text = "Alcance = " + Spells.rangeBurn });
            }
            private string GetValueTooltip(Champion.Spells spells)
            {
                var strinTooltip = Regex.Replace(spells.tooltip, "<[^>]*>", "");
                var strinRetorno = strinTooltip;
                var VarsMatches = Regex.Matches(strinTooltip, "{{[^}]*}}");
                foreach (Match match in VarsMatches) //tentar descobrir o que é {{f4.0}} e {{f1.0}}
                {
                    if (Regex.Match(match.Value, "e" + @"\d").Success)
                    {
                        var valueMatch = Regex.Match(match.Value, @"\d+").Value;
                        var number = int.Parse(valueMatch);
                        if (Regex.Match(strinRetorno, match.Value).Success)
                        {
                            if (spells.effectBurn[number] != null)
                                strinRetorno = Regex.Replace(strinRetorno, match.Value, spells.effectBurn[number]);
                            else
                                strinRetorno = Regex.Replace(strinRetorno, match.Value, "[error]");
                        }
                    }
                    else if (Regex.Match(match.Value, "a" + @"\d").Success)
                    {
                        var valueMatch = Regex.Match(match.Value, @"\d+").Value;
                        var key = "a" + valueMatch;
                        if (Regex.Match(strinRetorno, match.Value).Success)
                        {
                            var variavel = spells.vars.First(p => p.key == key).coeff as double?;
                            if (variavel != null)
                                strinRetorno = Regex.Replace(strinRetorno, match.Value, (variavel * 100).ToString() + "%");
                        }
                    }
                }
                return strinRetorno;
            }
        }
    }
}
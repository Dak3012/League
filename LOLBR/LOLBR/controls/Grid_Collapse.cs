using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LOLBR.controls
{
    public class Grid_Collapse : Grid
    {
        private Button ButtonCollapse { get; set; }
        public bool ItemCollapsed { get; set; } = true;
        public string Text
        {
            get => ButtonCollapse.Text;
            set => ButtonCollapse.Text = value;
        }
        public Grid_Collapse()
        {
            this.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition{Height= new GridLength(1, GridUnitType.Auto) }
            };
            ButtonCollapse = new Button();
            this.Children.Add(ButtonCollapse);
            SetRow(Children[0], 0);
            ChildAdded += Frame_Collapse_ChildAdded;
            ButtonCollapse.Text = "ButtonCollapse";
            ButtonCollapse.Clicked += ButtonCollapse_Clicked;
            CollapseChange();
        }

        private void ButtonCollapse_Clicked(object sender, EventArgs e)
        {
            if (ItemCollapsed)
                ItemCollapsed = false;
            else
                ItemCollapsed = true;
            CollapseChange();
        }
        private void CollapseChange()
        {
            if (ItemCollapsed)
            {
                var listOfChild = Children.Where(p => p != Children[0]);
                foreach (var i in listOfChild)
                {
                    i.IsVisible = false;
                }
            }
            else
            {
                var listOfChild = Children.Where(p => p != Children[0]);
                foreach (var i in listOfChild)
                {
                    i.IsVisible = true;
                }
            }

        }
        private void Frame_Collapse_ChildAdded(object sender, ElementEventArgs e)
        {
            var indexof = Children.First(p => p == e.Element);
            SetRow(indexof, 1);
            CollapseChange();
        }
    }
}

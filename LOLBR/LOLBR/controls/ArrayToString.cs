using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LOLBR.controls
{
    class arraytostring : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringArray = ((string[])value).ToList();
            string SArray = "";
            foreach (var i in stringArray)
            {
                SArray += i;
                if (stringArray.Last() != i)
                    SArray += ", ";
            }
            return SArray;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
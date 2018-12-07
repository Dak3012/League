using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LOLBR.controls
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        // You exclude the 'Extension' suffix when using in Xaml markup
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}

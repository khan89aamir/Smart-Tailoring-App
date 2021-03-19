using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Smart_Tailoring_Solution_App
{
    public sealed class StandardPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
         BindableProperty.Create(nameof(Image), typeof(string), typeof(StandardPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

      
    }
}

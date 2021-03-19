using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smart_Tailoring_Solution_App;
using Smart_Tailoring_Solution_App.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]
namespace Smart_Tailoring_Solution_App.Droid
{
    public class StandardEntryRenderer : EntryRenderer
    {
        public StandardEntryRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
        }


    }
}
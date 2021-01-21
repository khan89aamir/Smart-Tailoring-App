using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            _ObjMonkey = new ObservableCollection<Monkey>();
            
             
        }
        private void LoadStyle()
        {
            _ObjMonkey.Add(new Monkey
            {
                ID = 1,
                Name = "Color Style 1",
                Location = "SKU 1009",
                //   ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                ImageUrl = "style1.jpeg",
                QTY = "0"


            }); ;
            _ObjMonkey.Add(new Monkey
            {
                ID = 1,
                Name = "Color Style 1",
                Location = "SKU 1004",
                //   ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                ImageUrl = "style2.jpeg",
                QTY = "0"


            }); ;

            _ObjMonkey.Add(new Monkey
            {
                ID = 2,
                Name = "Color Style 2",
                Location = "SKU 1005",
                //  ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                ImageUrl = "style3.jpeg",
                QTY = "0"
            });

            _ObjMonkey.Add(new Monkey
            {
                ID = 3,
                Name = "Color Style 3",
                Location = "SKU 1004",
                //ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                ImageUrl = "style4.jpeg",
                QTY = "0"
            });



            lstMonkey.ItemsSource = _ObjMonkey;
        }
        ObservableCollection<Monkey> _ObjMonkey;
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            //foreach (View item in pnlcartProducts.Children)
            //{
            //    if (item.GetType() == typeof(Frame))
            //    {
            //        Frame E = (Frame)item;
            //        if (E.BackgroundColor==Color.LightGreen)
            //        {

            //        }
            //        else
            //        {
            //            E.BackgroundColor = Color.Transparent;
            //        }
                   

            //    }
            //}

            //Frame frame = (Frame)sender;
            //frame.BackgroundColor = Color.LightGray;

        }

        private void btnAddStyle_Clicked(object sender, EventArgs e)
        {
            pnlMeasurment.IsVisible = false;
          
            pnlStyle.IsVisible = true;

            LoadStyle();

        }

        private void btnAddProperties_Clicked(object sender, EventArgs e)
        {
            pnlMeasurment.IsVisible = false;
         
            pnlStyle.IsVisible = false;
        }

        private void btnAddMeasurment_Clicked(object sender, EventArgs e)
        {
            pnlMeasurment.IsVisible = true;
         
            pnlStyle.IsVisible = false;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
         

           
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void btnDone_Clicked(object sender, EventArgs e)
        {
            pnlMeasurment.IsVisible = false;
            pnlStyle.IsVisible = false;

        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
           

        }
    }
}
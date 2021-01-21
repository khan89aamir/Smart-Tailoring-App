using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Smart_Tailoring_Solution_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Measurement : ContentPage
    {
        ObservableCollection<Monkey> _ObjMonkey;
      
        public Measurement()
        {
            InitializeComponent();
            FlowListView.Init();
            _ObjMonkey = new ObservableCollection<Monkey>();


            _ObjMonkey.Add(new Monkey
            {
                ID = 1,
                Name = "Jaket",
                Location = "SKU-1001",
                //   ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                ImageUrl = "jacket.png",
                QTY = "0"

                


            }); ;

            _ObjMonkey.Add(new Monkey
            {
                ID = 2,
                Name = "Kurta",
                Location = "SKU-1002",
                //  ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
                ImageUrl = "Kurat.png",
                  QTY = "0"
            });

            _ObjMonkey.Add(new Monkey
            {
                ID = 3,
                Name = "T-Shirt",
                Location = "SKU-1003",
                //ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
                ImageUrl = "Tshirt.png",
                  QTY = "0"
            });

            _ObjMonkey.Add(new Monkey
            {
                ID = 4,
                Name = "shirt ",
                Location = "SKU-1004",
                // ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
                ImageUrl = "shirt.png",
                QTY = "0"
            });
            _ObjMonkey.Add(new Monkey
            {
                ID = 4,
                Name = "Trousers  ",
                Location = "SKU-1005",
                // ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
                ImageUrl = "Trouser.png",
                QTY = "0"
            });

            lstMonkey.ItemsSource = _ObjMonkey;


        }
      

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Test.Page1());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainPage();
            return true;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }


        private void Data()
        {
        
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ////thats all you need to make a search  
            IList<Monkey> data = _ObjMonkey.Where(p =>
                       p.Name != null &&
                       p.Name.StartsWith(e.NewTextValue) 
                       ).ToList();

            int count = data.Count;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {

              

                lstMonkey.ItemsSource = _ObjMonkey;
            }

            else
            {
                ObservableCollection<Monkey> myCollection = new ObservableCollection<Monkey>(data);

                lstMonkey.ItemsSource = myCollection;
            }
        }
      
        private void TextCell_Tapped(object sender, EventArgs e)
        {
            
           
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Monkey selectedItem = e.SelectedItem as Monkey;
        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
           
         


        }
        ViewCell lastCell;
        private  void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#E8E8E8");
                lastCell = viewCell;
            }

          

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            lstMonkey.IsRefreshing = true;

            Button button = (Button)sender;

            // this will give you all info 
             Monkey _monkey= (Monkey)button.BindingContext;

            string result = await DisplayPromptAsync("Enter QTY", "Enter Selected Product QTY : ", initialValue: _monkey.QTY, maxLength: 3, keyboard: Keyboard.Numeric);
            _monkey.QTY = result;

            lstMonkey.ItemsSource = null;
            lstMonkey.ItemsSource = _ObjMonkey;
            lstMonkey.IsRefreshing = false;
            return;
        }
        
    }

    public class Monkey
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

     

        public string QTY { get; set; }
     

    }
}
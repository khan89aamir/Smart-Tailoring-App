using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Smart_Tailoring_Solution_App.MenuItems;

namespace Smart_Tailoring_Solution_App.View
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public MainPage()
        {
            InitializeComponent();
           
            menuList = new List<MasterPageItem>();

            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(HomePage) });
            menuList.Add(new MasterPageItem() { Title = "Order", Icon = "order.png", TargetType = typeof(Customer) });
            menuList.Add(new MasterPageItem() { Title = "Setting", Icon = "setting.png", TargetType = typeof(SettingPage) });
            menuList.Add(new MasterPageItem() { Title = "Contact US", Icon = "contactus.png", TargetType = typeof(HomePage) });
            menuList.Add(new MasterPageItem() { Title = "Help", Icon = "help.png", TargetType = typeof(HomePage) });
            menuList.Add(new MasterPageItem() { Title = "Logs", Icon = "logicon.png", TargetType = typeof(View.frmLogs) });
            menuList.Add(new MasterPageItem() { Title = "LogOut", Icon = "logout.png", TargetType = typeof(View.frmLogin) });

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
        ViewCell lastCell;
        private void ViewCell_Tapped(object sender, System.EventArgs e)
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
    }
}

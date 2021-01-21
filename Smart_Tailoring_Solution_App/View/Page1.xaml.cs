using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.Model;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
    
        public Page1()
        {
            InitializeComponent();

       

      
      
        }
     
        private async void Button_Clicked(object sender, EventArgs e)
        {
          
          
        }

        private void btnNormal_Clicked(object sender, EventArgs e)
        {
            Utility.ShowMessageBox("Setting Option is selected from Home Message.", Utility.MessageType.Success, this);
            
        }

        private void btnNormalIfo_Clicked(object sender, EventArgs e)
        {
            Utility.ShowMessageBox("Setting Option is selected from Home Message.", this);
        }

        private async void btnQuestion_Clicked(object sender, EventArgs e)
        {
           var result=  await Utility.ShowQuestionMessageBox("Are you sure, you want to go ?", this);
        }
    }
}
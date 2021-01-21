using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Smart_Tailoring_Solution_App.ViewModel;

namespace Smart_Tailoring_Solution_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class frmMessageBox : ContentPage
    {
        MessageViewModel vm_message = new MessageViewModel();

        private TaskCompletionSource<bool> tcs { get; set; }

        // Use this to wait on the page to be finished with/closed/dismissed
        public Task PageClosedTask { get { return tcs.Task; } }
        public frmMessageBox(MessageViewModel viewModel)
        {
            InitializeComponent();
            
            tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();

            vm_message = viewModel;

            BindingContext = vm_message;
            // show the model based on the model type.
            if (vm_message.DispalyModelType==MessageViewModel.ModelType.NormalModel)
            {
                NormalModel.IsVisible = true;
                QuestionModel.IsVisible = false;
            }
            else if (vm_message.DispalyModelType == MessageViewModel.ModelType.QuestionModel)
            {
                NormalModel.IsVisible = false;
                QuestionModel.IsVisible = true;
            }

        }
        public string strImageName { get; set; }
        public string strMessage { get; set; }
        public string strTitle { get; set; }

        public bool Result { get; set; }

        private async void Button_Clicked(object sender, EventArgs e)
        {
          
            await Navigation.PopModalAsync();


        }

        private void btnYes_Clicked(object sender, EventArgs e)
        {
            vm_message.ModelResult = true;
            Navigation.PopModalAsync(true);
        }

        private void btnNo_Clicked(object sender, EventArgs e)
        {
            vm_message.ModelResult = false;
            Navigation.PopModalAsync(true);
        }
        // Either override OnDisappearing 
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            tcs.SetResult(true);
        }
    }
}
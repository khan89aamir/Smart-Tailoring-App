using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Smart_Tailoring_Solution_App.View;

namespace Smart_Tailoring_Solution_App.Model
{
    public class Utility
    {
        public static int MasterGarmentID = 0;
        public static int GarmentID = 0;
        public static string GarmentName = "";
        public static int StyleID = 0;

        private static object _Lock = new object();
        public const string strMessageTitle = "Smart Tailoring Solution";
        public static void WriteLog(string strLog)
        {
            lock (_Lock)
            {
                var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Logs.txt");
                using (StreamWriter streamWriter = new StreamWriter(filename, true))
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    stringBuilder.AppendLine(strLog);


                    streamWriter.WriteLine(stringBuilder.ToString());

                }
            }

        }
        public static string ReadLogs()
        {
            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Logs.txt");
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                return sr.ReadToEnd();
            }

            return "No File to read";
        }
        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }
        public static string GetSize(Int64 value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
        }
        public static string GetLogFileSize()
        {
            string filesize = "";
            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Logs.txt");
            if (File.Exists(filename))
            {

                FileInfo fileInfo = new FileInfo(filename);
                filesize = GetSize(fileInfo.Length, SizeUnits.MB);


            }

            return filesize;

        }

        public enum MessageType { Success, Error };
        public static async void ShowMessageBox(string strMessage, MessageType messageType, ContentPage contentPage)
        {

            ViewModel.MessageViewModel messageBoxViewModel = new ViewModel.MessageViewModel();
            messageBoxViewModel.MessageText = strMessage;
            messageBoxViewModel.DispalyModelType = ViewModel.MessageViewModel.ModelType.NormalModel;
            messageBoxViewModel.MessageTitle = strMessageTitle;
            if (messageType == MessageType.Success)
            {
                messageBoxViewModel.MessageIcon = "check";
            }
            else if (messageType == MessageType.Error)
            {
                messageBoxViewModel.MessageIcon = "close";
            }

            await contentPage.Navigation.PushModalAsync(new View.frmMessageBox(messageBoxViewModel));
        }
        public static async void ShowMessageBox(string strMessage, ContentPage contentPage)
        {

            ViewModel.MessageViewModel messageBoxViewModel = new ViewModel.MessageViewModel();
            messageBoxViewModel.MessageText = strMessage;
            messageBoxViewModel.MessageTitle = strMessageTitle;
            messageBoxViewModel.DispalyModelType = ViewModel.MessageViewModel.ModelType.NormalModel;
            messageBoxViewModel.MessageIcon = "";
            await contentPage.Navigation.PushModalAsync(new View.frmMessageBox(messageBoxViewModel));
        }
        public static async Task<bool> ShowQuestionMessageBox(string strMessage, ContentPage contentPage)
        {

            ViewModel.MessageViewModel messageBoxViewModel = new ViewModel.MessageViewModel();
            messageBoxViewModel.DispalyModelType = ViewModel.MessageViewModel.ModelType.QuestionModel;
            messageBoxViewModel.MessageText = strMessage;
            messageBoxViewModel.MessageTitle = strMessageTitle;
            messageBoxViewModel.MessageIcon = "question";


            View.frmMessageBox frmMessageBox = new frmMessageBox(messageBoxViewModel);

            await contentPage.Navigation.PushModalAsync(frmMessageBox);
            await frmMessageBox.PageClosedTask; // Wait here until the SettingsPage is dismissed

            bool result = messageBoxViewModel.ModelResult;
            return result;
        }

        public static async Task<bool> ShowQuestionMessageBox(string strMessage, MasterDetailPage masterPage)
        {
            ViewModel.MessageViewModel messageBoxViewModel = new ViewModel.MessageViewModel();
            messageBoxViewModel.DispalyModelType = ViewModel.MessageViewModel.ModelType.QuestionModel;
            messageBoxViewModel.MessageText = strMessage;
            messageBoxViewModel.MessageTitle = strMessageTitle;
            messageBoxViewModel.MessageIcon = "question";

            View.frmMessageBox frmMessageBox = new frmMessageBox(messageBoxViewModel);

            await masterPage.Navigation.PushModalAsync(frmMessageBox);
            await frmMessageBox.PageClosedTask; // Wait here until the SettingsPage is dismissed

            bool result = messageBoxViewModel.ModelResult;
            return result;
        }

        public static bool DeleteLogFile()
        {
            bool result = false;
            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Logs.txt");
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                    result = true;
                }
                catch (Exception)
                {
                    result = false;

                }

            }
            return result;
        }

    }
}

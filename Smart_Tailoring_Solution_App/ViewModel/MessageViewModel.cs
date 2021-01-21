using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Tailoring_Solution_App.ViewModel
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private string _strMessageText;
        private string _strTitle;
        private string _strIcon;
        public string MessageText
        {
            get
            {
                return _strMessageText;
            }
            set
            {
                if (_strMessageText != value)
                {
                    _strMessageText = value;
                    OnPropertyChanged();
                }
            }
        }
        public Task<string> GetValue()
        {
            return tcs.Task;
        }
        public string MessageTitle
        {
            get
            {
                return _strTitle;
            }
            set
            {
                if (_strTitle != value)
                {
                    _strTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MessageIcon
        {
            get
            {
                return _strIcon;
            }
            set
            {
                if (_strIcon != value)
                {
                    _strIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public enum ModelType { NormalModel, QuestionModel }

        public ModelType DispalyModelType { get; set; }
        public bool ModelResult { get; set; }
    }
}

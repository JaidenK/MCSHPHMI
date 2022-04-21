using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MCSHPHMI.Controls
{
    /// <summary>
    /// Interaction logic for BooleanIndicator.xaml
    /// </summary>
    public partial class BooleanIndicator : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool _state = false;

        public bool State
        {
            get { return _state; }
            set { _state = value; OnPropertyChanged(); }
        }

        private string _label = "LABEL";
        private string _onText = "ON";
        private string _offText = "OFF";

        public string ProcessLabel
        {
            get { return _label; }
            set { _label = value; OnPropertyChanged(); }
        }

        public string OnText
        {
            get { return _onText; }
            set { _onText = value; }
        }

        public string OffText
        {
            get { return _offText; }
            set { _offText = value; }
        }
        
        public BooleanIndicator(string OnText, string OffText) : this()
        {
            this.OnText = OnText;
            this.OffText = OffText;
        }

        public BooleanIndicator()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}

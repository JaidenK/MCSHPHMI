using System;
using static MCSHPHMI.Core.Globals;
namespace MCSHPHMI.Core
{
    public class ProcessVariable : ObservableObject
    {
        private string _ID = "ID";
        private string _shortDesc = "Desc.";
        private string _Units = "Units";
        private string _Description = "Description";
        private double _Value = 0.0;
        private double _minScale = -10;
        private double _maxScale = 10;
        private double _minAlarm = -8;
        private double _maxAlarm = 8;
        private double _minIdeal = -3;
        private double _maxIdeal = 3;
        private double _minTrend = 0;
        private double _maxTrend = 0;

        public string ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged();

            }
        }
        
        public string ShortDesc
        {
            get { return _shortDesc; }
            set
            {
                _shortDesc = value;
                OnPropertyChanged();
            }
        }
        
        public string Units
        {
            get { return _Units; }
            set
            {
                _Units = value;
                OnPropertyChanged();
            }
        }
        
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }
        
        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged();
            }
        }
        
        public double minScale
        {
            get { return _minScale; }
            set { _minScale = value; OnPropertyChanged(); }
        }
        
        public double maxScale
        {
            get { return _maxScale; }
            set { _maxScale = value; OnPropertyChanged(); }
        }
        
        public double minAlarm
        {
            get { return _minAlarm; }
            set { _minAlarm = value; OnPropertyChanged(); }
        }
        
        public double maxAlarm
        {
            get { return _maxAlarm; }
            set { _maxAlarm = value; OnPropertyChanged(); }
        }
        
        public double minIdeal
        {
            get { return _minIdeal; }
            set { _minIdeal = value; OnPropertyChanged(); }
        }
        
        public double maxIdeal
        {
            get { return _maxIdeal; }
            set { _maxIdeal = value; OnPropertyChanged(); }
        }
        
        public double minTrend
        {
            get { return _minTrend; }
            set { _minTrend = value; OnPropertyChanged(); }
        }
        
        public double maxTrend
        {
            get { return _maxTrend; }
            set { _maxTrend = value; OnPropertyChanged(); }
        }

        public int packetIndex { get; set; }

        public ProcessVariable(string ID, string Nomen, string Units, string Description, bool excludeFromList = false)
        {
            this.ID = ID;
            this.ShortDesc = Nomen;
            this.Units = Units;
            this.Description = Description;
            this.Value = 0;
            
            // This flag is used in places that generate the default placeholder objects
            if(!excludeFromList)
                AllProcessVariables.Add(this);
        }

        public ProcessVariable SetRanges(double minScale, double maxScale, double minAlarm, double maxAlarm, double minIdeal, double maxIdeal)
        {
            this.minScale = minScale;
            this.maxScale = maxScale;
            this.minAlarm = minAlarm;
            this.maxAlarm = maxAlarm;
            this.minIdeal = minIdeal;
            this.maxIdeal = maxIdeal;
            return this;
        }

        private static ProcessVariable _nullProcess;
        public static ProcessVariable NullProcess
        {
            get
            {
                return _nullProcess ?? (_nullProcess = new ProcessVariable("NULL","NULL","NULL","NULL",true).SetRanges(-10,10,-7,7,-3,3));
            }
        }
               
    }

}

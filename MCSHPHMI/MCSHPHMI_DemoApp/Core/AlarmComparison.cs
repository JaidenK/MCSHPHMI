using System;

namespace MCSHPHMI_DemoApp.Core
{
    public class AlarmComparison
    {
        public Func<double, double, bool> compare { get; private set; } = (x, y) => false;
        private string label;

        public static AlarmComparison NullComparison = new AlarmComparison((x, y) => false, "NULL COMPARISON");
        public static AlarmComparison LessThan = new AlarmComparison((x, y) => x < y, "Less Than");
        public static AlarmComparison GreaterThan = new AlarmComparison((x, y) => x > y, "Greater Than");
        public static AlarmComparison MinRange = new AlarmComparison((x, y) => x < y, "Below Mininum Range");
        public static AlarmComparison MaxRange = new AlarmComparison((x, y) => x > y, "Above Max Range");

        public AlarmComparison(Func<double, double, bool> comparison, string label)
        {
            compare = comparison;
            this.label = label;
        }

        new public string ToString()
        {
            return label;
        }
    }
}

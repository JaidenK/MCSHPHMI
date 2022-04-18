using MCSHPHMI_DemoApp.Controls;
namespace MCSHPHMI_DemoApp.Core
{
    public interface IAlarmable
    {
        Alarm Alarm { get; set; }
        void AlarmStateChanged(Alarm alarm);
    }
}

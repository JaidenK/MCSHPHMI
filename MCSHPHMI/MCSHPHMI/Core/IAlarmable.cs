using MCSHPHMI.Controls;
namespace MCSHPHMI.Core
{
    public interface IAlarmable
    {
        Alarm Alarm { get; set; }
        void AlarmStateChanged(Alarm alarm);
    }
}

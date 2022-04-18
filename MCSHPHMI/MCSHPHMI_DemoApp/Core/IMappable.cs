namespace MCSHPHMI_DemoApp.Core
{
    /// <summary>
    /// Mappables must add themselves to MappableUserControls
    /// in their constructor.
    /// </summary>
    public interface IMappable
    {
        SysChan sysChan { get; set; }

        SysChan MapToSystemChannel();
    }
}
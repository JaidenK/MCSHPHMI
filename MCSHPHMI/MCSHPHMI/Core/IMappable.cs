namespace MCSHPHMI.Core
{
    /// <summary>
    /// Mappables must add themselves to MappableUserControls
    /// in their constructor.
    /// </summary>
    public interface IMappable
    {
        ProcessVariable ProcVar { get; set; }

        ProcessVariable MapToSystemChannel();
    }
}
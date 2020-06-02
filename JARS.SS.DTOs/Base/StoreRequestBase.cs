namespace JARS.SS.DTOs.Base
{
    /// <summary>
    /// Inherit the store requests from this class to be able to indicate if a request was made in relation to an appointment.
    /// </summary>
    public abstract class StoreRequestBase
    {
        public bool IsAppointment { get; set; }
    }

    /// <summary>
    /// Inherit the delete requests from this class to be able to indicate if a request was made in relation to an appointment.
    /// </summary>
    public abstract class DeleteRequestBase
    {
        public bool IsAppointment { get; set; }
    }
}
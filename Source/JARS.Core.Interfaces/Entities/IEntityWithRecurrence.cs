namespace JARS.Core.Interfaces.Entities
{
    /// <summary>
    /// This interface helps to indicate that a job might be recurring over several records
    /// good example will be the same job that stretched more than a day or over multiple days but is linked to the same original order no
    /// </summary>
    interface IEntityWithRecurrence: IEntityWithExternalReference
    {
        int RecurrenceIndex { get; set; }
    }
}

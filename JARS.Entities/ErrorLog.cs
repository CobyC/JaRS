using JARS.Core.Entities;
using System;
using System.Text;

namespace JARS.Entities
{
    /// <summary>
    /// This class can be used for recording error information.
    /// </summary>
    [Serializable]
    public class ErrorLog : EntityBase<int>
    {
        public ErrorLog()
        { }

        /// <summary>
        /// The name of the user(logged in windows user) associated with the error
        /// </summary>
         
        public virtual string EnvironmentUserName { get; set; }

        /// <summary>
        /// Error text
        /// </summary>
         
        public virtual string ErrorText { get; set; }

        /// <summary>
        /// The time the error occurred.
        /// </summary>
         
        public virtual DateTime? ErrorTime { get; set; }

        /// <summary>
        /// The type of error that was caught.
        /// </summary>
         
        public virtual string ErrorType { get; set; }

        /// <summary>
        /// An override of the default ToString that will show all the information of the error Log record.
        /// </summary>
        /// <returns>string representing the ErrorLog record.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendLine(EnvironmentUserName);
                sb.AppendLine(ErrorType);
                sb.AppendLine(ErrorTime.ToString());
                sb.AppendLine(ErrorText);
            }
            catch { sb.Append(base.ToString()); }
            return sb.ToString();
        }
    }
}
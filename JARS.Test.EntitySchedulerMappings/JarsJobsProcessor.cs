using JARS.Core.Interfaces.Entities;
using JARS.Entities;

namespace JARS.Test.EntitySchedulerMappings
{
    public class JarsEntityProcessor 
    {
        /// <summary>
        /// Generate a new job from the External job information
        /// </summary>
        /// <param name="jex">The External job information used to generate the basic job details.</param>
        /// <returns></returns>
        public JarsJob CreateJarsEntityFromExternalEntity(IExternalEntityBase<int> jex)
        {
            JarsJob job = new JarsJob();
            job.Location = jex.Location;
            job.Description = jex.Description;
            job.ExtRefId = jex.ExtRefId;
            job.LineOfWork = jex.LineOfWork;
            job.TargetDate = jex.TargetDate;

            return job;
        }
    }
}

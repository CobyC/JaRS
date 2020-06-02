using DevExpress.XtraScheduler;
using JARS.Core.Attributes;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Entities;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Threading.Tasks;
using JARS.Core.Security;

namespace JARS.WinForms.Plugins.Processors
{
    [ExportProcessor(typeof(JarsResource),typeof(IPluginToResourceHeader))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourceRouteCalculationPluginToHeader : IPluginToResourceHeader
    {
        public JarsResource Entity { get; set; }

        public string PluginText => "Calculate best route.";

        public bool AutoExecute => false;
        public string[] RequiredRoles => new string[] { JarsRoles.Admin, JarsRoles.Manager, JarsRoles.PowerUser, JarsRoles.User };

        public string[] RequiredPermissions => null;

        public ISchedulerStorage schedulerDataStorage { get; set; }

        public Image Image => DevExpress.Images.ImageResourceCache.Default.GetImage("images/maps/geopointmap_32x32.png");

        public Task ExecuteAsync()
        {
            return Task.Run(()=>{ System.Diagnostics.Process.Start("https://www.google.com/maps/dir/SG6/SG5"); });
        }
    }
}

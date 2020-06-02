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
    /// <summary>
    /// This plugin enables the option to make a call using the resource mobile number.
    /// </summary>
    [ExportProcessor(typeof(JarsResource), typeof(IPluginToResourceHeader))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ResourcePhoneCallPluginToHeader : IPluginToResourceHeader
    {
        public JarsResource Entity { get; set; }

        public string PluginText { get { return Entity != null ? $"Call on :{Entity.MobileNo}" : "Call"; } }

        public bool AutoExecute => false;

        public ISchedulerStorage schedulerDataStorage { get; set; }

        public Image Image => DevExpress.Images.ImageResourceCache.Default.GetImage("images/communication/phone_32x32.png");

        public string[] RequiredRoles => new string[] { JarsRoles.Admin, JarsRoles.Manager, JarsRoles.PowerUser, JarsRoles.User };

        public string[] RequiredPermissions => null;

        public Task ExecuteAsync()
        {
            return Task.Run(() => { System.Diagnostics.Process.Start("https://www.ringcentral.co.uk/"); });
        }
    }
}

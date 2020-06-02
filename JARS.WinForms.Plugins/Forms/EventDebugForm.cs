using JARS.Core.WinForms.Interfaces.Forms;
using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Threading.Tasks;

namespace JARS.Win.Plugins
{
    [Export(typeof(IEventDebugForm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EventDebugForm : DevExpress.XtraEditors.XtraForm, IEventDebugForm
    {
        public string PluginText { get => ""; set => value = ""; }
        public Bitmap SmallImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Bitmap LargeImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool AutoExecute => false;

        public EventDebugForm()
        {
            InitializeComponent();          
        }

        public delegate void AddEventToListDelegate(string a, string b, string c);


        public void AddEventItemToList(string formName, string cmdType, string infoString)
        {
            if (this.InvokeRequired)
                this.Invoke(new AddEventToListDelegate(AddEventToControl), formName, cmdType, infoString);
            else
            {
                lbEvents.Items.Add($"Form:{formName} cmd:{cmdType} info:{infoString}");
                lbEvents.Refresh();
            }
        }

        private void AddEventToControl(string formName, string cmdType, string infoString)
        {
            lbEvents.Items.Add($"Form:{formName} cmd:{cmdType} info:{infoString}");
            lbEvents.Refresh();
        }

        public void ClearEventItems()
        {
            lbEvents.Items.Clear();
        }

        public void ClosePluginForm()
        {
            Close();
        }

        public void ShowPluginForm()
        {
            Show();
        }       
    }
}

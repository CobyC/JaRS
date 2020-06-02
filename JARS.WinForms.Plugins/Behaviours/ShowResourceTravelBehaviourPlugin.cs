using DevExpress.XtraBars;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.WinForms.Behaviours;
using JARS.Core.WinForms.Interfaces.Plugins;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace JARS.WinForms.Plugins.Behaviours
{
    [ExportPluginToMainRibbon(typeof(IPluginAsBehaviour), "Load Activity Logs", "Behaviours", "Home", "")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ShowResourceTravelBehaviourPlugin : BehaviourPluginBase, IPluginAsBehaviour
    {
        BarCheckItem _BarCheckItem;

        public BarItem BarItem
        {
            get
            {
                if (_BarCheckItem == null)
                {
                    _BarCheckItem = new BarCheckItem()
                    {
                        Caption = PluginText,
                        Glyph = Properties.Resources.ActivityLogs_16x16,
                        LargeGlyph = Properties.Resources.ActivityLogs_32x32,
                        Id = 705
                    };
                    //_BarCheckItem.CheckedChanged += _BarItem_CheckedChanged;
                }
                return _BarCheckItem;
            }
        }

        
        Dictionary<string, object> _PluginSettings;
        public Dictionary<string, object> PluginSettings
        {
            get
            {
                if (_PluginSettings == null)
                {
                    _PluginSettings = new Dictionary<string, object>();        
                }
                return _PluginSettings;
            }
            set
            {
                _PluginSettings = value;
            }
        }

        public string PluginText => this.GetPluginTextFromAttributeValue();

        public void Activate()
        {
            System.Windows.Forms.MessageBox.Show("Activated");
        }

        public void Deactivate()
        {
            System.Windows.Forms.MessageBox.Show("De-activated");
        }

        public byte[] GetStateInformation()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>
            {
                { "Checked", ((BarCheckItem)BarItem).Checked },
                { "BehaviourSettings", PluginSettings}
            };
            return this.SerializeAndCompressStateInformation(settings);
        }

        public void LoadStateInformation(byte[] stateInfo)
        {
            Dictionary<string, object> settings = this.DeserializeAndDecompressStateInformation(stateInfo);
            ((BarCheckItem)BarItem).Checked = settings["Checked"] != null ? (bool)settings["Checked"] : ((BarCheckItem)BarItem).Checked;
            PluginSettings = (settings.ContainsKey("BehaviourSettings") && settings["BehaviourSettings"] != null) ? (Dictionary<string, object>)settings["BehaviourSettings"] : PluginSettings;
            //_BarItem_CheckedChanged(null, new ItemClickEventArgs(BarCheckItem, null));
        }
    }
}

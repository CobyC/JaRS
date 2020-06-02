using DevExpress.XtraLayout;
using DevExpress.XtraScheduler;
using JARS.Core.Interfaces.Attributes;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{
    public partial class ResourcePluginsForm : FormBase
    {
        [Import]
        protected IPluginFactory _pluginFactory;


        protected JarsResource _resource;
        protected SchedulerControl _schedulerControl;
        protected List<Lazy<Core.Interfaces.Processors.IProcessor, IProcessorMetadata>> _resourcePlugins;
        public ResourcePluginsForm()
        {
            InitializeComponent();
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);

        }
        public ResourcePluginsForm(IPluginFactory pluginFactory) : base()
        {
            _pluginFactory = pluginFactory;
        }


        void BuildPluginList()
        {
            
            if (_resourcePlugins.Count() > 0)
            {
                //add the plugins to the controls list
                foreach (var plugin in _resourcePlugins)
                {
                    Button btn = new Button
                    {
                        Text = "blank"
                    };
                    if (plugin.Value is IPluginToResourceHeader hdrPlugin)
                    {
                        hdrPlugin.Entity = _resource;
                        btn = new Button()
                        {
                            Text = hdrPlugin.PluginText,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Image = hdrPlugin.Image,
                            ImageAlign = ContentAlignment.MiddleCenter,
                            Height = 36,
                            Tag = plugin
                        };
                        btn.Click += Btn_Click;

                        LayoutControlItem item = new LayoutControlItem(layoutControl1, btn)
                        {
                            Text = hdrPlugin.PluginText,
                            //TextAlignMode = TextAlignModeItem.AutoSize,
                            TextVisible = false,
                            Height = 45
                        };
                    }
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Lazy<Core.Interfaces.Processors.IProcessor, IProcessorMetadata> tagPlug = (Lazy<Core.Interfaces.Processors.IProcessor, IProcessorMetadata>)((Button)sender).Tag;
            if (tagPlug.Value is IPluginToResourceHeader plugin)
            {
                plugin.Entity = _resource;
                plugin.schedulerDataStorage = _schedulerControl.DataStorage;
                plugin.ExecuteAsync();
            }
        }


        public static void Show(JarsResource resource, List<Lazy<Core.Interfaces.Processors.IProcessor, IProcessorMetadata>> list, SchedulerControl schedulerControl)
        {
            ResourcePluginsForm frm = new ResourcePluginsForm
            {
                _resource = resource,
                _resourcePlugins = list,
                _schedulerControl = schedulerControl
            };
            frm.textEdit1.Text = resource.DisplayName;
            frm.textEdit2.Text = resource.ExtRef;
            frm.BuildPluginList();
            //now just show the form plugins should be visible on show..
            frm.ShowDialog();
        }
    }
}

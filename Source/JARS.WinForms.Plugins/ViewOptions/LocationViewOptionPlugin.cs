using DevExpress.XtraBars;
using DevExpress.XtraScheduler;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Client;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.ViewOptions;
using JARS.Entities;
using JARS.SS.DTOs;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;

namespace JARS.WinForms.Plugins.ViewOptions
{
    [ExportPluginAsViewOption(typeof(IPluginAsViewOption), typeof(IEntityWithLocation), "Location", "LOCATION")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LocationViewOptionPlugin : ViewOptionPluginBase, IPluginAsViewOption
    {
        public string PluginText => this.GetPluginTextFromAttributeValue();

        public Type LinkedToInterfaceType => this.GetLinkedInterfaceTypeFromAttribute();


        BarCheckItem _BarCheckItem;
        public BarCheckItem BarCheckItem
        {
            get
            {
                if (_BarCheckItem == null)
                {
                    _BarCheckItem = new BarCheckItem()
                    {
                        Caption = PluginText,
                        Glyph = Properties.Resources.ViewArea_16x16,
                        LargeGlyph = Properties.Resources.ViewArea_32x32,
                        Id = 603
                    };
                    _BarCheckItem.CheckedChanged += _BarItem_CheckedChanged;
                }
                return _BarCheckItem;
            }
        }

        private void _BarItem_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            BarCheckItem item = BarCheckItem as BarCheckItem;
            if (item.Checked)
            {
                //set things here that needs to happen

                var lblResponse = Context.ServiceClient.Get(new FindApptLabels() { ViewType = "LOCATION", InterfaceTypeName = typeof(IEntityWithLocation).AssemblyQualifiedName });
                if (!lblResponse.IsErrorResponse())
                    SchedulerLabels = lblResponse.Labels.ConvertAll(l => l.ConvertTo<ApptLabel>());
                var stsResponse = Context.ServiceClient.Get(new FindApptStatuses() { ViewType = "LOCATION", InterfaceTypeName = typeof(IEntityWithLocation).AssemblyQualifiedName });
                if (!stsResponse.IsErrorResponse())
                    SchedulerStatuses = stsResponse.Statuses.ConvertAll(s => s.ConvertTo<ApptStatus>()); ;

                //rase the select event if not null
                base.OnSelected(this, e);
            }
            else
            {
                //unload the things (disable things)
                base.OnUnselected(this, e);
            }
        }

        public byte[] GetStateInformation()
        {
            Dictionary<string, object> settings = new Dictionary<string, object>
            {
                { "Checked", BarCheckItem.Checked }
            };
            return this.SerializeAndCompressStateInformation(settings);
        }

        public void LoadStateInformation(byte[] stateInfo)
        {
            Dictionary<string, object> settings = this.DeserializeAndDecompressStateInformation(stateInfo);
            BarCheckItem.Checked = settings["Checked"] != null ? (bool)settings["Checked"] : BarCheckItem.Checked;
            //_BarItem_CheckedChanged(null, new ItemClickEventArgs(BarCheckItem, null));
        }

        public void AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e, AppointmentStatusDataStorage statuses)
        {
            try
            {
                //apply the colours according to the value of the location.
                //e.ViewInfo.Appearance.BackColor <-- this is label colour
                //e.ViewInfo.Status <-- this is status strip colour (we set it on the appointment, not on the entity in "ENTITY" custom field.
                if (e.ViewInfo.Appointment.CustomFields != null)
                    if (e.ViewInfo.Appointment.CustomFields["ENTITY"] is IEntityWithLocation)
                    {
                        Appointment appt = e.ViewInfo.Appointment;
                        DataTable table = appt.CustomFields["ENTITY"].ConvertToDataTable();//TempDataTableCreator.CreateDataTableFromEntity(appt.CustomFields["ENTITY"]);
                        ApptLabel label = GetApptLabel(table);
                        ApptStatus status = GetApptStatus(table);

                        if (label != null)
                            e.ViewInfo.Appearance.BackColor = Color.FromArgb(label.ColourRGB);
                        else
                            e.ViewInfo.Appearance.BackColor = Color.FromArgb(SchedulerLabels.First(l => l.SortIndex == 99).ColourRGB);

                        if (status != null)
                            e.ViewInfo.Status = statuses.Items.Find(s => s.Id.ToString() == status.Id.ToString());
                        else
                            e.ViewInfo.Status = statuses.Items.CreateNewStatus(Guid.NewGuid(), "", "", new SolidBrush(Color.FromArgb(SchedulerStatuses.First(s => s.SortIndex == 99).ColourRGB)));
                    }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw ex;
#endif
            }

        }
        public void InitAppointmentImages(object sender, AppointmentImagesEventArgs e, AppointmentStatusDataStorage statuses)
        {
            //if (e.ViewInfo.Appointment.CustomFields != null)
            //    if (e.ViewInfo.Appointment.CustomFields["ENTITY"] is ILocatableEntity)
            //    {
            //        ILocatableEntity entity = (e.ViewInfo.Appointment.CustomFields["ENTITY"] as ILocatableEntity);
            //        Appointment appt = e.ViewInfo.Appointment;

            //        Image im = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png");
            //        AppointmentImageInfo info = new AppointmentImageInfo();
            //        info.Image = im;//SystemIcons.Warning.ToBitmap();
            //        e.ImageInfoList.Add(info);
            //    }

        }

    }
}

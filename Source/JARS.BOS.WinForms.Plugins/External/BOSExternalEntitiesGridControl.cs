using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler;
using DevExpress.XtraTreeList;
using JARS.BOS.Entities;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Extensions;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.Utils;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Plugins;
using JARS.Entities;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JARS.BOS.WinForms.Plugins.External
{
    [ExportPluginToTabControl(typeof(IPluginWinForms), "BO System", 0)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class BOSExternalEntitiesGridControl : UserControlBasePlugin,
        IPluginToSchedulerStorage,
        IPluginToResourceTree,
        IPluginToDateNavigator,
        IPluginToViewOption,
        IPluginWithInitialize,
        IPluginWithActivate,
        IPluginWithExecute
    {
        public BOSExternalEntitiesGridControl()
        {
            InitializeComponent();
        }

        public override string[] RequiredRoles => new[] { JarsRoles.Admin, JarsRoles.User, JarsRoles.PowerUser, JarsRoles.Manager };
        public override string[] RequiredPermissions => new[] { JarsPermissions.Full, JarsPermissions.CanView, JarsPermissions.CanAdd, JarsPermissions.CanViewAppointment, JarsPermissions.CanAddAppointment };


        //the below values are set in when the plugin is loaded from the main form.
        public ISchedulerStorage schedulerDataStorage { get; set; }
        public TreeList resourceTree { get; set; }
        public DateNavigator dateNavigator { get; set; }

        IPluginAsViewOption _activeViewOption;
        public IPluginAsViewOption ActiveViewOption { get { return _activeViewOption; } }
        public void SetViewOptionPlugin(IPluginAsViewOption activeViewOption)
        {
            _activeViewOption = activeViewOption;
            gvBOSExternalEntity.FormatRules.Clear();

            //foreach (var apptLbl in _activeViewOption.SchedulerLabels)
            //{
            //    //if (apptLbl.LabelCriteria != "")
            //    //{
            //    GridFormatRule gridFormatRule = new GridFormatRule();
            //    FormatConditionRuleExpression formatConditionRuleExpression = new FormatConditionRuleExpression();
            //    gridFormatRule.ApplyToRow = true;
            //    formatConditionRuleExpression.Appearance.BackColor = Color.FromArgb(apptLbl.ColourRGB);
            //    formatConditionRuleExpression.Appearance.ForeColor = Color.FromArgb(apptLbl.ForeColourRGB);
            //    formatConditionRuleExpression.Expression = apptLbl.LabelCriteria;
            //    gridFormatRule.Rule = formatConditionRuleExpression;
            //    gvBOSExternalEntity.FormatRules.Add(gridFormatRule);
            //    //}
            //}
        }
        public bool AutoExecute => true;

        public void Execute()
        {
            IList<BOSExternalEntity> extEntity = ListAllOrders();
            List<BOSEntity> bosList = extEntity.ConvertAllTo<BOSEntity>().ToList();
            //BosEntityBindingSource.DataSource = extEntity;

            var existingList = BosEntityBindingSource.List as List<BOSEntity>;
            if (existingList != null && existingList.Any())
            {
                var missingList = existingList.Except<BOSEntity>(bosList, new EntityTypeAndExternalExtRefIdComparer());
                List<BOSEntity> delList = new List<BOSEntity>();
                foreach (var remBos in missingList)
                    delList.Add(remBos);

                foreach (var delBos in delList)
                    existingList.Remove(delBos);


                foreach (var bosEnt in bosList)
                {
                    var foundBosEnt = existingList.Find(b => b.ExtRefId == bosEnt.ExtRefId);
                    if (foundBosEnt != null)
                        foundBosEnt.PopulateWith(bosEnt);
                    else
                        existingList.Add(bosEnt);
                }
            }
            else
                BosEntityBindingSource.DataSource = bosList;
            //BosEntityBindingSource.DataSource = bosList;
            BosEntityBindingSource.ResetBindings(false);
        }

        public IList<BOSExternalEntity> ListAllOrders()
        {
            IList<BOSExternalEntity> orders = new List<BOSExternalEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=BackOfficeWorkOrders;Integrated Security=True"))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM WorkOrders";
                        command.CommandType = System.Data.CommandType.Text;
                        connection.Open();
                        SqlDataReader recs = command.ExecuteReader();
                        while (recs.Read())
                        {
                            BOSExternalEntity nwo = new BOSExternalEntity
                            {
                                Id = 0,//(int)recs["OrderNo"],
                                ExtRefId = recs["OrderNo"].ToString(),
                                Location = recs["Address"].ToString(),
                                LineOfWork = recs["Trade"].ToString(),
                                Description = recs["Description"].ToString(),
                                Duration = (int)recs["Duration"]
                            };
                            orders.Add(nwo);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                lbltimer.Stop();
                timer.Stop();
                lblTimerIndicator.Text = ex.Message;
            }
            return orders;
        }

        bool _IsActive;
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
        }
        Timer timer = new Timer();
        Timer lbltimer = new Timer();

        public event EventHandler OnActivate;
        public event EventHandler OnDeactivate;

        public void Activate()
        {
            //this could just as well be something like a synchronizer.
            //or subscription to the ServerEventsClient
            // anything that can be enabled / disabled.
            lbltimer.Interval = 1000;
            lbltimer.Tick += Lbltimer_Tick;
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();
            lbltimer.Start();
            OnActivate?.Invoke(this, new EventArgs());
            _IsActive = true;
        }

        int iCountdown = 5;
        private void Lbltimer_Tick(object sender, EventArgs e)
        {
            if (iCountdown == 0)
                iCountdown = 5;

            iCountdown--;
            lblTimerIndicator.InvokeIfRequired(c => c.Text = $"{iCountdown}");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Execute();
        }

        public void Deactivate()
        {
            timer.Tick -= Timer_Tick;
            timer.Stop();
            lbltimer.Stop();
            OnDeactivate?.Invoke(this, new EventArgs());
            _IsActive = false;
        }

        public void Init()
        {

        }

        GridHitInfo mouseDownHitInfo;

        private void gcBOSExternalEntity_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownHitInfo = null;
            //if another key is pressed at the same time as the mouse is pressed down ie combination keys
            if (ModifierKeys != Keys.None)
                return;
            GridHitInfo hitInfo = gvBOSExternalEntity.CalcHitInfo(e.Location);// new Point(e.X, e.Y));
            if (hitInfo.InRow && hitInfo.HitTest != GridHitTest.RowIndicator)
            {
                //check if there are any calendars visible, this is to make sure a job can be booked against someone.
                if (e.Button == MouseButtons.Left)
                    mouseDownHitInfo = hitInfo;

                //if (e.Button == MouseButtons.Right)
                //could extend it here with info??
            }
        }

        private void gcBOSExternalEntity_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridView view = ((GridControl)sender).MainView as GridView;
                if (e.Button == MouseButtons.Left && mouseDownHitInfo != null && !schedulerDataStorage.IsUpdateLocked)
                {
                    if (schedulerDataStorage.Resources.Items.Find(r => r.Visible == true) != null)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        Rectangle dragRect = new Rectangle(new Point(mouseDownHitInfo.HitPoint.X - dragSize.Width / 2,
                            mouseDownHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                        if (!dragRect.Contains(new Point(e.X, e.Y)) && view.FocusedRowHandle > -1)
                        {
                            view.GridControl.DoDragDrop(GetDragData(view), DragDropEffects.All);
                            mouseDownHitInfo = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a user from the group/user tree before trying to book a job", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        SchedulerDragData GetDragData(GridView gridView)
        {
            Appointment appt = null;
            try
            {
                int[] selection = gridView.GetSelectedRows();
                if (selection == null)
                    return null;

                if (gridView != null && gridView.FocusedRowHandle > -1)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is BOSEntity)
                    {
                        //BOSExternalEntity xdr = gridView.GetRow(gridView.FocusedRowHandle) as BOSExternalEntity;
                        BOSEntity dRec = gridView.GetRow(gridView.FocusedRowHandle) as BOSEntity;
                        appt = schedulerDataStorage.CreateAppointment(AppointmentType.Normal);
                        appt.Description = dRec.Description;
                        appt.Duration = TimeSpan.FromHours(dRec.Duration);
                        appt.Location = dRec.Location;
                        appt.Subject = dRec.ExtRefId;

                        //BOSEntity dRec = new BOSEntity();
                        appt.SetId(dRec.GuidValue);
                        //dRec.PopulateWith(dRec);
                        dRec.Id = 0;
                        //dRec.GuidValue = Guid.NewGuid().ToString();
                        //dRec.ExtRefId = xdr.Id.ToString();
                        //dRec.Description = xdr.Description;
                        dRec.ExtRefId = dRec.ExtRefId;
                        //dRec.Location = xdr.Location;
                        //dRec.LineOfWork = xdr.LineOfWork;
                        //dRec.ProgressStatus = xdr.ProgressStatus;
                        //dRec.Priority = xdr.Priority;
                        //dRec.TargetDate = xdr.TargetDate;

                        appt.CustomFields["ENTITY"] = dRec;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new Exception("GetDridData:", ex);
            }
            return new SchedulerDragData(appt);
        }

        private void gvBOSExternalEntity_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle > -1)
                {
                    if (ActiveViewOption != null)
                    {
                        //DataTable table1 = TempDataTableCreator.CreateDataTableFromEntity(gvExample.GetRow(e.RowHandle));
                        DataTable table = gvBOSExternalEntity.GetRow(e.RowHandle).ConvertToDataTable();
                        ApptStatus status = ActiveViewOption.GetApptStatus(table);
                        e.DefaultDraw();
                        if (status != null)
                        {
                            e.Appearance.BackColor = Color.FromArgb(status.ColourRGB);

                            e.Appearance.FillRectangle(e.Cache, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));

                            if (e.Info.IsRowIndicator)
                            {
                                if (!(e.Info.ImageIndex < 0))
                                {
                                    ImageCollection ic = e.Info.ImageCollection as ImageCollection;
                                    Image indicator = ic.Images[e.Info.ImageIndex];
                                    e.Cache.Paint.DrawImage(e.Graphics, indicator, new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 20, indicator.Width, indicator.Height));
                                }
                            }
                            e.Handled = true;
                        }
                    }
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

        private void gvBOSExternalEntity_CustomDrawRowPreview(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle > -1)
                {
                    if (ActiveViewOption != null)
                    {
                        if (sender is GridView view)
                        {
                            if (view.IsRowSelected(e.RowHandle))
                                return;


                            //DataTable table1 = TempDataTableCreator.CreateDataTableFromEntity(gvExample.GetRow(e.RowHandle));
                            //to represent the entity as the one on the scheduler, see if it is already an appointment, so check the scheduler
                            DataTable table = new DataTable();
                            var recInGrid = view.GetRow(e.RowHandle) as BOSEntity;

                            //look for the appointments with the ext ref as subject, and then extract the entity within the appointment with that reference
                            var appts = schedulerDataStorage.Appointments.Items.FindAll(a => (a.CustomFields["ENTITY"] is BOSEntity bosEnt) && bosEnt.ExtRefId == recInGrid.ExtRefId);
                            if (appts.Any())
                            {
                                Appointment appt = appts.FirstOrDefault(a => a.Start.Date == dateNavigator.SelectionStart.Date);
                                if (appt == null)
                                    appt = appts.FirstOrDefault();
                                //recInGrid.PopulateWith((BOSEntity)appt.CustomFields["ENTITY"]);<-- this causes issues
                                table = appt.CustomFields["ENTITY"].ConvertToDataTable();
                            }
                            else //there is no appointment on the scheduler that represents this external entity, so fake send a BosEntity
                                table = recInGrid.ConvertToDataTable();

                            ApptLabel label = ActiveViewOption.GetApptLabel(table);
                            if (label != null)
                            {
                                e.Appearance.BackColor = Color.FromArgb(label.ColourRGB);
                                e.Appearance.ForeColor = Color.FromArgb(label.ForeColourRGB);
                                //e.Handled = true;
                            }
                            else
                            {
                                e.DefaultDraw();
                            }
                        }
                    }
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

        private void gvBOSExternalEntity_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle > -1)
                {
                    if (ActiveViewOption != null)
                    {
                        if (sender is GridView view)
                        {
                            if (view.IsRowSelected(e.RowHandle))
                            {
                                return;
                            }
                        }
                        //DataTable table1 = TempDataTableCreator.CreateDataTableFromEntity(gvExample.GetRow(e.RowHandle));
                        //to represent the entity as the one on the scheduler, see if it is already an appointment, so check the scheduler
                        DataTable table = new DataTable();
                        var recInGrid = gvBOSExternalEntity.GetRow(e.RowHandle) as BOSEntity;
                        //look for the appointments with the ext ref as subject, and then extract the entity within the appointment with that reference
                        var appts = schedulerDataStorage.Appointments.Items.FindAll(a => (a.CustomFields["ENTITY"] is BOSEntity bosEnt) && bosEnt.ExtRefId == recInGrid.ExtRefId);
                        if (appts.Any())
                        {
                            Appointment appt = appts.FirstOrDefault(a => a.Start.Date == dateNavigator.SelectionStart.Date);
                            if (appt == null)
                                appt = appts.FirstOrDefault();

                            table = appt.CustomFields["ENTITY"].ConvertToDataTable();
                        }
                        else
                            table = recInGrid.ConvertToDataTable();

                        ApptLabel label = ActiveViewOption.GetApptLabel(table);

                        if (label != null)
                        {
                            e.Appearance.BackColor = Color.FromArgb(label.ColourRGB);
                            e.Appearance.ForeColor = Color.FromArgb(label.ForeColourRGB);
                        }
                    }

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

        private void gvBOSExternalEntity_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
        {
            try
            {
                if (e.DataSourceRowIndex > -1)
                {
                    if (e.Row is BOSEntity bosEnt)
                        e.PreviewText = $"L: {bosEnt.Location}{Environment.NewLine}D: {bosEnt.Description}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("gvBOSExternalEntity_CalcPreviewText:", ex);
            }
        }
    }
}

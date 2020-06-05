using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler;
using JARS.Core;
using JARS.Core.Attributes;
using JARS.Core.Interfaces.Plugins;
using JARS.Core.Security;
using JARS.Core.WinForms.Extensions;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Plugins;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace JARS.Win.Plugins.Controls
{
    [ExportPluginToTabControl(typeof(IPluginWinForms), "Defaults", 99)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsDefaultAppointmentControl : UserControlBasePlugin,
        IPluginToSchedulerStorage,
        IPluginWithInitialize,
        IPluginWithExecuteAsync

    {
        public JarsDefaultAppointmentControl()
        {
            InitializeComponent();
        }

        public ISchedulerStorage schedulerDataStorage { get; set; }

        public bool AutoExecute => true;

        #region DesignerGenereatedCode

        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl gcDefAppt;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDefAppt;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        public BindingSource standardAppointmentDefaultBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colDuration;
        private DevExpress.XtraGrid.Columns.GridColumn colIsAllDay;
        private DevExpress.XtraGrid.Columns.GridColumn colShowOnMobile;
        //private System.ComponentModel.IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JarsDefaultAppointmentControl));
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcDefAppt = new DevExpress.XtraGrid.GridControl();
            this.standardAppointmentDefaultBindingSource = new System.Windows.Forms.BindingSource();
            this.gvDefAppt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsAllDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShowOnMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDefAppt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.standardAppointmentDefaultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefAppt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcDefAppt);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup3;
            this.layoutControl2.Size = new System.Drawing.Size(312, 485);
            this.layoutControl2.TabIndex = 2;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcDefAppt
            // 
            this.gcDefAppt.ContextMenuStrip = this.contextMenuStrip1;
            this.gcDefAppt.DataSource = this.standardAppointmentDefaultBindingSource;
            this.gcDefAppt.Location = new System.Drawing.Point(1, 1);
            this.gcDefAppt.MainView = this.gvDefAppt;
            this.gcDefAppt.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.gcDefAppt.Name = "gcDefAppt";
            this.gcDefAppt.Size = new System.Drawing.Size(310, 483);
            this.gcDefAppt.TabIndex = 0;
            this.gcDefAppt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDefAppt});
            this.gcDefAppt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcDefAppt_MouseDown);
            this.gcDefAppt.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gcDefAppt_MouseMove);
            // 
            // standardAppointmentDefaultBindingSource
            // 
            this.standardAppointmentDefaultBindingSource.DataSource = typeof(JARS.Entities.JarsDefaultAppointment);
            // 
            // gvDefAppt
            // 
            this.gvDefAppt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubject,
            this.colDuration,
            this.colIsAllDay,
            this.colShowOnMobile});
            this.gvDefAppt.GridControl = this.gcDefAppt;
            this.gvDefAppt.Name = "gvDefAppt";
            this.gvDefAppt.OptionsBehavior.Editable = false;
            this.gvDefAppt.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.gvDefAppt.OptionsBehavior.ReadOnly = true;
            this.gvDefAppt.OptionsDetail.EnableMasterViewMode = false;
            this.gvDefAppt.OptionsMenu.ShowSplitItem = false;
            this.gvDefAppt.OptionsView.ShowAutoFilterRow = true;
            this.gvDefAppt.OptionsView.ShowGroupPanel = false;
            this.gvDefAppt.OptionsView.ShowPreview = true;
            this.gvDefAppt.PreviewFieldName = "Description";
            this.gvDefAppt.PreviewIndent = 2;
            // 
            // colSubject
            // 
            this.colSubject.Caption = "Subject";
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 0;
            this.colSubject.Width = 250;
            // 
            // colDuration
            // 
            this.colDuration.FieldName = "DefaultDuration";
            this.colDuration.Name = "colDuration";
            this.colDuration.Visible = true;
            this.colDuration.VisibleIndex = 1;
            this.colDuration.Width = 96;
            // 
            // colIsAllDay
            // 
            this.colIsAllDay.FieldName = "IsAllDay";
            this.colIsAllDay.Name = "colIsAllDay";
            // 
            // colShowOnMobile
            // 
            this.colShowOnMobile.FieldName = "ShowOnMobile";
            this.colShowOnMobile.Name = "colShowOnMobile";
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(312, 485);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcDefAppt;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(312, 485);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reloadToolStripMenuItem.Image")));
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.reloadToolStripMenuItem.Text = "Refresh";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // JarsDefaultAppointmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.layoutControl2);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "JarsDefaultAppointmentControl";
            this.Size = new System.Drawing.Size(312, 485);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDefAppt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.standardAppointmentDefaultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefAppt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public override string[] RequiredRoles => base.RequiredRoles.Concat(new string[] { JarsRoles.User, JarsRoles.PowerUser, JarsRoles.Manager }).ToArray();
        public override string[] RequiredPermissions => new[] { JarsPermissions.Full, JarsPermissions.CanAddAppointment, JarsPermissions.CanView, JarsPermissions.CanEdit };

        public async Task ExecuteAsync()
        {
            JarsDefaultAppointmentsResponse resp = await Context.ServiceClient.GetAsync(new FindJarsDefaultAppointments());
            standardAppointmentDefaultBindingSource.DataSource = resp.Appointments.ConvertAllTo<JarsDefaultAppointment>();
        }

        public override void Refresh()
        {
            base.Refresh();
            standardAppointmentDefaultBindingSource.ResetBindings(false);
        }

        GridHitInfo mouseDownHitInfo;
        private void gcDefAppt_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownHitInfo = null;
            //if another key is pressed at the same time as the mouse is pressed down ie combination keys
            if (ModifierKeys != Keys.None)
                return;
            GridHitInfo hitInfo = gvDefAppt.CalcHitInfo(e.Location);// new Point(e.X, e.Y));
            if (hitInfo.InRow && hitInfo.HitTest != GridHitTest.RowIndicator)
            {
                //check if there are any calendars visible, this is to make sure a job can be booked against someone.
                if (e.Button == MouseButtons.Left)
                    mouseDownHitInfo = hitInfo;

                //if (e.Button == MouseButtons.Right)
                //could extend it here with info??
            }
        }

        private void gcDefAppt_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                GridView view = ((GridControl)sender).MainView as GridView;
                if (e.Button == MouseButtons.Left && mouseDownHitInfo != null && !schedulerDataStorage.IsUpdateLocked)
                {
                    RolesAndOrPermissions.ExecuteActionUIOnException(RequiredRoles, RequiredPermissions, () =>
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
                              MessageBox.Show("Please select a user from the group/user tree before trying to book a job", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      });
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
            Appointment appointment = null;
            try
            {
                int[] selection = gridView.GetSelectedRows();
                if (selection == null)
                    return null;

                if (gridView != null && gridView.FocusedRowHandle > -1)
                {
                    if (gridView.GetRow(gridView.FocusedRowHandle) is JarsDefaultAppointment)
                    {

                        JarsDefaultAppointment defAppt = gridView.GetRow(gridView.FocusedRowHandle) as JarsDefaultAppointment;
                        appointment = schedulerDataStorage.CreateAppointment(AppointmentType.Normal); //create a devexpress appointment
                        appointment.Subject = defAppt.Subject;
                        appointment.Description = defAppt.Description;
                        if (defAppt.IsAllDay)
                            appointment.AllDay = true;
                        else
                            appointment.Duration = TimeSpan.FromHours(defAppt.DefaultDuration);

                        //appointment that will be wrapped by the devexpress appt.
                        StandardAppointment stdAppointment = new StandardAppointment();

                        //convert to standard appointment that can be saved 
                        stdAppointment = appointment.ConvertTo<StandardAppointment>();
                        appointment.SetId(stdAppointment.GuidValue);
                        appointment.CustomFields["ENTITY"] = stdAppointment;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
#if DEBUG
                throw new Exception("DefaultStandardAppointmentControl_GetDridData", ex);
#endif
            }
            return new SchedulerDragData(appointment);
        }

        public void Init()
        {
            Context.SSEventClient.OnMessage += new Action<ServerEventMessage>(OnMessageEvent);
            Context.SSEventClient.SubscribeToChannels(typeof(JarsDefaultAppointment).Name);
        }

        public void OnMessageEvent(ServerEventMessage msg)
        {
            if (msg.Channel == typeof(JarsDefaultAppointment).Name)
            {
                // throw new NotImplementedException();
                ServerEventMessageData msgData = msg.Json.FromJson<ServerEventMessageData>();
                switch (msg.Selector)
                {
                    case SelectorTypes.store:

                        List<JarsDefaultAppointment> appts = msgData.jsonDataString.ConvertTo<List<JarsDefaultAppointment>>();
                        foreach (var item in appts)
                        {
                            //first see if the appt is already in the list, if it is then update it, otherwise add it.
                            var findItm = ((List<JarsDefaultAppointment>)standardAppointmentDefaultBindingSource.List).FirstOrDefault(x => x.Id == item.Id);
                            if (findItm != null)
                                findItm.PopulateWith(item);
                            else
                                standardAppointmentDefaultBindingSource.List.Add(item.ConvertTo<JarsDefaultAppointment>());
                        }

                        this.BeginInvokeIfRequired(c =>
                        {
                            c.Refresh();
                        });
                        break;
                    case SelectorTypes.delete:

                        List<int> ids = msgData.jsonDataString.ConvertTo<List<int>>();
                        var findList = ((List<JarsDefaultAppointment>)standardAppointmentDefaultBindingSource.List).Where(j => ids.Contains(j.Id));//.CreateCopy();
                        this.BeginInvokeIfRequired(c =>
                        {
                            int[] idx = new int[findList.Count()];
                            int x = 0;
                            foreach (var item in findList)
                            {
                                idx[x] = standardAppointmentDefaultBindingSource.IndexOf(item);
                                x++;
                            }
                            for (int i = 0; i < idx.Length; i++)
                            {
                                standardAppointmentDefaultBindingSource.RemoveAt(idx[i]);
                            }
                            c.Refresh();
                        });
                        break;
                    default:
                        break;
                }
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BeginInvokeIfRequired(async c =>
            {
                await c.ExecuteAsync();
                c.Refresh();
            });
        }
    }



}

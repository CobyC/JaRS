using DevExpress.Data.Filtering;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using JARS.Core.Security;
using System;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{
    public partial class RibbonFormCrudBase : RibbonFormBase
    {
        //string[] defaultRoles = null;
        //string[] defaultPermissions = null;
        public virtual string[] RequiredRoles => new[] { JarsRoles.Admin, JarsRoles.PowerUser, JarsRoles.Manager, JarsRoles.User };
        public virtual string[] RequiredPermissions => new[] { JarsPermissions.Full, JarsPermissions.CanView, JarsPermissions.CanAdd, JarsPermissions.CanEdit, JarsPermissions.CanDelete };

        private FormEditStates _formEditState;
        protected FormEditStates FormEditState
        {
            get => _formEditState;
            set
            {
                _formEditState = value;
                if (!this.DesignMode)
                {
                    if (!RolesAndOrPermissions.CheckMatchAny(roles: RequiredRoles, permissions: RequiredPermissions))
                        _formEditState = FormEditStates.BrowseOnly;
                    else
                    {
                        if (value == FormEditStates.Browsing)
                        {
                            if (Context.LoggedInUser.Roles.Contains(JarsRoles.Admin))
                            {
                                _formEditState = FormEditStates.Browsing;
                            }
                            else
                            {
                                _formEditState = FormEditStates.BrowseOnly;
                                if (RolesAndOrPermissions.CheckMatchAll(roles: null, permissions: new[] { JarsPermissions.CanView, JarsPermissions.CanAdd }))
                                    _formEditState = FormEditStates.BrowseAddOnly;
                                if (RolesAndOrPermissions.CheckMatchAll(roles: null, permissions: new[] { JarsPermissions.CanView, JarsPermissions.CanEdit }))
                                    _formEditState = FormEditStates.BrowseEditOnly;
                                if (RolesAndOrPermissions.CheckMatchAll(roles: null, permissions: new[] { JarsPermissions.CanView, JarsPermissions.CanAdd, JarsPermissions.CanEdit }))
                                    _formEditState = FormEditStates.BrowseAddEditOnly;
                                if (RolesAndOrPermissions.CheckMatchAll(roles: null, permissions: new[] { JarsPermissions.CanView, JarsPermissions.CanAdd, JarsPermissions.CanEdit, JarsPermissions.CanDelete }))
                                    _formEditState = value;                                
                                    
                            }
                        }
                    }
                    SetControlButtonsStatus();
                }
            }
        }

        public RibbonFormCrudBase()
        {
            InitializeComponent();
        }

        GridControl _gridControl;
        GridControl[] _gridControls;
        /// <summary>
        /// Set the main grid control on the form, this then links up to the grid options in the ribbon control.
        /// </summary>
        /// <param name="gridControl">The Main grid control for the form, this is the control on the List tab</param>
        public void SetGridControl(GridControl gridControl)
        {

            _gridControl = gridControl;
            barCkShowGroupPanel.Checked = ((GridView)_gridControl.MainView).OptionsView.ShowGroupPanel;
            barCkFilterRow.Checked = ((GridView)_gridControl.MainView).OptionsView.ShowAutoFilterRow;
            barCkPreviewRow.Checked = ((GridView)_gridControl.MainView).OptionsView.ShowPreview;
            ((GridView)_gridControl.MainView).DoubleClick += RibbonCRUDFormBase_DoubleClick;
        }

        public void SetGridControls(GridControl[] gridControls)
        {

            _gridControls = gridControls;
            for (int i = 0; i < _gridControls.Length; i++)
            {
                barCkShowGroupPanel.Checked = ((GridView)_gridControls[i].MainView).OptionsView.ShowGroupPanel;
                barCkFilterRow.Checked = ((GridView)_gridControls[i].MainView).OptionsView.ShowAutoFilterRow;
                barCkPreviewRow.Checked = ((GridView)_gridControls[i].MainView).OptionsView.ShowPreview;
                ((GridView)_gridControls[i].MainView).DoubleClick += RibbonCRUDFormBase_DoubleClick;
            }

        }

        private void RibbonCRUDFormBase_DoubleClick(object sender, EventArgs e)
        {
            xtraTabControl.SelectedTabPage = xtraTabPageDetails;
        }

        /// <summary>
        /// This method sets the control buttons enabled state and visibility according to the FormEditState property
        /// </summary>
        public virtual void SetControlButtonsStatus()
        {

            if (FormEditState == FormEditStates.Editing || FormEditState == FormEditStates.Adding)
            {
                //when the records is being added or edited
                barBtnRefresh.Enabled = false;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barbtnAdd.Enabled = false;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnEdit.Enabled = false;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnSave.Enabled = true;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnCancel.Enabled = true;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnFirst.Enabled = false;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = false;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = false;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = false;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, true);
            }
            else if (FormEditState == FormEditStates.Browsing || FormEditState == FormEditStates.Search)
            {
                //when browsing with add edit and delete
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = true;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnEdit.Enabled = true;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = true;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnFirst.Enabled = true;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = true;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = true;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = true;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, false);
            }
            else if (FormEditState == FormEditStates.BrowseOnly)
            {
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = false;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnEdit.Enabled = false;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnFirst.Enabled = true;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = true;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = true;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = true;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;


                SetControlEnabledState(xtraTabControl, false);
            }
            else if (FormEditState == FormEditStates.BrowseAddEditOnly)
            {
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = true;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnEdit.Enabled = true;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnFirst.Enabled = true;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = true;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = true;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = true;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, false);
            }
            else if (FormEditState == FormEditStates.BrowseAddOnly)
            {
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = true;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnEdit.Enabled = false;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnFirst.Enabled = true;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = true;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = true;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = true;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, false);
            }
            else if (FormEditState == FormEditStates.BrowseEditOnly)
            {
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = false;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnEdit.Enabled = true;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnFirst.Enabled = true;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = true;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = true;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = true;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, false);
            }
            else
            {
                //any other state
                //when browsing
                barBtnRefresh.Enabled = true;
                barBtnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barbtnAdd.Enabled = false;
                barbtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnEdit.Enabled = false;
                barBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnSave.Enabled = false;
                barBtnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnCancel.Enabled = false;
                barBtnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barBtnDelete.Enabled = false;
                barBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnFirst.Enabled = false;
                barBtnFirst.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnPrevious.Enabled = false;
                barBtnPrevious.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnNext.Enabled = false;
                barBtnNext.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                barBtnLast.Enabled = false;
                barBtnLast.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SetControlEnabledState(xtraTabControl, false);
            }

            SetNavigationButtonsEnabled();
        }

        /// <summary>
        /// This method sets the navigation buttons enabled state according to the current record position in the defaultBindingSource
        /// </summary>
        protected void SetNavigationButtonsEnabled()
        {

            if (defaultBindingSource.DataSource == null || defaultBindingSource.Count == 0)
            {
                barBtnFirst.Enabled = false;
                barBtnPrevious.Enabled = false;
                barBtnNext.Enabled = false;
                barBtnLast.Enabled = false;
            }
            else
            {
                if (defaultBindingSource.Position == defaultBindingSource.Count - 1)
                {
                    barBtnFirst.Enabled = true;
                    barBtnPrevious.Enabled = true;
                    barBtnNext.Enabled = false;
                    barBtnLast.Enabled = false;
                }
                else if (defaultBindingSource.Position == 0)
                {
                    barBtnFirst.Enabled = false;
                    barBtnPrevious.Enabled = false;
                    barBtnNext.Enabled = true;
                    barBtnLast.Enabled = true;
                }
                else
                {
                    barBtnFirst.Enabled = true;
                    barBtnPrevious.Enabled = true;
                    barBtnNext.Enabled = true;
                    barBtnLast.Enabled = true;
                }
            }
            if (FormEditState == FormEditStates.Adding || FormEditState == FormEditStates.Editing)
            {
                barBtnFirst.Enabled = false;
                barBtnPrevious.Enabled = false;
                barBtnNext.Enabled = false;
                barBtnLast.Enabled = false;
            }

        }



        public void barBtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnRefreshDataAsync();
        }
        public virtual void OnRefreshDataAsync()
        {
            FormEditState = FormEditStates.Refreshing;
        }


        public void barbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnAddData();
        }

        CriteriaOperator activeFilter;
        public virtual void OnAddData()
        {
            xtraTabControl.SelectedTabPage = xtraTabPageDetails;
            FormEditState = FormEditStates.Adding;
            activeFilter = ((GridView)_gridControl.MainView).ActiveFilterCriteria;
            ((GridView)_gridControl.MainView).ResetAutoFilterConditions();
        }


        public void barBtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnEditData();
        }
        public virtual void OnEditData()
        {
            xtraTabControl.SelectedTabPage = xtraTabPageDetails;
            FormEditState = FormEditStates.Editing;
        }

        public void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //make sure the focus is changed, so that the binding source updates its values.
            this.Select(true, true);
            OnSaveData();
        }
        public virtual void OnSaveData()
        {
            defaultBindingSource.ResetBindings(false);
            FormEditState = FormEditStates.Browsing;
            OnRefreshDataAsync();
            if (!ReferenceEquals(null, activeFilter))
                ((GridView)_gridControl.MainView).ActiveFilterCriteria = activeFilter;
        }

        public void barBtnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (MessageBox.Show($"This will permanently delete the record.{Environment.NewLine}Are you sure?", "Delete Record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            OnDeleteData();
            //else
            //return;
        }

        /// <summary>
        /// This method fires when pressing the delete button.
        /// When using this with the doDefault = true, use it at the beginning of your call, the record will be removed from the datasource.
        /// Process flow should then continue on deleting the record after which the process can be called again to trigger a refresh.
        /// If it is called with doDefault as false, then the OnRefreshDataAsync will be called and nothing else.
        /// When overriding the method call the base.OnDelete() at the beginning as it resets the defaultBindingSource.
        /// </summary>
        /// <param name="doDefault">indicates if the default message box is shown, false will not do anything, true will reset the default binding source. default value is false</param>
        public virtual bool OnDeleteData(bool doDefault = false)
        {
            if (doDefault)
            {
                if (MessageBox.Show($"This will permanently delete the record.{Environment.NewLine}Are you sure?", "Delete Record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //defaultBindingSource.RemoveCurrent();
                    //defaultBindingSource.ResetBindings(false);
                    return true;
                }
                else { return false; }
            }
            else
                OnRefreshDataAsync();

            return true;
        }
        /// <summary>
        /// This method fires when pressing the delete button.
        /// When overriding the method call the base.OnDelete() at the end as it resets the defaultBindingSource.
        /// It calls the OnDeleteData(false).
        /// </summary>
        public virtual bool OnDeleteData()
        {
            return OnDeleteData(false);
        }

        private void barBtnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show($"This will cancel all changes made to the record.{Environment.NewLine}Are you sure?", "Cancel Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OnCancelData();
                FormEditState = FormEditStates.Browsing;
            }
            else
                return;
        }
        public virtual void OnCancelData()
        {
            if (FormEditState == FormEditStates.Adding)
            {
                //remove the current record.
                defaultBindingSource.RemoveCurrent();
                defaultBindingSource.ResetBindings(false);
            }
            if (FormEditState == FormEditStates.Editing)
            {
                //cancel changes to current item
                defaultBindingSource.CancelEdit();
                defaultBindingSource.ResetCurrentItem();
            }
            ((GridView)_gridControl.MainView).ActiveFilterCriteria = activeFilter;
        }

        public void barBtnFirst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnMoveToFirstRecord();
            SetNavigationButtonsEnabled();
        }
        public virtual void OnMoveToFirstRecord()
        {
            if (!ReferenceEquals(null, activeFilter))
                (_gridControl.MainView as GridView).MoveFirst();
            else
                defaultBindingSource.MoveFirst();
            SetNavigationButtonsEnabled();
        }

        public void barBtnPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnMoveToPreviousRecord();
            SetNavigationButtonsEnabled();
        }
        public virtual void OnMoveToPreviousRecord()
        {
            if (!ReferenceEquals(null, activeFilter))
                (_gridControl.MainView as GridView).MovePrev();
            else
                defaultBindingSource.MovePrevious();
        }

        public void barBtnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnMoveToNextRecord();
            SetNavigationButtonsEnabled();
        }
        public virtual void OnMoveToNextRecord()
        {
            if (!ReferenceEquals(null, activeFilter))
                (_gridControl.MainView as GridView).MoveNext();
            else
                defaultBindingSource.MoveNext();
        }

        public void barBtnLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnMoveToLastRecord();
            SetNavigationButtonsEnabled();
        }
        public virtual void OnMoveToLastRecord()
        {
            if (!ReferenceEquals(null, activeFilter))
                (_gridControl.MainView as GridView).MoveLastVisible();
            else
                defaultBindingSource.MoveLast();
        }

        private void barCkShowGroupPanel_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_gridControl != null)
                ((GridView)_gridControl.MainView).OptionsView.ShowGroupPanel = barCkShowGroupPanel.Checked;

            if (_gridControls != null)
                for (int i = 0; i < _gridControls.Length; i++)
                {
                    ((GridView)_gridControls[i].MainView).OptionsView.ShowGroupPanel = barCkShowGroupPanel.Checked;
                }
        }

        private void barCkFilterRow_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_gridControl != null)
                ((GridView)_gridControl.MainView).OptionsView.ShowAutoFilterRow = barCkFilterRow.Checked;
            if (_gridControls != null)
                for (int i = 0; i < _gridControls.Length; i++)
                {
                    ((GridView)_gridControls[i].MainView).OptionsView.ShowAutoFilterRow = barCkFilterRow.Checked;
                }

        }

        private void barCkPreviewRow_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_gridControl != null)
                ((GridView)_gridControl.MainView).OptionsView.ShowPreview = barCkPreviewRow.Checked;
            if (_gridControls != null)
                for (int i = 0; i < _gridControls.Length; i++)
                {
                    ((GridView)_gridControls[i].MainView).OptionsView.ShowPreview = barCkPreviewRow.Checked;
                }

        }

        private void RibbonCRUDFormBase_Load(object sender, EventArgs e)
        {
            FormEditState = FormEditStates.Browsing;
        }

        private void defaultBindingSource_PositionChanged(object sender, EventArgs e)
        {
            SetNavigationButtonsEnabled();
        }
    }
}

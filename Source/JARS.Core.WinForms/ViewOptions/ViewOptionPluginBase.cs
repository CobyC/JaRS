using DevExpress.Data.Filtering;
using JARS.Core.Attributes;
using JARS.Core.Client;
using JARS.Core.Interfaces.Attributes;
using JARS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JARS.Core.WinForms.ViewOptions
{
    public abstract class ViewOptionPluginBase
    {
        private GlobalContext _Context;
        /// <summary>
        /// The global context of the application
        /// </summary>
        protected GlobalContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = GlobalContext.Instance;
                return _Context;
            }
            private set { }
        }
        

        public Type GetLinkedInterfaceTypeFromAttribute()
        {
            IPluginAsViewOptionMetadata attribute = (ExportPluginAsViewOption)GetType().GetCustomAttributes(false).FirstOrDefault(i => i is ExportPluginAsViewOption);
            return (attribute != null) ? attribute.ApplyToEntityInterfaceType : null;
        }

        public event EventHandler Selected;
        protected virtual void OnSelected(object sender, EventArgs e)
        {
            Selected?.Invoke(sender, e);
        }

        public event EventHandler Unselected;
        protected virtual void OnUnselected(object sender, EventArgs e)
        {
            Unselected?.Invoke(sender, e);
        }

        public IList<ApptLabel> SchedulerLabels { get; set; }

        public IList<ApptStatus> SchedulerStatuses { get; set; }

        public ApptStatus GetApptStatus(DataTable table)
        {
            if (table == null || SchedulerStatuses == null)
                return null;

            ApptStatus status = null;
            try
            {
                foreach (ApptStatus sts in SchedulerStatuses)
                {
                    table.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(sts.StatusCriteria));
                    if (table.DefaultView.Count > 0 && sts.StatusCriteria != "")
                    {
                        status = sts;
                        break;
                    }
                    if (sts.StatusCriteria == "")
                        status = sts; //default label
                }
            }
            catch (Exception e)
            {
            }
            return status;
        }

        public ApptLabel GetApptLabel(DataTable table)
        {
            if (table == null || SchedulerLabels == null)
                return null;

            ApptLabel label = null;
            try
            {
                foreach (ApptLabel lbl in SchedulerLabels)
                {
                    table.DefaultView.RowFilter = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(lbl.LabelCriteria));
                    if (table.DefaultView.Count > 0 && lbl.LabelCriteria != "")
                    {
                        label = lbl;
                        break;
                    }

                    if (lbl.LabelCriteria == "")
                        label = lbl; //default label
                }
            }
            catch (Exception e)
            {
            }
            return label;
        }
    }
}

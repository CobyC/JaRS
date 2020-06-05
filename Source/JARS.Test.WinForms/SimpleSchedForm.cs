using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using JARS.Entities;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.SS.DTOs.Utils;
using JARS.WinForms.Plugins.Processors;
using ServiceStack;

namespace JARS.Test.WinForms
{
    public partial class SimpleSchedForm : Form
    {
        StandardAppointmentProcessor processor;
        IDataRepositoryFactory _repoFactory;

        public SimpleSchedForm(Core.Interfaces.Repositories.IDataRepositoryFactory _repFactory)
        {
            InitializeComponent();
            _repoFactory = _repFactory;
            processor = new StandardAppointmentProcessor();

            IGenericEntityRepositoryBase<JarsResource, IDataContextNhJars> jarsRepo = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JARS.Entities.JarsResource, IDataContextNhJars>>();

            List<JarsResource> ResList = jarsRepo.GetAll().ToList();
            resourceBindingSource.DataSource = ResList;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}

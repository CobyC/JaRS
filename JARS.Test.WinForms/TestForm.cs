using DevExpress.XtraBars.Ribbon;
using JARS.Core.Extensions;
using JARS.Core.Utils;
using JARS.Core.WinForms.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARS.Test.WinForms
{
    public partial class TestForm : RibbonForm
    {
        public TestForm()
        {
            InitializeComponent();
        }


        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        internal void LoadComboboxWithItems(IList<Type> items)
        {
            foreach (var item in items)
            {
                comboBoxEdit1.Properties.Items.Add(item.ToTypeNameItem());
            }

        }
        public void LoadComboboxWithItems(Dictionary<string, Type> items)
        {
            foreach (var item in items)
            {
                comboBoxEdit1.Properties.Items.Add(item);
            }
        }
        public void LoadComboboxWithItems(List<object> items)
        {
            foreach (var item in items)
            {
                comboBoxEdit1.Properties.Items.Add(item);
            }
        }
        public void LoadComboboxWithItems(List<TypeNameItem> items)
        {
            foreach (var item in items)
            {
                comboBoxEdit1.Properties.Items.Add(item);
            }
        }
    }
}

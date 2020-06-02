using JARS.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{
    public partial class SelectEntitiesForm : DevExpress.XtraEditors.XtraForm
    {
        IList<SearchEntity<int>> _existingValues;
        public SelectEntitiesForm()
        {
            InitializeComponent();
        }
              
        /// <summary>
        /// Shows the form as a dialog box and will return the selected values if the dialog result was ok.
        /// </summary>
        /// <param name="existingValues">The values that will be marked as selected already</param>
        /// <param name="AllValues">The list of values that can be chosen from</param>
        /// <returns></returns>
        public static IList<SearchEntity<int>> ShowForm(IList<SearchEntity<int>> existingValues, IList<SearchEntity<int>> AllValues,string formTitle)
        {

            IList<SearchEntity<int>> returnList = new List<SearchEntity<int>>();

            SelectEntitiesForm frm = new SelectEntitiesForm();
            frm.Text = formTitle;
            frm._existingValues = existingValues;
            //mark the matching entities as checked          
            foreach (var item in AllValues)
            {
                if (existingValues.FirstOrDefault(x => x.ValueId == item.ValueId) != null)
                    item.IsSelected = true;
            }

            frm.searchEntityBindingSource.DataSource = AllValues;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in frm.chkLstBoxCtrl_Entities.CheckedItems)
                {
                    returnList.Add((SearchEntity<int>)item);
                }
                return returnList;
            }
            else
                return existingValues;
        }
        
    }
}

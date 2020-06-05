using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Forms
{

    public partial class PluginSettingsForm : XtraForm
    {
        RepositoryItem textEditor, boolEditor, decimalEditor, integerEditor, colorEditor, dateEditor;
        public PluginSettingsForm()
        {
            InitializeComponent();
            textEditor = new RepositoryItemTextEdit();
            boolEditor = new RepositoryItemCheckEdit();
            decimalEditor = new RepositoryItemCalcEdit();
            integerEditor = new RepositoryItemSpinEdit() { MinValue = 1, MaxValue = 10, IsFloatValue = false, EditMask = "D" };
            integerEditor.ParseEditValue += IntegerEditor_ParseEditValue;
            colorEditor = new RepositoryItemColorPickEdit();
            dateEditor = new RepositoryItemDateEdit();
            gcSettings.RepositoryItems.AddRange(new RepositoryItem[] { textEditor, boolEditor, decimalEditor, integerEditor, colorEditor, dateEditor });
            gvSettings.Columns["Value"].ColumnEdit = textEditor;
        }

        private void IntegerEditor_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Handled = true;
            e.Value = int.Parse(e.Value.ToString());
        }

        public static Dictionary<string, object> ShowSettings(Dictionary<string, object> pluginSettings)
        {
            IList<PluginSetting> settings = new List<PluginSetting>();
            foreach (KeyValuePair<string, object> keyVal in pluginSettings)
            {
                settings.Add(new PluginSetting() { Key = keyVal.Key, Value = keyVal.Value });
            }

            PluginSettingsForm frm = new PluginSettingsForm();
            frm.gcSettings.DataSource = settings;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                settings = frm.gcSettings.DataSource as List<PluginSetting>;

                foreach (var stng in settings)
                {
                    pluginSettings[stng.Key] = stng.Value;
                }
            }
            return pluginSettings;
        }

        private void gvSettings_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Value")
            {
                if (e.CellValue is bool)
                    e.RepositoryItem = boolEditor;
                if (e.CellValue is int)
                    e.RepositoryItem = integerEditor;
                if (e.CellValue is double)
                    e.RepositoryItem = decimalEditor;
                if (e.CellValue is Color)
                    e.RepositoryItem = colorEditor;
                if (e.CellValue is DateTime)
                    e.RepositoryItem = dateEditor;
            }
        }
    }

    class PluginSetting
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }

}

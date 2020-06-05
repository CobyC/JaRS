using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JARS.Core.WinForms.Utils
{
    public class CustomControlStateConditionValidationRule : ConditionValidationRule
    {
        public CustomControlStateConditionValidationRule(string name):base(name)
        {            
        }
        public override bool Validate(Control control, object value)
        {
            return base.Validate(control, value);
        }

        public override bool CanValidate(Control control)
        {
            return base.CanValidate(control);
        }
    }
}

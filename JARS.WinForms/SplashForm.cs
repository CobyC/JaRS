using DevExpress.XtraSplashForm;
using System;

namespace JARS.WinForms
{
    enum SplashCommand
    {
        UpdateLabel
    }
    public partial class SplashForm : SplashFormBase
    {
        public SplashForm()
        {
            InitializeComponent();
            Text = ".";
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            if (cmd == null || SplashCommand.UpdateLabel == (SplashCommand)cmd )
                lblInfoText.Text = arg.ToString();

            //base.ProcessCommand(cmd, arg);
        }        
    }
}

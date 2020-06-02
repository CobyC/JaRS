using JARS.Core.WinForms.Forms;
using ServiceStack;

namespace JARS.Win.Plugins
{
    public partial class MessagingForm : RibbonFormBase
    {

        public MessagingForm()
        {
            InitializeComponent();
        }
        
        private void barBtnGlobalMsg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //SSEventClient.ServiceClient.Post(new SyncEventData { Channel = Name, FromUserId = "DOTO", Selector = "chat.", ToUserId = "ALL", Message = "This is a global message!" });
        }
    }
}

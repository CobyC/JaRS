using JARS.Core.Interfaces.Rules;
using JARS.Core.Utils;
using JARS.Core.WinForms.Forms;
using JARS.Data.FakeData;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JARS.Test.WinForms
{
    [TestClass]
    public class TestFormsDialogFormsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestForm frm = new TestForm
            {
                ShowInTaskbar = false //<-- this enable forms to show up for testing! otherwise it doesn't show.
            };
            frm.ShowDialog();

        }

        [TestMethod]
        public void AssignRulesToEntities()
        {
            JarsResource resource = FakeDataHelper.FakeResources[0];

            JarsJob testJob = FakeDataHelper.FakeJarsJobs[1];

            //set up a rule for a target entity where target entity can not be changed
            IJarsRule singleEntityRule = JarsRulePopupForm.AddRuleOnEntity(resource, testJob.GetType());
            Assert.IsTrue(!string.IsNullOrEmpty(singleEntityRule.TargetCriteriaString));

            ////set up a rule for an entity type instead of a certain entity
            //IEntityRule singleTypeRule = EntityRuleForm.AddRuleOnEntity(resource);
            //Assert.IsTrue(singleTypeRule.TargetTypeName == typeof(Resource).Name && singleTypeRule.SourceTypeName == typeof(Resource).Name);


            ////set up a rule for an entity type that relies on values from another entity type
            //IEntityRule linkedTypeRule = EntityRuleForm.AddRuleOnEntity(resource, typeof(JarsJob));
            //Assert.IsTrue(linkedTypeRule.TargetTypeName == typeof(Resource).Name
            //    && linkedTypeRule.SourceTypeName == typeof(JarsJob).Name
            //    && !string.IsNullOrEmpty(linkedTypeRule.SourceCriteriaString)
            //    && !string.IsNullOrEmpty(linkedTypeRule.TargetCriteriaString));


            singleEntityRule = JarsRulePopupForm.EditRuleOnEntity(resource, singleEntityRule);
            Assert.IsTrue(!string.IsNullOrEmpty(singleEntityRule.TargetCriteriaString));

        }

        [TestMethod]
        public async Task Test_Combobox_items_display_Test()//needs to be Task and without an assert statement the async method hangs?!
        {
            IList<Type> types = await AssemblyLoaderUtil.FindAllEntityTypesThatAllowRules();
            Assert.IsNotNull(types);
            TestForm frm = new TestForm
            {
                ShowInTaskbar = false //<-- this enable forms to show up for testing! otherwise it doesn't show.
            };

            //List<TypeNameItem> ojs = new List<TypeNameItem>();
            //foreach (var type in types)
            //{
            //    ojs.Add(new TypeNameItem(type));
            //}

            frm.LoadComboboxWithItems(types);
            frm.ShowDialog();

        }

        [TestMethod]
        public void Test_login_form()
        {
            JsonServiceClient authClient = new JsonServiceClient("https://localhost:44320");
            LoginRegisterForm frm = new LoginRegisterForm(authClient)
            {
                ShowInTaskbar = false //<-- this enable forms to show up for testing! otherwise it doesn't show.
            };
            var authResp = frm.LoginOrRegister();
            Assert.IsNotNull(authResp);
            Assert.IsNotNull(authResp.BearerToken);
        }
    }
}

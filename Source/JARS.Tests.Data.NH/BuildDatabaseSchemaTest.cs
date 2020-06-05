using System;
using System.Diagnostics;
using JARS.Data.NH;
using JARS.Data.NH.Jars;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Tests.Data.NH
{
    [TestClass]
    public class BuildDatabaseSchemaTest
    {
        private DataContextNhJars _DataContext;
        public DataContextNhJars DBContext
        {
            get
            {
                if (_DataContext == null)
                    _DataContext = new DataContextNhJars();
                return _DataContext;
            }
        }

        [TestInitialize]
        public void AutoInitialized()
        {
            bool pass = false;
            try
            {
                pass = !DBContext.SessionFactory.IsClosed;
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                pass = false;
#if DEBUG
                throw ex;
#endif
            }
            Assert.IsTrue(pass);
        }


        [TestMethod]
        public void Drop_Database()
        {
            string error = "";
            try
            {
                DBContext.DropDatabaseTables();
            }
            catch (Exception ex)
            {
                error = ex.Message;
#if DEBUG
                throw ex;
#endif
            }
            Assert.IsTrue(error == "");
        }

        [TestMethod]
        public void Update_Database()
        {
            string error = "";
            try
            {
                DBContext.UpdateDatabaseTableSchemas();
            }
            catch (Exception ex)
            {
                error = ex.Message;
#if DEBUG
                throw ex;
#endif
            }
            Assert.IsTrue(error == "");
        }

        [TestMethod]
        public void Create_Database()
        {
            string error = "";
            try
            {
                DBContext.CreateDatabaseTableSchemas();
            }
            catch (Exception ex)
            {
                error = ex.Message;
#if DEBUG
                throw ex;
#endif
            }
            Assert.IsTrue(error == "");
        }
    }
}

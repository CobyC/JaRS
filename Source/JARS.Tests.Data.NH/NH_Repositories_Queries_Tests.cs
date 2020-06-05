using JARS.BOS.Data;
using JARS.BOS.Entities;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace JARS.Tests.Data.NH
{
    [TestClass]
    public class NH_Repositories_Queries_Tests
    {
        [Import]
        IDataRepositoryFactory _repFactory;

        [Import(typeof(IDataContextBOS))]
        IDataContextBaseNh ExternalContext;
        [Import(typeof(IDataContextNhJars))]
        IDataContextBaseNh JarsContext;
        

        public NH_Repositories_Queries_Tests(IDataRepositoryFactory repFactory, IDataContextBaseNh externalContext, IDataContextBaseNh jarsContext, IDataContextBaseNh qlContext)
        {
            _repFactory = repFactory;
            ExternalContext = externalContext;
            JarsContext = jarsContext;
           
        }


        [TestInitialize]
        public void Initialize()
        {
            JarsCore.Container = MEFBusinessLoader.Init();//<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]
        }



        [TestMethod]
        public void query_information_using_queryover_method_generic_repositories()
        {
            IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars> jarsRepo = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();
          
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> extRepo = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();

            //var jQry = QueryOver.Of<JarsJob>().Where(j => j.Id > 0).And(j => j.CreatedBy.IsLike("MEF",MatchMode.Anywhere)).OrderBy(j => j.CreatedDate).Asc;
            //IList<JarsJob> jList = jarsRepo.QueryOverOf(jQry);
            IList<JarsJob> jList = jarsRepo.Where(j => j.Id > 0 && j.CreatedBy.IsLike("MEF", MatchMode.Anywhere)).OrderBy(j => j.CreatedBy).ToList();

            Assert.IsTrue(jList.Count > 0);

            jList = jarsRepo.Where(j => j.Id > 1 && j.Id < 3);
            Assert.IsTrue(jList.Count == 1);


            //var xQry = QueryOver.Of<ExternalJobHeader>().Where(j => j.Id > 0).And(j => j.ExtRefID == "123").OrderBy(j => j.Priority).Desc;
            IList<BOSEntity> xList = extRepo.Where(j => j.Id > 0 && j.ExtRefId == "123").OrderByDescending(j => j.Priority).ToList();
            Assert.IsTrue(xList.Count > 0);

        }



    }
}

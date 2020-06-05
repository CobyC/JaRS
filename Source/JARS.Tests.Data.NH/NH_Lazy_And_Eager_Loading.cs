using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Core.Utils;
using JARS.Data.FakeData;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;

namespace JARS.Tests.Data.NH
{
    [TestClass]
    public class NH_Lazy_And_Eager_Loading
    {
        [Import]
        IDataRepositoryFactory _repFactory;

        public NH_Lazy_And_Eager_Loading(IDataRepositoryFactory repFactory)
        {
            _repFactory = repFactory;
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void Initialize()
        {
            JarsCore.Container = MEFBusinessLoader.Init(); //<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]            
        }

        [TestMethod]
        public void Test_Lazy_Loading()
        {
            var jarsRep = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();

            var jobList = FakeDataHelper.FakeJarsJobs;
            foreach (var j in jobList)
            {
                j.Id = 0;
                foreach (var jl in j.JobLines)
                {
                    jl.Id = 0;
                    //j.Resource = null;
                }
                //j.Resource = null;
            }

            jarsRep.CreateUpdateList(jobList, "TEST_RUN");

            var items = jarsRep.GetAll(true);

            Assert.IsTrue(items[0].JobLines.Count > 0);
            //Assert.IsTrue(items[0].Resource != null);
        }


        [TestMethod]
        public void Linq_QueryBuilder_select_data_test_with_nHibernate_NotEager()
        {
            JarsJob jj = new JarsJob();
            var _repository = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();

            Expression<Func<JarsJob, bool>> query = null;
            bool hasWhere = false;

            int Id = 1;
            string ExtRefId = "11";


            //Id
            if (Id != 0)
            {
                query = LinqExpressionBuilder.True<JarsJob>().And(j => j.Id == Id);
                hasWhere = true;
            }

            //ExtRefId
            if (ExtRefId != "0")
                if (!hasWhere)
                {
                    query = LinqExpressionBuilder.True<JarsJob>().And(j => j.ExtRefId == ExtRefId);
                    hasWhere = true;
                }
                else
                    query.And(j => j.ExtRefId == ExtRefId);

            var res = _repository.Where(query);
            Assert.IsNotNull(res);

            int[] ids = new[] { 1, 2, 3 };
            query = LinqExpressionBuilder.True<JarsJob>().And(j => ids.ToList().Contains(j.Id));

            var rIn = _repository.Where(query);
            Assert.IsNotNull(rIn);

            query = LinqExpressionBuilder.True<JarsJob>().And(j => j.LineOfWork.Like("MU%"));
            var rLike = _repository.Where(query);
            Assert.IsNotNull(rLike);
        }


        [TestMethod]
        public void LinqQueryBuilder_select_data_test_with_nHibernate_Eager()
        {
            JarsJob jj = new JarsJob();
            var _repository = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();

            Expression<Func<JarsJob, bool>> query = null;
            bool hasWhere = false;

            int Id = 1;
            string ExtRefId = "11";


            //Id
            if (Id != 0)
            {
                query = LinqExpressionBuilder.True<JarsJob>().And(j => j.Id == Id);
                hasWhere = true;
            }

            //ExtRefId
            if (ExtRefId != "0")
                if (!hasWhere)
                {
                    query = LinqExpressionBuilder.True<JarsJob>().And(j => j.ExtRefId == ExtRefId);
                    hasWhere = true;
                }
                else
                    query.And(j => j.ExtRefId == ExtRefId);

            var res = _repository.Where(query, true);
            Assert.IsNotNull(res);

            int[] ids = new[] { 1, 2, 3 };
            query = LinqExpressionBuilder.True<JarsJob>().And(j => ids.ToList().Contains(j.Id));

            var rIn = _repository.Where(query, true);
            Assert.IsNotNull(rIn);

            string[] refs = new[] { "11", "33", "55" };
            query = LinqExpressionBuilder.True<JarsJob>().And(j => j.LineOfWork.Like("MU%"));

            var rLike = _repository.Where(query, true);
            Assert.IsNotNull(rLike);

        }
    }
}

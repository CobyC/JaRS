using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JARS.Core.Extensions;
using JARS.Data.FakeData;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Test.Miscellaneous
{
    [TestClass]
    public class PropertiesExpressionTest
    {
        [TestMethod]
        public void Extract_List_Type_Properties()
        {
            List<Expression<Func<JarsJob, object>>> list = GetListPropertiesAsExpressionList<JarsJob>();

        }

        internal List<Expression<Func<T, object>>> GetListPropertiesAsExpressionList<T>()
        {
            List<Expression<Func<T, object>>> list = new List<Expression<Func<T, object>>>();
            IEnumerable<PropertyInfo> pi = typeof(T).GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.Name == "IList`1");
            foreach (var p in pi)
            {
                var param = System.Linq.Expressions.Expression.Parameter(typeof(T));
                var field = System.Linq.Expressions.Expression.PropertyOrField(param, p.Name);
                list.Add(System.Linq.Expressions.Expression.Lambda<Func<T, object>>(field, param));
            }
            return list;
        }

        [TestMethod]
        public void Get_SubList_properties_and_types()
        {

            JarsJob job = FakeDataHelper.FakeJarsJobs[0];

            IList<Type> jarsTypes = job.GetGenericListTypes();
            Assert.AreNotEqual(0, jarsTypes.Count);

            //QLJob qlJob = FakeDataHelper.FakeQLJobs[0];
            //IList<Type> qlTypes = job.GetGenericListTypes();
            //Assert.AreNotEqual(0, qlTypes.Count);

            IList<Type> oneSubs = FakeDataHelper.FakeUserAccount.GetGenericListTypes();
            Assert.AreNotEqual(0, oneSubs.Count);

            //IList<Type> noSubs = FakeDataHelper.FakeExternalLines[0].GetGenericListTypes();
            //Assert.AreEqual(0, noSubs.Count);

        }



    }
}

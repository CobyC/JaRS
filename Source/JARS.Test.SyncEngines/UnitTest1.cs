using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Test.SyncEngines
{
    [TestClass]
    public class UnitTest1
    {
        bool IsBusyJobs, IsBusyApp = false;
        object lockObj = new object();



        [TestMethod]
        public void TestMethod1()
        {

            RunMultipleTimes();
            Thread.Sleep(1000);

            while (true)
            {
                Thread.Sleep(1000);
                Debug.WriteLine("Waiting..");
            }

        }


        void RunMultipleTimes()
        {
            var x = LoadResourceData().Result;

        }


        //create a task that runs and fires an event when completed...
        //use TaskCompletionSource       


        public Task<IList<int>> LoadResourceData()
        {
            //TaskCompletionSource<IList<int>> tcs = new TaskCompletionSource<IList<int>>();
            GetDataEngine eng = new GetDataEngine();
            //eng.FinishedLoading += Eng_FinishedLoading;
            //Debug.WriteLine("OP - Start");

            //EventHandler<IList<int>> evt = (s, e) => tcs.TrySetResult(null);
            //try
            //{
            //    eng.FinishedLoading += evt;
            //    eng.LoadTheData();

            //}
            //finally
            //{ eng.FinishedLoading -= evt; }
            //return tcs.Task;
            eng.GetDataTask.Wait();
            return eng.GetDataTask;
        }

        private void Eng_FinishedLoading(object sender, IList<int> e)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> LoadJobData(DateTime fromTime)
        {
            if (!IsBusyJobs)
            {
                Debug.WriteLine("JOB - Start");
                var busyness = new Progress<bool>(busy => { IsBusyJobs = busy; });
                List<int> response = await Task<List<int>>.Run(() =>
                {
                    ((IProgress<bool>)busyness).Report(true);
                    //do something that takes a long time
                    List<int> retlist = new List<int>();
                    for (int i = 0; i < 100; i++)
                    {
                        retlist.Add(i);
                        Thread.Sleep(200);
                    }
                    return retlist;
                });
                ((IProgress<bool>)busyness).Report(false);
                Debug.WriteLine("JOB - End");
                return response;
            }
            else
                return null;
        }

        public async Task<List<int>> LoadApptData(DateTime fromTime)
        {
            if (!IsBusyApp)
            {
                Debug.WriteLine("APPT - Start");
                var busyness = new Progress<bool>(busy => { IsBusyApp = busy; });
                List<int> response = await Task<List<int>>.Run(() =>
                {
                    ((IProgress<bool>)busyness).Report(true);
                    //do something that takes a long time
                    List<int> retlist = new List<int>();
                    for (int i = 0; i < 40; i++)
                    {
                        retlist.Add(i);
                        Thread.Sleep(300);
                    }
                    return retlist;
                });
                ((IProgress<bool>)busyness).Report(false);
                Debug.WriteLine("APPT - END");
                return response;
            }
            else
                return null;
        }


        class GetDataEngine
        {
            //public event EventHandler<IList<int>> FinishedLoading;
            public GetDataEngine()
            { }

            private TaskCompletionSource<IList<int>> _getDataTaskCompletionSource = new TaskCompletionSource<IList<int>>();
            public Task<IList<int>> GetDataTask
            {
                get
                {
                    return _getDataTaskCompletionSource.Task;
                }
            }


            //public Task<IList<int>> LoadTheData()
            public void LoadTheData()
            {
                List<int> retlist = new List<int>();
                for (int i = 0; i < 20; i++)
                {
                    retlist.Add(i);
                    Thread.Sleep(500);
                }

                //FinishedLoading = (s, e) => _getDataTaskCompletionSource.TrySetResult(retlist);
                _getDataTaskCompletionSource.TrySetResult(retlist);

                //return _getDataTaskCompletionSource.Task;
            }
        }


    }
}

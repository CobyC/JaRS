using JARS.Entities;
using System;
using System.Collections.Generic;

namespace JARS.Data.FakeData
{
    public class FakeDataHelper
    {
        //for using fake data in memory
        //static int[] ids = Enumerable.Range(0, 20 + 1).ToArray();

        //for loading fake data to database
        static int[] ids = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        static string[] loWork = new[] { "CARP", "PLUM", "FENC", "MULT", "ELEC" };
        static string[] tCode = new[] { "CP", "PL", "FN", "MT", "EL" };
        static string[] statCodes = new[] { "NEW", "INPROG", "HOLD", "CANCEL", "COMP" };

        static IList<JarsResourceGroup> _FakeResourceGroup;
        public static IList<JarsResourceGroup> FakeResourceGroups
        {
            get
            {
                if (_FakeResourceGroup == null)
                {
                    _FakeResourceGroup = new List<JarsResourceGroup>();
                    JarsResourceGroup g1 = new JarsResourceGroup { Id = ids[1], Code = tCode[0], IsActive = true, Name = "Carpenters", SortIndex = 0 };
                    JarsResourceGroup g2 = new JarsResourceGroup { Id = ids[2], Code = tCode[1], IsActive = true, Name = "Plumbers", SortIndex = 1 };
                    JarsResourceGroup g3 = new JarsResourceGroup { Id = ids[3], Code = tCode[2], IsActive = true, Name = "Fencer", SortIndex = 2 };
                    JarsResourceGroup g4 = new JarsResourceGroup { Id = ids[2], Code = tCode[4], IsActive = true, Name = "Electricians", SortIndex = 3 };

                    g1.Resources.Add(FakeResources[0]);
                    g2.Resources.Add(FakeResources[1]);
                    g3.Resources.Add(FakeResources[2]);
                    g4.Resources.Add(FakeResources[3]);

                    _FakeResourceGroup.Add(g1);
                    _FakeResourceGroup.Add(g2);
                    _FakeResourceGroup.Add(g3);
                    _FakeResourceGroup.Add(g4);
                }

                return _FakeResourceGroup;
            }
        }

        static IList<JarsResource> _FakeResources;
        public static IList<JarsResource> FakeResources
        {
            get
            {
                if (_FakeResources == null)
                {
                    _FakeResources = new List<JarsResource>();
                    JarsResource o1 = new JarsResource
                    {
                        Id = ids[1],
                        IsActive = true,
                        IsMobileResource = true,
                        DayStartTime = TimeSpan.FromHours(7.30),
                        ExtRef1 = "OP1",
                        VehicleRegistration = "VEH1",
                        DisplayName = "Opername Oneeee"

                    };

                    JarsResource o2 = new JarsResource
                    {
                        Id = ids[2],
                        IsActive = true,
                        IsMobileResource = true,
                        DayStartTime = TimeSpan.FromHours(8.00),
                        ExtRef1 = "OP2",
                        VehicleRegistration = "VEH2",
                        DisplayName = "Opername Twoooo"
                    };
                    JarsResource o3 = new JarsResource
                    {
                        Id = ids[3],
                        IsActive = true,
                        IsMobileResource = true,
                        DayStartTime = TimeSpan.FromHours(8.30),
                        ExtRef1 = "OP3",
                        VehicleRegistration = "VEH3",
                        DisplayName = "Opername Threeee"
                    };

                    JarsResource o4 = new JarsResource
                    {
                        Id = ids[4],
                        IsActive = true,
                        IsMobileResource = false,
                        DayStartTime = TimeSpan.FromHours(9.00),
                        ExtRef1 = "OP4",
                        VehicleRegistration = "VEH4",
                        DisplayName = "Opername Fooour"
                    };

                    _FakeResources.Add(o1);
                    _FakeResources.Add(o2);
                    _FakeResources.Add(o3);
                    _FakeResources.Add(o4);
                }
                return _FakeResources;
            }
            set
            {
                _FakeResources = value;
            }

        }
        //static IList<QLJob> _FakeQLJobs;
        //public static IList<QLJob> FakeQLJobs
        //{
        //    get
        //    {
        //        if (_FakeQLJobs == null)
        //        {
        //            _FakeQLJobs = new List<QLJob>();

        //            QLJob j1 = new QLJob
        //            {
        //                Id = ids[1],
        //                ExtRefId = "11",
        //                StartDate = DateTime.Today.AddHours(4.0),
        //                EndDate = DateTime.Today.AddHours(6.0),
        //                Location = "Test1",
        //                ResourceId = FakeResources[0].Id,
        //                Description = "Test Fake Job 1",
        //                ApptSlot = "FR",
        //                Priority = "EM",
        //                ProgressStatus = statCodes[0],
        //                LineOfWork = loWork[0],
        //                StatusKey = 1.ToString(),
        //                LabelKey = 1.ToString()

        //            };
        //            j1.JobLines.Add(new QLJobLine { Id = ids[1], LineNum = 1, LineCode = "CODE1", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 1", Resource = FakeResources[0] });
        //            j1.JobLines.Add(new QLJobLine { Id = ids[2], LineNum = 2, LineCode = "CODE2", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 1", Resource = FakeResources[0] });

        //            QLJob j2 = new QLJob
        //            {
        //                Id = ids[2],
        //                ExtRefId = "22",
        //                StartDate = DateTime.Today.AddHours(4.0),
        //                EndDate = DateTime.Today.AddHours(6.0),
        //                Location = "Test2",
        //                ResourceId = FakeResources[1].Id,
        //                Description = "Test Fake Job 2",
        //                ApptSlot = "AM",
        //                Priority = "7D",
        //                ProgressStatus = statCodes[1],
        //                LineOfWork = loWork[1],
        //                StatusKey = 2.ToString(),
        //                LabelKey = 1.ToString()
        //            };
        //            j2.JobLines.Add(new QLJobLine { Id = ids[3], LineNum = 1, LineCode = "CODE3", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 2", Resource = FakeResources[1] });
        //            j2.JobLines.Add(new QLJobLine { Id = ids[4], LineNum = 2, LineCode = "CODE4", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 2", Resource = FakeResources[1] });

        //            QLJob j3 = new QLJob
        //            {
        //                Id = ids[3],
        //                ExtRefId = "33",
        //                StartDate = DateTime.Today.AddHours(4.0),
        //                EndDate = DateTime.Today.AddHours(6.0),
        //                Location = "Test3",
        //                ResourceId = FakeResources[2].Id,
        //                Description = "Test Fake Job 3",
        //                ApptSlot = "PM",
        //                Priority = "SA",
        //                ProgressStatus = statCodes[3],
        //                LineOfWork = loWork[2],
        //                StatusKey = 3.ToString(),
        //                LabelKey = 3.ToString()
        //            };
        //            j3.JobLines.Add(new QLJobLine { Id = ids[5], LineNum = 1, LineCode = "CODE5", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 3", Resource = FakeResources[2] });
        //            j3.JobLines.Add(new QLJobLine { Id = ids[6], LineNum = 2, LineCode = "CODE6", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 3", Resource = FakeResources[2] });

        //            QLJob j4 = new QLJob
        //            {
        //                Id = ids[4],
        //                ExtRefId = "44",
        //                StartDate = DateTime.Today.AddHours(7.0),
        //                EndDate = DateTime.Today.AddHours(9.0),
        //                Location = "Test4",
        //                ResourceId = FakeResources[0].Id,
        //                Description = "Test Fake Job 4",
        //                ApptSlot = "AM",
        //                Priority = "24H",
        //                ProgressStatus = statCodes[2],
        //                LineOfWork = loWork[4],
        //                StatusKey = 1.ToString(),
        //                LabelKey = 2.ToString()
        //            };
        //            j4.JobLines.Add(new QLJobLine { Id = ids[7], LineNum = 1, LineCode = "CODE7", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 4", Resource = FakeResources[0] });
        //            j4.JobLines.Add(new QLJobLine { Id = ids[8], LineNum = 2, LineCode = "CODE8", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 4", Resource = FakeResources[0] });

        //            QLJob j5 = new QLJob
        //            {
        //                Id = ids[5],
        //                ExtRefId = "55",
        //                StartDate = DateTime.Today.AddHours(7.0),
        //                EndDate = DateTime.Today.AddHours(9.0),
        //                Location = "Test5",
        //                ResourceId = FakeResources[1].Id,
        //                Description = "Test Fake Job 5",
        //                ApptSlot = "FR",
        //                Priority = "24H",
        //                ProgressStatus = statCodes[0],
        //                LineOfWork = loWork[3],
        //                StatusKey = 2.ToString(),
        //                LabelKey = 2.ToString()
        //            };
        //            j5.JobLines.Add(new QLJobLine { Id = ids[9], LineNum = 1, LineCode = "CODE9", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 5", Resource = FakeResources[1] });
        //            j5.JobLines.Add(new QLJobLine { Id = ids[10], LineNum = 2, LineCode = "CODE10", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 5", Resource = FakeResources[1] });

        //            QLJob j6 = new QLJob
        //            {
        //                Id = ids[6],
        //                ExtRefId = "66",
        //                StartDate = DateTime.Today.AddHours(7.0),
        //                EndDate = DateTime.Today.AddHours(9.0),
        //                Location = "Test6",
        //                ResourceId = FakeResources[2].Id,
        //                Description = "Test Fake Job 6",
        //                ApptSlot = "",
        //                Priority = "",
        //                ProgressStatus = statCodes[3],
        //                LineOfWork = loWork[4],
        //                StatusKey = 3.ToString(),
        //                LabelKey = 2.ToString()
        //            };
        //            j6.JobLines.Add(new QLJobLine { Id = ids[11], LineNum = 1, LineCode = "CODE11", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 6", Resource = FakeResources[2] });
        //            j6.JobLines.Add(new QLJobLine { Id = ids[12], LineNum = 2, LineCode = "CODE12", OriginalQty = 1, FullDescription = "Fake SOR line on Fake Job 6", Resource = FakeResources[2] });

        //            _FakeQLJobs.Add(j1);
        //            _FakeQLJobs.Add(j2);
        //            _FakeQLJobs.Add(j3);
        //            _FakeQLJobs.Add(j4);
        //            _FakeQLJobs.Add(j5);
        //            _FakeQLJobs.Add(j6);

        //        }


        //        return _FakeQLJobs;
        //    }
        //}

        static IList<JarsJob> _FakeJarsJobs;
        public static IList<JarsJob> FakeJarsJobs
        {
            /*
              Description = "Test Job",
                        LabelKey = 1,
                        StatusKey = 1,
                        Location = "1 some street AB12 3CD",
                        ProgressStatus = "NEW",
                        LineOfWorkCode = "PRO",

                 */
            get
            {
                if (_FakeJarsJobs == null)
                {
                    _FakeJarsJobs = new List<JarsJob>();

                    JarsJob j1 = new JarsJob
                    {
                        Id = ids[1],
                        ExtRefId = "11",
                        StartDate = DateTime.Today.AddHours(4.0),
                        EndDate = DateTime.Today.AddHours(6.0),
                        ResourceId = FakeResources[0].Id,
                        Description = "Test Fake JARS Job 1",
                        Location = "1 fake JaRS street AB12 3CD",
                        LineOfWork = loWork[0],
                        ProgressStatus = statCodes[0],
                        LabelKey = 1.ToString(),
                        StatusKey = 1.ToString(),

                    };
                    j1.JobLines.Add(new JarsJobLine { Id = ids[1], LineNum = 1, LineCode = "CJARS1", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 1", Resource = FakeResources[0] });
                    j1.JobLines.Add(new JarsJobLine { Id = ids[2], LineNum = 2, LineCode = "CJARS2", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 1", Resource = FakeResources[0] });

                    JarsJob j2 = new JarsJob
                    {
                        Id = ids[2],
                        ExtRefId = "22",
                        StartDate = DateTime.Today.AddHours(4.0),
                        EndDate = DateTime.Today.AddHours(6.0),
                        ResourceId = FakeResources[1].Id,
                        Description = "Test Fake JARS Job 2",
                        Location = "2 fake JaRS street AB12 4CD",
                        LineOfWork = loWork[0],
                        ProgressStatus = statCodes[0],
                        LabelKey = 1.ToString(),
                        StatusKey = 2.ToString(),
                    };
                    j2.JobLines.Add(new JarsJobLine { Id = ids[3], LineNum = 1, LineCode = "CJARS3", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 2", Resource = FakeResources[1] });
                    j2.JobLines.Add(new JarsJobLine { Id = ids[4], LineNum = 2, LineCode = "CJARS4", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 2", Resource = FakeResources[1] });

                    JarsJob j3 = new JarsJob
                    {
                        Id = ids[3],
                        ExtRefId = "33",
                        StartDate = DateTime.Today.AddHours(4.0),
                        EndDate = DateTime.Today.AddHours(6.0),
                        ResourceId = FakeResources[2].Id,
                        Description = "Test Fake JARS Job 3",
                        Location = "3 fake JaRS street AB13 1CD",
                        LineOfWork = loWork[1],
                        ProgressStatus = statCodes[1],
                        LabelKey = 2.ToString(),
                        StatusKey = 3.ToString(),
                    };
                    j3.JobLines.Add(new JarsJobLine { Id = ids[5], LineNum = 1, LineCode = "CJARS5", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 3", Resource = FakeResources[2] });
                    j3.JobLines.Add(new JarsJobLine { Id = ids[6], LineNum = 2, LineCode = "CJARS6", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 3", Resource = FakeResources[2] });

                    JarsJob j4 = new JarsJob
                    {
                        Id = ids[4],
                        ExtRefId = "44",
                        StartDate = DateTime.Today.AddHours(7.0),
                        EndDate = DateTime.Today.AddHours(9.0),
                        Description = "Test Fake JARS Job 4",
                        ResourceId = FakeResources[0].Id,
                        Location = "4 fake JaRS street AB14 1CD",
                        LineOfWork = loWork[2],
                        ProgressStatus = statCodes[2],
                        LabelKey = 3.ToString(),
                        StatusKey = 3.ToString(),
                    };
                    j4.JobLines.Add(new JarsJobLine { Id = ids[7], LineNum = 1, LineCode = "CJARS7", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 4", Resource = FakeResources[0] });
                    j4.JobLines.Add(new JarsJobLine { Id = ids[8], LineNum = 2, LineCode = "CJARS8", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 4", Resource = FakeResources[0] });

                    JarsJob j5 = new JarsJob
                    {
                        Id = ids[5],
                        ExtRefId = "55",
                        StartDate = DateTime.Today.AddHours(7.0),
                        EndDate = DateTime.Today.AddHours(9.0),
                        ResourceId = FakeResources[1].Id,
                        Description = "Test Fake JARS Job 5",
                        Location = "5 fake JaRS street AB21 1CD",
                        LineOfWork = loWork[3],
                        ProgressStatus = statCodes[3],
                        LabelKey = 1.ToString(),
                        StatusKey = 3.ToString(),
                    };
                    j5.JobLines.Add(new JarsJobLine { Id = ids[9], LineNum = 1, LineCode = "CJARS9", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 5", Resource = FakeResources[1] });
                    j5.JobLines.Add(new JarsJobLine { Id = ids[10], LineNum = 2, LineCode = "CJARS10", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 5", Resource = FakeResources[1] });

                    JarsJob j6 = new JarsJob
                    {
                        Id = ids[6],
                        ExtRefId = "66",
                        StartDate = DateTime.Today.AddHours(7.0),
                        EndDate = DateTime.Today.AddHours(9.0),
                        ResourceId = FakeResources[2].Id,
                        Description = "Test Fake JARS Job 6",
                        Location = "6 fake JaRS street AC23 1CB",
                        LineOfWork = loWork[3],
                        ProgressStatus = statCodes[4],
                        LabelKey = 3.ToString(),
                        StatusKey = 1.ToString(),
                    };
                    j6.JobLines.Add(new JarsJobLine { Id = ids[11], LineNum = 1, LineCode = "CJARS11", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 6", Resource = FakeResources[2] });
                    j6.JobLines.Add(new JarsJobLine { Id = ids[12], LineNum = 2, LineCode = "CJARS12", OriginalQty = 1, FullDescription = "Fake JARS SOR line on Fake Job 6", Resource = FakeResources[2] });

                    _FakeJarsJobs.Add(j1);
                    _FakeJarsJobs.Add(j2);
                    _FakeJarsJobs.Add(j3);
                    _FakeJarsJobs.Add(j4);
                    _FakeJarsJobs.Add(j5);
                    _FakeJarsJobs.Add(j6);

                }


                return _FakeJarsJobs;
            }
        }


        //public static IList<IExternalJob> FakeExternalJobs
        //{
        //    get
        //    {
        //        //IExternalJob j1 = new ExternalJobHeader { ExtJobRef = 111, TradeCode = "EL", DurationInHours = "2.3", FullAddress = "Some Address 111", Postcode = "AB1 2CD", Comment = "Test External Fake Job 1" };
        //        //IExternalJob j2 = new ExternalJobHeader { ExtJobRef = 222, TradeCode = "EL", DurationInHours = "1.3", FullAddress = "Some Address 222", Postcode = "AB3 4CD", Comment = "Test External Fake Job 2" };
        //        //IExternalJob j3 = new ExternalJobHeader { ExtJobRef = 333, TradeCode = "CP", DurationInHours = "0.5", FullAddress = "Some Address 333", Postcode = "AB1 5CD", Comment = "Test External Fake Job 3" };
        //        //IExternalJob j4 = new ExternalJobHeader { ExtJobRef = 444, TradeCode = "CP", DurationInHours = "3.0", FullAddress = "Some Address 444", Postcode = "AB2 2CD", Comment = "Test External Fake Job 4" };
        //        //IExternalJob j5 = new ExternalJobHeader { ExtJobRef = 555, TradeCode = "PL", DurationInHours = "2.8", FullAddress = "Some Address 555", Postcode = "AB4 5CD", Comment = "Test External Fake Job 5" };
        //        //IExternalJob j6 = new ExternalJobHeader { ExtJobRef = 666, TradeCode = "PL", DurationInHours = "1.2", FullAddress = "Some Address 666", Postcode = "AB5 2CD", Comment = "Test External Fake Job 6" };

        //        return new List<IExternalJob>() { j1, j2, j3, j4, j5, j6 };
        //    }
        //}

        //public static IList<IExternalJobLine<int>> FakeExternalLines
        //{
        //    get
        //    {
        //        IExternalJobLine<int> l1 = new ExternalJobLine { ExtJobRef = 111, LineNum = 1, LineCode = "CODE1", Quantity = 1, Rate = 1, ShortDescription = "Some Short Description 111", FullDescription = "Test External Fake Job Line 1 Long Description" };
        //        IExternalJobLine<int> l2 = new ExternalJobLine { ExtJobRef = 222, LineNum = 1, LineCode = "CODE2", Quantity = 2, Rate = 1, ShortDescription = "Some Short Description 222", FullDescription = "Test External Fake Job Line 2 Long Description" };
        //        IExternalJobLine<int> l3 = new ExternalJobLine { ExtJobRef = 333, LineNum = 1, LineCode = "CODE3", Quantity = 1, Rate = 1, ShortDescription = "Some Short Description 333", FullDescription = "Test External Fake Job Line 3 Long Description" };
        //        IExternalJobLine<int> l4 = new ExternalJobLine { ExtJobRef = 444, LineNum = 1, LineCode = "CODE4", Quantity = 3, Rate = 1, ShortDescription = "Some Short Description 444", FullDescription = "Test External Fake Job Line 4 Long Description" };
        //        IExternalJobLine<int> l5 = new ExternalJobLine { ExtJobRef = 555, LineNum = 1, LineCode = "CODE5", Quantity = 5, Rate = 1, ShortDescription = "Some Short Description 555", FullDescription = "Test External Fake Job Line 5 Long Description" };
        //        IExternalJobLine<int> l6 = new ExternalJobLine { ExtJobRef = 666, LineNum = 1, LineCode = "CODE6", Quantity = 1, Rate = 1, ShortDescription = "Some Short Description 666", FullDescription = "Test External Fake Job Line 6 Long Description" };

        //        return new List<IExternalJobLine<int>>() { l1, l2, l3, l4, l5, l6 };
        //    }
        //}
        public static JarsUser FakeUserAccount
        {
            get
            {
                return new JarsUser
                {
                    UserName = "TestAccount",
                    Email = "test@testing.com",
                    Id = ids[1],
                    IsActive = true,
                    UserCode = "TU001",
                    UserCode1 = "TEUSE",
                    UserCode2 = "TUSER",
                };
            }
        }

    }
}






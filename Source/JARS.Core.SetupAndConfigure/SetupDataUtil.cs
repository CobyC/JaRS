using JARS.Entities;
using System;
using System.Collections.Generic;

namespace JARS.Core.SetupAndConfigure
{
    /// <summary>
    /// Class for helping to set up default data in the database.
    /// </summary>
    public class SetupDataUtil
    {
        static DateTime _now = DateTime.Now;
        static string crmo = "SETUP";
        //public static IList<JarsUserRole> GetDefaultUserRoles()
        //{
        //    return new List<JarsUserRole> {
        //        new JarsUserRole { Name="Admin", DefaultPermissions = "CREATE,READ,UPDATE,DELETE", Description="The default Administrator role.", CreatedBy=crmo,CreatedDate=_now },
        //        new JarsUserRole { Name="SuperUsers", DefaultPermissions = "CREATE,READ,UPDATE,DELETE",Description="The default super user role, can add edit and remove records", CreatedBy=crmo,CreatedDate=_now },
        //        new JarsUserRole { Name="PowerUsers", DefaultPermissions = "CREATE,READ,UPDATE",Description="The default power user role. can add and edit records", CreatedBy=crmo,CreatedDate=_now },
        //        new JarsUserRole { Name="NormalUsers", DefaultPermissions = "READ,UPDATE" ,Description="The default Jars user role. can drag and drop and manage jobs", CreatedBy=crmo,CreatedDate=_now },
        //        new JarsUserRole { Name="Guests", DefaultPermissions = "READ" ,Description="The default guest role. can view but not manage jobs.", CreatedBy=crmo,CreatedDate=_now },
        //        new JarsUserRole { Name="WebGuest",DefaultPermissions = "READ" , Description="The default web user role. can view but without details", CreatedBy=crmo,CreatedDate=_now },
        //    };
        //}

        public static IList<JarsUser> GetDefaultUserAccounts()
        {
            return new List<JarsUser> {
                new JarsUser(){ UserName="Admin",IsActive=true, CreatedBy = crmo, CreatedDate=_now, Permissions=new List<string>(){"ALL" }},
            };
        }

        public static IList<LookupValue> GetDefaultLookupValues()
        {
            return new List<LookupValue> {
                //default category and sub categories
                new LookupValue{CategoryCode = "L_U_C", DisplayText="Lookup Categories", Value="L_U_C"  },
                new LookupValue{CategoryCode = "L_U_C", DisplayText="Job Statuses", Value="JOB_STATUS"  },
                new LookupValue{CategoryCode = "L_U_C", DisplayText="Person Title", Value="TITLE"  },
                //Job status category default values
                new LookupValue{CategoryCode = "JOB_STATUS", DisplayText="New", Value="NEW"  },
                new LookupValue{CategoryCode = "JOB_STATUS", DisplayText="In Progress", Value="INPROG"  },
                new LookupValue{CategoryCode = "JOB_STATUS", DisplayText="On Hold", Value="HOLD"  },
                new LookupValue{CategoryCode = "JOB_STATUS", DisplayText="Completed", Value="COMP"  },
                new LookupValue{CategoryCode = "JOB_STATUS", DisplayText="Completed With VO", Value="COMP_VO"  },
                //person title default values.
                new LookupValue{CategoryCode = "TITLE", DisplayText="Mr.", Value="MR"  },
                new LookupValue{CategoryCode = "TITLE", DisplayText="Mrs.", Value="MRS"  }
            };
        }

        public static IList<ApptLabel> GetDefaultAppointmentLabels()
        {
            return new List<ApptLabel>() {
                new ApptLabel { ViewName = "DEFAULT", LabelName = "Default", SortIndex = 0, ColourRGB = -12490271, LabelCriteria= "([LabelKey] = '1')", UseInterfaceType="JARS.Core.Interfaces.Entities.IStatusLabeledEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "CUSTOM", LabelName = "Default", SortIndex = 99, ColourRGB = -12490271, LabelCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.IViewOptionCustomEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "CUSTOM", LabelName = "P1 Priority", SortIndex = 99, ColourRGB = -2517356, LabelCriteria= "([Priority] = 'P1')", UseInterfaceType="JARS.Core.Interfaces.Entities.IViewOptionCustomEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptLabel { ViewName = "LOCATION", LabelName = "Default", SortIndex = 99, ColourRGB = -12490271, LabelCriteria= "(([Location]) is null or (len([Location]) = 0))", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LOCATION", LabelName = "GA1 Location", SortIndex = 99, ColourRGB = -3883625, LabelCriteria= "([LocationCode] like 'GA1%')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LOCATION", LabelName = "GA2 Location", SortIndex = 99, ColourRGB = -6966313, LabelCriteria= "([LocationCode] like 'GA2%')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LOCATION", LabelName = "GA3 Location", SortIndex = 99, ColourRGB = -7156260, LabelCriteria= "([LocationCode] like 'GA3%')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LOCATION", LabelName = "GA4 Location", SortIndex = 99, ColourRGB = -6571175, LabelCriteria= "([LocationCode] like 'GA4%')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptLabel { ViewName = "STATUS", LabelName = "Default", SortIndex = 99, ColourRGB = -12490271, LabelCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "STATUS", LabelName = "New", SortIndex = 99, ColourRGB = -3883625, LabelCriteria= "([ProgressStatus] = 'NEW')", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "STATUS", LabelName = "Started", SortIndex = 99, ColourRGB = -3358247, LabelCriteria= "([ProgressStatus] = 'START')", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "STATUS", LabelName = "Delayed", SortIndex = 99, ColourRGB = -12490271, LabelCriteria= "([ProgressStatus] = 'DELAY')", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "STATUS", LabelName = "Canceled", SortIndex = 99, ColourRGB = -8421505, LabelCriteria= "([ProgressStatus] = 'CANCEL')", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "STATUS", LabelName = "Completed", SortIndex = 99, ColourRGB = -16732080, LabelCriteria= "([ProgressStatus] = 'COMP')", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptLabel { ViewName = "LINEOFWORK", LabelName = "Default", SortIndex = 99, ColourRGB = -12490271, LabelCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LINEOFWORK", LabelName = "WRK Label", SortIndex = 99, ColourRGB = -3942757, LabelCriteria= "([LineOfWork] = 'WRK')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LINEOFWORK", LabelName = "FN Label", SortIndex = 99, ColourRGB = -7156260, LabelCriteria= "([LineOfWork] = 'FN')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LINEOFWORK", LabelName = "EL Label", SortIndex = 99, ColourRGB = -16384, LabelCriteria= "([LineOfWork] = 'EL')", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
                new ApptLabel { ViewName = "LINEOFWORK", LabelName = "PLU or PLUMB Label", SortIndex = 99, ColourRGB = -16384, LabelCriteria= "(([LineOfWork] = 'PLU') Or ([LineOfWork] = 'PLUMB'))", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" }
            };
        }

        public static IList<ApptStatus> GetDefaultAppointmentStatuses()
        {
            return new List<ApptStatus>() {
                new ApptStatus { ViewName = "DEFAULT", StatusName = "Default", SortIndex = 0, ColourRGB = -12490271, StatusCriteria= "([StatusKey] = '1')", UseInterfaceType="JARS.Core.Interfaces.Entities.IStatusLabeledEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptStatus { ViewName = "CUSTOM", StatusName = "Default", SortIndex = 99, ColourRGB = -12490271, StatusCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.IViewOptionCustomEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptStatus { ViewName = "LOCATION", StatusName = "Default", SortIndex = 99, ColourRGB = -12490271, StatusCriteria= "(([Location]) is null or (len([Location]) = 0))", UseInterfaceType="JARS.Core.Interfaces.Entities.ILocatableEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptStatus { ViewName = "STATUS", StatusName = "Default", SortIndex = 99, ColourRGB = -12490271, StatusCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.IProgressStatusEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },

                new ApptStatus { ViewName = "LINEOFWORK", StatusName = "Default", SortIndex = 99, ColourRGB = -12490271, StatusCriteria= "", UseInterfaceType="JARS.Core.Interfaces.Entities.ILineOfWorkEntity, JARS.Core.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
            };
        }

        public static IList<JarsResourceGroup> GetDefaultResourceGroup()
        {
            List<JarsResourceGroup> _ResourceGroup = new List<JarsResourceGroup>();
            var carp = new JarsResourceGroup {Code = "CP", IsActive = true, Name = "Carpenters", SortIndex = 0, CreatedBy = crmo };
            //JarsResource cp1 = new JarsResource
            //{
            //    IsActive = true,
            //    IsMobileResource = true,
            //    DayStartTime = TimeSpan.FromHours(7.30),
            //    DayEndTime = TimeSpan.FromHours(7.30).Add(TimeSpan.FromHours(8)),
            //    ExtRef = "CRP01",
            //    VehicleRegistration = "VEH1",
            //    FirstName = "Carpin",
            //    LastName = "Terra",
            //    CreatedBy = crmo,
            //    MobileNo = "01234567890",
            //    eMail = "cp1@test.com"
            //    //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Carpenter", ExpiryMatters = false, DocumentCode = "CAPRQUAL01" } }
            //};
            //cp1.DisplayName = cp1.GenerateDisplayName(false, true, true);
            //carp.Resources.Add(cp1);
            _ResourceGroup.Add(carp);

            var el = new JarsResourceGroup { Code = "EL", IsActive = true, Name = "Electricians", SortIndex = 1, CreatedBy = crmo };
            //JarsResource el1 = new JarsResource
            //{
            //    IsActive = true,
            //    IsMobileResource = true,
            //    DayStartTime = TimeSpan.FromHours(8.00),
            //    DayEndTime = TimeSpan.FromHours(7.30).Add(TimeSpan.FromHours(8)),
            //    ExtRef = "ELE01",
            //    VehicleRegistration = "VEH2",
            //    FirstName = "Elli",
            //    LastName = "Tris",
            //    CreatedBy = crmo,
            //    MobileNo = "01234567890",
            //    eMail = "el1@test.com"
            //    //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Electrician", ExpiryMatters = true, DocumentCode = "ELECQUAL01", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddYears(2) } }
            //};
            //el1.DisplayName = el1.GenerateDisplayName(false, true, true);
            //el.Resources.Add(el1);
            _ResourceGroup.Add(el);

            var plu = new JarsResourceGroup { Code = "PL", IsActive = true, Name = "Plumbers", SortIndex = 2, CreatedBy = crmo };
            //JarsResource pl1 = new JarsResource
            //{
            //    IsActive = true,
            //    IsMobileResource = true,
            //    DayStartTime = TimeSpan.FromHours(8.30),
            //    DayEndTime = TimeSpan.FromHours(8.30).Add(TimeSpan.FromHours(8)),
            //    ExtRef = "PLU01",
            //    VehicleRegistration = "VEH3",
            //    FirstName = "Plumbi",
            //    LastName = "Erro",
            //    CreatedBy = crmo,
            //    MobileNo = "01234567890",
            //    eMail = "pl1@test.com"
            //    //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Plumber", ExpiryMatters = true, DocumentCode = "PLUMBQUAL01", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddYears(2) } }
            //};
            //pl1.DisplayName = pl1.GenerateDisplayName(false, true, true);
            //plu.Resources.Add(pl1);
            _ResourceGroup.Add(plu);

            var fen = new JarsResourceGroup {Code = "FN", IsActive = true, Name = "Fencing", SortIndex = 3, CreatedBy = crmo };
            //JarsResource fn1 = new JarsResource
            //{
            //    IsActive = true,
            //    IsMobileResource = true,
            //    DayStartTime = TimeSpan.FromHours(8.00),
            //    DayEndTime = TimeSpan.FromHours(8.00).Add(TimeSpan.FromHours(8)),
            //    ExtRef1 = "FEN01",
            //    VehicleRegistration = "VEH4",
            //    FirstName = "Fenn",
            //    LastName = "Sirr",
            //    CreatedBy = crmo,
            //    MobileNo = "01234567890",
            //    eMail = "fn1@test.com"
            //    //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Carpenter", ExpiryMatters = false, DocumentCode = "FENCQUAL01" } }
            //};
            //fn1.DisplayName = fn1.GenerateDisplayName(false, true, true);
            //fen.Resources.Add(fn1);
            _ResourceGroup.Add(fen);

            var mlt = new JarsResourceGroup { Code = "MT", IsActive = true, Name = "Multi Traders", SortIndex = 5, CreatedBy = crmo };
            _ResourceGroup.Add(mlt);

            var eqp = new JarsResourceGroup { Code = "EQUIP", IsActive = true, Name = "Equipment", SortIndex = 6, CreatedBy = crmo };
            //JarsResource ph1 = new JarsResource
            //{
            //    IsActive = true,
            //    IsMobileResource = false,
            //    DayStartTime = TimeSpan.FromHours(8),
            //    DayEndTime = TimeSpan.FromHours(8).Add(TimeSpan.FromHours(8)),
            //    VehicleRegistration = "",
            //    FirstName = "Cherry",
            //    LastName = "Picker",
            //    CreatedBy = crmo,
            //};
            //ph1.DisplayName = ph1.GenerateDisplayName(false, true, true);
            //eqp.Resources.Add(ph1);
            _ResourceGroup.Add(eqp);

            return _ResourceGroup;
        }

        public static IList<JarsResource> GetDefaultResources()
        {
            IList<JarsResource> _Resources = new List<JarsResource>();
            JarsResource cp1 = new JarsResource
            {
                IsActive = true,
                IsMobileResource = true,
                DayStartTime = TimeSpan.FromHours(7.30),
                DayEndTime = TimeSpan.FromHours(7.30).Add(TimeSpan.FromHours(8)),
                ExtRef = "CRP01",
                VehicleRegistration = "VEH1",
                FirstName = "Carpin",
                LastName = "Terra",
                CreatedBy = crmo,
                MobileNo = "01234567890",
                eMail = "cp1@test.com"
                //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Carpenter", ExpiryMatters = false, DocumentCode = "CAPRQUAL01" } }
            };
            cp1.DisplayName = cp1.GenerateDisplayName(false, true, true);
            //cp1.Groups.Add(new JarsResourceGroup { Code = "CP", IsActive = true, Name = "Carpenters", SortIndex = 0, CreatedBy = crmo });

            JarsResource el1 = new JarsResource
            {
                IsActive = true,
                IsMobileResource = true,
                DayStartTime = TimeSpan.FromHours(8.00),
                DayEndTime = TimeSpan.FromHours(7.30).Add(TimeSpan.FromHours(8)),
                ExtRef = "ELE01",
                VehicleRegistration = "VEH2",
                FirstName = "Elli",
                LastName = "Tris",
                CreatedBy = crmo,
                MobileNo = "01234567890",
                eMail = "el1@test.com"
                //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Electrician", ExpiryMatters = true, DocumentCode = "ELECQUAL01", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddYears(2) } }
            };
            el1.DisplayName = el1.GenerateDisplayName(false, true, true);
            //el1.Groups.Add(new JarsResourceGroup { Code = "EL", IsActive = true, Name = "Electricians", SortIndex = 1, CreatedBy = crmo });

            JarsResource pl1 = new JarsResource
            {
                IsActive = true,
                IsMobileResource = true,
                DayStartTime = TimeSpan.FromHours(8.30),
                DayEndTime = TimeSpan.FromHours(8.30).Add(TimeSpan.FromHours(8)),
                ExtRef = "PLU01",
                VehicleRegistration = "VEH3",
                FirstName = "Plumbi",
                LastName = "Erro",
                CreatedBy = crmo,
                MobileNo = "01234567890",
                eMail = "pl1@test.com"
                //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Certified Plumber", ExpiryMatters = true, DocumentCode = "PLUMBQUAL01", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddYears(2) } }
            };
            pl1.DisplayName = pl1.GenerateDisplayName(false, true, true);
            //pl1.Groups.Add(new JarsResourceGroup {  Code = "PL", IsActive = true, Name = "Plumbers", SortIndex = 2, CreatedBy = crmo });

            JarsResource fn1 = new JarsResource
            {
                IsActive = true,
                IsMobileResource = true,
                DayStartTime = TimeSpan.FromHours(8.00),
                DayEndTime = TimeSpan.FromHours(8.00).Add(TimeSpan.FromHours(8)),
                ExtRef1 = "FEN01",
                VehicleRegistration = "VEH4",
                FirstName = "Fenn",
                LastName = "Sirr",
                CreatedBy = crmo,
                MobileNo = "01234567890",
                eMail = "fn1@test.com"
                //Skills = new List<OperativeSkill>() { new OperativeSkill { CurrentLevel = 100, Description = "Carpenter", ExpiryMatters = false, DocumentCode = "FENCQUAL01" } }
            };
            fn1.DisplayName = fn1.GenerateDisplayName(false, true, true);
            //fn1.Groups.Add(new JarsResourceGroup { Code = "FN", IsActive = true, Name = "Fencing", SortIndex = 3, CreatedBy = crmo });

            JarsResource ph1 = new JarsResource
            {
                IsActive = true,
                IsMobileResource = false,
                DayStartTime = TimeSpan.FromHours(8),
                DayEndTime = TimeSpan.FromHours(8).Add(TimeSpan.FromHours(8)),
                VehicleRegistration = "",
                FirstName = "Cherry",
                LastName = "Picker",
                CreatedBy = crmo,
            };
            ph1.DisplayName = ph1.GenerateDisplayName(false, true, true);
            //ph1.Groups.Add(new JarsResourceGroup {  Code = "MT", IsActive = true, Name = "Multi Traders", SortIndex = 5, CreatedBy = crmo });
            //ph1.Groups.Add(new JarsResourceGroup {  Code = "EQUIP", IsActive = true, Name = "Equipment", SortIndex = 6, CreatedBy = crmo });


            _Resources.Add(cp1);
            _Resources.Add(el1);
            _Resources.Add(pl1);
            _Resources.Add(fn1);
            _Resources.Add(ph1);

            return _Resources;
        }

        public static IList<JarsDefaultAppointment> GetDefaultAppointments()
        {
            List<JarsDefaultAppointment> defAppts = new List<JarsDefaultAppointment>();

            defAppts.Add(new JarsDefaultAppointment() { Subject = "Meetings", Description = "On or Off site meetings", DefaultDuration = 1, ShowOnMobile = true, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Training", Description = "On or Off site training", DefaultDuration = 2, ShowOnMobile = true, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Lunch", Description = "Standard lunch hour.", DefaultDuration = 1, ShowOnMobile = false, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Half day", Description = "Half a day taken as holiday or leave.", DefaultDuration = 4, ShowOnMobile = false, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Stock take", Description = "Time spend taking stock or doing documentation", DefaultDuration = 0.5, ShowOnMobile = false, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Sickness", Description = "Time taken off as sick", DefaultDuration = 8, ShowOnMobile = false, IsAllDay = false });
            defAppts.Add(new JarsDefaultAppointment() { Subject = "Holiday", Description = "Standard day as holiday", DefaultDuration = 0, ShowOnMobile = false, IsAllDay = true });
            return defAppts;
        }

    }
}

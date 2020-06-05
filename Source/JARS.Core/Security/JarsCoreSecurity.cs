using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Security
{
    /// <summary>
    /// Roles are collections of permissions
    /// </summary>
    public struct JarsRoles
    {
        /// <summary>
        /// Full access, only assigned to admin users
        /// </summary>
        public const string Admin = nameof(Admin);
        /// <summary>
        /// The user role assigned to managers, has more power than a power user.
        /// </summary>
        public const string Manager = nameof(Manager);
        /// <summary>
        /// A user with configuration privileges, lower than admin and manager higher than power user
        /// </summary>
        public const string Configurator = nameof(Configurator);
        /// <summary>
        /// The standard user with some additional privileges
        /// </summary>
        public const string PowerUser = nameof(PowerUser);
        /// <summary>
        /// The standard internal user
        /// </summary>
        public const string User = nameof(User);
        /// <summary>
        /// The lowest privileged user
        /// </summary>
        public const string Guest = nameof(Guest);
        /// <summary>
        /// The user used for mobile applications or external access from 3rd parties.
        /// </summary>
        public const string MobileApp = nameof(MobileApp);

        /// <summary>
        /// Privileged Mobile user for 3rd party access with more power.
        /// </summary>
        public const string MobileAdmin = nameof(MobileAdmin);

        /// <summary>
        /// Roles: Guest
        /// </summary>
        public static string[] External = new string[] { Guest };
        /// <summary>
        /// Roles: Admin, Manager, PowerUser, User, MobileApp
        /// </summary>
        public static string[] Internal = new string[] { Admin, Manager, PowerUser, User };
        /// <summary>
        /// Roles: Admin, Manager, Configurator
        /// </summary>
        public static string[] AppConfig = new string[] { Admin, Manager, Configurator };
        /// <summary>
        /// Roles: Admin, Manager
        /// </summary>
        public static string[] DataAdmin = new string[] { Admin, Manager };
    }

    /// <summary>
    /// Permissions are actions that are allowed (usually contained within a role)
    /// </summary>
    public struct JarsPermissions
    {
        public const string None = nameof(None);
        public const string Full = nameof(Full);
        public const string CanView = nameof(CanView);
        public const string CanAdd = nameof(CanAdd);
        public const string CanEdit = nameof(CanEdit);
        public const string CanDelete = nameof(CanDelete);
        public const string CanAddAppointment = nameof(CanAddAppointment);
        public const string CanEditAppointment = nameof(CanEditAppointment);
        public const string CanDeleteAppointment = nameof(CanDeleteAppointment);
        public const string CanViewAppointment = nameof(CanViewAppointment);

        /// <summary>
        /// Permission: None
        /// </summary>
        public static string[] External = new string[] { None };
        /// <summary>
        /// Permission: CanView, CanViewAppointment
        /// </summary>
        public static string[] Viewers = new string[] { CanView, CanViewAppointment };
        /// <summary>
        /// 
        /// </summary>
        public static string[] Adders = new string[] { CanView, CanAdd, CanViewAppointment, CanAddAppointment };
        /// <summary>
        /// 
        /// </summary>
        public static string[] Editor = new string[] { CanView, CanAdd, CanEdit, CanViewAppointment, CanAddAppointment, CanEditAppointment };
    }
}

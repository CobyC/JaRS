using JARS.Core.Interfaces.Entities;
using ServiceStack;
using ServiceStack.Auth;
using System;
using System.Collections.Generic;

namespace JARS.SS.Auth.Entities
{
    [Serializable]
    public class JarsUserAuth : UserAuth, IEntityBase<int>, IUserAuth, IUserAuthDetailsExtended, IMeta
    {
        public JarsUserAuth()
        {
            Roles = new List<string>();
            Permissions = new List<string>();
        }
        public JarsUserAuth(IUserAuth userAuth)
        {
            Id = userAuth.Id;
            UserName = userAuth.UserName;
            Email = userAuth.Email;
            PrimaryEmail = userAuth.PrimaryEmail;
            FirstName = userAuth.FirstName;
            LastName = userAuth.LastName;
            DisplayName = userAuth.DisplayName;
            PasswordHash = userAuth.PasswordHash;
            Salt = userAuth.Salt;
            DigestHa1Hash = userAuth.DigestHa1Hash;
            CreatedDate = userAuth.CreatedDate;
            ModifiedDate = userAuth.ModifiedDate;
            Permissions = userAuth.Permissions;
            Roles = userAuth.Roles;
        }
        object IEntityBase.Id
        {
            get { return this.Id; }
            set { this.Id = (int)Convert.ChangeType(value, typeof(int)); }
        }

        //<-- mapping nhibernate to access the private field
        public virtual IList<string> RolesBase
        {
            get
            {
                return Roles;
            }
            set => Roles = new List<string>(value);

        }

        //<-- mapping nhibernate to access the private field
        public virtual IList<string> PermissionsBase
        {
            get
            {   
                return Permissions;
            }

            set => Permissions = new List<string>(value);
        }
    }
}

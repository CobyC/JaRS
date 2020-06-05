using JARS.Core.Entities;
using JARS.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents a user account in JaRS.
    /// These are accounts linked to people using he JaRS application.    
    /// </summary>  
    /// 
    [Serializable]
    public class JarsUserBase : AuditableEntityBase, IJarsUserBase
    {
        public JarsUserBase()
        {
            Roles = new List<string>();
            Permissions = new List<string>();
        }

        private string _UserName;
        public virtual string UserName
        {
            get => _UserName;
            set
            {
                _UserName = value;
                OnPropertyChanged(() => UserName);
            }
        }

        private string _DisplayName;
        public virtual string DisplayName
        {
            get => _DisplayName;
            set
            {
                _DisplayName = value;
                OnPropertyChanged(() => DisplayName);
            }
        }

        private string _FirstName;
        public virtual string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                OnPropertyChanged(() => FirstName);
            }
        }

        private string _LastName;
        public virtual string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged(() => LastName);
            }
        }

        private string _Email;
        public virtual string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged(() => Email);
            }
        }

        private string _UserCode;
        /// <summary>
        /// This could be a short code used to identify the user within JaRS or an external system.
        /// ie.JDOE1
        /// </summary>
        public virtual string UserCode
        {
            get => _UserCode;
            set
            {
                _UserCode = value;
                OnPropertyChanged(() => UserCode);
            }
        }

        private string _UserCode1;
        /// <summary>
        /// This is a code that can be used to identify the same person in an external system
        /// ie.JO.DOE1
        /// </summary>
        public virtual string UserCode1
        {
            get => _UserCode1;
            set
            {
                _UserCode1 = value;
                OnPropertyChanged(() => UserCode1);
            }
        }

        private string _UserCode2;
        ///<summary>
        /// This is a code that can be used to identify the same person in a secondary external system
        /// ie.JO_DOE2_FIN
        /// </summary>
        public virtual string UserCode2
        {
            get => _UserCode2;
            set
            {
                _UserCode2 = value;
                OnPropertyChanged(() => UserCode2);
            }
        }

        bool _IsActive;
        public virtual bool IsActive
        {
            get => _IsActive;
            set
            {
                _IsActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }

        //List<string> _RolesList;
        string _Roles;//<-- mapping nHibernate to access the private field
        public virtual List<string> Roles
        {
            get
            {
                if (string.IsNullOrEmpty(_Roles))
                    return new List<String>();
                else
                    return _Roles.Trim(new[] { '[', ']' }).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            set
            {
                _Roles = $"[{string.Join(",", value)}]";
                OnPropertyChanged(() => Roles);
                //_Roles = _RolesList;
            }
        }

        //List<string> _PermissionsList;
        string _Permissions;//<-- mapping nHibernate to access the private field
        public virtual List<string> Permissions
        {
            get
            {
                if (string.IsNullOrEmpty(_Permissions))
                    return new List<String>();
                else
                    return _Permissions.Trim(new[] { '[', ']' }).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            set
            {

                _Permissions = $"[{string.Join(",", value)}]";
                OnPropertyChanged(() => Permissions);
                //_Permissions = _PermissionsList;
            }
        }

    }
}

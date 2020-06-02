using JARS.Core.Entities;
using ServiceStack;
using ServiceStack.Auth;
using System;
using System.Collections.Generic;

namespace JARS.SS.Auth.Entities
{
    [Serializable]
    public class JarsUserAuthDetails : EntityBase<int>, IUserAuthDetails, IAuthTokens, IUserAuthDetailsExtended, IMeta
    {
        public JarsUserAuthDetails()
        {
            ItemsBase = new Dictionary<string, string>();
        }
        //public virtual int Id { get; set; } <-- implemented in base
        public virtual string Gender { get; set; }
        public virtual string Language { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string RefreshToken { get; set; }
        public virtual string Culture { get; set; }
        public virtual DateTime? RefreshTokenExpiry { get; set; }
        public virtual string RequestTokenSecret { get; set; }
        public virtual Dictionary<string, string> Items { get; set; }
        public virtual string AccessToken { get; set; }
        public virtual string AccessTokenSecret { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual int? RefId { get; set; }
        public virtual string RequestToken { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual int UserAuthId { get; set; }
        public virtual string Provider { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string State { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Company { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string BirthDateRaw { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string LastName { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }

        public virtual IDictionary<string, string> ItemsBase
        {
            get { return Items; }
            set { Items = new Dictionary<string, string>(value); }
        }
    }
}

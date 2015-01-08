namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Account
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string Name_Title { get; set; }

        [StringLength(200)]
        public string Name_FirstName { get; set; }

        [StringLength(200)]
        public string Name_MiddleNames { get; set; }

        [StringLength(200)]
        public string Name_LastName { get; set; }

        [StringLength(200)]
        public string Name_KnownAs { get; set; }

        [StringLength(600)]
        public string Name_FullName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public int? OrganisationID { get; set; }

        public int? ResellerID { get; set; }

        public int? EmployeeID { get; set; }

        public int? ActingAsEmployeeID { get; set; }

        public string PasswordHash { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public Role? ActiveRole { get; set; }

        public DateTime? LastInvalidLogin { get; set; }

        public int InvalidLoginCount { get; set; }

        public DateTime? LastPasswordChange { get; set; }

        public bool MustChangePasswordOnNextLogin { get; set; }

        public string RolesAsString { get; set; }

        public bool OldEncryptionMethod { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Reseller Reseller { get; set; }

        [NotMapped]
        public bool LastFailedLoginWasWithinFailedLoginWindow
        {
            get
            {
                return (this.LastInvalidLogin.HasValue && ((DateTime.UtcNow - this.LastInvalidLogin.Value) < TimeSpan.FromMinutes(5)));
            }
        }

        [NotMapped]
        public bool IsLockedOut
        {
            get
            {
                return (this.InvalidLoginCount >= 3 && this.LastFailedLoginWasWithinFailedLoginWindow);
            }

            set
            {
                throw new NotImplementedException();  // Stupid implementation in other project
            }
        }

        #region RoleStuff

        [NotMapped]
        public Role[] Roles
        {
            get
            {
                return this.GetRolesAsRoleArray();
            }
        }

        public Role[] SetRoles(params Role[] roles)
        {
            this.SetRoles(roles.Select(r => r.ToString()).Distinct());
            return this.Roles;
        }

        public Role[] AddRole(Role role)
        {
            if (!this.Roles.Any(r => r.Equals(role)))
            {
                var roles = new List<string>(this.GetRolesAsStringArray()) { role.ToString() };
                this.SetRoles(roles);
            }

            return this.Roles;
        }

        public Role[] RemoveRole(Role role)
        {
            var newRoles = this.Roles.Where(r => !r.Equals(role)).ToArray();
            this.SetRoles(newRoles.ToArray());
            return this.Roles;
        }

        private Role[] GetRolesAsRoleArray()
        {
            var roleStrings = String.IsNullOrWhiteSpace(this.RolesAsString)
                                  ? new string[0]
                                  : this.RolesAsString.Split(',');
            var roles = new List<Role>();
            foreach (var roleString in roleStrings)
            {
                Role role;
                if (Enum.TryParse(roleString, true, out role))
                {
                    roles.Add(role);
                }
            }

            return roles.ToArray();
        }

        private void SetRoles(IEnumerable<string> roles)
        {
            this.RolesAsString = String.Join(",", roles);
        }

        private IEnumerable<string> GetRolesAsStringArray()
        {
            if (String.IsNullOrWhiteSpace(this.RolesAsString))
            {
                return new string[0];
            }

            return this.RolesAsString.Split(',');
        }
        #endregion
    }

    public enum Role
    {
        [Description("Support")]
        Support = 8,

        [Description("System Wide Administrator")]
        ProviderAdministrator = 1,

        [Description("Administrator for a Reseller")]
        ResellerAdministrator = 2,

        [Description("Administrator")]
        CustomerAdministrator = 3,

        [Description("Champion")]
        Champion = 4,

        [Description("Manager")]
        Manager = 5,

        [Description("Coordinator")]
        Coordinator = 6,

        [Description("Area Administrator")]
        AreaAdministrator = 7,

        [Description("Sales Manager")]
        SalesManager = 9
    }
}

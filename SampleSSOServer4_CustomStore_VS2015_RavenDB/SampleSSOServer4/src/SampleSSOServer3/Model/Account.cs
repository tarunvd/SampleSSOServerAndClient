namespace SampleSSOServer4.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Account
    {
        public string Id { get; set; }

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

        public string OrganisationId { get; set; }

        public string ResellerId { get; set; }

        public string EmployeeId { get; set; }

        public string ActingAsEmployeeId { get; set; }

        public string PasswordHash { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public Role? ActiveRole { get; set; }

        public DateTime? LastInvalidLogin { get; set; }

        public int InvalidLoginCount { get; set; }

        public DateTime? LastPasswordChange { get; set; }

        public string RolesAsString { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }

        public bool LastFailedLoginWasWithinFailedLoginWindow
        {
            get
            {
                return (this.LastInvalidLogin.HasValue && ((DateTime.UtcNow - this.LastInvalidLogin.Value) < TimeSpan.FromMinutes(5)));
            }
        }

        public bool IsLockedOut { get; set; }

        #region RoleStuff

        [NotMapped]
        private Role[] Roles { get; set; }

        public Role[] GetRoles()
        {
            return this.GetRolesAsRoleArray();
        }

        public Role[] SetRoles(params Role[] roles)
        {
            this.SetRoles(roles.Select(r => r.ToString()).Distinct());
            return this.GetRoles();
        }

        public Role[] AddRole(Role role)
        {
            if (!this.GetRoles().Any(r => r.Equals(role)))
            {
                var roles = new List<string>(this.GetRolesAsStringArray()) { role.ToString() };
                this.SetRoles(roles);
            }

            return this.GetRoles();
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

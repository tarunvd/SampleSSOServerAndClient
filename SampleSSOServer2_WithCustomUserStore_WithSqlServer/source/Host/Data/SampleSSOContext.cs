namespace SampleSSOServer2.Data
{
    using System.Data.Entity;

    using SampleSSOServer2.Model;

    public partial class SampleSSOContext : DbContext
    {
        public SampleSSOContext()
            : base("name=SampleSSOContext")
        {
            ////Using the AbsenceManagerDefault database
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AbsenceImportInsertUpdate> AbsenceImportInsertUpdates { get; set; }
        public virtual DbSet<AbsenceImportJobError> AbsenceImportJobErrors { get; set; }
        public virtual DbSet<AbsenceImportJob> AbsenceImportJobs { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountVerification> AccountVerifications { get; set; }
        public virtual DbSet<ConfigurationFlag> ConfigurationFlags { get; set; }
        public virtual DbSet<ConfigurationTemplate> ConfigurationTemplates { get; set; }
        public virtual DbSet<ConfigurationTemplateSection> ConfigurationTemplateSections { get; set; }
        public virtual DbSet<ConfigurationText> ConfigurationTexts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustomFieldImportInsertUpdate> CustomFieldImportInsertUpdates { get; set; }
        public virtual DbSet<CustomFieldImportJobError> CustomFieldImportJobErrors { get; set; }
        public virtual DbSet<CustomFieldImportJob> CustomFieldImportJobs { get; set; }
        public virtual DbSet<DailyJob> DailyJobs { get; set; }
        public virtual DbSet<EmailJob> EmailJobs { get; set; }
        public virtual DbSet<EmployeeImportInsertUpdate> EmployeeImportInsertUpdates { get; set; }
        public virtual DbSet<EmployeeImportJobError> EmployeeImportJobErrors { get; set; }
        public virtual DbSet<EmployeeImportJob> EmployeeImportJobs { get; set; }
        public virtual DbSet<GroupChild> GroupChilds { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<HourlyJob> HourlyJobs { get; set; }
        public virtual DbSet<LinkedAccountClaim> LinkedAccountClaims { get; set; }
        public virtual DbSet<LinkedAccount> LinkedAccounts { get; set; }
        public virtual DbSet<OrganisationCreationDetail> OrganisationCreationDetails { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<OrganisationUnitImportInsertUpdate> OrganisationUnitImportInsertUpdates { get; set; }
        public virtual DbSet<OrganisationUnitImportJobError> OrganisationUnitImportJobErrors { get; set; }
        public virtual DbSet<OrganisationUnitImportJob> OrganisationUnitImportJobs { get; set; }
        public virtual DbSet<PasswordResetSecret> PasswordResetSecrets { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PublicHolidayGroup> PublicHolidayGroups { get; set; }
        public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }
        public virtual DbSet<ReportDefinition> ReportDefinitions { get; set; }
        public virtual DbSet<Reseller> Resellers { get; set; }
        public virtual DbSet<ScheduledExportFile> ScheduledExportFiles { get; set; }
        public virtual DbSet<SMSJob> SMSJobs { get; set; }
        public virtual DbSet<SystemTask> SystemTasks { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }
        public virtual DbSet<TwoFactorAuthToken> TwoFactorAuthTokens { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserCertificate> UserCertificates { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<WebServiceCallJob> WebServiceCallJobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationTemplate>()
                .HasMany(e => e.ConfigurationTemplateSections)
                .WithOptional(e => e.ConfigurationTemplate)
                .HasForeignKey(e => e.ConfigurationTemplate_ID);

            modelBuilder.Entity<EmployeeImportJob>()
                .HasMany(e => e.EmployeeImportJobErrors)
                .WithRequired(e => e.EmployeeImportJob)
                .HasForeignKey(e => e.EmployeeImportJob_ID);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.GroupChilds)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.ReportDefinitions)
                .WithMany(e => e.Organisations)
                .Map(m => m.ToTable("ReportDefinitionToOrganisation").MapLeftKey("OrganisationID").MapRightKey("ReportDefinitionID"));

            modelBuilder.Entity<PublicHolidayGroup>()
                .HasMany(e => e.PublicHolidays)
                .WithMany(e => e.PublicHolidayGroups)
                .Map(m => m.ToTable("PublicHolidayToPublicHolidayGroup").MapLeftKey("PublicHolidayGroupID").MapRightKey("PublicHolidayID"));

            modelBuilder.Entity<Reseller>()
                .HasMany(e => e.Organisations)
                .WithRequired(e => e.Reseller)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.LinkedAccountClaims)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.LinkedAccounts)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.PasswordResetSecrets)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.TwoFactorAuthTokens)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.UserCertificates)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);

            modelBuilder.Entity<UserAccount>()
                .HasMany(e => e.UserClaims)
                .WithRequired(e => e.UserAccount)
                .HasForeignKey(e => e.ParentKey);
        }
    }
}

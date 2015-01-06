namespace SampleSSOServer2.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserAccount
    {
        public UserAccount()
        {
            LinkedAccountClaims = new HashSet<LinkedAccountClaim>();
            LinkedAccounts = new HashSet<LinkedAccount>();
            PasswordResetSecrets = new HashSet<PasswordResetSecret>();
            TwoFactorAuthTokens = new HashSet<TwoFactorAuthToken>();
            UserCertificates = new HashSet<UserCertificate>();
            UserClaims = new HashSet<UserClaim>();
        }

        [Key]
        public int Key { get; set; }

        public Guid ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Tenant { get; set; }

        [Required]
        [StringLength(254)]
        public string Username { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsAccountClosed { get; set; }

        public DateTime? AccountClosed { get; set; }

        public bool IsLoginAllowed { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastFailedLogin { get; set; }

        public int FailedLoginCount { get; set; }

        public DateTime? PasswordChanged { get; set; }

        public bool RequiresPasswordReset { get; set; }

        [StringLength(254)]
        public string Email { get; set; }

        public bool IsAccountVerified { get; set; }

        public DateTime? LastFailedPasswordReset { get; set; }

        public int FailedPasswordResetCount { get; set; }

        [StringLength(100)]
        public string MobileCode { get; set; }

        public DateTime? MobileCodeSent { get; set; }

        [StringLength(20)]
        public string MobilePhoneNumber { get; set; }

        public DateTime? MobilePhoneNumberChanged { get; set; }

        public int AccountTwoFactorAuthMode { get; set; }

        public int CurrentTwoFactorAuthStatus { get; set; }

        [StringLength(100)]
        public string VerificationKey { get; set; }

        public int? VerificationPurpose { get; set; }

        public DateTime? VerificationKeySent { get; set; }

        [StringLength(100)]
        public string VerificationStorage { get; set; }

        [StringLength(200)]
        public string HashedPassword { get; set; }

        public virtual ICollection<LinkedAccountClaim> LinkedAccountClaims { get; set; }

        public virtual ICollection<LinkedAccount> LinkedAccounts { get; set; }

        public virtual ICollection<PasswordResetSecret> PasswordResetSecrets { get; set; }

        public virtual ICollection<TwoFactorAuthToken> TwoFactorAuthTokens { get; set; }

        public virtual ICollection<UserCertificate> UserCertificates { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}

namespace SampleSSOServer2.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Helpers;

    using SampleSSOServer2.Model;

    #region SupportingClassesForPasswordManager and LoginManager

    public class CryptoWrapper
    {
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }
    }

    public class PasswordCheckerResult
    {
        private readonly List<string> validationMessages = new List<string>();

        public bool IsValid { get; set; }

        public List<string> ValidationMessages
        {
            get { return this.validationMessages; }
        }

        public PasswordCheckerResult()
        {
        }

        public PasswordCheckerResult(string validationMessage)
        {
            this.IsValid = false;
            this.validationMessages.Add(validationMessage);
        }
    }

    public enum VerifyLoginResult
    {
        Success,

        WrongPassword,

        AccountDisabled,

        AccountLockedOut,

        AccountExpired,

        PasswordNotSet,

        RolesNotSet
    }

    public class PasswordChecker
    {
        private readonly string passwordToCheck;

        public PasswordChecker(string passwordToCheck)
        {
            this.passwordToCheck = passwordToCheck;
        }

        public PasswordCheckerResult CheckPassword(int minimumLength, string compareString, bool checkComplexity)
        {
            var result = new PasswordCheckerResult();

            var lengthCheckResult = this.HasMinimumLength(minimumLength);
            var compareResult = !string.IsNullOrEmpty(compareString) ? this.CompareToString(compareString, 2) : new PasswordCheckerResult { IsValid = true };
            var complexityResult = checkComplexity ? this.CheckComplexity() : new PasswordCheckerResult { IsValid = true };

            result.IsValid = lengthCheckResult.IsValid && compareResult.IsValid && complexityResult.IsValid;

            result.ValidationMessages.AddRange(lengthCheckResult.ValidationMessages);
            result.ValidationMessages.AddRange(compareResult.ValidationMessages);
            result.ValidationMessages.AddRange(complexityResult.ValidationMessages);

            return result;
        }

        public PasswordCheckerResult HasMinimumLength(int minimumRequiredLength)
        {
            var result = new PasswordCheckerResult { IsValid = true };

            if (this.passwordToCheck.Length < minimumRequiredLength)
            {
                result.IsValid = false;
                result.ValidationMessages.Add(string.Format("The Password must be at least {0} characters long", minimumRequiredLength));
            }

            return result;
        }

        public PasswordCheckerResult CompareToString(string compareString, int length)
        {
            var result = new PasswordCheckerResult { IsValid = true };

            for (int i = 0; i <= (compareString.Length - length); i++)
            {
                var chk = compareString.Substring(i, length);

                if (this.passwordToCheck.IndexOf(chk, 0, System.StringComparison.OrdinalIgnoreCase) <= -1)
                {
                    continue;
                }

                result.IsValid = false;
                result.ValidationMessages.Add(string.Format("The Password is too similar to '{0}'", compareString));
                break;
            }
            return result;
        }

        public PasswordCheckerResult CheckComplexity()
        {
            var result = new PasswordCheckerResult { IsValid = true };

            string[] groups = new string[4];

            groups[0] = "abcdefghijklmnopqrstuvwxyz";
            groups[1] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            groups[2] = "1234567890";
            groups[3] = "!\"£$%^&*()-_=+`¬\\|,<.>/?;:'@#~[{]}";

            int groupCount = 0;

            foreach (var group in groups)
            {
                if (this.passwordToCheck.IndexOfAny(group.ToCharArray()) > -1)
                {
                    groupCount++;
                }
            }

            if (groupCount < 3)
            {
                result.IsValid = false;
                result.ValidationMessages.Add("Your password does not meet the minimum complexity requirements. Please use numbers, punctuation and/or both upper and lower case letters");
            }

            return result;
        }
    }

    internal static class SaltedHash
    {
        //Generate hash using the password and the salt

        internal static string GetHash(string data)
        {
            return GetHash(data, 16);
        }

        internal static string GetHash(string data, int saltLength)
        {
            return GetHash(data, GetSalt(saltLength));
        }

        internal static bool VerifyHash(string data, string storedHash)
        {
            const int defaultHashLength = 32;
            if (storedHash.Length > 32)
            {
                var hashedBytes = Convert.FromBase64String(storedHash);
                var salt = new byte[hashedBytes.Length - defaultHashLength];
                Array.Copy(hashedBytes, defaultHashLength, salt, 0, hashedBytes.Length - defaultHashLength);

                var newHash = GetHash(data, salt);

                return (storedHash == newHash);
            }
            else
            {
                return false;
            }
        }

        private static byte[] ComputeHash(byte[] data, byte[] salt)
        {
            var dataAndSalt = new byte[data.Length + salt.Length];

            Array.Copy(data, dataAndSalt, data.Length);
            Array.Copy(salt, 0, dataAndSalt, data.Length, salt.Length);

            var hashProvider = new SHA256Managed();

            return hashProvider.ComputeHash(dataAndSalt);
        }

        private static string GetHash(string data, byte[] salt)
        {
            string newStringHash;
            var hash = ComputeHash(Encoding.UTF8.GetBytes(data), salt);
            var saltedHash = new byte[hash.Length + salt.Length];
            Array.Copy(hash, saltedHash, hash.Length);
            Array.Copy(salt, 0, saltedHash, hash.Length, salt.Length);
            newStringHash = Convert.ToBase64String(saltedHash);
            return newStringHash;
        }

        private static byte[] GetSalt(int length)
        {
            var salt = new byte[length];

            var random = new RNGCryptoServiceProvider();

            random.GetNonZeroBytes(salt);
            return salt;
        }
    }

    public class LoginResult
    {
        public int UserId { get; set; }

        public Role[] Roles { get; set; }

        public LoginResultEnum LoginResultEnum { get; set; }

        public Role? ActiveRole { get; set; }
    }

    public enum LoginResultEnum
    {
        [Description("Unable to validate your credentials")]
        WrongUserName,
        Success,
        [Description("Unable to validate your credentials")]
        WrongPassword,
        [Description("Sorry, this account has been disabled")]
        AccountDisabled,
        [Description("Sorry, there have been too many failed attempts to login. Please wait five minutes and try again")]
        AccountLockedOut,
        [Description("The password for this account has expired")]
        AccountExpired,
        [Description("Unable to validate your credentials")]
        PasswordNotSet,
        [Description("Unable to validate your credentials")]
        RolesNotSet
    }

    #endregion

    public class PasswordManager
    {
        private readonly CryptoWrapper crypto;

        public PasswordManager(CryptoWrapper crypto)
        {
            this.crypto = crypto;
        }

        public PasswordCheckerResult ChangePassword(Account account, string oldPassword, string newPassword)
        {
            var verifyLogin = this.VerifyLogin(account, oldPassword);
            if (verifyLogin == VerifyLoginResult.Success || verifyLogin == VerifyLoginResult.AccountExpired)
            {
                return this.SetPassword(account, newPassword);
            }

            return new PasswordCheckerResult("OldPassword:Your Old Password is incorrect. Please try again.");
        }

        public bool IsExpired(Account account)
        {
            var expired = false;
            if (account.Organisation != null)
            {
                var fromDate = account.LastPasswordChange ?? account.Created;
                var maxPasswordAge = 100;

                if (fromDate != null && maxPasswordAge > 0)
                {
                    var toDate = ((DateTime)fromDate).AddDays((double)maxPasswordAge);
                    expired = DateTime.UtcNow.Date >= toDate.Date;
                }
            }

            return expired;
        }

        public PasswordCheckerResult SetPassword(Account account, string password)
        {
            var result = new PasswordCheckerResult { IsValid = true };
            if (account.Organisation != null)
            {
                var passwordChecker = new PasswordChecker(password);
                var minPasswordLength = 6;
                var employeeName = account.Name_FullName;

                result = passwordChecker.CheckPassword(
                    minPasswordLength, employeeName, true);
            }

            if (result.IsValid)
            {
                account.PasswordHash = this.crypto.HashPassword(password);
                account.LastPasswordChange = DateTime.UtcNow;
            }

            return result;
        }

        public VerifyLoginResult VerifyLogin(Account account, string password)
        {
            if (!account.IsEnabled)
            {
                return VerifyLoginResult.AccountDisabled;
            }

            if (account.IsLockedOut)
            {
                return VerifyLoginResult.AccountLockedOut;
            }

            if (account.PasswordHash == null)
            {
                return VerifyLoginResult.PasswordNotSet;
            }

            if (account.Roles.Length == 0)
            {
                return VerifyLoginResult.RolesNotSet;
            }

            if (account.OldEncryptionMethod)
            {
                if (SaltedHash.VerifyHash(password, account.PasswordHash))
                {
                    this.SetPassword(account, password);
                    account.OldEncryptionMethod = false;
                    account.LastLoggedIn = DateTime.UtcNow;

                    account.InvalidLoginCount = 0;
                    account.LastInvalidLogin = null;

                    // Check for expired
                    return this.IsExpired(account) ? VerifyLoginResult.AccountExpired : VerifyLoginResult.Success;
                }
            }

            if (this.crypto.VerifyHashedPassword(account.PasswordHash, password))
            {
                account.LastLoggedIn = DateTime.UtcNow;

                account.InvalidLoginCount = 0;
                account.LastInvalidLogin = null;

                // Check for expired
                return this.IsExpired(account) ? VerifyLoginResult.AccountExpired : VerifyLoginResult.Success;
            }

            if (account.LastFailedLoginWasWithinFailedLoginWindow)
            {
                account.InvalidLoginCount++;
            }
            else
            {
                account.InvalidLoginCount = 1;
            }

            account.LastInvalidLogin = DateTime.UtcNow;

            return VerifyLoginResult.WrongPassword;
        }
    }

}
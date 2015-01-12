namespace SampleSSOServer3.Data
{
    using System;

    using SampleSSOServer3.Model;

    public class LoginManager
    {
        private PasswordManager passwordManager;

        private IAccountRepository accountRepository;

        public LoginManager(PasswordManager passwordManager, IAccountRepository accountRepository)
        {
            this.passwordManager = passwordManager;
            this.accountRepository = accountRepository;
        }

        public LoginResult Login(string email, string password)
        {
            var result = new LoginResult() { Roles = new Role[0], UserId = null };
            var user = this.accountRepository.FindByEmail(email);
            if (user == null)
            {
                result.UserId = null;
                result.LoginResultEnum = LoginResultEnum.WrongUserName;
                return result;
            }

            result.UserId = user.Id;
            result.Roles = user.GetRoles();
            result.ActiveRole = user.ActiveRole;

            // Null out the ActingAsEmployeeId when we log in so that the user sees their own employees by default
            user.ActingAsEmployeeId = null;

            var loginResult = this.passwordManager.VerifyLogin(user, password);
            this.accountRepository.Update(user);
            switch (loginResult)
            {
                case VerifyLoginResult.AccountDisabled:
                    result.LoginResultEnum = LoginResultEnum.AccountDisabled;
                    break;
                case VerifyLoginResult.AccountLockedOut:
                    result.LoginResultEnum = LoginResultEnum.AccountLockedOut;
                    break;
                case VerifyLoginResult.WrongPassword:
                    result.LoginResultEnum = LoginResultEnum.WrongPassword;
                    break;
                case VerifyLoginResult.Success:
                    result.LoginResultEnum = LoginResultEnum.Success;
                    break;
                case VerifyLoginResult.AccountExpired:
                    result.LoginResultEnum = LoginResultEnum.AccountExpired;
                    break;
                case VerifyLoginResult.PasswordNotSet:
                    result.LoginResultEnum = LoginResultEnum.PasswordNotSet;
                    break;
                case VerifyLoginResult.RolesNotSet:
                    result.LoginResultEnum = LoginResultEnum.RolesNotSet;
                    break;
                default:
                    throw new NotImplementedException(String.Format("LoginManager.Login does not know how to handle {0}", loginResult));
            }

            return result;
        }
    }
}
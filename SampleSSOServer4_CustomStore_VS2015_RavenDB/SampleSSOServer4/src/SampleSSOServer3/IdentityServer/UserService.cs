namespace SampleSSOServer4.IdentityServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using SampleSSOServer4.Data;
    using SampleSSOServer4.Model;

    using Thinktecture.IdentityManager;
    using Thinktecture.IdentityServer.Core.Models;
    using Thinktecture.IdentityServer.Core.Services;

    public class UserService<TAccount> : IUserService, IDisposable where TAccount : Account
    {
        protected readonly IAccountRepository accountRepository;

        protected readonly LoginManager loginManager;

        private IDisposable cleanup;

        public UserService(IAccountRepository accountRepository, LoginManager loginManager, IDisposable cleanup = null)
        {
            if (accountRepository == null)
            {
                throw new ArgumentNullException("UserService");
            }

            this.accountRepository = accountRepository;
            this.cleanup = cleanup;
            this.loginManager = loginManager;
        }

        public virtual void Dispose()
        {
            if (this.cleanup == null)
            {
                return;
            }

            this.cleanup.Dispose();
            this.cleanup = (IDisposable)null;
        }

        public virtual Task<IEnumerable<Claim>> GetProfileDataAsync(
            ClaimsPrincipal subject,
            IEnumerable<string> requestedClaimTypes = null)
        {
            TAccount account = null;

            account =
                (TAccount)
                this.accountRepository.FindById(subject.Claims.First(
                    c => c.Type.Equals(Constants.ClaimTypes.Subject, StringComparison.OrdinalIgnoreCase)).Value);

            if (account == null)
            {
                throw new ArgumentException("Invalid subject identifier");
            }

            var enumerable = this.GetClaimsFromAccount(account);

            if (requestedClaimTypes != null)
            {
                enumerable = enumerable.Where(x => requestedClaimTypes.Contains(x.Type));
            }

            return Task.FromResult(enumerable);
        }

        protected virtual IEnumerable<Claim> GetClaimsFromAccount(TAccount account)
        {
            var list = new List<Claim>()
                                   {
                                       new Claim(Constants.ClaimTypes.Subject, account.Id.ToString()),
                                       new Claim(Constants.ClaimTypes.Name, account.Name_FullName)
                                   };
            if (!string.IsNullOrWhiteSpace(account.Email))
            {
                list.Add(new Claim(Constants.ClaimTypes.Email, account.Email));
            }

            var roles = account.RolesAsString.Split(',');

            list.AddRange(roles.Select(x => new Claim(Constants.ClaimTypes.Role, x)));

            return list;
        }

        public virtual Task<AuthenticateResult> PreAuthenticateAsync(
            IDictionary<string, object> env,
            SignInMessage message)
        {
            return Task.FromResult<AuthenticateResult>((AuthenticateResult)null);
        }

        public virtual async Task<AuthenticateResult> AuthenticateLocalAsync(
            string username,
            string password,
            SignInMessage message)
        {
            AuthenticateResult authenticateResult = null;

            var loginResult = this.loginManager.Login(username, password);

            switch (loginResult.LoginResultEnum)
            {
                case LoginResultEnum.Success:
                    var account = (TAccount)this.accountRepository.FindByEmail(username);

                    var result = await this.PostAuthenticateLocalAsync(account, message);

                    if (result != null)
                    {
                        authenticateResult = result;
                    }
                    else
                    {
                        var subject = account.Id.ToString();
                        var name = account.Name_FullName;
                        authenticateResult = new AuthenticateResult(
                            subject,
                            name);
                    }

                    break;

                case LoginResultEnum.AccountLockedOut:
                    authenticateResult = new AuthenticateResult("Account is locked out");
                    break;

                case LoginResultEnum.AccountExpired:
                    authenticateResult = new AuthenticateResult("Account is locked out");
                    break;
            }

            return authenticateResult;
        }

        public virtual async Task<AuthenticateResult> AuthenticateExternalAsync(
            ExternalIdentity externalUser,
            SignInMessage message)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> IsActiveAsync(ClaimsPrincipal subject)
        {
            TAccount account = null;

            account =
                (TAccount)this.accountRepository.FindById(subject.Claims.First(
                    c => c.Type.Equals(Constants.ClaimTypes.Subject, StringComparison.OrdinalIgnoreCase)).Value);


            return account == null ? Task.FromResult(false) : Task.FromResult<bool>(account.IsEnabled && !account.IsLockedOut);
        }

        public virtual Task<AuthenticateResult> PreAuthenticateAsync(SignInMessage message)
        {
            return Task.FromResult<AuthenticateResult>((AuthenticateResult)null);
        }

        protected virtual Task<AuthenticateResult> PostAuthenticateLocalAsync(TAccount account, SignInMessage message)
        {
            return Task.FromResult<AuthenticateResult>((AuthenticateResult)null);
        }

        public virtual Task SignOutAsync(ClaimsPrincipal subject)
        {
            return (Task)Task.FromResult<object>((object)null);
        }
    }
}
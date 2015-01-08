namespace SampleSSOServer2.IdentityServer
{
    using SampleSSOServer2.Data;
    using SampleSSOServer2.Model;

    using Thinktecture.IdentityServer.Core.Services;

    public class UserServiceFactory
    {
        public static IUserService Factory()
        {
            var db = new SampleSSOContext();
            var repo = new AccountRepository(db);
            var loginManager = new LoginManager(new PasswordManager(new CryptoWrapper()), repo);

            var userSvc = new UserService<Account>(repo, loginManager, db);
            return userSvc;
        }
    }
}
namespace SampleSSOServer4.IdentityServer
{
    using Raven.Client.Document;
    using Raven.Client.UniqueConstraints;

    using SampleSSOServer4.Data;
    using SampleSSOServer4.Model;

    using Thinktecture.IdentityServer.Core.Services;

    public class UserServiceFactory
    {
        public static IUserService Factory()
        {
            var store = new DocumentStore
            {
                Url = "http://localhost:8080/", // server URL
                DefaultDatabase = "EmpactisIdentity"
                // default database
            };

            store.Conventions.FindIdentityProperty = info => info.Name == "Id";
            store.Conventions.IdentityPartsSeparator = "-";
            store.RegisterListener(new UniqueConstraintsStoreListener());
            store.Conventions.SaveEnumsAsIntegers = true;

            store.Initialize();

            var repo = new AccountRepository(store);
            var loginManager = new LoginManager(new PasswordManager(new CryptoWrapper()), repo);

            var userSvc = new UserService<Account>(repo, loginManager, store);
            return userSvc;
        }
    }
}
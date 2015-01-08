namespace SampleSSOServer2.Config
{
    using SampleSSOServer2.IdentityServer;

    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Services;
    using Thinktecture.IdentityServer.Core.Services.InMemory;

    public class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory();

            factory.UserService =
                new Registration<IUserService>(resolver => UserServiceFactory.Factory());

            var scopeStore = new InMemoryScopeStore(Scopes.Get());
            factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

            var clientStore = new InMemoryClientStore(Clients.Get());
            factory.ClientStore = new  Registration<IClientStore>(resolver => clientStore);

            return factory;
        }
    }
}
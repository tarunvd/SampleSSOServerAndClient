namespace SampleSSOServer3.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Raven.Client;

    using SampleSSOServer3.Model;
    using AutoMapper;

    public interface IAccountRepository
    {
        void Add(Account entity);

        void Update(Account entity);

        void Delete(string id);

        Account FindById(string id);

        IEnumerable<Account> Where(Expression<Func<Account, bool>> criteria);

        IQueryable<Account> FindAllUsers();

        Account FindByEmail(string email);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly IDocumentStore store;

        public AccountRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public void Add(Account entity)
        {
            using (var session = this.store.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }

        public void Update(Account entity)
        {
            using (var session = this.store.OpenSession())
            {
                var account = session.Load<Account>(entity.Id);

                if (account != null)
                {
                    ////account = Mapper.Map<Account>(entity);

                    //TODO: implement this in a cleaner way
                    account.Name_Title = entity.Name_Title;
                    account.Name_FirstName = entity.Name_FirstName;
                    account.Name_MiddleNames = entity.Name_MiddleNames;
                    account.Name_LastName = entity.Name_LastName;
                    account.Name_KnownAs = entity.Name_KnownAs;
                    account.Name_FullName = entity.Name_FullName;
                    account.Email = entity.Email;
                    account.OrganisationId = entity.OrganisationId;
                    account.ResellerId = entity.ResellerId;
                    account.EmployeeId = entity.EmployeeId;
                    account.ActingAsEmployeeId = entity.ActingAsEmployeeId;
                    account.PasswordHash = entity.PasswordHash;
                    account.IsEnabled = entity.IsEnabled;
                    account.LastLoggedIn = entity.LastLoggedIn;
                    account.ActiveRole = entity.ActiveRole;
                    account.LastInvalidLogin = entity.LastInvalidLogin;
                    account.InvalidLoginCount = entity.InvalidLoginCount;
                    account.LastPasswordChange = entity.LastPasswordChange;
                    account.RolesAsString = entity.RolesAsString;
                    account.Created = entity.Created;
                    account.CreatedBy = entity.CreatedBy;
                    account.Modified = entity.Modified;
                    account.ModifiedBy = entity.ModifiedBy;

                    session.SaveChanges();
                }
            }
        }

        public void Delete(string id)
        {
            using (var session = this.store.OpenSession())
            {
                var account = session.Load<Account>(id);
                session.Delete(account);
                session.SaveChanges();
            }
        }

        public Account FindByEmail(string email)
        {
            using (var session = this.store.OpenSession())
            {
                return session.Query<Account>().FirstOrDefault(a => a.Email.Equals(email));
            }
        }

        public Account FindById(string id)
        {
            using (var session = this.store.OpenSession())
            {
                return session.Load<Account>(id);

            }
        }

        public IQueryable<Account> FindAllUsers()
        {
            using (var session = this.store.OpenSession())
            {
                return session.Query<Account>();

            }
        }

        public IEnumerable<Account> Where(Expression<Func<Account, bool>> criteria)
        {
            using (var session = this.store.OpenSession())
            {
                return session.Query<Account>().Where(criteria);
            }
        }
    }
}
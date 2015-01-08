namespace SampleSSOServer2.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using SampleSSOServer2.Model;

    public interface IAccountRepository
    {
        void Add(Account entity);

        void Delete(int id);

        Account FindById(int id);

        int Save();

        IEnumerable<Account> Where(Expression<Func<Account, bool>> criteria);

        void DeleteAccountVerification(Guid confirmationId);

        IQueryable<Account> FindAllUsers();

        Account FindByConfirmationGuid(Guid id);

        Account FindByEmail(string email);

        Account FindById(int id, Expression<Func<Account, object>> includeProperty);

        Account FindById(int id, Expression<Func<Account, object>>[] includeProperties);

        void SaveAccountVerificationId(AccountVerification accountVerification);

        void DisableTracking();
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly SampleSSOContext context;

        public AccountRepository(SampleSSOContext context)
        {
            this.context = context;
        }

        public void Add(Account entity)
        {
            this.context.Accounts.Add(entity);
        }

        public void Delete(int id)
        {
            var account = this.context.Accounts.Find(id);
            this.context.Accounts.Remove(account);
        }

        public void DeleteAccountVerification(Guid confirmationId)
        {
            this.context.AccountVerifications.Remove(
                this.context.AccountVerifications.SingleOrDefault(x => x.VerificationId == confirmationId));
            this.context.SaveChanges();
        }

        public Account FindByConfirmationGuid(Guid id)
        {
            var verification = this.context.AccountVerifications.SingleOrDefault(
                x => x.VerificationId == id);
            if (verification == null)
            {
                return null;
            }
            return this.context.Accounts.Single(x => x.ID == verification.AccountId);
        }

        public Account FindByEmail(string email)
        {
            return
                    this.context.Accounts.FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Account FindById(int id)
        {
            return this.context.Accounts.FirstOrDefault(a => a.ID == id);
        }

        public Account FindById(int id, Expression<Func<Account, object>> includeProperty)
        {
            return this.FindById(id, new[] { includeProperty });
        }

        public Account FindById(int id, Expression<Func<Account, object>>[] includeProperties)
        {
            var queryable = this.context.Accounts.AsQueryable();
            queryable = includeProperties.Aggregate(queryable, (current, include) => current.Include(include));
            return queryable.FirstOrDefault(a => a.ID == id);
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public void SaveAccountVerificationId(AccountVerification accountVerification)
        {
            this.context.AccountVerifications.Add(accountVerification);
            this.context.SaveChanges();
        }

        public IQueryable<Account> FindAllUsers()
        {
            return this.context.Accounts;
        }

        public IEnumerable<Account> Where(Expression<Func<Account, bool>> criteria)
        {
            return this.context.Accounts.Where(criteria);
        }

        public void DisableTracking()
        {
            this.context.Configuration.AutoDetectChangesEnabled = false;
            this.context.Configuration.ValidateOnSaveEnabled = false;
        }
    }
}
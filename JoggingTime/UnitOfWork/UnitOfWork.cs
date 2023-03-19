using JoggingTime.Helpers;
using JoggingTime.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JoggingTime.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public ApplicationDBContext context { get; }
        public int UserID { set; get; }
        public UnitOfWork(ApplicationDBContext context)
        {
            SetUserID();
            this.context = context;
            BeginTransaction();
        }
        public UnitOfWork()
        {
            SetUserID();
            context = new ApplicationDBContext();
        }
        public bool Save(bool isPartial = false)
        {
            try
            {
                context.SaveChanges();
                if (!isPartial)
                    CommitTransaction();
                return true;
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }

        }
        public bool SavePartial()
        {
            return Save(true);
        }
        private void SetUserID()
        {
            //try
            //{
            //    string accessToken = HttpRequestHelper.GetHeaderValue("token");
            //    UserID = int.Parse(SecurityHelper.GetUserIDFromToken(accessToken).ToString());
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private void BeginTransaction()
        {
            context.Database.CloseConnection();

            if (context.Database.CurrentTransaction == null)
                context.Database.BeginTransaction();
        }

        private void RollbackTransaction()
        {
            if (context.Database.CurrentTransaction != null)
                context.Database.RollbackTransaction();
        }

        private void CommitTransaction()
        {
            if (context.Database.CurrentTransaction != null)
                context.Database.CommitTransaction();
        }

      
    }
}

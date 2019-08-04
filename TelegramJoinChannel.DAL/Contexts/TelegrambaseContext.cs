using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.Entities;
using TelegramJoinChannel.DAL.Initializer;

namespace TelegramJoinChannel.DAL.Contexts
{
    class TelegrambaseContext : DbContext
    {
        //static TelegrambaseContext()
        //{
        //    Database.SetInitializer<TelegrambaseContext>(new TelegrambaseContextInitializer());
        //}

        public TelegrambaseContext() : base("telegramsoftDB")
        { }

        public virtual void Save()
        {
            base.SaveChanges();
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }

            catch (DbUpdateException dbu)
            {
                var exception = HandleDbUpdateException(dbu);
                throw exception;
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

        

        public DbSet<Channels> Channels { get; set; }
        public DbSet<CheckStatus> CheckStatuses { get; set; }
    }
}

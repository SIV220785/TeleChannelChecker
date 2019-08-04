using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.Repositories.Base;

namespace TelegramJoinChannel.DAL.UnitOfWork.Base
{
    internal interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : class;
    }
}

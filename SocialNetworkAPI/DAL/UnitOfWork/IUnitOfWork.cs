using System;
using System.Threading.Tasks;
using DAL.Repository.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository {get;}

        Task<bool> SaveChanges();
    }
}
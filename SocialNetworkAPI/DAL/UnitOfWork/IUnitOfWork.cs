using System;
using DAL.Repository.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IAuthRepository AuthRepository {get;}
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository {get;}

        int SaveChanges();
    }
}
using System;
using System.Threading.Tasks;
using DAL.Repository.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository {get;}
        IMessageRepository MessageRepository {get;}
        IPhotoRepository PhotoRepository {get;}
        IFriendshipRepository FriendshipRepository { get; }
        IPostRepository PostRepository { get; }

        Task<bool> SaveChanges();
    }
}
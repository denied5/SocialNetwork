using DAL.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }
        IPostRepository PostRepository { get; }
        ILikeRepository LikeRepository { get; }
        IRoleRepository RoleRepository { get; }

        Task<bool> SaveChanges();
    }
}
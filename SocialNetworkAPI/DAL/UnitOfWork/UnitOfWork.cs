using DAL.Data;
using DAL.Repository;
using DAL.Repository.Interfaces;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _context;
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;
        private IPhotoRepository _photoRepository;
        private IFriendshipRepository _friendshipRepository;
        private IPostRepository _postRepository;
        private ILikeRepository _likeRepository;
        private IRoleRepository _roleRepository;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }
        public IMessageRepository MessageRepository
        {
            get
            {
                return _messageRepository = _messageRepository ?? new MessageRepository(_context);
            }
        }

        public IPhotoRepository PhotoRepository
        {
            get
            {
                return _photoRepository = _photoRepository ?? new PhotoRepository(_context);
            }
        }

        public IFriendshipRepository FriendshipRepository
        {
            get
            {
                return _friendshipRepository = _friendshipRepository ?? new FriendshipRepository(_context);
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                return _postRepository = _postRepository ?? new PostRepository(_context);
            }
        }

        public ILikeRepository LikeRepository
        {
            get
            {
                return _likeRepository = _likeRepository ?? new LikeRepository(_context);
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                return _roleRepository = _roleRepository ?? new RoleRepository(_context);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
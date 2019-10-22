using DAL.Data;
using DAL.Repository;
using DAL.Repository.Interfaces;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _context;
        private IAuthRepository _authRepository;
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IAuthRepository AuthRepository
        {
            get
            {
                return _authRepository = _authRepository ?? new AuthRepository(_context);
            }
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
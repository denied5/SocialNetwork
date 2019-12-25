using AutoMapper;
using BIL.DTO;
using BIL.Services.Interrfaces;
using DAL.Models;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteComment(int userId, int commentId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAll();
            var comment = comments.Where(c => c.Id == commentId && c.UserId == userId).LastOrDefault();

            if (comment == null)
            {
                throw new Exception("Comment Doesn't exsist");
            }

            _unitOfWork.CommentRepository.Remove(comment);

            return await _unitOfWork.SaveChanges();
        }

        public async Task<CommentDTO> SetComment(CommentToCreateDTO comment)
        {
            var commentToAdd = _mapper.Map<Comment>(comment);

            _unitOfWork.CommentRepository.Add(commentToAdd);

            if (await _unitOfWork.SaveChanges())
            {
                var commentToReturn = await _unitOfWork.CommentRepository.GetComment(commentToAdd.Id);
                var dewjnd = _mapper.Map<CommentDTO>(commentToReturn);
                return _mapper.Map<CommentDTO>(commentToReturn);
            }

            return null;
        }

        public async Task<CommentDTO> GetComment(int id)
        {
            var commentFromRepo = _unitOfWork.CommentRepository.GetComment(id);

            if (commentFromRepo == null)
            {
                throw new Exception("Comment doesn't exsist");
            }

            return _mapper.Map<CommentDTO>(commentFromRepo);
        }
    }
}

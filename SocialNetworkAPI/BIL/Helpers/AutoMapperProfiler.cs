using AutoMapper;
using BIL.DTO;
using BIL.Extensions;
using DAL.Models;
using System.Linq;

namespace BIL.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).URL);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                }).ReverseMap();
            CreateMap<User, UserForRegisterDTO>().ReverseMap();
            CreateMap<User, UserForDetailedDTO>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.Where(p => p.Approved == true).FirstOrDefault(p => p.IsMain).URL);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoForDetailedDTO>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<Post, PostForReturnDTO>()
                .ForMember(m => m.UserKnownAs, opt => opt
                    .MapFrom(u => u.User.KnownAs))
                .ForMember(m => m.UserPhotoUrl, opt => opt
                    .MapFrom(u => u.User.Photos.FirstOrDefault(p => p.IsMain).URL))
                .ForMember(m => m.Likers, opt => opt
                    .MapFrom(l => l.Likes));

            CreateMap<Like, LikersDTO>()
                .ForMember(m => m.KnownAs, opt =>
                    opt.MapFrom(u => u.User.KnownAs))
                .ForMember(m => m.PhotoUrl, opt =>
                    opt.MapFrom(u => u.User.Photos.FirstOrDefault(p => p.IsMain).URL))
                .ForMember(m => m.Id, opt =>
                    opt.MapFrom(u => u.UserId));


            CreateMap<PostForCreatinDTO, Post>();
            CreateMap<PhotoForCreationDTO, Photo>();
            CreateMap<UserForUpdateDTO, User>();
            CreateMap<MessageForCreationDTO, Message>();
            CreateMap<Message, MessageToReturnDTO>()
               .ForMember(m => m.SenderPhotoUrl, opt => opt
                   .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).URL))
               .ForMember(m => m.RecipientPhotoUrl, opt => opt
                   .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).URL));

            CreateMap<CommentToCreateDTO, Comment>();
            CreateMap<Comment, CommentDTO > ()
                .ForMember(c => c.UserPhotoUrl, opt => 
                    opt.MapFrom(u => u.User.Photos.FirstOrDefault(p => p.IsMain).URL));
        }
    }
}

using AutoMapper;
using BIL.DTO;
using BIL.Extensions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIL.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember( dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).URL);
                })
                .ForMember( dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForRegisterDTO>().ReverseMap();
            CreateMap<User, UserForDetailedDTO>()
                .ForMember( dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).URL);
                })
                .ForMember( dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoForDetailedDTO>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<PhotoForCreationDTO, Photo>();
            CreateMap<UserForUpdateDTO, User>();
            CreateMap<MessageForCreationDTO, Message>();
            CreateMap<Message, MessageToReturnDTO>()
               .ForMember(m => m.SenderPhotoUrl, opt => opt
                   .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).URL))
               .ForMember(m => m.RecipientPhotoUrl, opt => opt
                   .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).URL));
        }
    }
}
 
using AutoMapper;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.classes
{
    public class Mapper: Profile
    {
        public Mapper()
        {

            CreateMap<Comment, GetCommentDTO>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Username))
               .ForMember(dest => dest.Discussion, opt => opt.MapFrom(src => src.Discussion.Title));


            CreateMap<Discussion, GetDiscussionDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.CountOfComments, opt => opt.MapFrom(src => src.Comments.Count));

            CreateMap<Subject, SubjectDTO>().ReverseMap();

            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateDiscussionDTO, Discussion>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
               .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
               .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId));

            CreateMap<CreateCommentDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DiscussionId, opt => opt.MapFrom(src => src.DiscussionId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            CreateMap<Subject, GetSubjectDTO>()
                .ForMember(dest => dest.DiscussionsCount, opt => opt.Ignore());

            CreateMap<Subject, GetByIDSubjectDTO>()
                .ForMember(dest => dest.Discussions, opt => opt.Ignore());

        }
    }
}

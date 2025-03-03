using AutoMapper;
using DTO.classes;
using Microsoft.Extensions.Logging;
using DAL.Interfaces;
using DAL.Models;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository SubjectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<string> logger;

        public SubjectService(ISubjectRepository SubjectRepository, IMapper mapper, ILogger<string> logger)
        {
            this.SubjectRepository = SubjectRepository;
            this.logger = logger;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DTO.classes.Mapper>();
            });
            this.mapper = config.CreateMapper();
        }

        public async Task<SubjectDTO> AddNewSubjectAsync(SubjectDTO e)
        {
            try
            {
                e.Id = 0;
                var map = mapper.Map<Subject>(e);
                var answer=await SubjectRepository.AddAsync(map);
                return mapper.Map<SubjectDTO>(answer);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to add Subject in the service" + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await SubjectRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("faild to delete Subject in the service" + ex.Message);
                throw;
            }
        }

        public async Task<List<GetSubjectDTO>> GetAllSubjectsAsync()
        {
            try
            {
                var subjects = await SubjectRepository.GetAllAsync();

                var subjectsWithCount = subjects.Select(subject =>
                {
                    var dto = mapper.Map<GetSubjectDTO>(subject); 
                    dto.DiscussionsCount = SubjectRepository.CountOfDiscussionsForSubject(subject.Id);
                    return dto;
                }).ToList();


                return subjectsWithCount;
            
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get all Subjects in the service" + ex.Message);
                throw;
            }
        }

        public async Task<GetByIDSubjectDTO> GetByIdAsync(int id)
        {
            try
            {
                var answer = await SubjectRepository.GetByIdAsync(id);
                var dto = mapper.Map<GetByIDSubjectDTO>(answer);

                List<Discussion> discussions = await SubjectRepository.ListOfDiscussionsForSubject(id);
                List<GetDiscussionDTO> discussionsDTO=mapper.Map<List<GetDiscussionDTO>>(discussions);
                dto.Discussions =discussionsDTO ;

                return dto;
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get Subject in the service" + ex.Message);
                throw;
            }
        }

        public async Task<SubjectDTO> UpdateAsync(SubjectDTO e)
        {
            try
            {
                var map= mapper.Map<Subject>(e);
                var answer= await SubjectRepository.UpdateAsync(map);
                return mapper.Map<SubjectDTO>(answer);
            }
            catch(Exception ex)
            {
                logger.LogError("faild to update Subject in the service" + ex.Message);
                throw;
            }
        }
    }
}

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

        public async Task<List<SubjectDTO>> GetAllSubjectsAsync()
        {
            try
            {
                var answer= await SubjectRepository.GetAllAsync();
                return mapper.Map<List<SubjectDTO>>(answer);    
            }
            catch (Exception ex)
            {
                logger.LogError("faild to get all Subjects in the service" + ex.Message);
                throw;
            }
        }

        public async Task<SubjectDTO> GetByIdAsync(int id)
        {
            try
            {
                var answer= await SubjectRepository.GetByIdAsync(id);
                return mapper.Map<SubjectDTO>(answer);
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

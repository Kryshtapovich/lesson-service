﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CloudStorage;
using AutoMapper;
using Domain.Models.Student;
using Domain.Models.Survey;
using Domain.Repositories.StudentRepository;
using Domain.Repositories.SurveyRepository;

namespace API.Services.InstructorService
{
    public class InstructorService : IInstructorService
    {
        private readonly ISurveyRepository surveyRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICloudStorage cloud;
        private readonly IMapper mapper;

        public InstructorService(ISurveyRepository surveyRepository, IStudentRepository studentRepository, ICloudStorage cloud, IMapper mapper)
        {
            this.surveyRepository = surveyRepository;
            this.studentRepository = studentRepository;
            this.cloud = cloud;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(int pageNumber, int pageSize)
        {
            return await studentRepository.GetGroupsAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<SurveyDto>> GetSurveysAsync(int pageNumber, int pageSize)
        {
            var surveys = await surveyRepository.GetSurveysAsync(pageNumber, pageSize);
            return mapper.Map<IEnumerable<SurveyDto>>(surveys);
        }

        public async Task<IEnumerable<AnswerDto>> GetStudentAnswersAsync(Guid surveyId, long studentId)
        {
            var answers = await surveyRepository.GetStudentAnswersAsync(surveyId, studentId);
            return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async Task ChangeSurveyStatusAsync(Guid surveyId, bool isOpened)
        {
            await surveyRepository.ChangeSurveyStatusAsync(surveyId, isOpened);
        }

        public async Task<SurveyDto> CreateSurveyAsync(SurveyDto surveyDto)
        {
            var newSurvey = await surveyRepository.AddSurveyAsync(mapper.Map<Survey>(surveyDto));
            return mapper.Map<SurveyDto>(newSurvey);
        }

        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            var images = await surveyRepository.GetAnswersImagesAsync(surveyId);
            foreach (var image in images)
            {
                await cloud.DeleteImageAsync(image.FileName);
            }
            await surveyRepository.DeleteSurveyAsync(surveyId);
        }
    }
}

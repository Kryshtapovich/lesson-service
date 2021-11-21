﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Survey;

namespace Domain.Repositories.SurveyRepository
{
    public interface ISurveyRepository
    {
        ValueTask<IEnumerable<Survey>> GetSurveysAsync(int pageNumber, int pageSize);
        ValueTask<Survey> AddSurveyAsync(Survey survey);
        ValueTask DeleteSurveyAsync(Guid surveyId);

        ValueTask<bool> CheckIfSurveyClosedAsync(int messageId);
        ValueTask<bool> CheckIfSurveyClosedAsync(string pollId);

        ValueTask<Image> GetAnswerImageAsync(int messageId);
        ValueTask<IEnumerable<Image>> GetAnswersImagesAsync(Guid surveyId);

        ValueTask<bool> GetSurveyStatusAsync(Guid survey);
        ValueTask ChangeSurveyStatusAsync(Guid surveyId, bool isOpened);

        ValueTask<ICollection<Question>> GetSurveyQuestionsAsync(Guid surveyId);
        ValueTask<IEnumerable<QuestionMessage>> GetSurveyOptionQuestionsAsync(Guid surveyId);
        ValueTask AddQuestionMessageAsync(int questionId, QuestionMessage message);

        ValueTask<IEnumerable<Answer>> GetStudentAnswersAsync(Guid surveyId, long studentId);
        ValueTask RegisterAnswerAsync(int messageId, string answerText = null, Image image = null);
        ValueTask RegisterAnswerAsync(string pollId, string optionText);
    }
}

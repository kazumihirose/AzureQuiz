using Microsoft.AspNetCore.Components;
using BlazorSignalRApp.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorSignalRApp.Client.Services
{
    public class QuestionService
    {
        public QuestionService(HttpClient httpClient)
        {
            _http = httpClient;
        }
        public HttpClient _http { get; }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _http.GetJsonAsync<List<Question>>("api/Questions");
        }

        public async Task<Question> GetQuestionByIdAsync(int Id)
        {
            return await _http.GetJsonAsync<Question>("api/Questions/" + Id.ToString());
        }

        public async Task PutQuestionByIdAsync(Question q)
        {
            await _http.PutJsonAsync("api/Questions/" + q.QuestionId, q);
        }
        public async Task DeleteQuestionByIdAsync(Question q)
        {
            await _http.DeleteAsync("api/Questions/" + q.QuestionId);
        }

        public async Task<Question> PostQuestionsAsync(Question q)
        {
            return await _http.PostJsonAsync<Question>("api/Questions/", q);
        }
    }

}

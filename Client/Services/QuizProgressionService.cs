using Microsoft.AspNetCore.Components;
using BlazorSignalRApp.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorSignalRApp.Client.Services
{
    public class QuizProgressionService
    {
        public QuizProgressionService(HttpClient httpClient)
        {
            _http = httpClient;
        }
        public HttpClient _http { get; }

        public async Task<QuizProgression> GetQuizProgressionsAsync()
        {
            return await _http.GetJsonAsync<QuizProgression>("api/QuizProgressions");
        }
    }

}

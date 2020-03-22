using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using BlazorSignalRApp.Shared;

namespace BlazorSignalRApp.Server.Hubs
{
    public class QuizHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public async Task SendQuiz(Question q)
        {
            await Clients.All.SendAsync("ReceiveQuiz", q);
        }

        public async Task SendProgression(QuizProgression p, Question q)
        {
            await Clients.All.SendAsync("ReceiveProgression", p, q);
        }


    }
}

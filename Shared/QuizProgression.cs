using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlazorSignalRApp.Shared
{
    public class QuizProgression
    {
        [Key]
        public int QuizProgressionID { get; set; }

        public int CurrentQuestionID { get; set; }

        /// <summary>
        /// 0: "please wait."
        /// 1: "choice the answer."
        /// 2: "closed."
        /// 3: "show correct answer."
        /// </summary>
        public ContextStateType CurrentState { get; set; }
    }

    public enum ContextStateType
    {
        PleaseWait,
        ChooseTheAnswer,
        Closed,
        ShowCorrectAnswer
    }
}
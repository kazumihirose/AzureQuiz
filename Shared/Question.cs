using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlazorSignalRApp.Shared
{
    public class Question
    {
        public Question()
        {
            this.CreateAt = DateTime.UtcNow;
        }

        [Key]
        public int QuestionId { get; set; }

        [Display(Name = "問題本文"), Required]
        public string Body { get; set; }

        [Display(Name = "回答選択肢1"), Required]
        public string Option1 { get; set; }

        [Display(Name = "回答選択肢2"), Required]
        public string Option2 { get; set; }

        [Display(Name = "回答選択肢3")]
        public string Option3 { get; set; }

        [Display(Name = "回答選択肢4")]
        public string Option4 { get; set; }

        [Display(Name = "回答選択肢5")]
        public string Option5 { get; set; }

        [Display(Name = "回答選択肢6")]
        public string Option6 { get; set; }

        [Display(Name = "正解の選択肢の番号"), Required, RegularExpression(@"[1-6]+", ErrorMessage = "選択肢は1-6までです")]
        [Range(1,6, ErrorMessage = "正解番号は1から6で入力してください。")]
        public string IndexOfCorrectOption { get; set; }

        [Display(Name = "解説")]
        public string Comment { get; set; }

        [Display(Name = "投稿日時")]
        public DateTime CreateAt { get; set; }

    }
}
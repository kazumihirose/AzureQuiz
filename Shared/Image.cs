using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlazorSignalRApp.Shared
{
    public class Image
    {
        public Image()
        {
            this.CreateAt = DateTime.UtcNow;
        }

        [Key]
        public int ImageID { get; set; }
        [Display(Name = "画像データ")]
        public string ImageData { get; set; }
        [Display(Name = "画像ファイル名")]
        public string ImageUrl { get; set; }
        [Display(Name = "画像Url")]
        public string ImageFileName { get; set; }
        [Display(Name = "投稿日時")]
        public DateTime CreateAt { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlazorSignalRApp.Shared
{
    public class User
    {
        public User()
        {
            this.CreateAt = DateTime.UtcNow;
            this.UserId = Guid.NewGuid().ToString();
            this.IndexOfUserOption = "0";
        }

        [Key]
        public string UserId { get; set; }

        public string IdProviderName { get; set; }

        public string Name { get; set; }
        public int Point { get; set; }

        public DateTime? CreateAt { get; set; }

        [RegularExpression(@"[0-6]+", ErrorMessage = "選択肢は0-6までです")]
        public string IndexOfUserOption { get; set; }
    }
}
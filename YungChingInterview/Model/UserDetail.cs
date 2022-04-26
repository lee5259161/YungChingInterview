using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserDetail
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]*$", ErrorMessage = "只能是英文、數字或特殊符號(_-)")]
        [Display(Name = "帳號")]
        public string UserID { get; set; }
        [Required]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "不正確的E-mail格式")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        public string Access { get; set; }
        [Required]
        [Display(Name = "帳號狀態")]
        public int IsDelete { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

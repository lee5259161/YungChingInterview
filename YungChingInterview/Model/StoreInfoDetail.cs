using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class StoreInfoDetail
    {
        [Required]
        [Display(Name = "門市編號")]
        public int StoreID { get; set; }

        [Required]
        [Display(Name = "門市名稱")]
        public string StoreName { get; set; }
        
        [Required]
        [Display(Name = "縣市別")]
        public string Country { get; set; }

        [Display(Name = "開幕日期")]
        public Nullable<System.DateTime> OpenDate { get; set; }

        [Display(Name = "閉幕日期")]
        public Nullable<System.DateTime> CloseDate { get; set; }

        [Required]
        [Display(Name = "店長")]
        public string Manager { get; set; }

        [Display(Name = "更新者")]
        public string UpdateUser { get; set; }

        [Display(Name = "更新日期")]
        public DateTime UpdateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YungChingInterview.Models
{
    public class CheckBox
    {
        public IList<string> SelectedItem { get; set; }
        public IList<SelectListItem> ItemList { get; set; }

        public CheckBox()
        {
            SelectedItem = new List<string>();
            ItemList = new List<SelectListItem>();
        }
    }
}
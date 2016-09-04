using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magazin.Models
{
    public class DialogViewModel
    {
        public DialogViewModel()
        {
            DialogId = "dialog_" + Guid.NewGuid().ToString("N");
        }

        public string DialogId { get; }

        public int CurrentCategory { get; set; }
    }
}
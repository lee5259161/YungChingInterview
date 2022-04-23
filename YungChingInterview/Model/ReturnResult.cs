using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class ReturnResult
    {

        public bool Success { get; set; }


        public string Code { get; set; }

        public string Msg { get; set; }

        public ReturnResult()
        {
            this.Success = false;
            this.Code = "0";
            this.Msg = "";
        }
    }
}

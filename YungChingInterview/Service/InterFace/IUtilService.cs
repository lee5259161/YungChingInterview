﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.InterFace
{
    public interface IUtilService
    {
        string encryptPWD(string uid, string PWD);
    }
}

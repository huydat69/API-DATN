﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ApiResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}

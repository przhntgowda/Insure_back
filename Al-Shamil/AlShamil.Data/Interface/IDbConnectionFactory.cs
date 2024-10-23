﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data.Interface
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

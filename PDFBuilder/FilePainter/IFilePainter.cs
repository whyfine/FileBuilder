﻿using FileBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBuilder
{
    public interface IFilePainter
    {
        void Drawing(FileContext context);
    }
}

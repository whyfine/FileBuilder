﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDFBuilder.Models;

namespace PDFBuilder
{
    public interface IRenderer
    {
        PDFDocument Render();
    }
}

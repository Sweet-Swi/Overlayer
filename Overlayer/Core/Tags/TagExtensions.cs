﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlayer.Core
{
    public static class TagExtensions
    {
        public static float Round(this float value, double digits)
        {
            if (digits == -1)
                return value;
            return (float)Math.Round(value, (int)digits);
        }
        public static float Round(this double value, double digits)
        {
            if (digits == -1)
                return (float)value;
            return (float)Math.Round(value, (int)digits);
        }
    }
}

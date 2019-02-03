using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Con
{
    public partial class Cout
    {
        public ConColor ConColor
        {
            get
            {
                return new ConColor
                {
                    BackgroundColor = Console.BackgroundColor,
                    ForegroundColor = Console.ForegroundColor,
                };
            }
            set
            {
                Console.BackgroundColor = value.BackgroundColor;
                Console.ForegroundColor = value.ForegroundColor;
            }
        }

    }
}

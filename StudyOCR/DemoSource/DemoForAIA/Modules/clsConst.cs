using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoForAIA
{
    public class clsConst
    {
        public enum BatchStatus : int
        {
            New = 0,
            Assigned = 1,
            Printed = 2,
            HaveVoid = 3,
            NeedPrint = 4,
            CanVoid = 5
        }
    }
}

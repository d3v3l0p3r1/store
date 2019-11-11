using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Exceptions.Balance
{
    public class BalanceBelowZeroException : Exception
    {
        public BalanceBelowZeroException() : base("Нет достаточного количества товара для продажи")
        {

        }
    }
}

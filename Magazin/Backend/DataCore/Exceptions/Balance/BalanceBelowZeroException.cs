using System;

namespace BaseCore.DAL.Implementations.Exceptions.Balance
{
    public class BalanceBelowZeroException : Exception
    {
        public BalanceBelowZeroException() : base("Нет достаточного количества товара для продажи")
        {

        }
    }
}

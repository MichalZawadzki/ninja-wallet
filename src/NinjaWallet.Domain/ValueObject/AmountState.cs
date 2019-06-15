using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaWallet.Domain.ValueObject
{
    public class AmountState
    {
        public Money Amount { get; }
        public DateTimeOffset Date { get; }

        public AmountState(Money amount, DateTimeOffset date)
        {
            Amount = amount;
            Date = date;
        }
    }
}

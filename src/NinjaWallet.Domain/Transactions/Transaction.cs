using NinjaWallet.Domain.ValueObject;
using System;

namespace NinjaWallet.Domain.Transactions
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Money Amount { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Text { get; set; }

        public Transaction(Money amount, DateTimeOffset date, string text)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Date = date;
            Text = text;
        }

        public Transaction(Guid id, Money amount, DateTimeOffset date, string text)
        {
            Id = id;
            Amount = amount;
            Date = date;
            Text = text;
        }
    }
}

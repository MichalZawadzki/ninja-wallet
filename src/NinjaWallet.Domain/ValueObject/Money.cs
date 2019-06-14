using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaWallet.Domain.ValueObject
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "PLN")
        {
            Amount = amount;
            Currency = currency ?? "PLN";
        }

        public static Money operator+(Money firstAmount, Money secondAmount)
        {
            if (firstAmount == null || secondAmount == null)
            {
                throw new ArgumentException("At least one amount is null");
            }

            if(firstAmount.Currency != secondAmount.Currency)
            {
                throw new Exception("Amounts have different currencies");
            }

            return new Money(firstAmount.Amount + secondAmount.Amount, firstAmount.Currency);
        }

        public static Money operator -(Money firstAmount, Money secondAmount)
        {
            if (firstAmount == null || secondAmount == null)
            {
                throw new ArgumentException("At least one amount is null");
            }

            if (firstAmount.Currency != secondAmount.Currency)
            {
                throw new Exception("Amounts have different currencies");
            }

            return new Money(firstAmount.Amount - secondAmount.Amount, firstAmount.Currency);
        }

        public static Money operator -(Money amount)
        {
            if(amount == null)
            {
                throw new ArgumentException("Amount is null");
            }

            return new Money(-amount.Amount, amount.Currency);
        }

        public static Money operator *(Money amount, decimal factor)
        {
            if (amount == null)
            {
                throw new ArgumentException("Amount is null");
            }

            return new Money(amount.Amount * factor, amount.Currency);
        }
    }
}

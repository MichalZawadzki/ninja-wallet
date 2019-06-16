using NinjaWallet.Domain.Bills;
using NinjaWallet.Domain.Transactions;
using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NinjaWallet.Domain.Tests.Unit.BillTests
{
    public class GetCurrentAmount_Bill
    {
        [TestCase(new int[] { 10, -10, 20 }, 20)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { -3450, -10, 20 }, -3440)]
        public void GetCurrentAmount_BillWithTransactionsNoInitialState_AmountReturned(int[] values, decimal expectedValue)
        {
            // Arrange
            Bill bill = new Bill("PLN");
            foreach (var value in values)
            {
                Transaction transaction = new Transaction(new Money((decimal)value, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
                bill.AddTransaction(transaction);
            }
            
            // Act
            Money result = bill.GetCurrentAmount();

            // Assert
            Assert.AreEqual(expectedValue, result.Amount);
        }

        [TestCase(new int[] { 10, -10, 20 }, 60, 80)]
        [TestCase(new int[] { }, 0, 0)]
        [TestCase(new int[] { -3450, -10, 20 }, -500, -3940)]
        public void GetCurrentAmount_BillWithTransactionsAndInitialState_AmountReturned(int[] values, decimal initialValue, decimal expectedValue)
        {
            // Arrange
            Bill bill = new Bill("PLN");
            bill.SetInitialAmount(new AmountState(new Money(initialValue), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero)));
            foreach (var value in values)
            {
                Transaction transaction = new Transaction(new Money((decimal)value, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
                bill.AddTransaction(transaction);
            }

            // Act
            Money result = bill.GetCurrentAmount();

            // Assert
            Assert.AreEqual(expectedValue, result.Amount);
        }
    }
}
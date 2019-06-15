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
        [TestCase(10, "PLN", 20, "PLN", 30, "PLN")]
        [TestCase(0, "PLN", 0, "PLN", 0, "PLN")]
        [TestCase(-10, "PLN", 0, "PLN", -10, "PLN")]
        public void GetCurrentAmount_BillWith2TransactionsNoInitialState_AmountReturned(
            decimal firstValue,
            string firstCurrency,
            decimal secondValue,
            string secondCurrency,
            decimal resultValue,
            string resultCurrency)
        {
            // Arrange
            Bill bill = new Bill();
            Transaction firstTransaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
            Transaction secondTransaction = new Transaction(new Money(200, "PLN"), new DateTimeOffset(2010, 10, 11, 10, 10, 10, TimeSpan.Zero), "Dinner");
            bill.AddTransaction(firstTransaction);
            bill.AddTransaction(secondTransaction);

            // Act
            ICollection<Transaction> result = bill.GetAllTransactions();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(firstTransaction, result.First());
            Assert.AreEqual(secondTransaction, result.Last());
        }
    }
}
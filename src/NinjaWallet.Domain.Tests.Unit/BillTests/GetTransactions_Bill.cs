using NinjaWallet.Domain.ValueObject;
using NinjaWallet.Domain.Bills;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NinjaWallet.Domain.Transactions;

namespace NinjaWallet.Domain.Tests.Unit.BillTests
{
    public class GetTransactions_Bill
    {
        [Test]
        public void GetAllTransactions_EmptyBill_EmpltyListReturned()
        {
            // Arrange
            Bill bill = new Bill();

            // Act
            ICollection<Transaction> result = bill.GetAllTransactions();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetAllTransactions_BillWith2Transactions_ListReturned()
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

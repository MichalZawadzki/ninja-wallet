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
    public class AddTransaction_Bill
    {
        [Test]
        public void AddTransaction_AddIncomeTransactionToEmptyBill_IncomeAdded()
        {
            // Arrange
            Bill bill = new Bill();
            Transaction transaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");

            // Act
            bill.AddTransaction(transaction);

            // Assert
            Assert.AreEqual(1, bill.Transactions.Count);
            Assert.AreEqual(transaction, bill.Transactions.FirstOrDefault());
        }

        [Test]
        public void AddTransaction_AddIncomeTransactionWithDifferentCurrency_ExceptionThrown()
        {
            // Arrange
            Bill bill = new Bill("PLN");
            Transaction transaction = new Transaction(new Money(100, "USD"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");

            // Act
            void addTransaction() => bill.AddTransaction(transaction);

            // Assert
            Exception ex = Assert.Throws<Exception>(() => addTransaction());
            Assert.AreEqual(ex.Message, "Transaction has different currency than bill");
        }

        [Test]
        public void AddTransaction_Add2DifferentIncomeTransactionsToEmptyBill_IncomeAdded()
        {
            // Arrange
            Bill bill = new Bill();
            Transaction firstTransaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
            Transaction secondTransaction = new Transaction(new Money(200, "PLN"), new DateTimeOffset(2010, 10, 11, 10, 10, 10, TimeSpan.Zero), "Dinner");

            // Act
            bill.AddTransaction(firstTransaction);
            bill.AddTransaction(secondTransaction);

            // Assert
            Assert.AreEqual(2, bill.Transactions.Count);
            Assert.AreEqual(firstTransaction, bill.Transactions.First());
            Assert.AreEqual(secondTransaction, bill.Transactions.Last());
        }

        [Test]
        public void AddTransaction_Add2IdenticalIncomeTransactionsToEmptyBill_ExceptionThrown()
        {
            // Arrange
            Bill bill = new Bill();
            Transaction transaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");

            // Act
            void addFirstTransactionDelegate() => bill.AddTransaction(transaction);
            void addIdenticalTransactionDelegate() => bill.AddTransaction(transaction);

            // Assert
            Assert.That(() => addFirstTransactionDelegate(), Throws.Nothing);
            Exception ex = Assert.Throws<Exception>(() => addIdenticalTransactionDelegate());
            Assert.That(ex.Message, Is.EqualTo("Transaction is already added to the bill"));
        }

        [Test]
        public void AddTransaction_AddNullTransaction_ExceptionThrown()
        {
            // Arrange
            Bill bill = new Bill();
            Transaction transaction = null;

            // Act
            void addTransactionDelegate() => bill.AddTransaction(transaction);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(() => addTransactionDelegate());
            Assert.That(ex.Message, Is.EqualTo("Transaction is null"));
        }
    }
}

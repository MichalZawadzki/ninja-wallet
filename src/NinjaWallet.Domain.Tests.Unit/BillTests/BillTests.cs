using NinjaWallet.Domain.Bill;
using NinjaWallet.Domain.Transactions;
using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tests.BillTests
{
    public class BillTests
    {
        [Test]
        public void SetInitialAmount_WithAllInformation_SuccesfullSet()
        {
            // Arrange
            Bill bill = new Bill("PLN");
            AmountState initialAmount = new AmountState(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero));

            // Act
            bill.SetInitialAmount(initialAmount);

            // Assert
            Assert.AreEqual(initialAmount.Amount, bill.InitialAmount.Amount);
            Assert.AreEqual(initialAmount.Date, bill.InitialAmount.Date);
        }

        [Test]
        public void SetInitialAmount_WithDifferentCurrency_ExceptionThrown()
        {
            // Arrange
            Bill bill = new Bill("PLN");
            AmountState initialAmount = new AmountState(new Money(100, "USD"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero));

            // Act
            void setInitialAmount() => bill.SetInitialAmount(initialAmount);

            // Assert
            Exception ex = Assert.Throws<Exception>(() => setInitialAmount());
            Assert.AreEqual(ex.Message, "Initial amount is in different currency");
        }

        [Test]
        public void SetInitialAmount_SetNull_InitialAmountCleared()
        {
            // Arrange
            Bill bill = new Bill("PLN");
            AmountState initialAmount = new AmountState(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero));
            bill.SetInitialAmount(initialAmount);

            // Act
            bill.SetInitialAmount(null);

            // Assert
            Assert.IsNull(bill.InitialAmount);
        }

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

        [Test]
        public void RemoveTransaction_ExistinTransaction_SuccesfullRemoved()
        {
            // Arrange
            Bill bill = new Bill("PLN");
            Transaction transaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
            bill.AddTransaction(transaction);

            // Act
            bill.RemoveTransactionFromBill(transaction.Id);

            // Assert
            Assert.AreEqual(0, bill.Transactions.Count);
        }

        [Test]
        public void RemoveTransaction_NotExistinTransaction_ExceptionThrown()
        {
            // Arrange
            Bill bill = new Bill();

            // Act
            void removeTransactionDelegate() => bill.RemoveTransactionFromBill(Guid.NewGuid());

            // Assert
            Exception ex = Assert.Throws<Exception>(() => removeTransactionDelegate());
            Assert.That(ex.Message, Is.EqualTo("Transaction not found"));
        }

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
using NinjaWallet.Domain.Bill;
using NinjaWallet.Domain.Transactions;
using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class BillTests
    {
        [Test]
        public void SetInitialAmount_WithAllInformation_SuccesfullSet()
        {
            Bill bill = new Bill();
            AmountState initialAmount = new AmountState(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero));

            bill.SetInitialAmount(initialAmount);

            Assert.AreEqual(initialAmount.Amount, bill.InitialAmount.Amount);
            Assert.AreEqual(initialAmount.Date, bill.InitialAmount.Date);
        }

        [Test]
        public void AddTransaction_AddIncomeTransactionToEmptyBill_IncomeAdded()
        {
            Bill bill = new Bill();
            Transaction transaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");

            bill.AddTransaction(transaction);

            Assert.AreEqual(1, bill.Transactions.Count);
            Assert.AreEqual(transaction, bill.Transactions.FirstOrDefault());
        }

        [Test]
        public void AddTransaction_Add2DifferentIncomeTransactionsToEmptyBill_IncomeAdded()
        {
            Bill bill = new Bill();
            Transaction firstTransaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");
            Transaction secondTransaction = new Transaction(new Money(200, "PLN"), new DateTimeOffset(2010, 10, 11, 10, 10, 10, TimeSpan.Zero), "Dinner");

            bill.AddTransaction(firstTransaction);
            bill.AddTransaction(secondTransaction);

            Assert.AreEqual(2, bill.Transactions.Count);
            Assert.AreEqual(firstTransaction, bill.Transactions.First());
            Assert.AreEqual(secondTransaction, bill.Transactions.Last());
        }

        [Test]
        public void AddTransaction_Add2IdenticalIncomeTransactionsToEmptyBill_ExceptionThrown()
        {
            Bill bill = new Bill();
            Transaction transaction = new Transaction(new Money(100, "PLN"), new DateTimeOffset(2010, 10, 10, 10, 10, 10, TimeSpan.Zero), "Cinema");

            void addFirstTransactionDelegate() => bill.AddTransaction(transaction);
            void addIdenticalTransactionDelegate() => bill.AddTransaction(transaction);

            Assert.That(() => addFirstTransactionDelegate(), Throws.Nothing);
            Exception ex = Assert.Throws<Exception>(() => addIdenticalTransactionDelegate());
            Assert.That(ex.Message, Is.EqualTo("Transaction is already added to the bill"));
        }

        [Test]
        public void AddTransaction_AddNullTransaction_ExceptionThrown()
        {
            Bill bill = new Bill();
            Transaction transaction = null;

            void addTransactionDelegate() => bill.AddTransaction(transaction);

            Exception ex = Assert.Throws<ArgumentException>(() => addTransactionDelegate());
            Assert.That(ex.Message, Is.EqualTo("Transaction is null"));
        }
    }
}
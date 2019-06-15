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
    public class RemoveTransaction_Bill
    {
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
    }
}

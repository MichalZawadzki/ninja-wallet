using NinjaWallet.Domain.ValueObject;
using NinjaWallet.Domain.Bills;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaWallet.Domain.Tests.Unit.BillTests
{
    public class SetInitialAmount_Bill
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
    }
}

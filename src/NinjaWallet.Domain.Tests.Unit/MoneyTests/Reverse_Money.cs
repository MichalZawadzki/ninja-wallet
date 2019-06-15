using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace NinjaWallet.Domain.Tests.Unit.MoneyTests
{
    public class Reverse_Money
    {
        [TestCase(10, "PLN", -10, "PLN")]
        [TestCase(0, "PLN", 0, "PLN")]
        [TestCase(-10, "PLN", 10, "PLN")]
        public void Reverse_NormalMoneyObject_SuccesfullCalculation(decimal firstValue, string  firstCurrency, decimal resultValue, string resultCurrency)
        {
            //Arrange
            Money amount = new Money(firstValue, firstCurrency);
            Money expectedAmount = new Money(resultValue, resultCurrency);

            //Act
            Money resultAmount = -amount;

            //Assert
            Assert.AreEqual(expectedAmount.Amount, resultAmount.Amount);
            Assert.AreEqual(expectedAmount.Currency, resultAmount.Currency);
        }

        [Test]
        public void Reverse_MoneyIsNull_FailedCalculation()
        {
            //Arrange
            Money amount = null;

            //Act
            object testDelegate() => -amount;

            //Assert
            Exception ex = Assert.Throws<ArgumentException>(() => testDelegate());
            Assert.That(ex.Message, Is.EqualTo("Amount is null"));
        }
    }
}
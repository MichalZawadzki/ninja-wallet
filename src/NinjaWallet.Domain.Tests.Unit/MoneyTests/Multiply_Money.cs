using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace NinjaWallet.Domain.Tests.Unit.MoneyTests
{
    public class Multiply_Money
    {
        [TestCase(10, "PLN", 1, 10, "PLN")]
        [TestCase(0, "PLN", 0, 0, "PLN")]
        [TestCase(-10, "PLN", 0.1, -1, "PLN")]
        [TestCase(-10, "PLN", -0.5, 5, "PLN")]
        public void Multiply_NormalMoneyObjects_SuccesfullCalculation(decimal firstValue, string  firstCurrency, decimal factor, decimal resultValue, string resultCurrency)
        {
            //Arrange
            Money firstAmount = new Money(firstValue, firstCurrency);
            Money expectedAmount = new Money(resultValue, resultCurrency);

            //Act
            Money resultAmount = firstAmount * factor;

            //Assert
            Assert.AreEqual(expectedAmount.Amount, resultAmount.Amount);
            Assert.AreEqual(expectedAmount.Currency, resultAmount.Currency);
        }

        [TestCase(5)]
        public void Multiply_MoneyIsNull_FailedCalculation(decimal factor)
        {
            //Arrange
            Money amount = null;

            //Act
            object testDelegate() => amount * factor;

            //Assert
            Exception ex = Assert.Throws<ArgumentException>(() => testDelegate());
            Assert.That(ex.Message, Is.EqualTo("Amount is null"));
        }
    }
}
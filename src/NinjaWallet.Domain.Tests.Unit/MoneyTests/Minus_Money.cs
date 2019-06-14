using NinjaWallet.Domain.ValueObject;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;

namespace Tests.MoneyTests
{
    public class Minus_Money
    {
        [TestCase(10, "PLN", 20, "PLN", -10, "PLN")]
        [TestCase(0, "PLN", 0, "PLN", 0, "PLN")]
        [TestCase(-10, "PLN", -0, "PLN", -10, "PLN")]
        [TestCase(-10, "PLN", -20, "PLN", 10, "PLN")]
        public void Subtract_NormalMoneyObject_SuccesfullCalculation(decimal firstValue, string  firstCurrency, decimal secondValue, string secondCurrency, decimal resultValue, string resultCurrency)
        {
            //Arrange
            Money firstAmount = new Money(firstValue, firstCurrency);
            Money secondAmount = new Money(secondValue, secondCurrency);
            Money expectedAmount = new Money(resultValue, resultCurrency);

            //Act
            Money resultAmount = firstAmount - secondAmount;

            //Assert
            Assert.AreEqual(expectedAmount.Amount, resultAmount.Amount);
            Assert.AreEqual(expectedAmount.Currency, resultAmount.Currency);
        }

        [TestCase(0, "PLN", 0, "USD")]
        public void Subtract_MoneyWithDifferentCurrencies_FailedCalculation(decimal firstValue, string firstCurrency, decimal secondValue, string secondCurrency)
        {
            //Arrange
            Money firstAmount = new Money(firstValue, firstCurrency);
            Money secondAmount = new Money(secondValue, secondCurrency);

            //Act
            object testDelegate() => firstAmount - secondAmount;

            //Assert
            Exception ex = Assert.Throws<Exception>(() => testDelegate());
            Assert.That(ex.Message, Is.EqualTo("Amounts have different currencies"));
        }

        [TestCase(10, "PLN")]
        public void Subtract_MoneyWithOneNull_FailedCalculation(decimal firstValue, string firstCurrency)
        {
            //Arrange
            Money firstAmount = new Money(firstValue, firstCurrency);
            Money secondAmount = null;

            //Act
            object testDelegate() => firstAmount - secondAmount;

            //Assert
            Exception ex = Assert.Throws<ArgumentException>(() => testDelegate());
            Assert.That(ex.Message, Is.EqualTo("At least one amount is null"));
        }
    }
}
﻿using NinjaWallet.Domain.Transactions;
using NinjaWallet.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaWallet.Domain.Bills
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BillType Type { get; set; }
        public string Currency { get; set; }
        public AmountState InitialAmount { get; private set; }
        public ICollection<Transaction> Transactions { get; }

        public Bill()
        {
            Currency = "PLN";
            Transactions = new List<Transaction>();
        }

        public Bill(string currecy)
        {
            Currency = currecy;
            Transactions = new List<Transaction>();
        }

        public void SetInitialAmount(AmountState initialAmount)
        {
            if (initialAmount == null || initialAmount.Amount.Currency == Currency)
            {
                InitialAmount = initialAmount;
            }
            else
            {
                throw new Exception("Initial amount is in different currency");
            }
        }

        public Money GetCurrentAmount()
        {
            Money currentAmount = InitialAmount?.Amount ?? new Money(decimal.Zero, "PLN");

            foreach (Transaction transaction in Transactions)
            {
                currentAmount = currentAmount + transaction.Amount;
            }

            return currentAmount;
        }

        public void AddTransaction(Transaction newTransaction)
        {
            if (newTransaction == null)
            {
                throw new ArgumentException("Transaction is null");
            }

            if (Transactions.Any(transaction => transaction.Id == newTransaction.Id))
            {
                throw new Exception("Transaction is already added to the bill");
            }

            if (newTransaction.Amount.Currency != Currency)
            {
                throw new Exception("Transaction has different currency than bill");
            }

            Transactions.Add(newTransaction);
        }

        public void RemoveTransactionFromBill(Guid transactionId)
        {
            Transaction transactionToRemove = Transactions.FirstOrDefault(transaction => transaction.Id == transactionId);
            if (transactionToRemove == null)
            {
                throw new Exception("Transaction not found");
            }

            Transactions.Remove(transactionToRemove);
        }

        public ICollection<Transaction> GetAllTransactions()
        {
            return Transactions;
        }
    }
}

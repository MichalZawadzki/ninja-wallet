using NinjaWallet.Domain.ValueObject;
using NinjaWallet.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaWallet.Domain.Bill
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BillType Type { get; set; }
        public AmountState InitialAmount { get; private set; }
        public ICollection<Transaction> Transactions { get; private set; }

        public Bill()
        {
            Transactions = new List<Transaction>();
        }

        public void SetInitialAmount(AmountState initialAmount)
        {
            InitialAmount = initialAmount;
        }

        public Money GetCurrentAmount()
        {
            throw new NotImplementedException();
        }

        public void AddTransaction(Transaction transaction)
        {
            if(transaction == null)
            {
                throw new ArgumentException("Transaction is null");
            }

            Transactions.Add(transaction);
        }

        public void RemoveTransactionFromBill(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Transaction> GetAllTransactions()
        {
            throw new NotImplementedException();
        }
    }
}

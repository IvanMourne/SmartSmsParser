namespace SmartSmsParser
{
    public static class Converter
    {
        public static Paymaster ToPaymaster(this SmartSms smartSms)
        {
            var result = new Paymaster();
            foreach (var operation in smartSms.Operations)
            {
                if (!operation.AffectStatistics) continue;
                var transaction = new Paymaster.Transaction
                {
                    AccountName = string.Join(' ', operation.AccountName, operation.CardNumber),
                    Category = operation.Category,
                    Comment = operation.SmsText,
                    Currency = operation.TransactionCurrency,
                    Date = DateOnly.FromDateTime(operation.Date.Date),
                    Sum = operation.TransactionAmount,
                    Type = OperationKindToTransactionType(operation.OperationKind ?? operation.OperationType),
                    Tag = string.Empty
                };
                result.Transactions.Add(transaction);
                var existedAccount = result.Accounts.FirstOrDefault(x => x.Name == transaction.AccountName);
                if (existedAccount == null)
                {
                    var account = new Paymaster.Account
                    {
                        Name = transaction.AccountName,
                        Currency = operation.Currency,
                        Sum = operation.Balance,
                    };
                    result.Accounts.Add(account);
                }
                else
                {
                    existedAccount.Sum = operation.Balance;
                }
            }
            return result;
        }

        private static string OperationKindToTransactionType(string operationKind) 
        {
            return operationKind.IsAnyOf("EXPENSE", "TRANSFER_OUT", "CASH_WITHDRAWAL")
                ? "Expense"
                : operationKind.IsAnyOf("TRANSFER_IN", "CASH_TRANSACTION", "CASH_BACK", "REVERSAL")
                ? "Income"
                : throw new Exception("Unexpected OperationKind: " + operationKind);
        }

        private static bool IsAnyOf(this string text, params string[] values)
        {
            foreach (var item in values)
            {
                if (text == item)
                    return true;
            }
            return false;
        }
    }
}

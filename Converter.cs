namespace SmartSmsParser
{
    public static class Converter
    {
        public static Paymaster ToPaymaster(this SmartSms smartSms)
        {
            var result = new Paymaster();
            foreach (var operation in smartSms.Operations)
            {
                if (!operation.AffectStatistics)
                    continue;

                var transaction = new Paymaster.Transaction
                {
                    AccountName = ParseAccountName(operation),
                    Category = operation.Category,
                    Comment = operation.SmsText,
                    Currency = operation.TransactionCurrency,
                    Date = DateOnly.FromDateTime(operation.Date.Date),
                    Sum = operation.TransactionAmount,
                    Type = operation.TransactionAmount > 0 ? "Income" : "Expense",
                    Tag = string.Empty
                };

                if (UpdateTransactions(result, transaction))
                {
                    UpdateAccounts(result, transaction, operation.Balance);
                }
            }
            return result;
        }

        private static string ParseAccountName(SmartSms.Operation operation)
        {
            if (!string.IsNullOrEmpty(operation.SmsText))
            {
                var index = operation.SmsText.IndexOf('*');
                var cardName = operation.SmsText.Contains("Карта") ? "Карта"
                             : operation.SmsText.Contains("Счет") ? "Счет"
                             : operation.AccountName;
                return string.Join(' ', cardName, operation.SmsText.Substring(index, 5));
            }
            if (operation.OperationType == "CASH_TRANSACTION" || operation.AccountName == "My account")
            {
                return "Мой кошелек";
            }
            return operation.AccountName;
        }

        private static bool UpdateTransactions(Paymaster result, Paymaster.Transaction transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.AccountName))
            {
                Console.WriteLine($"Could not define Account name. Operation date: {transaction.Date}, sum: {transaction.Sum}");
                return false;
            }

            if (transaction.Type == "Income" && string.IsNullOrEmpty(transaction.Comment) && transaction.Category != "Зарплата")
                return false;

            if (transaction.Category == "Перевод" && string.IsNullOrEmpty(transaction.Comment))
                return false;

            result.Transactions.Add(transaction);
            return true;
        }

        private static void UpdateAccounts(Paymaster result, Paymaster.Transaction transaction, double balance)
        {
            if (string.IsNullOrEmpty(transaction.AccountName))
                return;

            var existedAccount = result.Accounts.Find(x => x.Name == transaction.AccountName);

            if (existedAccount == null)
            {

                var account = new Paymaster.Account
                {
                    Name = transaction.AccountName,
                    Currency = transaction.Currency,
                    Sum = balance,
                };
                result.Accounts.Add(account);
            }
            else
            {
                existedAccount.Sum = balance;
            }
        }
    }
}

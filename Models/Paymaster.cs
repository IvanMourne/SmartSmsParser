namespace SmartSmsParser
{
    public class Paymaster
    {
        public List<Account> Accounts { get; set; } = [];
        public List<Transaction> Transactions { get; set; } = [];

        public class Account
        {
            public string Name { get; set; }
            public double Sum { get; set; }
            public string Currency { get; set; }

            public string GetValue(int cellIndex)
            {
                return cellIndex switch
                {
                    0 => Name,
                    1 => Sum.ToString(),
                    2 => Currency,
                    _ => throw new ArgumentOutOfRangeException("Unexpected cellIndex: " + cellIndex),
                };
            }
        }

        public class Transaction
        {
            public DateOnly Date { get; set; }
            public string Category { get; set; }
            public string AccountName { get; set; }
            public double Sum { get; set; }
            public string Currency { get; set; }
            public string Type { get; set; }
            public string Tag { get; set; }
            public string Comment { get; set; }

            public string GetValue(int cellIndex)
            {
                return cellIndex switch
                {
                    0 => Date.ToString(),
                    1 => Category,
                    2 => AccountName,
                    3 => Sum.ToString(),
                    4 => Currency,
                    5 => Type,
                    6 => Tag,
                    7 => Comment,
                    _ => throw new ArgumentOutOfRangeException("Unexpected cellIndex: " + cellIndex),
                };
            }
        }
    }
}

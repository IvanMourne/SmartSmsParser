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
        }
    }
}

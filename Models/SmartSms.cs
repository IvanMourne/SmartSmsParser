using System.Text.Json.Serialization;

namespace SmartSmsParser
{
    public class SmartSms
    {
        [JsonPropertyName("operations")]
        public Operation[] Operations {  get; set; }

        public class Operation
        {
            [JsonPropertyName("operationType")]
            public string OperationType { get; set; }

            [JsonPropertyName("operationKind")]
            public string OperationKind { get; set; }

            [JsonPropertyName("date")]
            public DateTimeOffset Date { get; set; }

            [JsonPropertyName("smsText")]
            public string SmsText { get; set; }

            [JsonPropertyName("transactionAmount")]
            public double TransactionAmount { get; set; }

            [JsonPropertyName("transactionCurrency")]
            public string TransactionCurrency { get; set; }

            [JsonPropertyName("clientAmount")]
            public double ClientAmount { get; set; }

            [JsonPropertyName("balance")]
            public double Balance { get; set; }

            [JsonPropertyName("cardNumber")]
            public string CardNumber { get; set; }

            [JsonPropertyName("accountName")]
            public string AccountName { get; set; }

            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("detectedMerchant")]
            public string DetectedMerchant { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }

            [JsonPropertyName("affectStatistics")]
            public bool AffectStatistics { get; set; }
        }
    }
}

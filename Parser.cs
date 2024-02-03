namespace SmartSmsParser
{
    public partial class Program
    {
        public static void Parse(string jsonPath) 
        {
            var json = File.ReadAllText(jsonPath);
            var operations = System.Text.Json.JsonSerializer.Deserialize<SmartSms>(json);
            var paymaster = Converter.ToPaymaster(operations);
            Excel.Write(paymaster);
        }
    }
}

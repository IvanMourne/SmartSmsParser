namespace SmartSmsParser
{
    public partial class Program
    {
        public static void Parse(string jsonPath, string xlsxPath) 
        {
            var json = File.ReadAllText(jsonPath);
            var operations = System.Text.Json.JsonSerializer.Deserialize<SmartSms>(json);
            //var templatePath = Path.Combine(Environment.CurrentDirectory, "Excel", "Template", "Paymaster import template.xlsx");
            //var paymaster = Excel.Read(templatePath);
            var paymaster = Converter.ToPaymaster(operations);
            Excel.Write(paymaster);
        }
    }
}

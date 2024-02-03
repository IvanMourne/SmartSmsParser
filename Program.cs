namespace SmartSmsParser
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var jsonName = "report.json";
            if (args.Length == 0 && !File.Exists(jsonName))
            {
                Console.WriteLine("Specify full path to report.json or place file in working directory");
                return;
            }
            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments");
                return;
            }
            if (args.Length == 1 && !args[0].EndsWith(".json") && !args[0].EndsWith(".xlsx"))
            {
                Console.WriteLine("Incorrect file extension");
                return;
            }
            Parse(args.Length == 1 ? args[0] : jsonName);
        }
    }
}
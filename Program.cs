namespace SmartSmsParser
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Add arguments: <input.json> <output.xlsx>");
            }
            if (!args[0].EndsWith(".json"))
            {
                throw new ArgumentException("Incorrect json path");
            }
            if (!args[1].EndsWith(".xlsx")) 
            {
                throw new ArgumentException("Incorrect xlsx path");
            }
           Parse(args[0], args[1]);
        }
    }
}
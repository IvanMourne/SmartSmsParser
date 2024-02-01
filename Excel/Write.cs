using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SmartSmsParser
{
    public partial class Excel
    {
        public static void Write(Paymaster paymaster)
        {
            using var fs = new FileStream("result.xlsx", FileMode.Create, FileAccess.Write);
            var workbook = new XSSFWorkbook();
            AddTransactions(workbook, paymaster.Transactions);
            AddAccounts(workbook, paymaster.Accounts);
            workbook.Write(fs);
        }

        private static void AddTransactions(XSSFWorkbook workbook, List<Paymaster.Transaction> transactions )
        {
            ISheet excelSheet = workbook.CreateSheet("Transactions");

            var columns = new List<string>();
            IRow row = excelSheet.CreateRow(0);
            int columnIndex = 0;

            foreach (var columnName in TransactionHeaders)
            {
                columns.Add(columnName);
                row.CreateCell(columnIndex).SetCellValue(columnName);
                columnIndex++;
            }

            int rowIndex = 1;
            foreach (var transaction in transactions)
            {
                row = excelSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (var col in columns)
                {
                    row.CreateCell(cellIndex).SetCellValue(transaction.GetValue(cellIndex));
                    cellIndex++;
                }
                rowIndex++;
            }
        }

        private static void AddAccounts(XSSFWorkbook workbook, List<Paymaster.Account> accounts)
        {
            ISheet excelSheet = workbook.CreateSheet("Accounts");

            var columns = new List<string>();
            IRow row = excelSheet.CreateRow(0);
            int columnIndex = 0;

            foreach (var columnName in AccountHeaders)
            {
                columns.Add(columnName);
                row.CreateCell(columnIndex).SetCellValue(columnName);
                columnIndex++;
            }

            int rowIndex = 1;
            foreach (var account in accounts)
            {
                row = excelSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (var col in columns)
                {
                    row.CreateCell(cellIndex).SetCellValue(account.GetValue(cellIndex));
                    cellIndex++;
                }
                rowIndex++;
            }
        }
    }
}

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SmartSmsParser
{
    public partial class Excel
    {
        public static readonly string[] TransactionHeaders =
        [
            "Date",
            "Category",
            "Account name",
            "Sum",
            "Currency",
            "Type",
            "Tag",
            "Comment text"
        ];

        public static readonly string[] AccountHeaders =
        [
            "Account name",
            "Sum",
            "Currency"
        ];

        public static Paymaster Read(string path)
        {
            ISheet sheet;
            var result = new Paymaster();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.Position = 0;
                var xssWorkbook = new XSSFWorkbook(stream);
                for (var s = 0; s < 2; s++)
                {
                    sheet = xssWorkbook.GetSheetAt(s);
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        {
                            var value = cell.ToString();
                            if ((s == 0 && value != TransactionHeaders[j]) || (s == 1 && value != AccountHeaders[j]))
                            {
                                throw new Exception("Unexpected column: " + value);
                            }
                        }
                    }
                    for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        var rowList = new List<string>();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                                {
                                    rowList.Add(row.GetCell(j).ToString());

                                }
                            }
                        }
                        if (rowList.Count > 0)
                        {
                            if (s == 0)
                            {
                                var transaction = new Paymaster.Transaction
                                {
                                    Date = DateOnly.Parse(rowList[0]),
                                    Category = rowList[1],
                                    AccountName = rowList[2],
                                    Sum = double.Parse(rowList[3]),
                                    Currency = rowList[4],
                                    Type = rowList[5],
                                    Tag = rowList.Count > 6 ? rowList[6] : null,
                                    Comment = rowList.Count > 7 ? rowList[7] : null
                                };
                                result.Transactions.Add(transaction);
                            }
                            if (s == 1)
                            {
                                var account = new Paymaster.Account
                                {
                                    Name = rowList[0],
                                    Sum = double.Parse(rowList[1]),
                                    Currency = rowList[2],
                                };
                                result.Accounts.Add(account);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

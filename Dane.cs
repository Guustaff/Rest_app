using OfficeOpenXml;
public class Item
{


    public int RowNumber { get; set; } = 0;
    public string Segment { get; set; } = "";
    public string Country { get; set; } = "";
    public string Product { get; set; } = "";
    public decimal UnitSold { get; set; } = 0;


    public Item(int RowNumber, string Segment, string Country, string Product, decimal UnitSold)
    {


        this.RowNumber = RowNumber;
        this.Segment = Segment;
        this.Country = Country;
        this.Product = Product;
        this.UnitSold = UnitSold;
    }
}



public  class DataContainer
{



    public static List<Item> DataList { get; set; } = _read();

    private const string FileName = "sample-xlsx-file-for-testing.xlsx";
    public static DataContainer _instance = null;
    public static DataContainer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new();
            }
            return _instance;
        }
    }



    public static List<Item> _read()
    {
        List<Item> _dataList = new List<Item>();
        System.Console.WriteLine(DateTime.Now.ToShortTimeString()); // test wielokrotności czytania
        using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(FileName)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
            int rows = worksheet.Dimension.End.Row;
            //  rows = 1000000000;



            for (int row = 2; row <= rows; row++)
            {
                string? segment = worksheet.Cells[row, 1].Value.ToString();
                string? country = worksheet.Cells[row, 2].Value.ToString();
                string? product = worksheet.Cells[row, 3].Value.ToString();
                decimal unitSold = Convert.ToDecimal(worksheet.Cells[row, 5].Value);
                int rowNumber = row;


                if (segment != null && country != null && product != null)
                {
                    _dataList.Add(new Item(rowNumber, segment, country, product, (decimal)unitSold));
                }
            }
        }
        return _dataList;
    }
    public void WriteToExcel(int row, string segment, string country, string product, decimal unitSold)
    {
        FileInfo file = new FileInfo(FileName);
        using (ExcelPackage excelPackage = new ExcelPackage(file))
        {
            ExcelWorkbook excelWorkBook = excelPackage.Workbook;
            ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
            excelWorksheet.Cells[row, 1].Value = segment;
            excelWorksheet.Cells[row, 2].Value = country;
            excelWorksheet.Cells[row, 3].Value = product;
            excelWorksheet.Cells[row, 5].Value = unitSold;

            excelPackage.Save();
        }

    }

    public void DeleteFromExcel(int row)
    {
        FileInfo file = new FileInfo(FileName);
        using (ExcelPackage excelPackage = new ExcelPackage(file))
        {
            ExcelWorkbook excelWorkBook = excelPackage.Workbook;
            ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
            excelWorksheet.DeleteRow(row);

            excelPackage.Save();
        }
    }
}

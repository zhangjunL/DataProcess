using CommandLine;
using OfficeOpenXml;

class Program
{
    class Options
    {
        [Option('s', "sourceFilePath", Required = true, HelpText = "源Excel文件路径。")]
        public string SourceFilePath { get; set; }

        [Option('t', "targetDirectory", Required = true, HelpText = "目标目录路径。")]
        public string TargetDirectory { get; set; }

        [Option('r', "rowsPerFile", Required = true, HelpText = "每个文件的行数。")]
        public int RowsPerFile { get; set; }

        [Option('c', "startCell", Required = true, HelpText = "起始单元格引用。")]
        public string StartCell { get; set; }

        [Option('e', "endCell", Required = true, HelpText = "结束单元格引用。")]
        public string EndCell { get; set; }

        [Option('f', "fileNameTemplate", Required = true, HelpText = "自定义文件名模板。使用占位符来引用单元格，例如 {A1}_{B1}_{C1}.xlsx")]
        public string FileNameTemplate { get; set; }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Excel文件拆分工具");

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                if (!File.Exists(options.SourceFilePath))
                {
                    Console.WriteLine("错误: 源文件不存在。");
                    return;
                }

                if (!Directory.Exists(options.TargetDirectory))
                {
                    Console.WriteLine("错误: 目标目录不存在。");
                    return;
                }

                SplitAndCopyStyles(options.SourceFilePath, options.TargetDirectory, options.RowsPerFile, options.StartCell, options.EndCell, options.FileNameTemplate);
            });
    }
    /// <summary>
    /// 拆分Excel文件并复制样式 
    /// </summary>  
    /// <param name="sourceFilePath"> </param>
    /// <param name="targetDirectory"></param>
    /// <param name="rowsPerFile"></param>
    /// <param name="startCell"></param>
    /// <param name="endCell"></param>
    /// <param name="fileNameTemplate"></param>
    static void SplitAndCopyStyles(string sourceFilePath, string targetDirectory, int rowsPerFile, string startCell, string endCell, string fileNameTemplate)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var sourcePackage = new ExcelPackage(new FileInfo(sourceFilePath)))
        {
            var sourceWorksheet = sourcePackage.Workbook.Worksheets[0];
            int totalRows = sourceWorksheet.Dimension.End.Row;

            for (int startRow = 1; startRow <= totalRows; startRow += rowsPerFile)
            {
                int endRow = Math.Min(startRow + rowsPerFile - 1, totalRows);

                // 创建一个新的ExcelPackage和工作表
                using (var targetPackage = new ExcelPackage())
                {
                    var targetWorksheet = targetPackage.Workbook.Worksheets.Add("Sheet1");
                    var sourceRange = sourceWorksheet.Cells[startCell + startRow + ":" + endCell + endRow];
                    var targetRange = targetWorksheet.Cells["A1"];
                    sourceRange.Copy(targetRange);

                    // 设置列宽度
                    for (int col = 1; col <= sourceWorksheet.Dimension.End.Column; col++)
                    {
                        targetWorksheet.Column(col).Width = sourceWorksheet.Column(col).Width;
                    }

                    // 从单元格获取数据
                    string fileName = GenerateFileName(fileNameTemplate, targetWorksheet);

                    // 保存新的Excel文件
                    string targetFilePath = Path.Combine(targetDirectory, fileName);

                    // 确保目录存在
                    Directory.CreateDirectory(targetDirectory);

                    targetPackage.SaveAs(new FileInfo(targetFilePath));

                    Console.WriteLine($"已保存文件 {targetFilePath}");
                    
                }
            }
            Console.WriteLine("所有文件已完成");
        }
    }
    /// <summary>
    /// 从工作表中生成文件名
    /// </summary>
    /// <param name="fileNameTemplate"></param>
    /// <param name="worksheet"></param>
    /// <returns></returns>
    static string GenerateFileName(string fileNameTemplate, ExcelWorksheet worksheet)
    {
        var cleanedFileName = CleanFileName(fileNameTemplate);

        foreach (var cell in worksheet.Cells)
        {
            cleanedFileName = cleanedFileName.Replace($"{{{cell.Address}}}", cell.Text);
        }

        return cleanedFileName;
    }
    /// <summary>
    /// 清理文件名中的非法字符
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    static string CleanFileName(string fileName)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();
        return new string(fileName.Select(c => invalidChars.Contains(c) ? '_' : c).ToArray());
    }
}

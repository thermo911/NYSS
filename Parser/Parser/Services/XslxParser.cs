using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;
using Parser.Entities;

namespace Parser.Services
{
    public class XslxParser
    {
        private FileInfo _fileInfo;

        public XslxParser(string filePath)
        {
            _fileInfo = new FileInfo(filePath);
        }

        public List<CyberDangerInfo> ParseXslx()
        {
            List<CyberDangerInfo> infos = new List<CyberDangerInfo>();
            try
            {
                using (var package = new ExcelPackage(_fileInfo))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int colCount = worksheet.Dimension.End.Column;
                    int rowCount = worksheet.Dimension.End.Row;

                    for (int i = 3; i <= rowCount; i++)
                    {
                        CyberDangerInfo.Builder builder = CyberDangerInfo.BuildNew();
                        builder.Id(Convert.ToInt32(worksheet.Cells[i, 1].Value));
                        builder.Name(worksheet.Cells[i, 2].Value as string);
                        builder.Description(worksheet.Cells[i, 3].Value as string);
                        builder.Source(worksheet.Cells[i, 4].Value as string);
                        builder.Target(worksheet.Cells[i, 5].Value as string);


                        bool conViolation = Convert.ToInt32(worksheet.Cells[i, 6].Value) == 1;
                        bool intViolation = Convert.ToInt32(worksheet.Cells[i, 7].Value) == 1;
                        bool avaViolation = Convert.ToInt32(worksheet.Cells[i, 8].Value) == 1;

                        builder.ConfidentialityViolation(conViolation);
                        builder.IntegrityViolation(intViolation);
                        builder.AvaliabilityViolation(avaViolation);

                        infos.Add(builder.Build());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ОШИБКА!", String.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show($"Обнаружено записей: {infos.Count}");

            return infos;
        }
    }
}

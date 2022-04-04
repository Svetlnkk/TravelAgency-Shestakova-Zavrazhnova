using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyBusinessLogic.OfficePackage.HelperEnums;
using TravelAgencyBusinessLogic.OfficePackage.HelperModels;

namespace TravelAgencyBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var pc in info.Guides)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = pc.GuideName,
                    StyleInfo = ExcelStyleInfoType.TextWithBroder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "",
                    StyleInfo = ExcelStyleInfoType.TextWithBroder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = "",
                    StyleInfo = ExcelStyleInfoType.TextWithBroder
                });
                MergeCells(new ExcelMergeParameters
                {
                    CellFromName = "A" + rowIndex,
                    CellToName = "C" + rowIndex
                });
                rowIndex++;
            }
            SaveExcel(info);
        }
        protected abstract void CreateExcel(ExcelInfo info);
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        protected abstract void SaveExcel(ExcelInfo info);
    }
}

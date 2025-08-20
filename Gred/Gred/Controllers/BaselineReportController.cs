
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System.Data;
//using System.Dynamic;
//using Dapper;
//using System.Drawing;

//using ClosedXML.Excel;
//using gred.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Gred.Controllers
//{
//  [Route("api/[controller]")]
//  [ApiController]
//  public class BaselineReportController : Controller
//  {
//    private readonly IConfiguration _configuration;
//    private readonly GredDbContext _context;



//    public BaselineReportController(IConfiguration configuration, GredDbContext context)
//    {
//      _configuration = configuration;
//      _context = context;
//    }

//    [HttpGet("GetBaselineReportData")]
//    public async Task<IActionResult> GetBaselineCount()
//    {
//      try
//      {
//        var baseLine = await _context.VwBaselineRpts.ToListAsync();
//        return Ok(baseLine);
//      }catch(Exception ex)
//      {
//        return Ok(ex.Message);
//      }
//    }

//    [HttpGet("DownloadBaselineReport")]
//    public async Task<IActionResult> DownloadBaselineReport()
//    {
//      using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//      await connection.OpenAsync();

//      var query = "SELECT * FROM vw_BaselineRPT";
//      var data = await connection.QueryAsync<dynamic>(query);
//      var dataList = data.ToList();

//      var dataTable = new DataTable();

//      if (dataList.Any())
//      {
//        var firstRow = (IDictionary<string, object>)dataList.First();
//        foreach (var key in firstRow.Keys)
//        {
//          dataTable.Columns.Add(key);
//        }

//        foreach (var item in dataList)
//        {
//          var dict = (IDictionary<string, object>)item;
//          var row = dataTable.NewRow();
//          foreach (var key in dict.Keys)
//          {
//            row[key] = dict[key] ?? DBNull.Value;
//          }
//          dataTable.Rows.Add(row);
//        }
//      }
//      else
//      {
//        dataTable.Columns.Add("Message");
//        var row = dataTable.NewRow();
//        row["Message"] = "No data available.";
//        dataTable.Rows.Add(row);
//      }

//      using var workbook = new XLWorkbook();
//      var worksheet = workbook.Worksheets.Add("BaselineReport");

//      // 1. Merge top row and apply title style
//      int colCount = dataTable.Columns.Count;
//      var titleCell = worksheet.Range(1, 1, 1, colCount).Merge();
//      titleCell.Value = "Patient Information";
//      titleCell.Style.Font.Bold = true;
//      titleCell.Style.Font.FontSize = 14;
//      titleCell.Style.Fill.BackgroundColor = XLColor.Yellow;
//      titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
//      titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
//      worksheet.Row(1).Height = 25;

//      // 2. Insert the table below the title (start from row 2)
//      var tableStartRow = 2;
//      var excelTable = worksheet.Cell(tableStartRow, 1).InsertTable(dataTable, "BaselineReport", true);

//      // 3. Style header row (row 2)
//      var headerRow = worksheet.Row(tableStartRow);
//      headerRow.Style.Font.Bold = true;
//      headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
//      headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

//      // 4. Alternate row shading for data rows
//      var usedRange = worksheet.RangeUsed();
//      var totalRows = usedRange.RowCount();
//      for (int i = tableStartRow + 1; i <= totalRows; i++)
//      {
//        if ((i - tableStartRow) % 2 == 1)
//          worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.LightYellow;
//      }

//      // 5. Auto-fit all columns
//      worksheet.Columns().AdjustToContents();

//      // 6. Add thin borders
//      usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
//      usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

//      // 7. Freeze header row
//      worksheet.SheetView.FreezeRows(tableStartRow);

//      // 8. Save and return stream
//      using var stream = new MemoryStream();
//      workbook.SaveAs(stream);
//      stream.Position = 0;

//      Response.Headers.Add("X-Record-Count", dataList.Count.ToString());

//      return File(
//          stream.ToArray(),
//"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
//          "Baseline_Report.xlsx"
//      );
//    }
//  }

//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Dynamic;
using Dapper;
using System.Drawing;

using ClosedXML.Excel;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BaselineReportController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;

    public BaselineReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }

    [HttpGet("GetBaselineReportData")]
    public async Task<IActionResult> GetBaselineCount()
    {
      try
      {
        var baseLine = await _context.VwBaselineRpts.ToListAsync();
        return Ok(baseLine);
      }
      catch (Exception ex)
      {
        return Ok(ex.Message);
      }
    }

    [HttpGet("DownloadBaselineReport")]
    public async Task<IActionResult> DownloadBaselineReport()
    {
      using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
      await connection.OpenAsync();

      var query = "SELECT * FROM vw_BaselineRPT";
      var data = await connection.QueryAsync<dynamic>(query);
      var dataList = data.ToList();

      var dataTable = new DataTable();

      if (dataList.Any())
      {
        var firstRow = (IDictionary<string, object>)dataList.First();
        foreach (var key in firstRow.Keys)
        {
          dataTable.Columns.Add(key);
        }

        foreach (var item in dataList)
        {
          var dict = (IDictionary<string, object>)item;
          var row = dataTable.NewRow();
          foreach (var key in dict.Keys)
          {
            row[key] = dict[key] ?? DBNull.Value;
          }
          dataTable.Rows.Add(row);
        }
      }
      else
      {
        dataTable.Columns.Add("Message");
        var row = dataTable.NewRow();
        row["Message"] = "No data available.";
        dataTable.Rows.Add(row);
      }

      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("BaselineReport");

      // 1. Merge top row and apply title style
      int colCount = dataTable.Columns.Count;
      var titleCell = worksheet.Range(1, 1, 1, colCount).Merge();
      titleCell.Value = "Patient Information";
      titleCell.Style.Font.Bold = true;
      titleCell.Style.Font.FontSize = 14;
      titleCell.Style.Fill.BackgroundColor = XLColor.Yellow;
      titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
      titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
      worksheet.Row(1).Height = 25;

      // 2. Insert the table below the title (start from row 2)
      var tableStartRow = 2;
      var excelTable = worksheet.Cell(tableStartRow, 1).InsertTable(dataTable, "BaselineReport", true);

      // 3. Style header row (row 2)
      var headerRow = worksheet.Row(tableStartRow);
      headerRow.Style.Font.Bold = true;
      headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
      headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

      // 4. Alternate row shading for data rows
      var usedRange = worksheet.RangeUsed();
      var totalRows = usedRange.RowCount();
      for (int i = tableStartRow + 1; i <= totalRows; i++)
      {
        if ((i - tableStartRow) % 2 == 1)
          worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.LightYellow;
      }

      // 5. Auto-fit all columns
      worksheet.Columns().AdjustToContents();

      // 6. Add thin borders
      usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
      usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

      // 7. Freeze header row
      worksheet.SheetView.FreezeRows(tableStartRow);

      // 8. Save and return stream
      using var stream = new MemoryStream();
      workbook.SaveAs(stream);
      stream.Position = 0;

      Response.Headers.Add("X-Record-Count", dataList.Count.ToString());

      return File(
          stream.ToArray(),
"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          "Baseline_Report.xlsx"
      );
    }
  }

}



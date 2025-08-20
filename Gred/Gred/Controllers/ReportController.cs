using Dapper;
using Gred.Data.Entities.Common;
using Gred.Data.Entities;
using Gred.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Dynamic;
using ClosedXML.Excel;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportController : Controller
  {

    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;



    public ReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }


    [HttpGet("DownloadFullReport")]
    public async Task<IActionResult> DownloadFullReport()
    {
      CommonRsult cmnRes = new CommonRsult();
     
      var Data = await _context.VwCompletedRpts.ToListAsync();

      cmnRes.Data = Data;
      return Ok(cmnRes);

      /*
      using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
      await connection.OpenAsync();

      var query = "SELECT * FROM vw_CompletedRPT";
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
      Console.WriteLine("DataList count: " + dataList.Count);

      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("FullReport");
      worksheet.Cell(1, 1).InsertTable(dataTable, "FullReport", true);

      using var stream = new MemoryStream();
      workbook.SaveAs(stream);
      stream.Position = 0;

      return File(
          stream.ToArray(),
          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          "Full_Baseline_Report.xlsx"
      );*/
    }



  }
}

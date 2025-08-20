using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Dynamic;
using Dapper;
using ClosedXML.Excel;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompletedReportController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;



    public CompletedReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpGet("GetCompletedReportData")]
    public async Task<IActionResult> GetCompletedReportCount()
    {
      try
      {
        var data = await _context.VwCompletedRpts.ToListAsync();
        return Ok(data);
      }
      catch (Exception ex)
      {
        return Ok(ex.Message);
      }
    }


    [HttpGet("DownloadCompletedReport")]
    public async Task<IActionResult> DownloadCompletedReport()
    {
      try
      {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var query = "SELECT COUNT(*) FROM vw_CompletedRPT"; 
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
         
          var schemaQuery = @"
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = 'vw_CompletedRPT'
            ORDER BY ORDINAL_POSITION";

          var columnNames = await connection.QueryAsync<string>(schemaQuery);

          foreach (var col in columnNames)
          {
            dataTable.Columns.Add(col);
          }

          var row = dataTable.NewRow();
          dataTable.Rows.Add(row);
        }

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("CompletedReport");
        worksheet.Cell(1, 1).InsertTable(dataTable, "CompletedReport", true);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(
          stream.ToArray(),
          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          "Completed_Report.xlsx"
        );
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error generating Completed report: " + ex.Message);
        return StatusCode(500, new { type = "E", message = "Failed to generate Completed report." });
      }
    }
  }
}

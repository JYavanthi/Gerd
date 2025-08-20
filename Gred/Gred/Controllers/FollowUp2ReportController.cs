using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using ClosedXML.Excel;
using System.Data;
using System.Dynamic;
using gred.Data;
using Microsoft.EntityFrameworkCore;

namespace Gred.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FollowUp2ReportController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;



    public FollowUp2ReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpGet("DownloadFollowUp1Report")]
    public async Task<IActionResult> DownloadFollowUp1Report()
    {
      try
      {
        var data = await _context.VwFollowup2Rpts.ToListAsync();
        return Ok(data);
      }
      catch (Exception ex)
      {
        return Ok(ex.Message);
      }
    }
    /*
        [HttpGet("DownloadFollowUp2Report")]
        public async Task<IActionResult> DownloadFollowUp2Report()
        {
          try
          {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var query = "SELECT * FROM vw_Followup2RPT";
            var data = await connection.QueryAsync<dynamic>(query);
            var dataList = data.ToList();

            var dataTable = new DataTable();

            if (dataList.Any())
            {
              var firstRow = (IDictionary<string, object>)dataList.First();

              foreach (var key in firstRow.Keys)
              {
                if (!dataTable.Columns.Contains(key))
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
                WHERE TABLE_NAME = 'vw_Followup2RPT'
                ORDER BY ORDINAL_POSITION";

              var columnNames = await connection.QueryAsync<string>(schemaQuery);

              foreach (var col in columnNames)
              {
                if (!dataTable.Columns.Contains(col))
                  dataTable.Columns.Add(col);
              }


              var emptyRow = dataTable.NewRow();
              dataTable.Rows.Add(emptyRow);
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("FollowUp2Report");
            worksheet.Cell(1, 1).InsertTable(dataTable, "FollowUp2Report", true);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;


            Response.Headers["X-Record-Count"] = dataList.Count.ToString();

            return File(
              stream.ToArray(),
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
              "FollowUp2_Report.xlsx"
            );
          }
          catch (Exception ex)
          {
            Console.WriteLine("Error generating Follow-Up 2 report: " + ex.Message);
            return StatusCode(500, new { type = "E", message = "Failed to generate Follow-Up 2 report." });
          }
        }*/
  }
}

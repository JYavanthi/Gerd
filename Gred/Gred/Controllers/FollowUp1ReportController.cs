
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
  public class FollowUp1ReportController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly GredDbContext _context;



    public FollowUp1ReportController(IConfiguration configuration, GredDbContext context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpGet("DownloadFollowUp1Report")]
    public async Task<IActionResult> DownloadFollowUp1Report()
    {
      try
      {
        var data = await _context.VwFollowup1Rpts.ToListAsync();
        return Ok(data);
      }
      catch (Exception ex)
      {
        return Ok(ex.Message);
      }
    }/*
    [HttpGet("DownloadFollowUp1Report")]
    public async Task<IActionResult> DownloadFollowUp1Report()
    {
      try
      {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var query = "SELECT * FROM vw_Followup1RPT"; // this view returns data
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
        var worksheet = workbook.Worksheets.Add("FollowUp1Report");
        worksheet.Cell(1, 1).InsertTable(dataTable, "FollowUp1Report", true);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(
          stream.ToArray(),
"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          "FollowUp1_Report.xlsx"
        );
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error generating report: " + ex.Message);
        return StatusCode(500, new { type = "E", message = "Failed to generate Follow-Up 1 report." });
      }
    }*/
  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GERD.SendNotificationEmailtoDoctors.Modal
{
  public class CommonResult<T>
  {
    public T? Data { get; set; }
    public string? Type { get; set; }
    public string? Message { get; set; }
    public int? Count { get; set; }
  }
}

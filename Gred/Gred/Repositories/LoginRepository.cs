using gred.Data;
using Gred.Data.Entities.Common;
using Gred.Services.Interface;
using Microsoft.EntityFrameworkCore;
using gred.Models;

namespace Gred.Repositories
{
  public class LoginRepository : ILogin
  {
    private readonly GredDbContext _context;

    public LoginRepository(GredDbContext context)
    {
      _context = context;
    }

    public async Task<CommonRsult> AuthenticateDoctor(string Email, string MobileNo, string password)
    {
      if (string.IsNullOrEmpty(MobileNo) || string.IsNullOrEmpty(password))
        return new CommonRsult { Type = "E", Message = "Username or password cannot be empty" };

      var user = await _context.VwDoctors
          .Where(u => (u.PhoneNo == MobileNo || u.Email == Email) && u.Password == password)
          .FirstOrDefaultAsync();

      if (user == null)
        return new CommonRsult { Type = "E", Message = "Invalid credentials" };

      // Manually map to gred.Models.VwDoctor
      var mappedUser = new gred.Models.VwDoctor
      {
        DoctorId = user.DoctorId,
        Name = user.Name,
        Email = user.Email,
        PhoneNo = user.PhoneNo, // 👈 Make sure field names match
        Mcicode = user.Mcicode,
        PlaceOfPractice = user.PlaceOfPractice,
        HospitalName = user.HospitalName,
        Password = user.Password,
        State = user.State,
        City = user.City,
        EnterCodeNo = user.EnterCodeNo,
        Status = user.Status,
        CreatedBy = user.CreatedBy,
        CreatedDt = user.CreatedDt,
        ModifiedBy = user.ModifiedBy,
        ModifiedDt = user.ModifiedDt
      };

      return new CommonRsult
      {
        Type = "S",
        Message = "Login successful",
        Data = new List<gred.Models.VwDoctor> { mappedUser }, // ✅ Correctly typed list
        Count = 1
      };
    }



  }
}

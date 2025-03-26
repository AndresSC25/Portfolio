using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtpController : Controller
{
    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(OTP otp)
    {
        try
        {
            var otpM = new OtpManager();
            otpM.Create(otp);
            return Ok(otp);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region PUT

    [HttpPut]
    [Route("Update")]
    public ActionResult Update(string email, int phone, int otp)
    {
        try
        {
            var decodedEmail = Uri.UnescapeDataString(email);
            //var emailSplit = email.Split(' ');
            //if (emailSplit.Length > 1) { 
            //    email = emailSplit[0]+"+"+ emailSplit[1];
            //}
            var otpM = new OtpManager();
            otpM.Update(decodedEmail, phone, otp);
            return Ok(otp);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}
using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class OtpManager
{
    public void Create(OTP otp)
    {
        var otpCrud = new OTPCrudFactory();
        if (UserExists(otp.UserId)) throw new Exception("El usuario no existe");
        var newOTP = GenerateOTP();
        otp.OtpData = newOTP;
        var uCrud = new UserCrudFactory();
        var tempPhone = uCrud.RetrieveById<User>(otp.UserId).Phone;
        var sms = new TwilioOTP();
        otpCrud.Create(otp);
        var number = "+506" + tempPhone;
        sms.SendMessage(number, newOTP);
    }

    public void Update(string email, int phone, int otp)
    {
        var otpCrud = new OTPCrudFactory();
        if (!EmailExist(email))
        {
            var uCrud = new UserCrudFactory();
            var temp = uCrud.RetrieveByEmail(email);
            otpCrud.UpdateNew(temp.Id, temp.Email, temp.Phone, otp);
        }
        else
        {
            throw new Exception("El usuario no existe");
        }
    }

    public bool UserExists(int id)
    {
        try
        {
            var urCrud = new UserRoleFactory();
            var user = urCrud.RetrieveById<User>(id);
            return user != null;
        }
        catch (Exception ex)
        {
            // Manejo de excepciones adecuado
            Console.WriteLine($"Error al verificar usuario: {ex.Message}");
            return false; // O puedes lanzar una excepción personalizada
        }
    }

    public bool EmailExist(string email)
    {
        var uCrud = new UserCrudFactory();
        var existingUser = uCrud.RetrieveByEmail(email);
        return existingUser == null;
    }

    public static int GenerateOTP()
    {
        var random = new Random();
        return random.Next(100000, 1000000);
    }
}
using System.Security.Cryptography;
using System.Text;
using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class PasswordManager
{
    public void Create(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        pCrud.Create(password);
    }

    public async Task AutoGeneratedPassword(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        var uCrud = new UserCrudFactory();
        var genPassword = GeneratePassword(8);
        password.PasswordContent = ComputeMD5Hash(genPassword);
        var id = password.UserId;
        var email = uCrud.RetrieveById<User>(id).Email;
        var emailSender = new SendGridEmail();
        emailSender.SendEmailAsync(email, genPassword);
        pCrud.Create(password);
    }

    public void ChangePassword(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        var uCrud = new UserCrudFactory();
        var beforeMd5Password = password.PasswordContent;
        password.PasswordContent = ComputeMD5Hash(password.PasswordContent);
        password.PasswordToChange = ComputeMD5Hash(password.PasswordToChange);
        var id = password.UserId;
        var email = uCrud.RetrieveById<User>(id).Email;
        if (Exists(password))
        {
            if (AlreadyExistPasswords(password))
                throw new Exception("La contraseña no puede ser igual a las últimas 5.");

            if (ValidarContraseña(beforeMd5Password))
            {
                var emailSender = new SendGridEmail();
                emailSender.SendEmailAsyncPasswordChanges(email);
                pCrud.Create(password);
            }
            else
            {
                throw new Exception(
                    "Revisa tu contraseña: debe tener 8 caracteres, 1 mayúscula, 1 número y 1 carácter especial.");
            }
        }
    }

    public void Update(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        pCrud.Update(password);
    }

    public void Delete(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        pCrud.Delete(password);
    }

    public List<Password> RetrieveAll()
    {
        var pCrud = new PasswordCrudFactory();
        return pCrud.RetrieveAll<Password>();
    }

    public List<Password> RetrievePasswordsById(int id)
    {
        var pCrud = new PasswordCrudFactory();
        return pCrud.RetrievePasswordsById(id);
    }

    internal bool isValidPassword(string password)
    {
        throw new NotImplementedException();
    }

    public static string GeneratePassword(int length)
    {
        if (length < 8) throw new ArgumentException("La longitud de la contraseña debe ser al menos 8 caracteres.");

        var letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var digits = "0123456789";
        var symbols = "!@#$%&*?";

        var random = new Random();
        var password = new StringBuilder(length);

        // Ensure at least one letter, digit, and symbol
        password.Append(letters[random.Next(letters.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(symbols[random.Next(symbols.Length)]);

        // Fill the rest of the password length with random characters from all sets
        var allCharacters = letters + digits + symbols;
        for (var i = 3; i < length; i++) password.Append(allCharacters[random.Next(allCharacters.Length)]);

        // Shuffle the result to ensure randomness
        var array = password.ToString().ToCharArray();
        for (var i = array.Length - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return new string(array);
    }

    public string ComputeMD5Hash(string password)
    {
        using (var md5 = MD5.Create())
        {
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++) sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString();
        }
    }

    public bool AlreadyExistPasswords(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        foreach (var item in pCrud.RetrievePasswordsById(password.UserId))
            if (password.PasswordContent == item.PasswordContent)
                return true;
        return false;
    }

    public bool Exists(Password password)
    {
        var pCrud = new PasswordCrudFactory();
        foreach (var item in pCrud.RetrievePasswordsById(password.UserId))
            if (password.PasswordToChange == item.PasswordContent)
                return true;
        return false;
    }

    private static bool ValidarContraseña(string password)
    {
        if (password.Length < 8) return false;

        var tieneMayuscula = false;
        var tieneNumero = false;
        var tieneCaracterEspecial = false;

        foreach (var caracter in password)
            if (char.IsUpper(caracter))
                tieneMayuscula = true;
            else if (char.IsDigit(caracter))
                tieneNumero = true;
            else if (!char.IsLetterOrDigit(caracter)) tieneCaracterEspecial = true;

        return tieneMayuscula && tieneNumero && tieneCaracterEspecial;
    }

    #region Validations

    #endregion
}
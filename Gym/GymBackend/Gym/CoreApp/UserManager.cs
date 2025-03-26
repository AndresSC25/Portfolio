using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserManager
{
    public void Create(User user)
    {
        var uCrud = new UserCrudFactory();

        if (!IsValidName(user)) throw new Exception("Nombre no válido.");

        if (!IsValidLastName(user)) throw new Exception("Apellido no válido.");

        if (!IsValidNumber(user)) throw new Exception("Número de teléfono debe tener 8 dígitos.");

        if (!IsValidEmail(user)) throw new Exception("Correo electrónico no válido.");

        if (!IsUniqueEmail(user)) throw new Exception("El correo electrónico ya está en uso.");

        if (!IsValidGender(user)) throw new Exception("Género no válido.");

        if (!IsValidDate(user)) throw new Exception("Debe seleccionar una fecha de nacimiento");

        if (!IsAtLeastOneRoleSelected(user)) throw new Exception("Debe seleccionar al menos un rol.");

        if (IsEntrenadorRoleSelected(user) && !IsValidTrainerAvailability(user))
            throw new Exception(
                "Debe seleccionar al menos un día de disponibilidad y llenar las horas de entrada y salida.");

        uCrud.Create(user);
        var pM = new PasswordManager();
        var sM = new ScheduleManager();
        // Hace Retrieve del user creado a través del Email
        var userJustCreated = uCrud.RetrieveByEmail(user.Email);

        // Se genera el nuevo password con la info recibida
        var beforeHashPassword = GeneratePassword(8);
        var emailSender = new SendGridEmail();
        emailSender.SendEmailAsync(user.Email, beforeHashPassword);
        var newUser = new Password
        {
            PasswordContent = ComputeMD5Hash(beforeHashPassword),
            UserId = userJustCreated.Id
        };
        pM.Create(newUser);

        // Checkea si DaysOfWeek contiene algo (Entrenador)
        if (!string.IsNullOrEmpty(user.DaysOfWeek))
        {
            var newSchedule = new Schedule
            {
                DaysOfWeek = user.DaysOfWeek,
                TimeOfEntry = user.TimeOfEntry ?? default(TimeOnly),
                TimeOfExit = user.TimeOfExit ?? default(TimeOnly),
                EmployeeId = userJustCreated.Id
            };
            sM.Create(newSchedule);
        }

        // Itera a través de los roles, y se genera una instancia de userRole para cada uno de ellos.
        foreach (var rol in user.ListaRole)
        {
            var urCrud = new UserRoleFactory();
            var newUserRole = new UserRole
            {
                RoleId = rol.Id,
                UserId = userJustCreated.Id
            };
            urCrud.Create(newUserRole);
        }
    }

    public void Update(User user)
    {
        var uCrud = new UserCrudFactory();
        //var urCrud = new UserRoleFactory();

        if (!IsValidName(user)) throw new Exception("Nombre no válido.");

        if (!IsValidLastName(user)) throw new Exception("Apellido no válido.");

        if (!IsValidNumber(user)) throw new Exception("Número de teléfono debe tener 8 dígitos.");

        if (!IsUniqueEmail(user)) throw new Exception("El correo electrónico ya está en uso.");

        if (!IsValidEmail(user)) throw new Exception("Correo electrónico no válido.");

        if (!IsValidGender(user)) throw new Exception("Género no válido.");

        if (!IsValidDate(user)) throw new Exception("Debe seleccionar una fecha de nacimiento");

        if (user.ListaRole != null)
        {
            if (!IsAtLeastOneRoleSelected(user)) throw new Exception("Debe seleccionar al menos un rol.");

            if (IsEntrenadorRoleSelected(user) && !IsValidTrainerAvailability(user))
                throw new Exception(
                    "Debe seleccionar al menos un día de disponibilidad y llenar las horas de entrada y salida.");
        }

        uCrud.Update(user);
        //urCrud.DeleteByUserId(user);

        // Itera a través de los roles, y se genera una instancia de userRole para cada uno de ellos.
        //foreach (var rol in user.ListaRole)
        //{
        //    var newUserRole = new UserRole
        //    {
        //        RoleId = rol.Id,
        //        UserId = user.Id
        //    };
        //    urCrud.Create(newUserRole);
        //}
    }

    public void Delete(User user)
    {
        var uCrud = new UserCrudFactory();
        var urCrud = new UserRoleFactory();
        var pCrud = new PasswordCrudFactory();

        urCrud.DeleteByUserId(user);
        pCrud.DeleteByUserId(user);
        uCrud.Delete(user);
    }

    public List<User> RetrieveAll()
    {
        var uCrud = new UserCrudFactory();
        var urCrud = new UserRoleFactory();

        var users = uCrud.RetrieveAll<User>();

        foreach (var user in users) user.ListaRole = urCrud.RetrieveByUserId(user.Id);
        return users;
    }

    public User RetrieveById(int id)
    {
        var rolManager = new RolManager();
        var uCrud = new UserCrudFactory();
        var userFound = uCrud.RetrieveById<User>(id);
        var listaRolesUsuario = rolManager.RetrieveAllRolesByUserId(userFound.Id);
        userFound.ListaRole = listaRolesUsuario;
        return userFound;
    }

    public User RetrieveUserByCredentials(string email, string password)
    {
        var uCrud = new UserCrudFactory();
        var hashedPassword = ComputeMD5Hash(password);
        var user = uCrud.RetrieveUserByCredentials(email, hashedPassword);
        if (user != null)
        {
            user.ListaRole = GetUserRoles(user.Id);
            return user;
        }

        throw new Exception("Correo o contraseña incorrectos");
    }

    public List<Rol> GetUserRoles(int userId)
    {
        var rCrud = new RoleCrudFactory();
        var roles = rCrud.RetrieveAllRolesByUserId(userId);
        return roles;
    }

    public string ComputeMD5Hash(string password)
    {
        using (var md5 = MD5.Create())
        {
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();
            foreach (var b in hashBytes) sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }

    public User RetrieveByEmail(string email)
    {
        var uCrud = new UserCrudFactory();
        var user = uCrud.RetrieveByEmail(email);
        if (user == null) throw new Exception("Email no encontrado");
        return user;
    }

    public List<User> RetrieveByUserRole(int userId)
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveByRole(userId);
    }

    public static string GeneratePassword(int length)
    {
        if (length < 8) throw new ArgumentException("La longitud de la contraseña debe ser al menos 8 caracteres.");

        var letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var digits = "0123456789";
        var symbols = "!@#$%&*?";
        var random = new Random();
        var password = new StringBuilder(length);

        password.Append(letters[random.Next(letters.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(symbols[random.Next(symbols.Length)]);

        var allCharacters = letters + digits + symbols;
        for (var i = 3; i < length; i++) password.Append(allCharacters[random.Next(allCharacters.Length)]);

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

    public List<User> RetrieveByUserRoleWithSchedule(int userId)
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveByRoleWithSchedule(userId);
    }

    #region Validations

    public bool IsValidName(User user)
    {
        return !string.IsNullOrWhiteSpace(user.Name) &&
               user.Name.Length >= 2 &&
               user.Name.Length <= 50 &&
               Regex.IsMatch(user.Name, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ]+$");
    }

    public bool IsValidLastName(User user)
    {
        return !string.IsNullOrWhiteSpace(user.LastName) &&
               user.LastName.Length >= 2 &&
               user.LastName.Length <= 50 &&
               Regex.IsMatch(user.LastName, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚüÜ]+$");
    }

    public bool IsValidNumber(User user)
    {
        return user.Phone.Length == 8 && Regex.IsMatch(user.Phone, @"^\d{8}$");
    }

    public bool IsValidEmail(User user)
    {
        return Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool IsValidPassword(string password)
    {
        return password.Length >= 8 &&
               Regex.IsMatch(password, @"[a-z]") &&
               Regex.IsMatch(password, @"[A-Z]") &&
               Regex.IsMatch(password, @"[0-9]") &&
               Regex.IsMatch(password, @"[\W_]");
    }

    public bool IsUniqueEmail(User user)
    {
        var uCrud = new UserCrudFactory();
        var existingUser = uCrud.RetrieveByEmail(user.Email);

        if (existingUser != null)
            if (existingUser.Id != user.Id)
                return false;

        return true;
    }

    public bool IsValidGender(User user)
    {
        var validGenders = new HashSet<string> { "M", "F", "O" };
        return validGenders.Contains(user.Gender);
    }

    public bool IsValidDate(User user)
    {
        var defaultDate = DateTime.Parse("1821-09-15T17:26:50.620Z");
        return user.BirthDate.Date != defaultDate.Date;
    }

    public bool IsAtLeastOneRoleSelected(User user)
    {
        return user.ListaRole != null && user.ListaRole.Count > 0;
    }

    public bool IsEntrenadorRoleSelected(User user)
    {
        return user.ListaRole.Any(r => r.Id == 2);
    }

    public bool IsValidTrainerAvailability(User user)
    {
        if (string.IsNullOrEmpty(user.DaysOfWeek)) return false;

        if (user.TimeOfEntry == null || user.TimeOfExit == null) return false;

        return true;
    }

    public bool EmailExist(User user)
    {
        try
        {
            var userDB = RetrieveByEmail(user.Email);

            // Si no se encuentra el usuario, retornamos false
            // Si se encuentra, retornamos true solo si el ID es diferente
            return userDB != null && userDB.Id != user.Id;
        }
        catch
        {
            // Si ocurre una excepción, asumimos que el email no existe
            return false;
        }
    }


    public bool IsValidUser(User user)
    {
        return IsValidName(user) &&
               IsValidLastName(user) &&
               IsValidNumber(user) &&
               IsValidEmail(user) &&
               IsValidGender(user) &&
               IsAtLeastOneRoleSelected(user);
    }

    #endregion
}
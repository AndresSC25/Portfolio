// Definimos la clase para manejar la vista de registro de usuario
function RegisterUserController() {
    this.ViewName = "RegisterUser";
    this.ApiBaseEndPoint = "User";

    // Método constructor de la vista
    this.InitView = function() {
        console.log("Register User view init!!!!");

        // Llamar a la función para restringir la fecha de nacimiento
        this.SetDateRestrictions();

        // Event listener para el botón de crear
        $("#btnCreate").click(() => {
            this.Create();
        });

        // Toggle visibility of "trainerAvailability" div based on "Entrenador" checkbox
        $("input[name='roles'][value='2']").change(function() {
            if ($(this).is(":checked")) {
                $("#trainerAvailability").show();
            } else {
                $("#trainerAvailability").hide();
            }
        });
    };

    // Método para restringir la fecha de nacimiento a no más tarde que la fecha actual
    this.SetDateRestrictions = function() {
        var today = new Date().toISOString().split("T")[0];
        $("#txtBirthDate").attr("max", today);
    };

    // Método para crear un nuevo usuario
    this.Create = function() {
        var date = "";
        var gender = "";

        if ($("#txtBirthDate").val() == "" || $("#txtBirthDate").val() == null) {
            date = "1821-09-15T17:26:50.620Z";
        } else {
            date = $("#txtBirthDate").val() + "T17:26:50.620Z";
        }

        if ($("#txtGender").val() == "" || $("#txtGender").val() == null) {
            gender = "X";
        } else {
            gender = $("#txtGender").val();
        }


        var user = {
            Name: $("#txtName").val(),
            LastName: $("#txtLastName").val(),
            Phone: $("#txtPhone").val(),
            Email: $("#txtEmail").val(),
            Gender: gender,
            BirthDate: date,
            Status: "Default",
            Password: "password",
            daysOfWeek: "",
            listaRole: []
        };

        $("input[name='roles']:checked").each(function() {
            var rol = {
                Id: parseInt($(this).val()),
                Name: ""
            };
            user.listaRole.push(rol);
        });

        // Agregar disponibilidad del entrenador si el rol "Entrenador" está seleccionado
        if ($("input[name='roles'][value='2']").is(":checked")) {
            var daysOfWeek = "";

            if ($("#txtHoraEntrada").val() == "") {
                user.TimeOfEntry = "00:00:00";
            }
            if ($("#txtHoraSalida").val() == "") {
                user.TimeOfExit = "00:00:00";
            } else {
                $("input[name='days']:checked").each(function() {
                    daysOfWeek += $(this).val();
                });
                user.TimeOfEntry = $("#txtHoraEntrada").val() + ":00";
                user.TimeOfExit = $("#txtHoraSalida").val() + ":00";
                user.DaysOfWeek = daysOfWeek;
            }


        }

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";
        ca.PostToAPI(endPointRoute,
            user,
            () => {
                console.log("User created successfully");
                this.ClearForm(); // Clear the form after successful creation
            });
    };

    // Método para limpiar el formulario
    this.ClearForm = function() {
        $("#txtName").val("");
        $("#txtLastName").val("");
        $("#txtPhone").val("");
        $("#txtEmail").val("");
        $("#txtGender").val("");
        $("#txtBirthDate").val("");
        $("input[name='roles']").prop("checked", false);
        $("#trainerAvailability").hide();
        $("#txtHoraEntrada").val("");
        $("#txtHoraSalida").val("");
        $("input[name='days']").prop("checked", false);
    };
}

// Instanciar y inicializar la vista
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var rc = new RegisterUserController();
        rc.InitView();
    }
});
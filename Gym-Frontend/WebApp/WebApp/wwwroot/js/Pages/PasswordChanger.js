function UsersViewController() {
    this.ViewNameRol = "Passwords";
    this.ApiBaseEndPointRol = "Password";
    var idUser = sessionStorage.getItem("UserId");

    this.InitView = function() {
        console.log("User view init!!!");
        console.log(idUser);

        //Bind del click
        $("#change-ps").click(function() {
            event.preventDefault();

            var newPassword = $("#newPassword").val();
            var confirmPassword = $("#confirmPassword").val();

            // Expresión regular para validar la contraseña
            var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

            if (newPassword !== confirmPassword) {
                $("#passwordMatchError").show();
                $("#passwordMatchError").text("Las contraseñas no coinciden.");
                $("#newPassword").attr("placeholder", "Las contraseñas no coinciden");
                $("#confirmPassword").attr("placeholder", "Las contraseñas no coinciden");
                $("#newPassword, #confirmPassword").addClass("error");
            } else if (!passwordRegex.test(newPassword)) {
                $("#passwordMatchError").show();
                $("#passwordMatchError")
                    .text(
                        "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.");
                $("#newPassword").attr("placeholder", "Contraseña débil");
                $("#newPassword, #confirmPassword").addClass("error");
            } else {
                $("#passwordMatchError").hide();
                $("#newPassword").attr("placeholder", "Nueva contraseña");
                $("#confirmPassword").attr("placeholder", "Confirmar contraseña");
                $("#newPassword, #confirmPassword").removeClass("error");

                console.log("La contraseña es válida. Proceder con el cambio.");
                var vc = new UsersViewController();
                vc.Create();
            }
        });


    };

    this.Create = function() {

        /*{
         "id": 0,
         "userId": 0,
         "passwordContent": "string",
         "created": "2024-08-04T22:46:08.073Z"
        }*/
        var password = {};
        password.id = 0;
        password.userId = idUser;
        password.passwordContent = $("#confirmPassword").val();
        password.created = "2024-08-04T22:46:08.073Z";
        password.passwordToChange = $("#currentPassword").val();

        var ca = new ControlActions();
        var endPointRoute = "Password" + "/ChangePassword";

        ca.PostToAPI(endPointRoute,
            password,
            (data, errorType) => {
                if (errorType) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        html: `<b>Error: ${errorType}</b><br/>Ocurrió un error al cambiar la contraseña.`,
                        footer: "SILUETA CLUB FITNESS"
                    });
                } else {
                    Swal.fire({
                            icon: "success",
                            title: "¡Éxito!",
                            text: "Se ha cambiado la contraseña exitosamente.",
                            footer: "SILUETA CLUB FITNESS"
                        })
                        .then(() => { window.location.href = "Menu"; });
                }
            },
            (errorType) => {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: `<b>Error: ${errorType}</b><br/>Ocurrió un error al buscar el usuario.`,
                    footer: "SILUETA CLUB FITNESS"
                });
            }
        );
    };


}

$(document).ready(function() {
    var vc = new UsersViewController();
    vc.InitView();
});
function UserViewController() {
    this.ViewName = "Users";
    this.ApiBaseEndPoint = "User";

    //Metodo constructor de la vista
    this.InitView = function() {
        $("#btnLogIn").click(function() {
            var vc = new UserViewController();
            vc.LogIn();
        });
    };

    this.LogIn = function() {
        var LoginRequest = {};
        LoginRequest.email = $("#InputEmail").val();
        LoginRequest.password = $("#InputPassword").val();

        //Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/LogIn";

        ca.PostToAPI(endPointRoute,
            LoginRequest,
            function(response) {
                console.log("User logged");
                sessionStorage.setItem("UserRoles", JSON.stringify(response.listaRole));
                sessionStorage.setItem("UserId", response.id.toString());
                sessionStorage.setItem("Status", response.status.toString());

                if (response.status.toString().toUpperCase() != "DISABLED") {
                    sessionStorage.setItem("UserRoles", JSON.stringify(response.listaRole));
                    sessionStorage.setItem("UserId", response.id.toString());
                    sessionStorage.setItem("Status", response.status.toString());

                    // AQUI FALTA TRAERSE EL USER STATUS TAMBIEN PARA VALIDACIONES LUEGO
                    //Cambiar estatus cuando se ocupe a default
                    if (response.status.toString().toUpperCase() === "DEFAULT") {
                        var otp = {};
                        otp.id = 0;
                        otp.data = 0;
                        otp.userId = response.id;
                        otp.expiredDate = "2024-08-01T20:01:15.091Z";
                        otp.wasUsed = "string";
                        otp.created = "2024-08-01T20:01:15.091Z";
                        endPointRouteOtp = "Otp/Create";

                        ca.PostToAPIForOTP(endPointRouteOtp,
                            otp,
                            function() {
                                console.log("Otp asignado");
                                setTimeout(redireccionar, 2000);
                            }
                        );
                    } else {
                        endPointRoute = "UserMembership/RetrieveByUserIdStatusChange?userId=" +
                            sessionStorage.getItem("UserId");
                        ca.GetToApiModificado(endPointRoute,
                            function(response, errorType) {
                                if (errorType) {
                                    // Mostrar el error utilizando SweetAlert y redirigir después de aceptar
                                    Swal.fire({
                                        icon: "error",
                                        title: "Error",
                                        text: "Su membresía ha acabado, recuerde pagar!",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            // Continuar con el flujo normal después de aceptar
                                            sessionStorage.setItem("IsAuthenticated", "true");
                                            window.location.href = "Menu";
                                        }
                                    });
                                } else if (response.err) {
                                    // Manejar el error que viene del response
                                    Swal.fire({
                                        icon: "error",
                                        title: "Error",
                                        text: "Su membresía ha acabado, recuerde pagar!",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            sessionStorage.setItem("IsAuthenticated", "true");
                                            window.location.href = "Menu";
                                        }
                                    });
                                } else {
                                    // Continuar con el flujo normal si no hay error
                                    sessionStorage.setItem("IsAuthenticated", "true");
                                    window.location.href = "Menu";
                                }
                            });
                    }
                } else if (response.status.toString().toUpperCase() === "DISABLED") {
                    Swal.fire({
                        icon: "error",
                        title: "Usuario Deshabilitado",
                        text: "Contacte un administrador",
                        confirmButtonText: "Aceptar"
                    }).then((result) => {
                        window.location.href = "Index";
                    });
                }
            }
        );
    };

    function redireccionar() {
        window.location.href = "OTPVerification";
    }
}

// Instanciar y inicializar la vista
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");

    if (isAuthenticated === "true") {
        window.location.href = "/Menu";
    } else {
        var vc = new UserViewController();
        vc.InitView();
    }
});
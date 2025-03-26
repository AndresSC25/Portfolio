function UserViewController() {

    this.InitView = function() {
        $("#btnCreate").click(function() {
            var emailtxt = $("#email").val();
            var phonetxt = $("#telefono").val();
            var otptxt = $("#otp").val();

            // Validación utilizando expresiones regulares para email y teléfono
            var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            var phoneRegex = /^\d{8}$/;

            if (!emailRegex.test(emailtxt)) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Por favor, ingrese un correo electrónico válido."
                });
                return false;
            } else if (!phoneRegex.test(phonetxt)) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Por favor, ingrese un número de teléfono válido."
                });
                return false;
            } else if (otptxt === "") {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Por favor, ingrese el código OTP."
                });
                return false;
            } else {
                // Reemplazar con la lógica de creación del usuario (llamada a API, etc.)
                var vc = new UserViewController();
                vc.Create();

            }
        });
    };
    this.Create = function() {
        var emailtxt = $("#email").val();
        var phonetxt = $("#telefono").val();
        var otptxt = $("#otp").val();
        ca = new ControlActions();
        /*        var endPointRoute = "Otp/Update?email=" + emailtxt + "&phone=" + phonetxt + "&otp=" + otptxt;*/


        const encodedEmail = encodeURIComponent(emailtxt);


        var endPointRoute = `Otp/Update?email=${encodedEmail}&phone=${phonetxt}&otp=${otptxt}`;
        ca.PutToAPIWithoutData(endPointRoute,
            function(response) {
                if (response) {
                    Swal.fire({
                        title: "OTP confirmado",
                        text: "OTP confirmado exitosamente.",
                        icon: "success",
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                    }).then((result) => {
                        if (result.isConfirmed) {
                            redireccionar();
                        }
                    });
                }
            });

    };

    function redireccionar() {
        window.location.href = "Menu";
    }
}

$(document).ready(function() {
    var vc = new UserViewController();
    vc.InitView();
});
//Variable para elegir a que usuario iniciarle el proceso de pago
var clientId = sessionStorage.getItem("UserId");


function PaymentUsersController() {
    this.ViewName = "PaymentUsers";

    //Metodo constructor de la vista
    this.InitView = function() {
        $("#btnPay").click(function() {
            var vc = new PaymentUsersController();
            vc.Select();
        });
    };

    //Metodo para confirmar si se requiere hacer el pago
    this.Select = function() {
        // Crear el DTO de UserMembership
        var UserMembership = {
            // Obtener y validar los valores de entrada
            id: 0,
            UserId: clientId,
            MemberShipId: 0,
            created: "2024-06-29T17:26:50.620Z"
        };

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "UserMembership/RetrieveNewestByUserId?userId=" + clientId;
        ca.GetToApi(endPointRoute,
            (response) => {
                if (response != null) {
                    console.log(response);
                    sessionStorage.setItem("clientId", clientId);
                    window.location.href = "/MakePaymentClient";
                }
            });
    };
}

//Instaciamiento de la clase
$(document).ready(function() {
    //Voy a comentar esto, luego Suncin lo ajusta :v
    //var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    //var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    //const requiredRoles = ["Admin", "Recepcionista", "Entrenador", "Cliente"];
    //const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    //if (isAuthenticated !== "true" || !hasRequiredRoles) {
    //    window.location.href = "/Index";
    //} else {

    //}

    var vc = new PaymentUsersController();
    vc.InitView();
});
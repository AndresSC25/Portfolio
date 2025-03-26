// JS para /MakePayment

//Variable para elegir a que usuario iniciarle el proceso de pago
var clientId = 0;


function PaymentUsersController() {
    this.ViewName = "PaymentUsers";

    //Metodo constructor de la vista
    this.InitView = function() {
        $("#btnSelect").click(function() {
            var vc = new PaymentUsersController();
            vc.Select();
        });

        this.LoadTableClients();

        // Esto es para obtener los datos de un row selecciona
        $("#tblClients tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var clientDTO = $("#tblClients").DataTable().row(row).data();


                //Mapeo de valores del DTO al formulario
                clientId = clientDTO.id;

            });


        // Esto es para resaltar al row seleccionado en un tabla
        $("table tbody").on("click",
            "tr",
            function() {

                // Obtener la tabla que contiene la fila seleccionada
                var table = $(this).closest("table");

                // Remover el color de fondo de todas las filas en la tabla actual
                table.find("tbody tr").css("background-color", "");

                // Cambiar el color de fondo de la fila seleccionada a gris oscuro
                $(this).css("background-color", "#B3CCF1");
            });

    };
    this.LoadTableClients = function() {
        var ca = new ControlActions();

        //Construir la ruta del API para consumir el servicio de Retrieve con id 4 para traer solo los clientes
        var urlService = ca.GetUrlApiService("User/RetrieveByRole?id=4");

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "name" };
        columns[2] = { 'data': "lastName" };
        columns[3] = { 'data': "gender" };
        columns[4] = { 'data': "email" };
        columns[5] = { 'data': "phone" };


        //Inicializar la tabla como un data table

        $("#tblClients").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: { //Esto es para adapta la tabla al español
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_  registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
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
                    window.location.href = "/MakeInvoice";
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
function MeasureFormViewController() {
    this.ViewName = "Measure";
    this.ApiBaseEndPoint = "Measure";

    //Metodo constructor de la vista
    this.InitView = function() {
        $("#btnCreate").click(function() {
            var vc = new MeasureFormViewController();
            vc.Create();
        });

        this.LoadTableClients();
        $("#tblClients tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var clientDTO = $("#tblClients").DataTable().row(row).data();


                //Mapeo de valores del DTO al formulario
                $("#txtId").val(clientDTO.id);
                $("#txtName").val(clientDTO.name + " " + clientDTO.lastName);

            });
        $("table tbody").on("click",
            "tr",
            function() {
                console.log("intento cambiar el color");
                // Obtener la tabla que contiene la fila seleccionada
                var table = $(this).closest("table");

                // Remover el color de fondo de todas las filas en la tabla actual
                table.find("tbody tr").css("background-color", "");

                // Cambiar el color de fondo de la fila seleccionada a gris oscuro
                $(this).css("background-color", "#B3CCF1"); // Puedes ajustar el color según tus necesidades
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

    //Metodo de creacion de medidas
    this.Create = function() {
        // Obtener el ID del usuario del localStorage
        var userId = localStorage.getItem("id");

        // Crear el DTO de Measure
        var measure = {
            // Obtener y validar los valores de entrada
            clientId: $("#txtId").val() || 0,
            weight: $("#txtWeight").val() || 0,
            height: $("#txtHeight").val() || 0,
            averageOfFat: $("#txtFatPorcentage").val() || 0,
            id: 0,
            created: "2024-06-29T17:26:50.620Z"
        };

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "Measures/Create";
        ca.PostToAPI(endPointRoute,
            measure,
            function() {
                console.log("Measure created");
                $("#tblClients").DataTable().ajax.reload(); // Recargar la tabla
            });
    };
}

//Instaciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista", "Entrenador", "Cliente"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new MeasureFormViewController();
        vc.InitView();
    }
});
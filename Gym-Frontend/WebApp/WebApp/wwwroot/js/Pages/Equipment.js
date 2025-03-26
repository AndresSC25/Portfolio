//JS que maneja todo el comportamiento de la vista EquipmentManager.cshtml

//Definimos la clase

function EquipmentViewController() {
    this.ViewName = "Equipment";
    this.ApiBaseEndPoint = "Equipment";

    //Metodo constructor de la vista
    this.InitView = function() {
        //Bind del click del boton create con el metodo
        $("#btnCreate").click(function() {
            var vc = new EquipmentViewController();
            vc.Create();
        });

        $("#btnUpdate").click(function() {
            var vc = new EquipmentViewController();
            vc.Update();
        });

        this.LoadTable();

        $("#tblEquipment tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");

                //Extraemos la data de la tabla
                var equipmentDTO = $("#tblEquipment").DataTable().row(row).data();

                //Mapeo de valores del DTO al formulario
                $("#txtId").val(equipmentDTO.id);
                $("#txtName").val(equipmentDTO.name);
                $("#txtDescription").val(equipmentDTO.description);
                $("#txtLocation").val(equipmentDTO.location);

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

    //Metodo de creacion de equipo
    this.Create = function() {

        //Validacion de que los forms si generen un objeto
        $("#txtId").val("");
        var location = $("#txtLocation").val();
        var description = $("#txtDescription").val();
        var name = $("#txtName").val();

        if (location == null || location == "") {
            location = "empty";
        }
        if (description == null || description == "") {
            description = "empty";
        }
        if (name == null || name == "") {
            name = "empty";
        }


        //Crear el DTO de equipment

        var equipment = {};
        equipment.name = name;
        equipment.location = location;
        equipment.description = description;

        //Valores default //Se debe ajustar para no enviar defaults en API
        equipment.id = 0;
        equipment.created = "2024-06-29T17:26:50.620Z";

        //Invocar API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";
        ca.PostToAPI(endPointRoute,
            equipment,
            function() {
                console.log("Equipment created");
                $("#tblEquipment").DataTable().ajax.reload(); // Recargar la tabla
            });

    };
    this.Update = function() {

        //Validacion de que los forms si generen un objeto
        var id = $("#txtId").val();
        var location = $("#txtLocation").val();
        var description = $("#txtDescription").val();
        var name = $("#txtName").val();

        if (id == null || id == "") {
            id = 0;
            location = "empty";
            description = "empty";
            name = "empty";
        }


        //Crear el DTO de equipment
        var equipment = {};
        equipment.id = id;
        equipment.name = name;
        equipment.location = location;
        equipment.description = description;

        //Valores default //Se debe ajustar para no enviar defaults en API
        equipment.created = "2024-06-29T17:26:50.620Z";


        //Invocar API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Update";
        ca.PutToAPI(endPointRoute,
            equipment,
            function() {
                console.log("equipment updated");
                $("#tblEquipment").DataTable().ajax.reload(); // Recargar la tabla
            });
        // Reseteo de form
        $("#txtId").val("");
        $("#txtLocation").val("");
        $("#txtDescription").val("");
        $("#txtName").val("");

    };

    this.LoadTable = function() {
        var ca = new ControlActions();

        //Construir la ruta del API para consumir el servicio de Retrieve
        var urlService = ca.GetUrlApiService(this.ApiBaseEndPoint + "/RetrieveAll");

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "name" };
        columns[2] = { 'data': "location" };
        columns[3] = { 'data': "description" };
        columns[4] = { 'data': "created" };

        //Inicializar la tabla como un data table

        $("#tblEquipment").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: { // Adaptar la tabla al español
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            },
            select: true
        });
    };
}

//Instaciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const hasRequiredRoles = userRoles.some(role => role.name === "Admin");

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new EquipmentViewController();
        vc.InitView();
    }
});
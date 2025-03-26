//JS que maneja todo el comportamiento de la vista RegisterGroupClass.cshtml

//Definimos la clase
let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
};

function GroupClassRegisterViewController() {
    this.ViewName = "GroupClass";
    this.ApiBaseEndPoint = "GroupClass";
    var ca = new ControlActions();

    //Metodo constructor de la vista
    this.InitView = function() {


        //Bind del click del boton create con el metodo
        $("#btnRegister").click(function() {
            var vc = new GroupClassRegisterViewController();
            vc.Register();
        });

        $("#btnCancel").click(function() {
            var vc = new GroupClassRegisterViewController();
            vc.Cancel();
        });

        this.LoadTable();

        $("table tbody").on("click",
            "tr",
            function() {
                // Obtener la tabla que contiene la fila seleccionada
                var table = $(this).closest("table");

                // Remover la clase 'table-active' de todas las filas en la tabla actual
                table.find("tbody tr").removeClass("table-active");

                // Agregar la clase 'table-active' solo a la fila seleccionada en la tabla actual
                $(this).addClass("table-active");
            });
        $("#tblGroupClasses tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var groupClassDTO = $("#tblGroupClasses").DataTable().row(row).data();


                //Mapeo de valores del DTO al formulario
                $("#txtId").val(groupClassDTO.id);
                $("#txtName").val(groupClassDTO.className);

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

    //Metodo de creacion de clase grupal
    this.Register = function() {

        //Crear el DTO de GroupClass
        var groupClassId = $("#txtId").val();
        var userId = sessionStorage.getItem("UserId");


        if (groupClassId == "") {
            groupClassId = 0;
        }
        if (userId == null || userId == "") {
            console.log("Error userId no cargado");
        }

        var userGroupClass = {
            id: 0,
            groupClassId: groupClassId,
            clientId: userId,
            created: "2024-07-30T19:16:15.997Z"

        };


        //Invocar API
        var ca = new ControlActions();
        var endPointRoute = "UserGroupClass/Create";
        ca.PostToAPI(endPointRoute,
            userGroupClass,
            function() {
                console.log("Clase registrada");
                $("#tblGroupClasses").DataTable().ajax.reload(); // Recargar la tabla
            });

    };

    this.Cancel = function() {

        //Crear el DTO de UserGroupClass
        var groupClassId = $("#txtId").val();
        var userId = sessionStorage.getItem("UserId");


        if (groupClassId == "") {
            groupClassId = 0;
        }
        if (userId == null || userId == "") {
            console.log("Error userId no cargado");
        }

        var userGroupClass = {
            id: 0,
            groupClassId: groupClassId,
            clientId: userId,
            created: "2024-07-30T19:16:15.997Z"

        };


        //Invocar API
        var ca = new ControlActions();
        var endPointRoute = "UserGroupClass/Delete";
        ca.DeleteToAPI(endPointRoute,
            userGroupClass,
            function() {
                console.log("Clase Cancelada");
                $("#tblGroupClasses").DataTable().ajax.reload(); // Recargar la tabla
                Swal.fire({
                    icon: "success",
                    title: "¡Éxito!",
                    text: "Se ha cancelado el registro a la clase.",
                    footer: "SILUETA CLUB FITNESS"
                });
            });

    };

    this.LoadTable = function() {
        var ca = new ControlActions();

        //Construir la ruta del API para consumir el servicio de Retrieve
        var urlService = ca.GetUrlApiService("GroupClass/RetrieveAvailableWithName");

        var columns = [
            { 'data': "id" },
            { 'data': "className" },
            {
                'data': "classDate",
                'render': (data) => formatDate(data)
            },
            { 'data': "startTime" },
            { 'data': "endTime" },
            { 'data': "maxCapacity" },
            { 'data': "currentRegistered" },
            { 'data': "employeeId" },
            { 'data': "employeeName" }
        ];


        $("#tblGroupClasses").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            columnDefs: [
                {
                    "targets": [0, 7], // Índice de la columna que quieres ocultar (comienza desde 0)
                    "visible": false,
                    "searchable": false
                }
            ],
            language: { //Esto es para adapta la tabla al español
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_  registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:",
                "emptyTable": "No hay clases grupales disponibles"
            }
        });
    };

}

//Instaciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista", "Cliente"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new GroupClassRegisterViewController();
        vc.InitView();
    }
});
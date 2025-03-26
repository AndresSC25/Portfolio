//JS que maneja todo el comportamiento de la vista Pages\CreateMeasureAppointment.cshtml


var daysMap = {
    'L': "Lunes",
    'K': "Martes",
    'M': "Miércoles",
    'J': "Jueves",
    'V': "Viernes",
    'S': "Sábado",
    'D': "Domingo"
};

function formatDays(days) {
    return days.split("").map(day => daysMap[day]).join(", ");
}

//Definimos la clase

//Simulacion de recibir dato desde localStorage traido desde el inicio de sesion
//En este caso inicio sesion un recepecionista o un entrenador
localStorage.clear();
localStorage.setItem("id", 10);

function MeetingViewController() {
    this.ViewName = "Meeting";
    this.ApiBaseEndPoint = "Meeting";

    //Metodo constructor de la vista
    this.InitView = function() {
        console.log("Meeting view init!!!!");

        this.LoadTableTrainers();
        this.LoadTableClients();


        $("#btnCreate").click(function() {
            var pt = new MeetingViewController();
            pt.Create();
        });

        $("#btnCancel").click(function() {
            var vc = new MeetingViewController();
            vc.Delete();
        });

        $("#tblTrainers tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var trainerDTO = $("#tblTrainers").DataTable().row(row).data();

                //Mapeo de valores del DTO al formulario
                $("#txtIdTrainer").val(trainerDTO.id);
                $("#txtNameTrainer").val(trainerDTO.name + " " + trainerDTO.lastName);

            });

        $("#tblClients tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var clientDTO = $("#tblClients").DataTable().row(row).data();

                //Mapeo de valores del DTO al formulario
                $("#txtIdClient").val(clientDTO.id);
                $("#txtNameClient").val(clientDTO.name + " " + clientDTO.lastName);
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

    //Metodo de creacion de entrenamiento personal
    this.Create = function() {
        // Obtener el ID del usuario del localStorage
        var userId = localStorage.getItem("id");

        // Obtener y procesar la hora de entrada
        var timeIn = $("#txtTimeIn").val();
        var timeOut;
        if (timeIn) {
            var [hours, minutes] = timeIn.split(":").map(Number);
            var date = new Date();
            date.setHours(hours, minutes, 0, 0);

            // Sumar una hora
            date.setHours(date.getHours() + 1);

            // Formatear la hora de salida
            timeOut = date.toTimeString().slice(0, 5);
        } else {
            timeIn = "00:00";
            timeOut = "00:00";
        }

        // Crear el DTO de entrenamiento personal
        var Meeting = {
            employeeId: $("#txtIdTrainer").val() || 0,
            clientId: $("#txtIdClient").val() || 0,
            isCancelled: "no",
            timeOfEntry: timeIn + ":00",
            timeOfExit: timeOut + ":00",
            programmedDate: $("#txtDate").val() || "2000-01-01",
            id: 0,
            created: "2024-06-29T17:26:50.620Z",
            clientName: "",
            employeeName: ""
        };

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "Meetings/Create";
        ca.PostToAPI(endPointRoute,
            Meeting,
            function() {
                console.log("Meeting created");
                $("#tblPersonalT").DataTable().ajax.reload(); // Recargar la tabla
            });
    };
    this.Delete = function() {
        // Obtener el ID del usuario del localStorage
        var userId = localStorage.getItem("id");

        // Obtener el ID del Meeting desde el input
        var meetingId = $("#txtIdPT").val();

        // Crear el objeto Meeting con valores por defecto
        var Meeting = {
            Id: meetingId,
            EmployeeId: 0,
            ClientId: 0,
            IsCancelled: "no",
            IsPaid: "no",
            TimeOfEntry: "00:00:00",
            TimeOfExit: "00:00:00",
            ProgrammedDate: "2024-06-29",
            HourlyRate: 0,
            created: "2024-06-29T17:26:50.620Z",
            EmployeeName: "",
            ClientName: ""
        };

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "Meeting/Delete?id=" + Meeting.Id;
        ca.DeleteToAPI(endPointRoute,
            Meeting,
            function() {
                console.log("Meeting deleted");
                $("#tblPersonalT").DataTable().ajax.reload(); // Recargar la tabla
            });

        // Limpiar el campo de entrada
        $("#txtIdPT").val("");
    };
    this.LoadTableTrainers = function() {
        var ca = new ControlActions();

        // Construir la ruta del API para consumir el servicio de Retrieve con id 2 para traer solo los entrenadores con los horarios
        var urlService = ca.GetUrlApiService("User/RetrieveByRoleWithSchedule?id=2");

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "name" },
            { 'data': "lastName" },
            { 'data': "email" },
            {
                'data': "daysOfWeek",
                'render': function(data) {
                    return formatDays(data);
                }
            },
            { 'data': "timeOfEntry" },
            { 'data': "timeOfExit" }
        ];

        // Inicializar la tabla como un data table
        $("#tblTrainers").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            "columnDefs": [
                {
                    "targets": [0, 3], // Índice de las columnas que quieres ocultar (comienza desde 0)
                    "visible": false,
                    "searchable": false
                }
            ],
            language: { // Adaptar la tabla al español
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
        });
    };

    this.LoadTableClients = function() {
        var ca = new ControlActions();

        // Construir la ruta del API para consumir el servicio de Retrieve con id 4 para traer solo los clientes
        var urlService = ca.GetUrlApiService("User/RetrieveByRole?id=4");

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "name" },
            { 'data': "lastName" },
            { 'data': "email" },
            { 'data': "phone" }
        ];

        // Inicializar la tabla como un data table
        $("#tblClients").DataTable({
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
    const requiredRoles = ["Admin", "Recepcionista"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new MeetingViewController();
        vc.InitView();
    }
});
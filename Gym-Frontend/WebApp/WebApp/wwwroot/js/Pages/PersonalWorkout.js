//JS que maneja todo el comportamiento de la vista Pages\PersonalWorkout.cshtml

let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${day}-${month}-${year}`;
};

//Definimos la clase

//Mapeo para mostrar los dias
var daysMap = {
    'L': "Lunes",
    'K': "Martes",
    'M': "Miércoles",
    'J': "Jueves",
    'V': "Viernes",
    'S': "Sábado",
    'D': "Domingo"
};

// Función para convertir la cadena de días a su representación completa
function formatDays(days) {
    return days.split("").map(day => daysMap[day]).join(", ");
}

function PersonalTrainingViewController() {
    this.ViewName = "PersonalTraining";
    this.ApiBaseEndPoint = "PersonalTraining";

    //Metodo constructor de la vista
    this.InitView = function() {

        this.LoadTableTrainers();
        this.LoadTablePT();

        $("#btnCreate").click(function() {
            var pt = new PersonalTrainingViewController();
            pt.Create();
        });

        $("#btnCancel").click(function() {
            var vc = new PersonalTrainingViewController();
            vc.Cancel();
        });

        $("#tblTrainers tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var trainerDTO = $("#tblTrainers").DataTable().row(row).data();
                console.log(trainerDTO);

                //Mapeo de valores del DTO al formulario
                $("#txtId").val(trainerDTO.id);
                $("#txtName").val(trainerDTO.name + " " + trainerDTO.lastName);

                console.log("En teoria ya");

            });

        $("#tblPersonalT tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var personalTrainingDTO = $("#tblPersonalT").DataTable().row(row).data();


                //Mapeo de valores del DTO al formulario
                $("#txtIdPT").val(personalTrainingDTO.id);
                $("#txtDatePT").val(personalTrainingDTO.programmedDate.split("T")[0]);
                $("#txtTimeInPT").val(personalTrainingDTO.timeOfEntry);
                $("#txtTimeOutPT").val(personalTrainingDTO.timeOfExit);

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
        // Obtener el ID del usuario desde el localStorage
        var userId = sessionStorage.getItem("UserId");


        // Obtener valores de los inputs y asignar valores predeterminados si están vacíos
        var employeeId = $("#txtId").val() || 0;
        var timeOfEntry = $("#txtTimeIn").val();
        var timeOfExit = $("#txtTimeOut").val();
        var programmedDate = $("#txtDate").val() + "T17:26:50.620Z";
        var hourlyRate = $("#txtCost").val();

        if (timeOfEntry != "00:00:00") {
            timeOfEntry += ":00";
        }
        if (timeOfExit != "00:00:00") {
            timeOfExit += ":00";
        }
        // Crear el DTO de entrenamiento personal
        var personalTraining = {
            EmployeeId: employeeId,
            ClientId: userId,
            IsCancelled: "no",
            IsPaid: "no",
            TimeOfEntry: timeOfEntry,
            TimeOfExit: timeOfExit,
            ProgrammedDate: programmedDate,
            HourlyRate: hourlyRate,
            Id: 0,
            created: "2023-12-01T17:26:50.620Z",
            EmployeeName: "",
            ClientName: ""
        };

        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "PersonalTraining/Create";
        ca.PostToAPI(endPointRoute,
            personalTraining,
            function() {
                console.log("Personal training created");
                $("#tblPersonalT").DataTable().ajax.reload(); // Recargar la tabla
            });
    };
    this.Cancel = function() {

        var timeIn = $("#txtTimeInPT").val();
        var timeOut = $("#txtTimeOutPT").val();
        if (timeIn == "00:00") {
            timeIn = "00:00:00";
        }
        if (timeOut == "00:00") {
            timeOut = "00:00:00";
        }

        // Crear el DTO de entrenamiento personal con los valores del formulario
        var personalTraining = {
            Id: $("#txtIdPT").val() || 0,
            EmployeeId: 0,
            ClientId: 0,
            IsCancelled: "",
            IsPaid: "no",
            TimeOfEntry: timeIn,
            TimeOfExit: timeOut,
            ProgrammedDate: $("#txtDatePT").val() + "T17:26:50.620Z",
            HourlyRate: 0,
            created: "2024-06-29T17:26:50.620Z",
            EmployeeName: "",
            ClientName: ""
        };

        // Invocar API para cancelar el entrenamiento
        var ca = new ControlActions();
        var endPointRoute = "PersonalTraining/Cancel";
        ca.PutToAPI(endPointRoute,
            personalTraining,
            function() {
                console.log("Personal training canceled");
                $("#tblPersonalT").DataTable().ajax.reload(); // Recargar la tabla
            });
    };
    this.LoadTableTrainers = function() {
        var ca = new ControlActions();

        //Construir la ruta del API para consumir el servicio de Retrieve con id 2 para traer solo los entrenadores con los horarios
        var urlService = ca.GetUrlApiService("User" + "/RetrieveByRoleWithSchedule" + "?id=2");

        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "name" };
        columns[2] = { 'data': "lastName" };
        columns[3] = { 'data': "email" };
        columns[4] = {
            'data': "daysOfWeek",
            'render': function(data) {
                return formatDays(data);
            }
        };
        columns[5] = { 'data': "timeOfEntry" };
        columns[6] = { 'data': "timeOfExit" };


        //Inicializar la tabla como un data table

        $("#tblTrainers").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            columnDefs: [
                {
                    "targets": [0, 3], // Índice de la columna que quieres ocultar (comienza desde 0)
                    "visible": false,
                    "searchable": false
                }
            ],
            language: {
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_  registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
        });

    };
    this.LoadTablePT = function() {
        var ca = new ControlActions();

        // Obtener el ID del usuario del localStorage
        var userId = sessionStorage.getItem("UserId");
        if (!userId) {
            console.error("User ID no encontrado en localStorage.");
            return; // Termina la función si no se encuentra el ID
        }

        // Construir la ruta del API para consumir el servicio de Retrieve con el ID del usuario
        var urlService = ca.GetUrlApiService("PersonalTraining/RetrieveByClientId?id=" + userId);

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "employeeName" },
            {
                'data': "programmedDate",
                'render': (data) => formatDate(data)
            },
            { 'data': "timeOfEntry" },
            { 'data': "timeOfExit" },
            { 'data': "hourlyRate" },
            { 'data': "isPaid" },
            { 'data': "isCancelled" }
        ];

        //Inicializar la tabla como un data table
        $("#tblPersonalT").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: { //Esto es para adaptar la tabla al español
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_  registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            },
            select: true // Habilitar la selección de filas
        });
    };
}

//Instaciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Entrenador", "Cliente"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new PersonalTrainingViewController();
        vc.InitView();
    }
});
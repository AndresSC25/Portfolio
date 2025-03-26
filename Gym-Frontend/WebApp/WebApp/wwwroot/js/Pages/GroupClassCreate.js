//JS que maneja todo el comportamiento de la vista CreateGroupClass.cshtml
let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
};
//Definimos la clase
var lstTrainers = [];

function GroupClassCreateViewController() {
    this.ViewName = "GroupClass";
    this.ApiBaseEndPoint = "GroupClass";
    var ca = new ControlActions();

    //Metodo constructor de la vista
    this.InitView = function() {


        // Cargar los entrenadores al cargar la página
        ca.GetToApi("User/RetrieveByRoleWithSchedule?id=2",
            (response) => {
                lstTrainers = response.map(user => ({
                    id: user.id,
                    fullName: user.name + " " + user.lastName,
                    daysOfWeek: user.daysOfWeek,
                    timeOfEntry: user.timeOfEntry,
                    timeOfExit: user.timeOfExit
                }));
            });

        //Bind del click del boton create con el metodo
        $("#btnCreate").click(function() {
            var vc = new GroupClassCreateViewController();
            vc.Create();
        });

        $("#btnModify").click(function() {
            var vc = new GroupClassCreateViewController();
            vc.Update();
        });

        $("#btnDelete").click(function() {
            var vc = new GroupClassCreateViewController();
            vc.Delete();
        });


        this.LoadTable();
        this.LoadTrainers();

        $("#trainer").change(function() {
            var selectedTrainer = $(this).val();
            var trainer = lstTrainers.find(trainer => trainer.id == selectedTrainer);

            if (!selectedTrainer) {
                $("#workDays").text("N/A");
                $("#startTimeInfo").text("N/A");
                $("#endTimeInfo").text("N/A");
            } else if (trainer) {
                var daysMap = {
                    "L": "Lunes",
                    "K": "Martes",
                    "M": "Miércoles",
                    "J": "Jueves",
                    "V": "Viernes",
                    "S": "Sábado",
                    "D": "Domingo"
                };

                var stringDays = trainer.daysOfWeek.split("").map(day => daysMap[day]).join(", ");

                $("#workDays").text(stringDays);
                $("#startTimeInfo").text(trainer.timeOfEntry);
                $("#endTimeInfo").text(trainer.timeOfExit);
            }
        });


        $("table tbody").on("click",
            "tr",
            function() {

                // Obtener la tabla que contiene la fila seleccionada
                var table = $(this).closest("table");

                // Remover el color de fondo de todas las filas en la tabla actual
                table.find("tbody tr").css("background-color", "");

                // Cambiar el color de fondo de la fila seleccionada a gris oscuro
                $(this).css("background-color", "#B3CCF1"); // Puedes ajustar el color según tus necesidades
            });

        $("#tblGroupClasses tbody").on("click",
            "tr",
            function() {


                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var groupClassDTO = $("#tblGroupClasses").DataTable().row(row).data();

                $("#clientsContainer").removeClass("d-none").show();
                loadClientsTable(groupClassDTO.id);

                //Mapeo de valores del DTO al formulario
                $("#classId").val(groupClassDTO.id);
                $("#className").val(groupClassDTO.className);
                $("#classDate").val(groupClassDTO.classDate.split("T")[0]);
                $("#startTime").val(groupClassDTO.startTime);
                $("#endTime").val(groupClassDTO.endTime);
                $("#maxCapacity").val(groupClassDTO.maxCapacity);
                $("#trainer").val(groupClassDTO.employeeId);


            });

    };

    //Metodo de creacion de clase grupal
    this.Create = function() {

        //Crear el DTO de GroupClass
        var employeeId = $("#trainer").val();
        var className = $("#className").val();
        var maxCapacity = $("#maxCapacity").val();
        var classDate = $("#classDate").val();
        var startTimeSplit = $("#startTime").val().split(":");
        var endTimeSplit = $("#endTime").val().split(":");

        var startTime = startTimeSplit[0] + ":" + startTimeSplit[1];
        var endTime = endTimeSplit[0] + ":" + endTimeSplit[1];

        if (employeeId == "") {
            employeeId = 0;
        }
        if (maxCapacity == "") {
            maxCapacity = 0;
        }
        if (classDate == "") {
            classDate = "2000-01-01T00:00:00.000Z";
        }
        if (startTime == "") {
            startTime = "00:00:00";
        } else {
            startTime += ":00";
        }
        if (endTime == "") {
            endTime = "00:00:00";
        } else {
            endTime += ":00";
        }

        var groupClass = {
            Id: 0,
            EmployeeId: employeeId,
            EmployeeName: "",
            ClassName: className,
            MaxCapacity: maxCapacity,
            CurrentRegistered: 0,
            ClassDate: classDate,
            StartTime: startTime,
            EndTime: endTime
        };


        //Invocar API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";
        ca.PostToAPI(endPointRoute,
            groupClass,
            function() {
                console.log("Equipment created");
                $("#tblGroupClasses").DataTable().ajax.reload(); // Recargar la tabla

                $("#classId").val("");
                $("#className").val("");
                $("#classDate").val("");
                $("#startTime").val("");
                $("#endTime").val("");
                $("#maxCapacity").val("");
                $("#trainer").val("");

            });


    };

    this.Update = function() {

        //Crear el DTO de GroupClass
        var employeeId = $("#trainer").val();
        var className = $("#className").val();
        var maxCapacity = $("#maxCapacity").val();
        var classDate = $("#classDate").val();
        var startTimeSplit = $("#startTime").val().split(":");
        var endTimeSplit = $("#endTime").val().split(":");

        var startTime = startTimeSplit[0] + ":" + startTimeSplit[1];
        var endTime = endTimeSplit[0] + ":" + endTimeSplit[1];

        if (employeeId == "") {
            employeeId = 0;
        }
        if (maxCapacity == "") {
            maxCapacity = 0;
        }
        if (classDate == "") {
            classDate = "2000-01-01T00:00:00.000Z";
        }
        if (startTime == "") {
            startTime = "00:00:00";
        } else {
            startTime += ":00";
        }
        if (endTime == "") {
            endTime = "00:00:00";
        } else {
            endTime += ":00";
        }

        var groupClass = {
            Id: $("#classId").val(),
            EmployeeId: employeeId,
            EmployeeName: "",
            ClassName: className,
            MaxCapacity: maxCapacity,
            CurrentRegistered: 0,
            ClassDate: classDate,
            StartTime: startTime,
            EndTime: endTime
        };

        if ($("#classId").val() == 0 || $("#classId").val() == "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                html: "Por favor elige una clase a modificar.",
                footer: "SILUETA CLUB FITNESS"
            });
        } else {
            //Invocar API
            var ca = new ControlActions();
            var endPointRoute = this.ApiBaseEndPoint + "/Update";
            ca.PutToAPI(endPointRoute,
                groupClass,
                function() {
                    console.log("Group class modified");
                    $("#tblGroupClasses").DataTable().ajax.reload(); // Recargar la tabla
                    $("#classId").val("");
                    $("#className").val("");
                    $("#classDate").val("");
                    $("#startTime").val("");
                    $("#endTime").val("");
                    $("#maxCapacity").val("");
                    $("#trainer").val("");
                });

        }
    };

    this.Delete = function() {

        //Crear el DTO de GroupClass
        var employeeId = $("#trainer").val();
        var className = $("#className").val();
        var maxCapacity = $("#maxCapacity").val();
        var classDate = $("#classDate").val();
        var startTimeSplit = $("#startTime").val().split(":");
        var endTimeSplit = $("#endTime").val().split(":");

        var startTime = startTimeSplit[0] + ":" + startTimeSplit[1];
        var endTime = endTimeSplit[0] + ":" + endTimeSplit[1];

        if (employeeId == "") {
            employeeId = 0;
        }
        if (maxCapacity == "") {
            maxCapacity = 0;
        }
        if (classDate == "") {
            classDate = "2000-01-01T00:00:00.000Z";
        }
        if (startTime == "") {
            startTime = "00:00:00";
        } else {
            startTime += ":00";
        }
        if (endTime == "") {
            endTime = "00:00:00";
        } else {
            endTime += ":00";
        }

        var groupClass = {
            Id: $("#classId").val(),
            EmployeeId: employeeId,
            EmployeeName: "",
            ClassName: className,
            MaxCapacity: maxCapacity,
            CurrentRegistered: 0,
            ClassDate: classDate,
            StartTime: startTime,
            EndTime: endTime
        };

        if ($("#classId").val() == 0 || $("#classId").val() == "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                html: "Por favor elige una clase a eliminar.",
                footer: "SILUETA CLUB FITNESS"
            });
        } else {
            Swal.fire({
                icon: "warning", // Cambiado de "error" a "warning" para indicar una acción de precaución
                title: "¿Estás seguro?",
                text:
                    "Esta acción eliminará la clase grupal seleccionada y cancelará el registro de todos los cliente. ¿Deseas continuar?",
                footer: "SILUETA CLUB FITNESS",
                showCancelButton: true, // Mostrar botón de cancelar
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar"
            }).then((result) => {
                if (result.isConfirmed) {
                    //Invocar API
                    var ca = new ControlActions();
                    var endPointRoute = this.ApiBaseEndPoint + "/Delete?id=" + groupClass.Id;
                    ca.DeleteToAPI(endPointRoute,
                        groupClass,
                        function() {
                            console.log("Group class Deleted");
                            Swal.fire({
                                icon: "success", // Cambiado a "success" para indicar que la operación fue exitosa
                                title: "Clase eliminada",
                                text: "La clase y todos sus registros asociados han sido eliminados correctamente.",
                                footer: "SILUETA CLUB FITNESS"
                            });
                            $("#tblGroupClasses").DataTable().ajax.reload(); // Recargar la tabla
                            $("#classId").val("");
                            $("#className").val("");
                            $("#classDate").val("");
                            $("#startTime").val("");
                            $("#endTime").val("");
                            $("#maxCapacity").val("");
                            $("#trainer").val("");
                        });
                }
            });


        }

    };

    this.LoadTable = function() {
        var ca = new ControlActions();

        //Construir la ruta del API para consumir el servicio de Retrieve
        var urlService = ca.GetUrlApiService("GroupClass/RetrieveAllWithName");

        var columns = [];
        columns[0] = { 'data': "className" };
        columns[1] = {
            'data': "classDate",
            'render': (data) => formatDate(data)
        };
        columns[2] = { 'data': "startTime" };
        columns[3] = { 'data': "endTime" };
        columns[4] = { 'data': "maxCapacity" };
        columns[5] = { 'data': "currentRegistered" };
        columns[6] = { 'data': "employeeName" };


        $("#tblGroupClasses").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: {
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ Registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
        });
    };

    function loadClientsTable(groupClassId) {
        var ca = new ControlActions();

        // Construir la ruta del API para consumir el servicio de Retrieve
        var urlService =
            ca.GetUrlApiService("UserGroupClass/RetrieveByGroupClassWithName?groupClassId=" + groupClassId);

        // Limpiar el DataTable si ya está inicializado
        if ($.fn.DataTable.isDataTable("#tblClients")) {
            $("#tblClients").DataTable().clear().destroy();
        }

        var columns = [];
        columns[0] = { 'data': "clientId" };
        columns[1] = { 'data': "clientName" };
        columns[2] = {
            'data': "created",
            'render': (data) => formatDate(data)
        };

        $("#tblClients").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: {
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ Registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:",
                emptyTable: "No hay clientes registrados en esta clase." // Texto a mostrar si la tabla está vacía
            }
        });
    }

    this.LoadTrainers = function() {
        var ca = new ControlActions();

        // Usar GetToApi para obtener los datos de roles
        ca.GetToApi("User/RetrieveByRoleWithSchedule?id=2",
            (response) => {
                var $sltUsers = $("#trainer");
                $sltUsers.empty(); // Limpiar el select antes de llenarlo

                // N/A option
                $sltUsers.append(new Option("Seleccione el entrenador", ""));


                response.forEach(user => {
                    $sltUsers.append(new Option(user.name + " " + user.lastName, user.id));
                });
            });
    };
}

//Instaciamiento de la clase
$(document).ready(function() {
    var vc = new GroupClassCreateViewController();
    vc.InitView();
});
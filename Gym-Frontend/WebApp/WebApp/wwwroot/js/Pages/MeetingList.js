

let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
};


function MeetingListViewController() {
    this.ViewName = "MeetingList";
    this.ApiBaseEndPoint = "Meeting";

    //Metodo constructor de la vista
    this.InitView = function() {

        this.LoadTableMeetings();

        $("#btnCancel").click(function() {
            var vc = new MeetingListViewController();
            vc.Cancel();
        });

        $("#tblMeetings tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var meetingDTO = $("#tblMeetings").DataTable().row(row).data();


                //Mapeo de valores del DTO al formulario
                $("#txtIdPT").val(meetingDTO.id);
                $("#txtDate").val(meetingDTO.programmedDate.split("T")[0]);
                $("#txtTimeIn").val(meetingDTO.timeOfEntry);
                $("#txtTimeOut").val(meetingDTO.timeOfExit);

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

    this.LoadTableMeetings = function() {
        var ca = new ControlActions();

        var urlService = ca.GetUrlApiService("Meetings/RetrieveAllWithName");
        var columns = [
            { 'data': "id" },
            { 'data': "clientId" },
            { 'data': "clientName" },
            { 'data': "employeeId" },
            { 'data': "employeeName" },
            {
                'data': "programmedDate",
                'render': (data) => formatDate(data)
            },
            { 'data': "timeOfEntry" },
            { 'data': "timeOfExit" },
            { 'data': "isCancelled" }
        ];

        //Inicializar la tabla como un data table
        $("#tblMeetings").DataTable({
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

    this.Cancel = function() {
        var dateInput = $("#txtDate").val();
        console.log(dateInput);
        if (dateInput) {
            dateInput = $("#txtDate").val() + "T17:26:50.620Z";
        } else {
            dateInput = "2000-01-01T17:26:50.620Z";
        }


        //Creacion del objeto meeting
        var meeting = {
            id: $("#txtIdPT").val() || 0,
            clientId: 0,
            employeeId: 0,
            timeOfEntry: $("#txtTimeIn").val() || "00:00:00",
            timeOfExit: $("#txtTimeOut").val() || "00:00:00",
            programmedDate: dateInput,
            isCancelled: "no",
            created: "2024-06-29T17:26:50.620Z",
            clientName: "",
            employeeName: ""
        };


        // Invocar API
        var ca = new ControlActions();
        var endPointRoute = "Meetings/CancelMeeting";
        ca.PutToAPI(endPointRoute,
            meeting,
            function() {

                $("#tblMeetings").DataTable().ajax.reload();
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
        var vc = new MeetingListViewController();
        vc.InitView();
    }
});
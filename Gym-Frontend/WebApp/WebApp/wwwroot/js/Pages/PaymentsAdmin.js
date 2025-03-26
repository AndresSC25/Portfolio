//JS que maneja todo el comportamiento de la vista Pages\PaymentsAdmin


function PaymentsViewController() {
    this.ViewName = "Meeting";
    this.ApiBaseEndPoint = "Meeting";

    //Metodo constructor de la vista
    this.InitView = function() {

        //Cargar tabla de facturas
        this.LoadTableInvoices();

        $("#tblInvoices tbody").on("click",
            "tr",
            function() {

                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");
                //Extraemos la data de la tabla
                var invoiceDTO = $("#tblInvoices").DataTable().row(row).data();

                //Mostramos la section de la factura 
                $("#invoiceContainer").removeClass("d-none");

                //Accion despues de seleccionar un invoice
                $("#invoiceNum").text(invoiceDTO.id);
                $("#personName").text(invoiceDTO.userName);
                $("#invoiceDate").text(invoiceDTO.created.split("T")[0]);
                $("#totalAmount").text(invoiceDTO.amount);
                $("#discountAmount").text(invoiceDTO.amount - invoiceDTO.amountAfterDiscount);
                $("#amountAfterDiscount").text(invoiceDTO.amountAfterDiscount);
                // Invocar API


                var ca = new ControlActions();
                var endPointRoute = "Detail/RetrieveByInvoiceId?invoiceId=" + invoiceDTO.id;
                ca.GetToApi(endPointRoute,
                    (response) => {
                        processDetails(response);
                    });


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

    };


    this.LoadTableInvoices = function() {
        var ca = new ControlActions();

        // Construir la ruta del API para consumir el servicio de Retrieve con id 4 para traer solo los clientes
        var urlService = ca.GetUrlApiService("Invoice/RetrieveAllwithDetails");

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "userId" },
            { 'data': "userName" },
            { 'data': "discountId" },
            { 'data': "amount" },
            { 'data': "amountAfterDiscount" },
            { 'data': "paymentMethod" },
            { 'data': "isConfirmed" },
            { 'data': "created" }
        ];

        // Inicializar la tabla como un data table
        $("#tblInvoices").DataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            "columnDefs": [
                { "className": "text-center", "targets": [0, 1, 2, 3, 4, 5] }
            ],
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


    function processDetails(details) {
        $("#personalClassesDetailsSection").hide();
        details.forEach(detailData => {
            if (detailData.personalTrainingId == 2) {
                //Aca se carga los detalles de la membresia
                processMembership(detailData);
            } else {
                //Aca se cargan los detalles de los personal training

                // Limpiar la tabla de detalles antes de agregar nuevas filas
                $("#tblInvoiceDetails tbody").empty();

                // Mostrar la sección de detalles si está oculta
                $("#personalClassesDetailsSection").show();

                var ca = new ControlActions();
                var endPointRoute = "PersonalTraining/RetrieveById?id=" + detailData.personalTrainingId;
                ca.GetToApi(endPointRoute,
                    (response) => {
                        processPersonalTraining(response);
                    });
            }
        });
    }

    function processMembership(detail) {
        var ca = new ControlActions();
        var endPointRoute = "UserMembership/RetrieveById?id=" + detail.userMembershipId;
        ca.GetToApi(endPointRoute,
            (response1) => {
                endPointRoute = "Membership/RetrieveById?id=" + response1.membershipId;
                ca.GetToApi(endPointRoute,
                    (response2) => {
                        $("#membershipType").text("Membresía " + response2.type);
                        $("#membershipPrice").text(response2.monthlyCost);
                    });
            });


    }

    function processPersonalTraining(personalTraining) {


        // Obtener la fecha de la clase (programmedDate)
        var classDate = personalTraining.programmedDate;

        // Calcular la cantidad de horas entre timeOfEntry y timeOfExit
        var entryTime = new Date("1970-01-01T" + personalTraining.timeOfEntry + "Z");
        var exitTime = new Date("1970-01-01T" + personalTraining.timeOfExit + "Z");
        var hours = (exitTime - entryTime) / 1000 / 60 / 60; // Conversión de ms a horas

        // Obtener el precio por hora
        var hourlyRate = personalTraining.hourlyRate;

        // Calcular el total
        var total = hours * hourlyRate;


        var ca = new ControlActions();
        var endPointRoute = "User/RetrieveById?id=" + personalTraining.employeeId;
        ca.GetToApi(endPointRoute,
            (response) => {

                // Aquí construyo el html para cargarlo en tblInvoiceDetails
                var newRow =
                    `
                <tr>
                    <td>${classDate.split("T")[0]}</td>
                    <td>${response.name + " " + response.lastName}</td>
                    <td>${hours.toFixed(2)}</td>
                    <td>${hourlyRate.toFixed(2)}</td>
                    <td>${total.toFixed(2)}</td>
                </tr>
            `;

                // Agregar la nueva fila a la tabla de detalles de la factura
                $("#tblInvoiceDetails tbody").append(newRow);


            });

    }

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
        var vc = new PaymentsViewController();
        vc.InitView();
    }
});
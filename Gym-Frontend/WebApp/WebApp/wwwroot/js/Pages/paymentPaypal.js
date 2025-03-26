// Definimos la clase

var membershipId = 0;
var clientId = sessionStorage.getItem("UserId");
var discount = 0;
var totalMembership = 0;
var totalMemAfterDisc = 0;
var totalPT = 0;
var totalAmount = 0;
var discountId = -1;

function InvoiceViewController() {
    this.ViewName = "Invoices";

    // Método constructor de la vista
    this.InitView = function() {

        $("#paypal-button").click(function() {
            var vc = new InvoiceViewController();
            vc.LoadPaypal();
        });


        // Carga de la tabla
        this.LoadTableMemberships();
        this.LoadDiscounts();
        this.LoadPersonalClasses();
        this.LoadInvoice();

        $("#tblMemberships tbody").on("click",
            "tr",
            function() {
                totalMembership = 0;
                totalPT = 0;
                totalAmount = 0;
                $("#paypal-button").removeClass("d-none").show();
                //Selecionar la fila a la que dio click
                var row = $(this).closest("tr");

                //Extraemos la data de la tabla
                var MembershipDTO = $("#tblMemberships").DataTable().row(row).data();

                //Mapeo de valores del DTO al formulario
                $("#membershipType").text(MembershipDTO.type);
                $("#membershipPrice").text(MembershipDTO.monthlyCost);
                membershipId = MembershipDTO.id;
                totalMembership += MembershipDTO.monthlyCost;

                //Logica para cargar las clases personales
                var personalTable = $("#tblPersonalT").DataTable();
                var discountTable = $("#tblDiscount").DataTable();
                // Obtén los datos de la tabla tblPersonalT
                var personalData = personalTable.rows().data().toArray();
                var discountData = discountTable.rows().data().toArray();

                if (personalData && personalData.length > 0) {
                    // Si hay clases personales, procesarlas
                    processPersonalClasses(personalData);
                } else {
                    // Si no hay clases, ocultar la sección de detalles
                    $("#personalClassesDetailsSection").hide();
                }

                var ca = new ControlActions();
                var endPointRoute = "UserMembership/RetrieveNewestByUserId?userId=" + clientId;
                totalAmount = totalMembership + totalPT;
                ca.GetToApi(endPointRoute,
                    (response) => {
                        var createdDate = new Date(response.created); //Fecha de la ultima membresia
                        var currentDate = new Date(); // Fecha actual
                        // Restar 3 meses de la fecha actual
                        var pastDate = new Date(currentDate);

                        pastDate.setMonth(pastDate.getMonth() - 3);
                        if (response.membershipId == 0) {
                            var percentage = (discountData[0].percentage) / 100;
                            var discount = totalMembership * (percentage);
                            $("#totalAmount").text(totalAmount);
                            $("#discountAmount").text(discount);
                            $("#amountAfterDiscount").text(totalMembership - discount + totalPT);
                        } else if (createdDate <= pastDate) {
                            var percentage = (discountData[1].percentage) / 100;
                            var discount = totalMembership * (percentage);
                            $("#totalAmount").text(totalAmount);
                            $("#discountAmount").text(discount);
                            $("#amountAfterDiscount").text(totalMembership - discount + totalPT);
                        } else {
                            $("#totalAmount").text(totalAmount);
                            $("#discountAmount").text(0);
                            $("#amountAfterDiscount").text(totalAmount);
                        }

                    });


            });


        $("#tblMemberships tbody, #tblDiscount tbody").on("click",
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


    this.LoadDiscounts = function() {
        var ca = new ControlActions();


        // Construir la ruta del API para consumir el servicio de Retrieve con el ID del usuario
        var urlService = ca.GetUrlApiService("Discount/RetrieveAll");

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "type" },
            { 'data': "coupon" },
            { 'data': "percentage" },
            {
                'data': "validFrom",
                'render': (data) => formatDate(data)
            },
            {
                'data': "validTo",
                'render': (data) => formatDate(data)
            },
            {
                'data': "created",
                'render': (data) => formatDate(data)
            }
        ];

        //Inicializar la tabla como un data table
        $("#tblDiscount").DataTable({
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
    this.LoadPaypal = function() {
        let paymentWindow;
        const overlay =
            $(
                '<div id="overlay" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background: rgba(0, 0, 0, 0.5); z-index: 9999;"></div>');
        $("body").append(overlay);

        // Listener para recibir mensajes desde la ventana emergente
        window.addEventListener("message",
            function(event) {
                // Aseguramos que el overlay se remueva siempre
                overlay.remove();

                if (event.data === "success") {
                    Swal.fire({
                        icon: "success",
                        title: "Éxito",
                        text: "Pago completado con éxito."
                    }).then(() => {
                        Confirm();
                        window.location.href = "/MenuPayments";
                    });
                } else if (event.data === "failed" || event.data === "error") {
                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: "Hubo un problema al completar tu pago."
                    });
                }
            },
            false);

        $.post("https://localhost:7038/api/PayPal/create-payment?amount=" + (totalMembership - discount + totalPT))
            .done(function(response) {
                if (response.redirectUrl) {
                    paymentWindow = window.open(response.redirectUrl, "_blank", "width=800,height=600");
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Error",
                        text: "No se pudo crear el pago."
                    }).then(() => {
                        overlay.remove();
                    });
                }
            })
            .fail(function(jqXHR, textStatus, errorThrown) {
                console.error("Error al crear el pago:", textStatus, errorThrown);
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "Hubo un problema al crear tu pago."
                }).then(() => {
                    overlay.remove();
                });
            });


    };

    function Confirm() {
        // Obtener el método de pago seleccionado
        var paymentMethod = "Paypal";

        // Crear el objeto invoice para enviar
        var invoice = {
            id: 0,
            userId: clientId,
            discountId: discountId,
            amount: 0,
            amountAfterDiscount: 0,
            paymentMethod: paymentMethod,
            isConfirmed: "si",
            created: new Date().toISOString(), // Obtén la fecha y hora actual en formato ISO
            membershipID: membershipId
        };


        // Si el usuario confirma, proceder con la creación de la factura

        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = "Invoice/Create";

        ca.PostToAPI(endPointRoute,
            invoice,
            function() {

                Swal.fire({
                    title: "Pago confirmado",
                    text: "La factura ha sido creada exitosamente.",
                    icon: "success",
                    allowOutsideClick:
                        false, // Evita que el usuario cierre el cuadro de diálogo haciendo clic fuera de él
                    allowEscapeKey: false, // Evita que el usuario cierre el cuadro de diálogo presionando la tecla Esc
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.href("/Menu");
                    }
                });
            });

    }

    this.LoadPersonalClasses = function() {
        var ca = new ControlActions();


        // Construir la ruta del API para consumir el servicio de Retrieve con el ID del usuario
        var urlService = ca.GetUrlApiService("PersonalTraining/RetrieveByClientIdPayable?id=" + clientId);

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
            { 'data': "isPaid" }
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
                search: "Filtrar:",
                emptyTable: "No hay entrenamientos personales por pagar"
            },
            select: true // Habilitar la selección de filas
        });
    };

    function processPersonalClasses(classes) {
        // Mostrar la sección de detalles si está oculta
        $("#personalClassesDetailsSection").show();

        // Limpiar la tabla de detalles antes de agregar nuevas filas
        $("#tblInvoiceDetails tbody").empty();

        // Recorrer las clases y agregarlas a la factura
        classes.forEach(classData => {
            // Obtener la fecha de la clase (programmedDate)
            var classDate = classData.programmedDate;

            // Calcular la cantidad de horas entre timeOfEntry y timeOfExit
            var entryTime = new Date("1970-01-01T" + classData.timeOfEntry + "Z");
            var exitTime = new Date("1970-01-01T" + classData.timeOfExit + "Z");
            var hours = (exitTime - entryTime) / 1000 / 60 / 60; // Conversión de ms a horas

            // Obtener el precio por hora
            var hourlyRate = classData.hourlyRate;

            // Calcular el total
            var total = hours * hourlyRate;
            totalPT += total;
            // Aquí construyo el html para cargarlo en tblInvoiceDetails
            var newRow =
                `
                <tr>
                    <td>${classDate.split("T")[0]}</td>
                    <td>${classData.employeeName}</td>
                    <td>${hours.toFixed(2)}</td>
                    <td>${hourlyRate.toFixed(2)}</td>
                    <td>${total.toFixed(2)}</td>
                </tr>
            `;

            // Agregar la nueva fila a la tabla de detalles de la factura
            $("#tblInvoiceDetails tbody").append(newRow);
        });
    }

    this.LoadTableMemberships = function() {
        var ca = new ControlActions();

        // Construimos la ruta del API para consumir el servicio del Retrieve
        var urlService = ca.GetUrlApiService("Membership/RetrieveAll");

        // Definir las columnas a extraer del json que devuelve el API
        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "type" };
        columns[2] =
        {
            'data': "amountClassesAllowed",
            'render': (data) => {
                if (data === 999) {
                    return "Ilimitado";
                } else {
                    return data;
                }
            }
        };
        columns[3] = { 'data': "monthlyCost" };


        // Inicializar la tabla, como un data table
        $("#tblMemberships").dataTable({
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

        // Asignar al evento click de la fila de la tabla
        $("#tblMemberships tbody").on("click",
            "tr",
            function() {
                // Seleccionar la fila a la que le dio click
                var row = $(this).closest("tr");

                // Extraemos la data de la tabla
                var membershipDTO = $("#tblMemberships").DataTable().row(row).data();

                // Mapeo de valores del DTO al formulario
                $("#txtId").val(membershipDTO.id);
                $("#txtType").val(membershipDTO.type);
                $("#txtAmountClassesAllowed").val(membershipDTO.amountClassesAllowed);
                $("#txtMonthlyCost").val(membershipDTO.monthlyCost);
            });
    };

    this.LoadDiscounts = function() {
        var ca = new ControlActions();


        // Construir la ruta del API para consumir el servicio de Retrieve con el ID del usuario
        var urlService = ca.GetUrlApiService("Discount/RetrieveAll");

        // Definir las columnas de la tabla
        var columns = [
            { 'data': "id" },
            { 'data': "type" },
            { 'data': "coupon" },
            { 'data': "percentage" },
            {
                'data': "validFrom",
                'render': (data) => formatDate(data)
            },
            {
                'data': "validTo",
                'render': (data) => formatDate(data)
            },
            {
                'data': "created",
                'render': (data) => formatDate(data)
            }
        ];

        //Inicializar la tabla como un data table
        $("#tblDiscount").DataTable({
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

    this.LoadInvoice = function() {
        var client = {};
        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = "User/RetrieveById?id=" + clientId;

        ca.GetToApi(endPointRoute,
            (response) => {
                client = {
                    id: response.id,
                    fullName: response.name + " " + response.lastName
                };
                $("#personName").text(client.fullName);
            });

        // Obtener la fecha actual
        const today = new Date();
        const day = String(today.getDate()).padStart(2, "0");
        const month = String(today.getMonth() + 1).padStart(2, "0"); // Los meses comienzan desde 0
        const year = today.getFullYear();

        // Formatear la fecha como dd/mm/yyyy
        const formattedDate = `${day}/${month}/${year}`;

        // Cargar la fecha en el elemento con id "invoiceDate"
        $("#invoiceDate").text(formattedDate);


    };

    // Función para formatear la fecha
    let formatDate = (dateString) => {
        var date = new Date(dateString);
        var year = date.getFullYear();
        var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
        var day = date.getDate().toString().padStart(2, "0");
        return `${year}-${month}-${day}`;
    };
}

// Instanciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    if (isAuthenticated !== "true") {
        window.location.href = "/Index";
    } else {
        var vc = new InvoiceViewController();
        vc.InitView();
    }
});
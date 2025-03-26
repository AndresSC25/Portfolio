// Definimos la clase
function MembershipViewController() {
    this.ViewName = "Memberships";
    this.ApiBaseEndPoint = "Membership";

    // Método constructor de la vista
    this.InitView = function() {
        // Bind del click del botón create con el método
        $("#btnCreate").click(function() {
            var vc = new MembershipViewController();
            vc.Create();
        });

        // Bind del click del botón update con el método
        $("#btnUpdate").click(function() {
            var vc = new MembershipViewController();
            vc.Update();
        });

        $("#btnDelete").click(function() {
            var vc = new MembershipViewController();
            vc.Delete();
        });

        // Carga de la tabla
        this.LoadTable();
    };

    // Método creación de membresías
    this.Create = function() {
        var typeX, amountClasses, monthlyCostX;

        if ($("#txtType").val() == "" || $("#txtType").val() == null) {
            typeX = "u";
        } else {
            typeX = $("#txtType").val();
        }

        if ($("#txtAmountClassesAllowed").val() == "" || $("#txtAmountClassesAllowed").val() == null) {
            amountClasses = -1;
        } else {
            amountClasses = $("#txtAmountClassesAllowed").val();
        }

        if ($("#txtMonthlyCost").val() == "" || $("#txtMonthlyCost").val() == null) {
            monthlyCostX = -1;
        } else {
            monthlyCostX = $("#txtMonthlyCost").val();
        }

        var membership = {};

        membership.type = typeX;
        membership.amountClassesAllowed = amountClasses;
        membership.monthlyCost = monthlyCostX;
        // Valores default
        membership.id = 0;

        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";

        ca.PostToAPI(endPointRoute,
            membership,
            function() {
                console.log("Membership creada");
                $("#tblMemberships").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.Update = function() {
        var membership = {};
        membership.id = $("#txtId").val();
        membership.type = $("#txtType").val();
        membership.amountClassesAllowed = $("#txtAmountClassesAllowed").val();
        membership.monthlyCost = $("#txtMonthlyCost").val();
        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Update";

        ca.PutToAPI(endPointRoute,
            membership,
            function() {
                console.log("Membership actualizada");
                $("#tblMemberships").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.Delete = function() {
        var membershipId = $("#txtId").val();

        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Delete/" + membershipId;

        ca.DeleteToAPI(endPointRoute,
            null,
            () => {
                Swal.fire(
                    "Eliminado",
                    "La membresía ha sido eliminada.",
                    "success"
                );
                console.log("Membership deleted");
                $("#tblMemberships").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.LoadTable = function() {
        var ca = new ControlActions();

        // Construimos la ruta del API para consumir el servicio del Retrieve
        var urlService = ca.GetUrlApiService(this.ApiBaseEndPoint + "/RetrieveAll");

        // Definir las columnas a extraer del json que devuelve el API
        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "type" };
        columns[2] = { 'data': "amountClassesAllowed" };
        columns[3] = { 'data': "monthlyCost" };
        columns[4] = {
            'data': "created",
            'render': (data) => formatDate(data)
        };

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
}

// Función para formatear la fecha
let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
};

let resetFormValues = () => {
    $("#txtId").val("");
    $("#txtType").val("");
    $("#txtAmountClassesAllowed").val("");
    $("#txtMonthlyCost").val("");
};

// Instanciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    if (isAuthenticated !== "true") {
        window.location.href = "/Index";
    } else {
        var vc = new MembershipViewController();
        vc.InitView();
    }
});
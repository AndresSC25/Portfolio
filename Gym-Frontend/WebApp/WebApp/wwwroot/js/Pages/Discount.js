// Definimos la clase
function DiscountViewController() {
    this.ViewName = "Discounts";
    this.ApiBaseEndPoint = "Discount";

    // Método constructor de la vista
    this.InitView = function() {
        // Bind del click del botón create con el método
        $("#btnCreate").click(function() {
            var vc = new DiscountViewController();
            vc.Create();
        });

        // Bind del click del botón update con el método
        $("#btnUpdate").click(function() {
            var vc = new DiscountViewController();
            vc.Update();
        });

        $("#btnDelete").click(function() {
            var vc = new DiscountViewController();
            vc.Delete();
        });

        // Carga de la tabla
        this.LoadTable();
    };

    // Método creación de descuentos
    this.Create = function() {
        var typeX, couponX, percentageX, validFromX, validToX;

        if ($("#txtType").val() == "" || $("#txtType").val() == null) {
            typeX = "u";
        } else {
            typeX = $("#txtType").val();
        }

        if ($("#txtCoupon").val() == "" || $("#txtCoupon").val() == null) {
            couponX = "u";
        } else {
            couponX = $("#txtCoupon").val();
        }

        if ($("#txtPercentage").val() == "" || $("#txtPercentage").val() == null) {
            percentageX = -1;
        } else {
            percentageX = $("#txtPercentage").val();
        }

        if ($("#txtValidFrom").val() == "" || $("#txtValidFrom").val() == null) {
            validFromX = "1821-08-09T00:00:00";
        } else {
            validFromX = $("#txtValidFrom").val();
        }

        if ($("#txtValidTo").val() == "" || $("#txtValidTo").val() == null) {
            validToX = "1821-08-09T00:00:00";
        } else {
            validToX = $("#txtValidTo").val();
        }

        var discount = {};

        discount.type = typeX;
        discount.coupon = couponX;
        discount.percentage = percentageX;
        discount.validFrom = validFromX;
        discount.validTo = validToX;
        // Valores default
        discount.id = 0;

        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";

        ca.PostToAPI(endPointRoute,
            discount,
            function() {
                console.log("Descuento creado");
                $("#tblDiscounts").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.Update = function() {
        var discount = {};
        discount.id = $("#txtId").val();
        discount.type = $("#txtType").val();
        discount.coupon = $("#txtCoupon").val();
        discount.percentage = $("#txtPercentage").val();
        discount.validFrom = $("#txtValidFrom").val();
        discount.validTo = $("#txtValidTo").val();
        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Update";

        ca.PutToAPI(endPointRoute,
            discount,
            function() {
                console.log("Descuento actualizado");
                $("#tblDiscounts").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.Delete = function() {
        var discountId = $("#txtId").val();

        // Invocar al API
        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Delete/" + discountId;

        ca.DeleteToAPI(endPointRoute,
            null,
            () => {
                Swal.fire(
                    "Eliminado",
                    "El descuento ha sido eliminado.",
                    "success"
                );
                console.log("Descuento eliminado");
                $("#tblDiscounts").DataTable().ajax.reload();
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
        columns[2] = { 'data': "coupon" };
        columns[3] = { 'data': "percentage" };
        columns[4] = {
            'data': "validFrom",
            'render': (data) => formatDate(data)
        };
        columns[5] = {
            'data': "validTo",
            'render': (data) => formatDate(data)
        };
        columns[6] = {
            'data': "created",
            'render': (data) => formatDate(data)
        };

        // Inicializar la tabla, como un data table
        $("#tblDiscounts").dataTable({
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
        $("#tblDiscounts tbody").on("click",
            "tr",
            function() {
                // Seleccionar la fila a la que le dio click
                var row = $(this).closest("tr");

                // Extraemos la data de la tabla
                var discountDTO = $("#tblDiscounts").DataTable().row(row).data();

                // Mapeo de valores del DTO al formulario
                $("#txtId").val(discountDTO.id);
                $("#txtType").val(discountDTO.type);
                $("#txtCoupon").val(discountDTO.coupon);
                $("#txtPercentage").val(discountDTO.percentage);
                $("#txtValidFrom").val(discountDTO.validFrom);
                $("#txtValidTo").val(discountDTO.validTo);
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
    $("#txtCoupon").val("");
    $("#txtPercentage").val("");
    $("#txtValidFrom").val("");
    $("#txtValidTo").val("");
};

// Instanciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new DiscountViewController();
        vc.InitView();
    }
});
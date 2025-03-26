function UsersViewController() {
    this.ViewNameUsers = "Users";
    this.ApiBaseEndPointUser = "User";
    this.ViewNameRol = "Rols";
    this.ApiBaseEndPointRol = "Rol";

    this.InitView = function() {
        console.log("User view init!!!");

        // Evento change para mostrar/ocultar disponibilidad del entrenador
        $("#selectedRol").change(function() {
            var selectedRol = $(this).val();
            var action = $("#actionSelect").val();
            if (selectedRol === "2" && action === "assign") {
                $("#trainerAvailability").show();
            } else {
                $("#trainerAvailability").hide();
            }
        });

        // Evento change para mostrar/ocultar campos según la acción seleccionada
        $("#actionSelect").change(function() {
            var action = $(this).val();
            var selectedRol = $("#selectedRol").val();
            if (selectedRol === "2" && action === "assign") {
                $("#trainerAvailability").show();
            } else {
                $("#trainerAvailability").hide();
            }
        });

        // Evento click para asignar o eliminar
        $("#btnSubmit").click(function() {
            var action = $("#actionSelect").val();
            var selectedRol = $("#selectedRol").val();
            var idUsuario = $("#textIdUserForNewRole").val();

            // Validación de campos vacíos
            if (action === "" || selectedRol === "0" || idUsuario === "") {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Por favor, rellene todos los campos.",
                });
                return false;
            }

            // Validación adicional para el rol de Entrenador cuando se selecciona "Asignar"
            if (selectedRol === "2" && action === "assign") {
                var isDaySelected = $("input[name='days']:checked").length > 0;
                var horaEntrada = $("#txtHoraEntrada").val();
                var horaSalida = $("#txtHoraSalida").val();

                if (!isDaySelected || !horaEntrada || !horaSalida) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Por favor, complete toda la disponibilidad del entrenador.",
                    });
                    return false;
                }
            }

            // Lógica para asignar o eliminar según la acción seleccionada
            var vc = new UsersViewController();
            if (action === "assign") {
                vc.Create();
            } else if (action === "remove") {
                vc.Delete();
            }
        });

        this.LoadTable();

        $("table tbody").on("click",
            "tr",
            function() {
                console.log("intento cambiar el color");
                var table = $(this).closest("table");
                table.find("tbody tr").css("background-color", "");
                $(this).css("background-color", "#B3CCF1");
            });
    };

    this.ClearForm = function() {
        $("#textIdUserForNewRole").val("");
        $("#selectedRol").val("0"); // Reiniciar la selección de rol
        $("#txtHoraEntrada").val("");
        $("#txtHoraSalida").val("");
        $("input[name='days']").prop("checked", false); // Desmarcar los días seleccionados
        $("#trainerAvailability").hide(); // Ocultar disponibilidad de entrenador
    };

    this.Create = function() {
        var userRol = {
            userId: $("#textIdUserForNewRole").val(),
            roleId: $("#selectedRol").val(),
            id: 0
        };

        // Agregar disponibilidad del entrenador si el rol "Entrenador" está seleccionado
        if (userRol.roleId === "2") {
            var daysOfWeek = "";
            var timeOfEntry = $("#txtHoraEntrada").val();
            var timeOfExit = $("#txtHoraSalida").val();

            if (timeOfEntry === "") {
                timeOfEntry = "00:00:00";
            } else {
                timeOfEntry += ":00";
            }

            if (timeOfExit === "") {
                timeOfExit = "00:00:00";
            } else {
                timeOfExit += ":00";
            }

            $("input[name='days']:checked").each(function() {
                daysOfWeek += $(this).val();
            });

            userRol.TimeOfEntry = timeOfEntry;
            userRol.TimeOfExit = timeOfExit;
            userRol.DaysOfWeek = daysOfWeek;
        }

        var ca = new ControlActions();
        var endPointRoute = "UserRol" + "/Create";

        ca.PostToAPI(endPointRoute,
            userRol,
            function() {
                console.log("Rol Asignado");
                $("#tblRol").DataTable().ajax.reload();
                var vc = new UsersViewController();
                vc.ClearForm(); // Limpiar el formulario después de crear
            });
    };

    this.Delete = function() {
        var userRol = {
            userId: $("#textIdUserForNewRole").val(),
            roleId: $("#selectedRol").val(),
            id: 0
        };

        var ca = new ControlActions();
        var endPointRoute = "Rol" + "/Delete";

        ca.DeleteToAPI(endPointRoute,
            userRol,
            function(response, errorType) {
                if (errorType) {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        html: "Ocurrió un error al eliminar el rol.",
                        footer: "SILUETA CLUB FITNESS"
                    });
                } else {
                    Swal.fire({
                        icon: "success",
                        title: "¡Éxito!",
                        text: "El rol se ha eliminado exitosamente.",
                        footer: "SILUETA CLUB FITNESS"
                    });

                    if ($("#tblRol").DataTable()) {
                        $("#tblRol").DataTable().ajax.reload();
                    }
                    var vc = new UsersViewController();
                    vc.ClearForm(); // Limpiar el formulario después de eliminar
                }
            });
    };

    this.LoadTable = function() {
        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService(this.ApiBaseEndPointUser + "/RetrieveAll");

        var columns = [
            { "data": "id" },
            { "data": "name" },
            { "data": "lastName" },
            { "data": "email" }
        ];

        $("#tblUsers").dataTable({
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

        $("#tblUsers tbody").on("click",
            "tr",
            function() {
                console.log("Testing click event");
                var row = $(this).closest("tr");
                var userDTO = $("#tblUsers").DataTable().row(row).data();
                $("#textIdUserForNewRole").val(userDTO.id);
                var urlService = new ControlActions().GetUrlApiService("Rol" + "/RetrieveByIdByUser?id=" + userDTO.id);
                $("#tblRol").dataTable({
                    destroy: true,
                    "ajax": {
                        "url": urlService,
                        "dataSrc": ""
                    },
                    columns: [
                        { "data": "id" },
                        { "data": "name" }
                    ],
                    language: {
                        info: "Mostrando página _PAGE_ de _PAGES_",
                        infoEmpty: "No se encuentran registros",
                        infoFiltered: "(Filtrado de _MAX_ registros totales)",
                        lengthMenu: "Mostrar _MENU_ Registros por página",
                        zeroRecords: "No se encuentran registros",
                        search: "Filtrar:"
                    }
                });
            });
    };
}

$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new UsersViewController();
        vc.InitView();
    }
});
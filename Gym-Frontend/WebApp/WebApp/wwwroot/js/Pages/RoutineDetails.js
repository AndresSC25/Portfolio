//Definimos la clase
function RoutineDetailsViewController() {

    //Metodo constructor de la vista
    this.InitView = function() {
        var routineId = sessionStorage.getItem("routineId"); // Obtener el ID del sessionStorage
        this.LoadRoutine(routineId);
        this.LoadRoutineProgression(routineId);
        $("#btnCreateProgress").click(function() {
            var vc = new RoutineDetailsViewController();
            vc.Create(routineId);
        });
        sessionStorage.removeItem("routineId"); // Opcionalmente, eliminar el ID después de usarlo
    };

    this.LoadRoutine = function(pRoutineId) {
        var ca = new ControlActions();

        if (pRoutineId) {
            ca.GetToApi(`Routine/RetrieveById?Id=${pRoutineId}`,
                (response) => {
                    var flatData = [];

                    response.exercise.forEach(exercise => {
                        flatData.push({
                            name: response.name, // El nombre de la rutina
                            exerciseId: exercise.id,
                            exerciseName: exercise.name,
                            type: exercise.type,
                            sets: exercise.sets,
                            weight: exercise.weight,
                            reps: exercise.reps,
                            duration: exercise.duration,
                            equipmentName: exercise.equipmentName
                        });
                    });

                    //Definir las columnas a extraer del json que deveulve el API
                    var columns = [];
                    columns[0] = { 'data': "exerciseName" };
                    columns[1] = { 'data': "type" };
                    columns[2] = {
                        'data': "sets",
                        'render': function(data) {
                            return data === 0 ? "N/A" : data;
                        }
                    };

                    columns[3] = {
                        'data': "weight",
                        'render': function(data) {
                            return data === 0 ? "N/A" : data;
                        }
                    };

                    columns[4] = {
                        'data': "reps",
                        'render': function(data) {
                            return data === 0 ? "N/A" : data;
                        }
                    };

                    columns[5] = {
                        'data': "duration",
                        'render': function(data) {
                            return data === 0 ? "N/A" : data;
                        }
                    };
                    columns[6] = { 'data': "equipmentName" };

                    // Verificar y destruir la instancia existente de DataTable
                    if ($.fn.DataTable.isDataTable("#tblRoutine")) {
                        $("#tblRoutine").DataTable().destroy();
                    }

                    //Inicializar la tabla, como un data table
                    $("#tblRoutine").dataTable({
                        data: flatData,
                        columns: columns,
                        language: { // Adaptar la tabla al español
                            info: "Mostrando página _PAGE_ de _PAGES_",
                            emptyTable: "No se encuentran registros",
                            infoFiltered: "(Filtrado de _MAX_ registros totales)",
                            lengthMenu: "Mostrar _MENU_ registros por página",
                            zeroRecords: "No se encuentran registros",
                            "loadingRecords": "Cargando...",
                            "infoEmpty": "Mostrando 0 entradas",
                            search: "Filtrar:"
                        },
                        "createdRow": function(row, data, dataIndex) {
                            $(row).attr("data-exercise-id", data.exerciseId);
                        }
                    });

                    $("#routineName").text(`Información de la rutina: ${flatData[0].name}`);

                    //Asignar al evento click de la fila de la tabla
                    $("#tblRoutine tbody").on("click",
                        "tr",
                        function() {

                            $("#progressForm").removeClass("d-none");

                            //Seleccionar la fila a la que le dio click
                            var row = $(this).closest("tr");

                            //Extraemos la data de la tabla
                            var routineDTO = $("#tblRoutine").DataTable().row(row).data();

                            var exerciseId = $(row).data("exercise-id");

                            //Mapeo de valores del DTO al formulario
                            $("#exerciseId").val(exerciseId);
                            $("#exerciseName").val(routineDTO.exerciseName);
                            $("#iSets").val(routineDTO.sets).prop("disabled", routineDTO.sets === 0);
                            $("#iWeight").val(routineDTO.weight).prop("disabled", routineDTO.weight === 0);
                            $("#iReps").val(routineDTO.reps).prop("disabled", routineDTO.reps === 0);
                            $("#iTime").val(routineDTO.duration).prop("disabled", routineDTO.duration === 0);
                        });
                });
        } else {
            $("#routineName").val("No se encontró la rutina");
        }
    };

    this.LoadRoutineProgression = function(pRoutineId) {
        var ca = new ControlActions();

        if (pRoutineId) {
            var urlService = ca.GetUrlApiService(`RoutineProgression/RetrieveByRoutineId?routineId=${pRoutineId}`);

            //Definir las columnas a extraer del json que deveulve el API
            var columns = [];
            columns[0] = { 'data': "exerciseName" };
            columns[1] = {
                'data': "sets",
                'render': function(data) {
                    return data === 0 ? "N/A" : data;
                }
            };

            columns[2] = {
                'data': "weight",
                'render': function(data) {
                    return data === 0 ? "N/A" : data;
                }
            };

            columns[3] = {
                'data': "reps",
                'render': function(data) {
                    return data === 0 ? "N/A" : data;
                }
            };

            columns[4] = {
                'data': "duration",
                'render': function(data) {
                    return data === 0 ? "N/A" : data;
                }
            };
            columns[5] = {
                'data': "comments",
                'render': function(data) {
                    return data.length === 0 ? "N/A" : data;
                }
            };
            columns[6] = {
                'data': "created",
                'render': function(data) {
                    // Convierte el string de fecha en un objeto Date y luego formatea la fecha
                    const date = new Date(data);
                    return date.toISOString().split("T")[0]; // Obtiene solo la parte de la fecha
                }
            };


            // Verificar y destruir la instancia existente de DataTable
            if ($.fn.DataTable.isDataTable("#tblRoutineProgress")) {
                $("#tblRoutineProgress").DataTable().destroy();
            }

            //Inicializar la tabla, como un data table
            $("#tblRoutineProgress").dataTable({
                "ajax": {
                    "url": urlService,
                    "dataSrc": ""
                },
                columns: columns,
                language: { // Adaptar la tabla al español
                    info: "Mostrando página _PAGE_ de _PAGES_",
                    emptyTable: "No se encuentran registros",
                    infoFiltered: "(Filtrado de _MAX_ registros totales)",
                    lengthMenu: "Mostrar _MENU_ registros por página",
                    zeroRecords: "No se encuentran registros",
                    "loadingRecords": "Cargando...",
                    "infoEmpty": "Mostrando 0 entradas",
                    search: "Filtrar:"
                }
            });
        }
    };

    this.Create = function(routineId) {
        var routineProgress = {};
        routineProgress.routineId = routineId;
        routineProgress.exerciseId = $("#exerciseId").val();
        routineProgress.sets = $("#iSets").val();
        routineProgress.weight = $("#iWeight").val();
        routineProgress.reps = $("#iReps").val();
        routineProgress.duration = $("#iTime").val();
        routineProgress.comments = $("#txtComments").val();

        if (routineProgress.sets === null || routineProgress.sets === "") {
            routineProgress.sets = 0;
        }
        if (routineProgress.weight === null || routineProgress.weight === "") {
            routineProgress.weight = 0;
        }
        if (routineProgress.reps === null || routineProgress.reps === "") {
            routineProgress.reps = 0;
        }
        if (routineProgress.duration === null || routineProgress.duration === "") {
            routineProgress.duration = 0;
        }

        //Valores default
        routineProgress.id = 0;

        //Invocar al API
        var ca = new ControlActions();

        ca.PostToAPI("RoutineProgression/Create",
            routineProgress,
            function() {
                $("#tblRoutineProgress").DataTable().ajax.reload();
                $("#progressForm").addClass("d-none");
            });
    };

}

$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Entrenador", "Cliente"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new RoutineDetailsViewController();
        vc.InitView();
    }
});
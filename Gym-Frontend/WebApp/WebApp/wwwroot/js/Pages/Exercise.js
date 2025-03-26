//Definimos la clase
function ExerciseViewController() {

    //Metodo constructor de la vistas
    this.InitView = function() {
        this.LoadEquipment();
        this.LoadExercises();

        $("#btnCreateExercise").click(function() {
            var vc = new ExerciseViewController();
            vc.Create();
        });

        $("#btnUpdateExercise").click(function() {
            var vc = new ExerciseViewController();
            vc.Update();
        });

        $("#btnDeleteExercise").click(function() {
            var vc = new ExerciseViewController();
            vc.Delete();
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

        $("#cbExerciseType").change(function() {
            var value = this.value;
            let time = document.querySelector(".time");
            let weight = document.querySelector(".weight");
            let sets = document.querySelector(".sets");
            let reps = document.querySelector(".reps");

            if (value === "Peso") {
                weight.classList.remove("d-none");
                sets.classList.remove("d-none");
                reps.classList.remove("d-none");

                time.classList.add("d-none");
            } else if (value === "Tiempo") {
                time.classList.remove("d-none");
                weight.classList.add("d-none");
                sets.classList.add("d-none");
                reps.classList.add("d-none");
            } else if (value === "AMRAP") {
                time.classList.remove("d-none");

                weight.classList.add("d-none");
                reps.classList.add("d-none");
                sets.classList.add("d-none");
            }
        });
    };

    this.Create = function() {
        var exercise = {};
        exercise.name = $("#exerciseName").val();
        exercise.type = $("#cbExerciseType").val();
        exercise.equipmentId = $("#sltEquipment").val();
        exercise.duration = $("#iTime").val();
        exercise.sets = $("#iSets").val();
        exercise.reps = $("#iReps").val();
        exercise.weight = $("#iWeight").val();
        if (exercise.duration === null || exercise.duration === "") {
            exercise.duration = 0;
        }
        if (exercise.sets === null || exercise.sets === "") {
            exercise.sets = 0;
        }
        if (exercise.reps === null || exercise.reps === "") {
            exercise.reps = 0;
        }
        if (exercise.weight === null || exercise.weight === "") {
            exercise.weight = 0;
        }

        //Valores default
        exercise.id = 0;

        if (exercise.name == "" || exercise.type == "" || exercise.equipmentId == "") {
            Swal.fire({
                icon: "error",
                title: "Faltan campos requeridos",
                text: "Por favor, complete todos los campos requeridos.",
                footer: "SILUETA CLUB FITNESS"
            });
        } else {
            //Invocar al API
            var ca = new ControlActions();

            ca.PostToAPI("Exercise/Create",
                exercise,
                function() {
                    console.log("Exercise created");
                    $("#tblExercises").DataTable().ajax.reload();
                    resetFormValues();
                });
        }
    };

    this.Update = function() {
        var exercise = {};
        exercise.id = $("#exerciseId").val();
        exercise.name = $("#exerciseName").val();
        exercise.type = $("#cbExerciseType").val();
        exercise.equipmentId = $("#sltEquipment").val();
        exercise.duration = $("#iTime").val();
        exercise.sets = $("#iSets").val();
        exercise.reps = $("#iReps").val();
        exercise.weight = $("#iWeight").val();
        if (exercise.duration === null || exercise.duration === "") {
            exercise.duration = 0;
        }
        if (exercise.sets === null || exercise.sets === "") {
            exercise.sets = 0;
        }
        if (exercise.reps === null || exercise.reps === "") {
            exercise.reps = 0;
        }
        if (exercise.weight === null || exercise.weight === "") {
            exercise.weight = 0;
        }

        //Invocar al API
        var ca = new ControlActions();

        ca.PutToAPI("Exercise/Update",
            exercise,
            function() {
                console.log("Exercise updated");
                $("#tblExercises").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    this.Delete = function() {
        var exercise = {};
        exercise.id = $("#exerciseId").val();
        exercise.name = $("#exerciseName").val();
        exercise.type = $("#cbExerciseType").val();
        exercise.equipmentId = $("#sltEquipment").val();
        exercise.duration = $("#iTime").val();
        exercise.sets = $("#iSets").val();
        exercise.reps = $("#iReps").val();
        exercise.weight = $("#iWeight").val();

        // Mostrar el SweetAlert de confirmación
        Swal.fire({
            title: "¿Estás seguro?",
            text: "¡No podrás deshacer esta acción!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then((result) => {
            if (result.isConfirmed) {

                // Invocar al API para eliminar el ejercicio
                var ca = new ControlActions();
                var endPointRoute = "Exercise/Delete";

                ca.DeleteToAPI(endPointRoute,
                    exercise,
                    () => {
                        Swal.fire(
                            "Eliminado",
                            "El ejercicio ha sido eliminado.",
                            "success"
                        );
                        $("#tblExercises").DataTable().ajax.reload();
                        resetFormValues();
                    });
            }
        });
    };

    this.LoadEquipment = function() {
        var ca = new ControlActions();

        // Usar GetToApi para obtener los datos de roles
        ca.GetToApi("Equipment/RetrieveAll",
            (response) => {
                var sltEquipment = document.getElementById("sltEquipment");
                sltEquipment.innerHTML = ""; // Limpiar el select antes de llenarlo

                //N/A option
                var NAoption = document.createElement("option");
                NAoption.value = "";
                NAoption.text = "Seleccione el equipo";
                sltEquipment.appendChild(NAoption);

                // Suponiendo que response es un array de roles
                response.forEach(equipment => {
                    var option = document.createElement("option");
                    option.value = equipment.id;
                    option.text = equipment.name;
                    sltEquipment.appendChild(option);
                });
            });
    };

    this.LoadExercises = function() {
        var ca = new ControlActions();

        //Construimos la ruta del API para consumir el servicio del Retrieve
        var urlService = ca.GetUrlApiService("Exercise/RetrieveAll");

        //Definir las columnas a extraer del json que deveulve el API
        var columns = [];
        columns[0] = { 'data': "id" };
        columns[1] = { 'data': "name" };
        columns[2] = { 'data': "type" };
        columns[3] = { 'data': "equipmentName" };
        columns[4] = { 'data': "sets" };
        columns[5] = { 'data': "reps" };
        columns[6] = { 'data': "weight" };
        columns[7] = { 'data': "duration" };

        // Verificar y destruir la instancia existente de DataTable
        if ($.fn.DataTable.isDataTable("#tblExercises")) {
            $("#tblExercises").DataTable().destroy();
        }

        //Inicializar la tabla, como un data table
        $("#tblExercises").dataTable({
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

                search: "Filtrar:"
            }
        });

        //Asignar al evento click de la fila de la tabla
        $("#tblExercises tbody").on("click",
            "tr",
            function() {

                //Seleccionar la fila a la que le dio click
                var row = $(this).closest("tr");

                //Extraemos la data de la tabla
                var exerciseDTO = $("#tblExercises").DataTable().row(row).data();

                //Mapeo de valores del DTO al formulario
                $("#exerciseId").val(exerciseDTO.id);
                $("#exerciseName").val(exerciseDTO.name);
                $("#cbExerciseType").val(exerciseDTO.type);
                $("#sltEquipment").val(exerciseDTO.equipmentId);
                $("#iTime").val(exerciseDTO.duration);
                $("#iSets").val(exerciseDTO.sets);
                $("#iReps").val(exerciseDTO.reps);
                $("#iWeight").val(exerciseDTO.weight);
            });
    };
}

let resetFormValues = () => {
    $("#exerciseId").val("");
    $("#exerciseName").val("");
    $("#cbExerciseType").val("");
    $("#sltEquipment").val("");
    $("#iTime").val("");
    $("#iSets").val("");
    $("#iReps").val("");
    $("#iWeight").val("");
};

//Instanciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Entrenador"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new ExerciseViewController();
        vc.InitView();
    }
});
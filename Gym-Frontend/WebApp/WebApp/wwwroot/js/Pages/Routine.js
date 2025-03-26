//Definimos la clase
function RoutineViewController() {

    //Metodo constructor de la vista
    this.InitView = function() {

        //Carga de los usuarios
        this.LoadUsers();
        this.LoadExercises();

        $("#btnCreate").click(function() {
            var vc = new RoutineViewController();
            vc.Create();
        });

        $("#sltExercises").change(function() {
            var vc = new RoutineViewController();
            vc.exerciseSelectFunction(this);
        });
    };

    this.Create = function() {
        var routine = {};
        routine.clientId = $("#sltUsers").val();
        routine.name = $("#routineName").val();

        //Valores default
        routine.id = 0;

        //Ejercicios 
        var selectedExercises = document.getElementById("selectedExercises");
        var exerciseIds = Array.from(selectedExercises.querySelectorAll(".exercise-item"))
            .map(item => item.querySelector("button").getAttribute("onclick").match(/\d+/)[0]);

        var dataToSend = {
            routine: routine,
            exerciseIds: exerciseIds
        };

        //Invocar al API
        var ca = new ControlActions();

        ca.PostToAPI("Routine/Create",
            dataToSend,
            function() {
                console.log("Routine created");

                Swal.fire({
                        icon: "success",
                        title: "¡Éxito!",
                        text: "Se ha creado una nueva rutina para el usuario.",
                        footer: "UCenfotec"
                    })
                    .then(() => { window.location.reload(); });
            });
    };

    this.LoadUsers = function() {
        var ca = new ControlActions();

        // Usar GetToApi para obtener los datos de roles
        ca.GetToApi("User/RetrieveByRole?id=4",
            (response) => {
                var sltUsers = document.getElementById("sltUsers");
                sltUsers.innerHTML = ""; // Limpiar el select antes de llenarlo

                // Crear y agregar el marcador de posición
                var placeholderOption = document.createElement("option");
                placeholderOption.value = "";
                placeholderOption.disabled = true;
                placeholderOption.selected = true;
                placeholderOption.textContent = "Seleccione un usuario";
                sltUsers.appendChild(placeholderOption);

                // Suponiendo que response es un array de roles
                response.forEach(user => {
                    var option = document.createElement("option");
                    option.value = user.id;
                    option.text = user.name + " " + user.lastName;
                    sltUsers.appendChild(option);
                });
            });
    };

    this.LoadExercises = function() {
        var ca = new ControlActions();

        ca.GetToApi("Exercise/RetrieveAll",
            (response) => {
                var sltExercise = document.getElementById("sltExercises");
                sltExercise.innerHTML = ""; // Limpiar el select antes de llenarlo

                // Crear y agregar el marcador de posición
                var placeholderOption = document.createElement("option");
                placeholderOption.value = "";
                placeholderOption.disabled = true;
                placeholderOption.selected = true;
                placeholderOption.textContent = "Seleccione un ejercicio";
                sltExercise.appendChild(placeholderOption);

                // Suponiendo que response es un array de roles
                response.forEach(exercise => {
                    var option = document.createElement("option");
                    option.value = exercise.id;
                    option.text = exercise.name;
                    sltExercise.appendChild(option);
                });
            });
    };

    this.exerciseSelectFunction = function(selectElement) {
        var selectedId = selectElement.value;
        var selectedText = selectElement.options[selectElement.selectedIndex].text;

        if (selectedId) {
            addExerciseLabel(selectedId, selectedText);
            // Limpiar selección
            this.value = "";
        }
    };
}

let addExerciseLabel = (id, name) => {
    var selectedExercises = document.getElementById("selectedExercises");
    var sltExercises = document.getElementById("sltExercises");

    // Verificar si el ejercicio ya está agregado
    var existingExercise = document.querySelector(`#selectedExercises .exercise-item[data-id="${id}"]`);
    if (existingExercise) {
        return; // Salir de la función si el ejercicio ya está agregado
    }

    // Crear el label y el botón de remover
    var container = document.createElement("div");
    container.classList.add("exercise-item");
    container.dataset.id = id; // Asignar el id al data-id para la verificación
    container.innerHTML = `
        <span>${name}</span>
        <button type="button" class="btn btn-danger btn-sm ms-2" onclick="removeExercise(this, ${id})">Remover</button>
    `;

    selectedExercises.appendChild(container);

    // Restablecer el select al primer option
    sltExercises.value = ""; // Establece el valor a vacío para seleccionar el primer option
    $(sltExercises).trigger("change"); // Actualiza el select2
};

function removeExercise(button, id) {
    var selectedExercises = document.getElementById("selectedExercises");
    selectedExercises.removeChild(button.parentElement);
}


//Instanciamiento de la clase
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Entrenador", "Cliente"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new RoutineViewController();
        vc.InitView();
    }
});
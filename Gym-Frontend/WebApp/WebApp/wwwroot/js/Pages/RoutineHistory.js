//Definimos la clase
function RoutineHistoryViewController() {

    //Metodo constructor de la vista
    this.InitView = function() {
        this.LoadRoutines();
    };

    this.LoadRoutines = function() {
        var ca = new ControlActions();

        var currentUserId = sessionStorage.getItem("UserId");

        // Usar GetToApi para obtener los datos de roles
        ca.GetToApi(`Routine/RetrieveByUser?clientId=${currentUserId}`,
            (response) => {

                var routineList = $("#routineList");
                routineList.empty(); // Limpiar la lista antes de llenarla

                if (response.length === 0) {
                    // Mostrar mensaje si no hay rutinas
                    routineList.append('<li class="list-group-item">Actualmente no tienes rutinas</li>');
                } else {
                    // Llenar la lista con rutinas
                    response.forEach(routine => {
                        routineList.append(`
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">${routine.name}</h4>
                            <a href="#" class="btn btn-primary" data-id="${routine.id}">Ver detalles</a>
                        </li>
                    `);
                    });

                    // Manejar el clic en un enlace de rutina
                    $(".btn-primary").click(function(e) {
                        e.preventDefault(); // Evitar el comportamiento por defecto del enlace
                        var routineId = $(this).data("id"); // Obtener el ID de la rutina
                        sessionStorage.setItem("routineId", routineId); // Almacenar el ID en sessionStorage
                        window.location.href = "/RoutineDetails"; // Redirigir a la página sin el ID en la URL
                    });
                }
            });
    };
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
        var vc = new RoutineHistoryViewController();
        vc.InitView();
    }
});
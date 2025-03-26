document.addEventListener("DOMContentLoaded",
    function() {
        var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
        if (isAuthenticated !== "true") {
            window.location.href = "/Index";
        }

        var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]"); // Convertir JSON a lista de roles
        var userStatus = sessionStorage.getItem("Status");

        if (userStatus.toUpperCase() == "ACTIVE") {

            // Crear un objeto para mapear roles a las páginas
            const pages = {
                Admin: [
                    ".profile", ".routine", ".groupClass", ".measurements", ".payments", ".exercise", ".equipment",
                    ".userManagement", ".discount", ".routineAssigment"
                ],
                Recepcionista: [".profile", ".groupClass", ".measurements", ".payments", ".userManagement"],
                Entrenador: [".profile", ".routine", ".groupClass", ".measurements", ".exercise", ".routineAssigment"],
                Cliente: [".profile", ".routine", ".groupClass", ".payments", ".personalWorkout", ".personalProgress"]
            };

            // Función para mostrar elementos basados en roles
            function showPagesForRoles(roles) {

                // Mostrar las páginas basadas en los roles
                roles.forEach(role => {
                    if (pages[role]) {
                        pages[role].forEach(selector => {
                            document.querySelector(selector)?.classList.remove("d-none");
                        });
                    }
                });
            }

            showPagesForRoles(userRoles.map(role => role.name));
        } else {
            document.querySelector(".profile")?.classList.remove("d-none");
            document.querySelector(".payments")?.classList.remove("d-none");
        }
    });
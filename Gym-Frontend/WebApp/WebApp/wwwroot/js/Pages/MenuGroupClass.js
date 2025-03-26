document.addEventListener("DOMContentLoaded",
    function() {
        var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
        if (isAuthenticated !== "true") {
            window.location.href = "/Index";
        }

        var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]"); // Convertir JSON a lista de roles

        // Crear un objeto para mapear roles a las páginas
        const pages = {
            Admin: [".createGroupClass"],
            Recepcionista: [".createGroupClass"],
            Entrenador: [".groupClassTrainer"],
            Cliente: [".registerGroupClass"]
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
    });
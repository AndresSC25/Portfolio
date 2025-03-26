document.addEventListener("DOMContentLoaded",
    function() {
        var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
        if (isAuthenticated !== "true") {
            window.location.href = "/Index";
        }

        var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]"); // Convertir JSON a lista de roles

        // Crear un objeto para mapear roles a las páginas
        const pages = {
            Admin: [".makePayment", ".discountManager", ".adminInvoices", ".membershipManager"],
            Recepcionista: [".makePayment", ".adminInvoices"],
            Entrenador: [""],
            Cliente: [".paymentsClient", ".makePaymentClient"]
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
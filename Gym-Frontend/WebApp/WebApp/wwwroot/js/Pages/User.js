function UserViewController() {
    this.ViewName = "Users";
    this.ApiBaseEndPoint = "User";
    this.currentUser = {}; // Store the selected user

    // Method to initialize the view
    this.InitView = function() {
        // Bind click events to buttons
        $("#btnCreate").click(() => this.Create());
        $("#btnUpdate").click(() => this.Update());
        $("#btnDelete").click(() => this.Delete());

        // Load the table data
        this.LoadTable();
    };

    // Method to create a user
    this.Create = function() {
        var user = {
            name: $("#txtName").val(),
            lastName: $("#txtLastName").val(),
            phone: $("#txtPhone").val(),
            email: $("#txtEmail").val(),
            status: $("#cbStatus").val(),
            gender: $("#cbGender").val(),
            birthDate: $("#txtBirthDate").val(),
            listaRole: $("#selectRoles").val().map(roleId => ({ id: roleId, name: "" })),
            id: 0 // Default value for new user
        };

        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Create";

        ca.PostToAPI(endPointRoute,
            user,
            () => {
                console.log("User created");
                $("#tblUsers").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    // Method to update a user
    this.Update = function() {
        if (!this.currentUser.id) {
            console.error("No user selected for update.");
            return;
        }

        var user = {
            id: this.currentUser.id, // Use the selected user ID
            name: $("#txtName").val(),
            lastName: $("#txtLastName").val(),
            phone: $("#txtPhone").val(),
            email: $("#txtEmail").val(),
            status: $("#cbStatus").val(),
            gender: $("#cbGender").val(),
            birthDate: $("#txtBirthDate").val(),
            lastLogin: $("#txtLastLogin").val(),
            created: $("#txtCreated").val(),
            //listaRole: this.currentUser.listaRole // Use the selected user's roles
        };

        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Update";

        ca.PutToAPI(endPointRoute,
            user,
            () => {
                console.log("User updated");
                $("#tblUsers").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    // Method to delete a user
    this.Delete = function() {
        var userId = $("#txtId").val();

        var ca = new ControlActions();
        var endPointRoute = this.ApiBaseEndPoint + "/Delete/" + userId;

        ca.DeleteToAPI(endPointRoute,
            null,
            () => {
                Swal.fire("Eliminado", "El ejercicio ha sido eliminado.", "success");
                console.log("User deleted");
                $("#tblUsers").DataTable().ajax.reload();
                resetFormValues();
            });
    };

    // Method to load the table
    this.LoadTable = function() {
        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService(this.ApiBaseEndPoint + "/RetrieveAll");

        var columns = [
            { 'data': "id" },
            { 'data': "name" },
            { 'data': "lastName" },
            { 'data': "phone" },
            { 'data': "email" },
            { 'data': "lastLogin", 'render': (data) => formatDate(data) },
            {
                'data': "status",
                'render': function(data) {
                    switch (data.toUpperCase()) {
                    case "ACTIVE":
                        return "Activo";
                    case "DISABLED":
                        return "Deshabilitado";
                    case "DEFAULT":
                        return "Pendiente";
                    case "EXPIRED":
                        return "Expirado";
                    default:
                        return data;
                    }
                }


            },
            {
                'data': "gender",
                'render': function(data) {
                    switch (data) {
                    case "M":
                        return "Masculino";
                    case "F":
                        return "Femenino";
                    case "O":
                        return "Otro";
                    default:
                        return data;
                    }
                }
            },
            { 'data': "birthDate", 'render': (data) => formatDate(data) },
            { 'data': "created", 'render': (data) => formatDate(data) }
        ];

        $("#tblUsers").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            columnDefs: [
                {
                    target: 0,
                    visible: false,
                    searchable: true
                }
            ],
            language: {
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
        });

        // Event handler for row selection
        $("#tblUsers tbody").on("click",
            "tr",
            (event) => {
                var row = $(event.currentTarget).closest("tr");
                var userDTO = $("#tblUsers").DataTable().row(row).data();

                // Store the selected user
                this.currentUser = userDTO;

                // Populate the form with selected user data
                $("#txtId").val(userDTO.id);
                $("#txtName").val(userDTO.name);
                $("#txtLastName").val(userDTO.lastName);
                $("#txtPhone").val(userDTO.phone);
                $("#txtEmail").val(userDTO.email);
                $("#cbGender").val(userDTO.gender);
                $("#txtBirthDate").val(new Date(userDTO.birthDate).toISOString().split("T")[0]);
                $("#txtLastLogin").val(new Date(userDTO.lastLogin).toISOString().split("T")[0]);
                $("#txtCreated").val(new Date(userDTO.created).toISOString().split("T")[0]);
                $("#selectRoles").val(userDTO.listaRole.map(role => role.id));
                $("#cbStatus").val(userDTO.status);
            });
    };
}

// Helper function to format dates
let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0");
    var day = date.getDate().toString().padStart(2, "0");
    return `${day}-${month}-${year}`;
};

let loadRoles = () => {
    var ca = new ControlActions();
    ca.GetToApi("Rol/RetrieveAll",
        (response) => {
            var select = document.getElementById("selectRoles");
            select.innerHTML = "";
            response.forEach(role => {
                var option = document.createElement("option");
                option.value = role.id;
                option.text = role.name;
                select.appendChild(option);
            });
        });
};

let resetFormValues = () => {
    $("#txtId").val("");
    $("#txtName").val("");
    $("#txtLastName").val("");
    $("#txtPhone").val("");
    $("#txtEmail").val("");
    $("#cbGender").val("");
    $("#txtBirthDate").val("");
    $("#txtLastLogin").val("");
    $("#txtCreated").val("");
    $("#selectRoles").val("");
    $("#cbStatus").val("");
};

// Initialize view when document is ready
$(document).ready(function() {
    var isAuthenticated = sessionStorage.getItem("IsAuthenticated");
    var userRoles = JSON.parse(sessionStorage.getItem("UserRoles") || "[]");
    const requiredRoles = ["Admin", "Recepcionista"];
    const hasRequiredRoles = userRoles.some(role => requiredRoles.includes(role.name));

    if (isAuthenticated !== "true" || !hasRequiredRoles) {
        window.location.href = "/Index";
    } else {
        var vc = new UserViewController();
        vc.InitView();
    }
});
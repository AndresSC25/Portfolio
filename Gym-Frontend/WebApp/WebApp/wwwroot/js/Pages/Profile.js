"use strict";

function ProfileViewController() {
    this.ViewName = "User";
    this.ApiBaseEndPoint = "User";
    this.isEditing = false;
    this.userData = {};

    this.InitView = function() {
        this.LoadUser();

        $("#btnAllowModify").click(() => {
            this.ToggleEdit();
        });

        $("#btnUpdate").click(() => {
            this.UpdateUser();
        });
    };

    this.LoadUser = function() {
        var ca = new ControlActions();
        var userId = sessionStorage.getItem("UserId");
        var urlService = "User/RetrieveById?id=" + userId;
        ca.GetToApi(urlService,
            (response) => {
                this.userData = response;
                $("#txtName").val(response.name + " " + response.lastName);
                $("#txtDateBirth").val(response.birthDate.split("T")[0]);
                $("#txtAge").val(response.age);
                $("#txtGender").val(response.gender === "M"
                    ? "Masculino"
                    : response.gender === "F"
                    ? "Femenino"
                    : "Otro");
                $("#txtEmail").val(response.email);
                $("#txtPhone").val(response.phone);

                // Guardar valores iniciales para verificar cambios
                this.initialEmail = response.email;
                this.initialPhone = response.phone;
            });
    };

    this.ToggleEdit = function() {
        if (this.isEditing) {
            this.CancelEdit();
        } else {
            this.StartEdit();
        }
    };

    this.StartEdit = function() {
        $("#txtEmail").prop("disabled", false);
        $("#txtPhone").prop("disabled", false);
        $("#btnAllowModify").text("Cancelar");
        $("#btnUpdate").show();
        this.isEditing = true;
    };

    this.CancelEdit = function() {
        $("#txtEmail").prop("disabled", true);
        $("#txtPhone").prop("disabled", true);
        $("#btnAllowModify").text("Modificar");
        $("#btnUpdate").hide();

        // Solo restablecer los valores si se han realizado cambios
        if ($("#txtEmail").val() !== this.initialEmail || $("#txtPhone").val() !== this.initialPhone) {
            $("#txtEmail").val(this.initialEmail);
            $("#txtPhone").val(this.initialPhone);
        }

        this.isEditing = false;
    };

    this.UpdateUser = function() {
        var email = $("#txtEmail").val();
        var phone = $("#txtPhone").val();

        // Verificar si se han realizado cambios
        if (email !== this.initialEmail || phone !== this.initialPhone) {
            var user = {
                id: this.userData.id,
                name: this.userData.name,
                lastName: this.userData.lastName,
                status: this.userData.status,
                gender: this.userData.gender,
                email: email,
                phone: phone,
                lastLogin: this.userData.lastLogin,
                birthDate: this.userData.birthDate,
                listaRole: this.userData.listaRole
            };

            var ca = new ControlActions();
            var urlService = "User/Update";

            ca.PutToAPI(urlService,
                user,
                () => {
                    console.log("User updated");
                    this.LoadUser();
                    this.CancelEdit();
                });
        } else {
            this.CancelEdit(); // Si no hay cambios, solo cancelar la edición
        }
    };
}

$(document).ready(function() {
    var vc = new ProfileViewController();
    vc.InitView();
});
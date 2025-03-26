function ControlActions() {

    //Ruta base del API
    // Esto se cambia
    this.URL_API = "https://localhost:7038/api/";

    this.GetUrlApiService = function(service) {
        return this.URL_API + service;
    };


    $.put = function(url, data, callback, errorCallback) {
        if ($.isFunction(data)) {
            errorCallback = callback;
            callback = data;
            data = {};
        }
        return $.ajax({
            url: url,
            type: "PUT",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            success: callback,
            error: errorCallback
        });
    };

    $.delete = function(url, data, callback) {
        if ($.isFunction(data)) {
            type = type || callback,
                callback = data,
                data = {};
        }
        return $.ajax({
            url: url,
            type: "DELETE",
            success: callback,
            data: JSON.stringify(data),
            contentType: "application/json"
        });
    };

    this.PostToAPI = function(service, data, callBackFunction) {

        $.ajax({
            type: "POST",
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (callBackFunction) {
                    Swal.fire(
                        "Transacción Completada",
                        "Éxito en la Transacción",
                        "success"
                    );
                    callBackFunction(data);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {

                var responseJson = jqXHR.responseJSON;
                var message = jqXHR.responseText;

                if (responseJson) {
                    var errors = responseJson.errors;
                    var errorMessages = Object.values(errors).flat();
                    message = errorMessages.join("<br/> ");
                }
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: message,
                    footer: "SILUETA CLUB FITNESS"
                });
            }
        });
    };

    this.PostToAPIForOTP = function(service, data, callBackFunction) {
        $.ajax({
            type: "POST",
            url: this.GetUrlApiService(service),
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                if (callBackFunction) {
                    Swal.fire(
                        "Transacción Completada",
                        "Éxito en la Transacción",
                        "success"
                    );
                    callBackFunction(data);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                // Verificar si el error es 500
                if (jqXHR.status === 500) {
                    // Redireccionar a OTPVerification en caso de error 500
                    window.location.href = "OTPVerification";
                    console.error("Error 500: Ya existe un OTP válido para este usuario.");
                } else {
                    // Manejar otros tipos de errores
                    let errorMessage = "Ocurrió un error inesperado.";
                    try {
                        const responseJson = JSON.parse(jqXHR.responseText);
                        if (responseJson && responseJson.errors) {
                            errorMessage = Object.values(responseJson.errors).flat().join("<br/>");
                        }
                    } catch (error) {
                        // No se pudo parsear el JSON, usar el mensaje original
                        errorMessage = jqXHR.responseText;
                    }
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        html: errorMessage,
                        footer: "SILUETA CLUB FITNESS"
                    });
                }
            }
        });
    };


    this.PutToAPI = function(service, data, callBackFunction) {
        console.log("Data:", data);
        $.put(
            this.GetUrlApiService(service),
            data,
            function(response) {
                Swal.fire(
                    "Transacción Completada",
                    "Éxito en la Transacción",
                    "success"
                );
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            function(response) {
                let errorMessage = "Ha ocurrido un error desconocido.";

                if (response.responseText) {
                    try {
                        const responseJSON = JSON.parse(response.responseText);
                        errorMessage = responseJSON.message || response.responseText;
                    } catch (e) {
                        errorMessage = response.responseText;
                    }
                } else {
                    errorMessage = response.statusText || errorMessage;
                }

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: errorMessage,
                    footer: "SILUETA CLUB FITNESS"
                });
            }
        );
    };

    this.PutToAPIWithoutData = function(service, callBackFunction) {
        $.ajax({
            url: this.GetUrlApiService(service),
            type: "PUT",
            // No se envían datos en el cuerpo
            success: function(response) {
                Swal.fire({
                    icon: "success",
                    title: "¡Éxito!",
                    text: "La solicitud se ha realizado correctamente.",
                    footer: "SILUETA CLUB FITNESS"
                });
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: "Ocurrió un error al procesar la solicitud. Por favor, intenta de nuevo.",
                    footer: "SILUETA CLUB FITNESS"
                });
                if (callBackFunction) {
                    callBackFunction(null, errorThrown);
                }
            }
        });
    };


    this.DeleteToAPI = function(service, data, callBackFunction) {
        $.ajax({
            url: this.GetUrlApiService(service),
            type: "DELETE",
            // Only include data if necessary:
            data: data ? JSON.stringify(data) : null, // Check if data exists before stringify
            contentType: "application/json", // Set the correct Content-Type
            success: function(response) {
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                var message = "";
                var errorType = "Desconocido";

                if (jqXHR.status === 0) {
                    message = "No hay conexión a internet.";
                    errorType = "Red";
                } else if (jqXHR.status === 404) {
                    message = "El recurso que intenta eliminar no existe.";
                    errorType = "Recurso no encontrado";
                } else if (jqXHR.status === 415) {
                    message = "El tipo de contenido enviado no es compatible con la API.";
                    errorType = "Unsupported Media Type";
                } else if (jqXHR.status >= 500) {
                    message = "Ocurrió un error en el servidor.";
                    errorType = "Servidor";
                } else {
                    message = "Ocurrió un error inesperado al eliminar.";
                    if (jqXHR.responseJSON && jqXHR.responseJSON.errors) {
                        message = jqXHR.responseJSON.errors.join("<br/>");
                    }
                }

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: `<b>Error: ${errorType}</b><br/>` + message,
                    footer: "SILUETA CLUB FITNESS"
                });

                if (callBackFunction) {
                    callBackFunction(null, errorType);
                }
            }
        });
    };


    this.GetToApi = function(service, callBackFunction) {
        $.ajax({
            url: this.GetUrlApiService(service),
            type: "GET",
            success: function(response) {
                if (callBackFunction) {
                    callBackFunction(response);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                var message = jqXHR.responseText;
                var errorType = "Desconocido";

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: `<br/>` + message,
                    footer: "SILUETA CLUB FITNESS"
                });

                if (callBackFunction) {
                    callBackFunction(null, errorType);
                }
            }
        });
    };

    this.GetToApiModificado = function(service, callBackFunction) {
        $.ajax({
            url: this.GetUrlApiService(service),
            type: "GET",
            success: function(response) {
                if (callBackFunction) {
                    // Pasamos el response y null en errorType para indicar que no hubo error
                    callBackFunction(response, null);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                var message = jqXHR.responseText;
                var errorType = "Desconocido";

                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    html: `<br/>` + message,
                    footer: "SILUETA CLUB FITNESS"
                });

                if (callBackFunction) {
                    // Enviamos null como response y el errorType para indicar que hubo un error
                    callBackFunction(null, errorType);
                }
            }
        });
    };


}
let formatDate = (dateString) => {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, "0"); // Los meses son 0-indexed
    var day = date.getDate().toString().padStart(2, "0");
    return `${year}-${month}-${day}`;
};
var lstMeasures = [];

var labels = [];

var weightData = [];

var heightData = [];

var averageFatData = [];

let myChart;

function MeasureProgressViewController() {
    var ca = new ControlActions();

    //Metodo constructor de la vista
    this.InitView = function() {
        var userId = sessionStorage.getItem("UserId");


        var urlService = "Measures/RetrieveByUserId?id=" + userId;

        ca.GetToApi(urlService,
            (response) => {
                response.forEach((measureAPI) => {

                    labels.push(measureAPI.created.split("T")[0]);
                    weightData.push(measureAPI.weight);
                    heightData.push(measureAPI.height);
                    averageFatData.push(measureAPI.averageOfFat);
                });

            });

        this.LoadGraph();
        this.LoadTable();
        setTimeout(1000);
        this.reload();
        document.getElementById("resetZoom").addEventListener("click",
            function() {
                myChart.resetZoom();
            });

    };

    this.LoadTable = function() {
        var userId = sessionStorage.getItem("UserId");
        var ca = new ControlActions();
        var urlService = ca.GetUrlApiService("Measures/RetrieveByUserId?id=" + userId);
        var columns = [];

        columns[0] = { "data": "weight", "render": (data) => (data + " Kg") };
        columns[1] = { "data": "height", "render": (data) => (data + " cm") };
        columns[2] = { "data": "averageOfFat", "render": (data) => (data + "%") };
        columns[3] = { "data": "created", 'render': (data) => formatDate(data) };


        $("#tblMeasures").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            columns: columns,
            language: {
                info: "Mostrando página _PAGE_ de _PAGES_",
                infoEmpty: "No se encuentran registros",
                infoFiltered: "(Filtrado de _MAX_ registros totales)",
                lengthMenu: "Mostrar _MENU_ Registros por página",
                zeroRecords: "No se encuentran registros",
                search: "Filtrar:"
            }
        });
    };


    this.LoadGraph = function() {
        // Filtrar y formatear datos para el gráfico

        const data = {
            labels: labels, // Etiquetas de las fechas
            datasets: [
                {
                    label: "Peso (kg)",
                    data: weightData,
                    borderColor: "rgba(75, 192, 192, 1)",
                    backgroundColor: "rgba(75, 192, 192, 0.2)",
                    fill: true
                },
                {
                    label: "Altura (cm)",
                    data: heightData,
                    borderColor: "rgba(255, 159, 64, 1)",
                    backgroundColor: "rgba(255, 159, 64, 0.2)",
                    fill: true
                },
                {
                    label: "Porcentaje de Grasa Corporal (%)",
                    data: averageFatData,
                    borderColor: "rgba(153, 102, 255, 1)",
                    backgroundColor: "rgba(153, 102, 255, 0.2)",
                    fill: true
                }
            ]
        };

        const config = {
            type: "line",
            data: data,
            options: {
                responsive: true,
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: "Fecha"
                        },
                        ticks: {
                            padding: 10 // Añadir padding entre los ticks y el borde del canvas
                        },
                        grid: {
                            offset: true // Desplazar las líneas de la cuadrícula
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: "Medidas"
                        }
                    }
                },
                plugins: {
                    zoom: {
                        zoom: {
                            wheel: {
                                enabled: true
                            },
                            pinch: {
                                enabled: true
                            },
                            mode: "xy"
                        },
                        pan: {
                            enabled: true,
                            mode: "xy"
                        }
                    }
                }
            }
        };

        // Crear y renderizar el gráfico
        const ctx = document.getElementById("myChart").getContext("2d");
        myChart = new Chart(ctx, config);
    };

    this.reload = function() {
        myChart.update();
    };


};

//Instaciamiento de la clase
$(document).ready(function() {
    var vc = new MeasureProgressViewController();
    vc.InitView();

});
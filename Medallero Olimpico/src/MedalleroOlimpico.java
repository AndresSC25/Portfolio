import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintStream;

public class MedalleroOlimpico {
    static BufferedReader in;
    static PrintStream out;
    //Se crean las listas estaticas debido a que van a ser ocupadas globalmente.
    static String[] paises;
    static int[] oro;
    static int[] plata;
    static int[] bronce;
    static int[] puntajeTotal;
    static double[] porcbronce;
    static int[] puntajeTotalNeto;
    static int[] puntajeTotalOrdenado;
    static String[] paisesOrdenado;

    static {
        //Se cambia el sout para no tener que poner el sout completo es cuestion de gustos.
        //Se puede cambiar el InputStreamReader por un scanner.
        in = new BufferedReader(new InputStreamReader(System.in));
        out = System.out;
    }

    public static void main(String[] args) {
        generarMenuPrincipal();
    }

    static void generarMenuPrincipal() {
        int opcionMenu;
        boolean[] controlOpciones = new boolean[8];
        do {
            imprimirMenuPrincipal();
            out.print("Digite la opción del menú: ");
            opcionMenu = leerRangosEnteros();
            switch (opcionMenu) {
                case 1:
                    controlOpciones[1] = true;
                    configPrincipal();
                    break;
                case 2:
                    if (controlOpciones[1]) {
                        controlOpciones[2] = true;
                        inicializarDatos();
                    } else {
                        out.println("Debe ingresar a la opción 1 antes de esta");
                    }
                    break;
                case 3:
                    if (controlOpciones[1] && controlOpciones[2]) {
                        controlOpciones[3] = true;
                        calculoPuntajeTotal();
                        menuPuntajeTotal();
                        break;
                    }

                    out.println("Debe ingresar a la opción 1 o 2 antes de esta");
                    break;
                case 4:
                    if (controlOpciones[1] && controlOpciones[2] && controlOpciones[3]) {
                        controlOpciones[4] = true;
                        puntajeMayor();
                        break;
                    }

                    out.println("Debe ingresar a la opción 1, 2 o 3 antes de esta");
                    break;
                case 5:
                    if (controlOpciones[1] && controlOpciones[2] && controlOpciones[3] && controlOpciones[4]) {
                        controlOpciones[5] = true;
                        porcentajeBronce();
                        break;
                    }

                    out.println("Debe ingresar a la opción 1, 2, 3 o 4 antes de esta");
                    break;
                case 6:
                    if (controlOpciones[1] && controlOpciones[2] && controlOpciones[3] && controlOpciones[4] && controlOpciones[5]) {
                        controlOpciones[6] = true;
                        medalleroInformativo();
                        break;
                    }

                    out.println("Debe ingresar a la opción 1, 2, 3, 4 o 5 antes de esta");
                    break;
                case 7:
                    if (controlOpciones[1] && controlOpciones[2] && controlOpciones[3] && controlOpciones[4] && controlOpciones[5] && controlOpciones[6]) {
                        controlOpciones[7] = true;
                        ordernarBurbuja();
                        menuPuntajeTotalOrdenado();
                        break;
                    }

                    out.println("Debe ingresar a la opción 1, 2, 3, 4, 5 o 6 antes de esta");
                    break;
                case 8:
                    out.println("¡HASTA LUEGO!");
                    break;
                default:
                    out.println("Digito una opción inválida [" + opcionMenu + "]");
            }
        } while (opcionMenu != 8);

    }

    static void imprimirMenuPrincipal() {
        out.println("Menu Principal");
        out.printf("%2d. %-30s%n", 1, "Ingreso de Numero de Paises a Participar");
        out.printf("%2d. %-30s%n", 2, "Registrar Informacion del Medallero");
        out.printf("%2d. %-30s%n", 3, "Imprimir Puntaje Total");
        out.printf("%2d. %-30s%n", 4, "Imprimir Pais con Mayor Puntaje");
        out.printf("%2d. %-30s%n", 5, "Imprimir Promedio de Medallas de bronce");
        out.printf("%2d. %-30s%n", 6, "Imprimir Medallero informativo");
        out.printf("%2d. %-30s%n", 7, "Imprimir Tabla de posiciones Ordenada");
        out.printf("%2d. %-30s%n", 8, "Salir");
    }

    static void configPrincipal() {
        out.println("Bienvenido al sistema");
        out.println("Por favor digite los siguientes datos:");
        out.print("Cantidad de paises: ");
        int cantidadPaises = leerEntero();
        paises = new String[cantidadPaises];
        oro = new int[cantidadPaises];
        plata = new int[cantidadPaises];
        bronce = new int[cantidadPaises];
        puntajeTotal = new int[cantidadPaises];
        porcbronce = new double[cantidadPaises];
        puntajeTotalNeto = new int[cantidadPaises];
        puntajeTotalOrdenado = new int[cantidadPaises];
        paisesOrdenado = new String[cantidadPaises];
    }

    static void inicializarDatos() {
        int numeroi = 0;

        int i;
        for (i = 0; i < paises.length; ++i) {
            ++numeroi;
            out.println("Digite la el nombre del pais " + numeroi + " :");
            paises[i] = leerTexto();
        }

        String var10001;
        for (i = 0; i < oro.length; ++i) {
            ++numeroi;
            var10001 = paises[i];
            out.println("Digite la cantidad de medallas oro de " + var10001 + " :");
            oro[i] = leerEntero();
        }

        for (i = 0; i < plata.length; ++i) {
            ++numeroi;
            var10001 = paises[i];
            out.println("Digite la cantidad de medallas plata de " + var10001 + " :");
            plata[i] = leerEntero();
        }

        for (i = 0; i < bronce.length; ++i) {
            ++numeroi;
            var10001 = paises[i];
            out.println("Digite la cantidad de medallas bronce de " + var10001 + " :");
            bronce[i] = leerEntero();
        }

    }

    static void calculoPuntajeTotal() {
        for (int i = 0; i < puntajeTotal.length; ++i) {
            puntajeTotal[i] = oro[i] * 3 + plata[i] * 2 + bronce[i];
            puntajeTotalNeto[i] = oro[i] + plata[i] + bronce[i];
        }

    }

    static void menuPuntajeTotal() {
        out.println("Juegos Olímpicos de Tokio 2020");
        out.println();
        out.println("Tabla de puntajes");
        out.printf("%-30s %11s%n", "País", "Puntaje total");

        for (int i = 0; i < paises.length; ++i) {
            out.printf("%-30s %11d%n", paises[i], puntajeTotal[i]);
        }

    }

    static void puntajeMayor() {
        int puntajeM = 0;
        String paisganador = "s";

        for (int i = 0; i < puntajeTotal.length; ++i) {
            if (puntajeTotal[i] > puntajeM) {
                puntajeM = puntajeTotal[i];
                paisganador = paises[i];
            }
        }

        out.println("El pais con mayor puntaje es: " + paisganador);
    }

    static void porcentajeBronce() {
        double porc = 0.0;

        for (int i = 0; i < puntajeTotal.length; ++i) {
            porc = (double) puntajeTotalNeto[i] / bronce[i];
            porcbronce[i] = porc;
            String var10001 = paises[i];
            out.println("El porcentaje de medallas de bronce de " + var10001 + " es de " + porcbronce[i]);
        }

    }

    static void medalleroInformativo() {
        out.println("Juegos Olímpicos de Tokio 2020");
        out.println();
        out.println("Medallero Informativo");
        out.printf("%-10s %11s %12s %13s%n", "País", "Medallas de oro", "Medallas plata", "Medallas bronce");

        for (int i = 0; i < paises.length; ++i) {
            out.printf("%-14s %-18d %-15d %-16d%n", paises[i], oro[i], plata[i], bronce[i]);
        }

    }

    static int leerRangosEnteros() {
        int resultado;
        do {
            resultado = leerEntero();
            if (resultado < 0 || resultado > 8) {
                out.printf("Valor fuera del rango válido [%d - %d]%n", 0, 8);
                out.print("Digite otro valor: ");
            }
        } while (0 > resultado || resultado > 8);

        return resultado;
    }

    static double leerRangosDobles(double inferior, double superior) {
        double resultado = 0.0;

        do {
            resultado = leerDoble();
            if (resultado < inferior || resultado > superior) {
                out.printf("Valor fuera del rango válido [%d - %d]%n", inferior, superior);
                out.print("Digite otro valor: ");
            }
        } while (!(inferior <= resultado) || !(resultado <= superior));

        return resultado;
    }

    static void ordernarBurbuja() {
        int i;
        for (i = 0; i < puntajeTotalOrdenado.length; ++i) {
            puntajeTotalOrdenado[i] = puntajeTotal[i];
            paisesOrdenado[i] = paises[i];
        }

        for (i = puntajeTotalOrdenado.length - 2; i >= 0; --i) {
            for (int j = 0; j <= i; ++j) {
                if (puntajeTotalOrdenado[j] > puntajeTotalOrdenado[j + 1]) {
                    int temp0 = puntajeTotalOrdenado[j + 1];
                    String temp1 = paisesOrdenado[j + 1];
                    puntajeTotalOrdenado[j + 1] = puntajeTotalOrdenado[j];
                    paisesOrdenado[j + 1] = paisesOrdenado[j];
                    puntajeTotalOrdenado[j] = temp0;
                    paisesOrdenado[j] = temp1;
                }
            }
        }

    }

    static void menuPuntajeTotalOrdenado() {
        out.println("Juegos Olímpicos de Tokio 2020");
        out.println();
        out.println("Tabla de puntajes");
        out.printf("%-30s %11s%n", "País", "Puntaje total");

        for (int i = paises.length - 1; i >= 0; --i) {
            out.printf("%-30s %11d%n", paisesOrdenado[i], puntajeTotalOrdenado[i]);
        }

    }

    static String leerTexto() {
        String resultado = null;

        try {
            resultado = in.readLine();
        } catch (Exception _) {
        }

        return resultado;
    }

    static int leerEntero() {
        int resultado = 0;
        boolean bandera = false;

        do {
            try {
                resultado = Integer.parseInt(in.readLine());
                bandera = false;
            } catch (Exception var3) {
                bandera = true;
                out.println("Usted no digito un entero, intente de nuevo por favor!");
            }
        } while (bandera);

        return resultado;
    }

    static double leerDoble() {
        double resultado = 0.0;
        boolean bandera = false;

        do {
            try {
                resultado = Double.parseDouble(in.readLine());
                bandera = false;
            } catch (Exception var4) {
                bandera = true;
                out.println("Usted no digito un doble, intente de nuevo por favor!");
            }
        } while (bandera);

        return resultado;
    }
}
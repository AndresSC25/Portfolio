import dataClasses.PersonQueue;
import entities.Gestor;
import entities.Persona;

import java.util.Scanner;

public class Main {
    static Gestor objGestor = new Gestor();
    static Scanner scanner = new Scanner(System.in);
    static PersonQueue personQueue = new PersonQueue();

    public static void main(String[] args) throws Exception {
        generarMenuPrincipal();
    }

    static void imprimirMenu() {
        System.out.println("Bienvenido el Menu de la Universidad.");
        System.out.println("1.[Agregar Cursos]");
        System.out.println("2.[Agregar entities.Estudiante o entities.Profesor]");
        System.out.println("3.[Agregar Cursos a entities.Estudiante o entities.Profesor]");
        System.out.println("4.[Buscar entities.Estudiante o entities.Profesor]");
        System.out.println("5.[Atender entities.Persona en Caja]");
        System.out.println("6.[Salir]");
    }

    static void generarMenuPrincipal() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenu();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 6:
                    System.out.println("¡Hasta Luego!");
                    break;
                case 1:
                    generarCursos();
                    break;
                case 2:
                    generarMenuPersona();
                    break;
                case 3:
                    generarMenuAgregarCursos();
                    break;
                case 4:
                    generarMenuBusqueda();
                    break;
                case 5:
                    generarMenuCaja();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 6);
    }

    static void imprimirMenuAgregarPersona() {
        System.out.println("Seleccione una opcion: ");
        System.out.println("1.[Agregar entities.Estudiante]");
        System.out.println("2.[Agregar entities.Profesor]");
        System.out.println("3.[Salir]");
    }

    static void imprimirMenuAgregarCursosPersona() {
        System.out.println("Seleccione una opcion: ");
        System.out.println("1.[Agregar entities.Curso a entities.Estudiante]");
        System.out.println("2.[Agregar entities.Curso a entities.Profesor]");
        System.out.println("3.[Salir]");
    }

    static void imprimirMenuBusqueda() {
        System.out.println("Seleccione una opcion: ");
        System.out.println("1.[Busqueda por ID]");
        System.out.println("2.[Busqueda por Nombre]");
        System.out.println("3.[Salir]");
    }

    static void imprimirMenuBusquedaID() throws Exception {
        System.out.println("Seleccione una opcion: ");
        System.out.println("1.[Busqueda por ID entities.Estudiante]");
        System.out.println("2.[Busqueda por ID entities.Profesor]");
        System.out.println("3.[Salir]");
    }

    static void imprimirMenuBusquedaNombre() throws Exception {
        System.out.println("Seleccione una opcion: ");
        System.out.println("1.[Busqueda por Nombre entities.Estudiante]");
        System.out.println("2.[Busqueda por Nombre entities.Profesor]");
        System.out.println("3.[Salir]");
    }

    static void imprimirMenuCajas() {
        System.out.println("1.[Ingresar cliente a la cola]");
        System.out.println("2.[Atender Cliente]");
        System.out.println("3.[Salir]");

    }

    static void generarMenuPersona() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenuAgregarPersona();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    break;
                case 1:
                    generarEstudiantes();
                    break;
                case 2:
                    generarProfesor();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarMenuAgregarCursos() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenuAgregarCursosPersona();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    break;
                case 1:
                    ingresoCursosEstudiante();
                    break;
                case 2:
                    ingresoCursoProfesor();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarMenuBusqueda() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenuBusqueda();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    break;
                case 1:
                    generarMenuBusquedaID();
                    break;
                case 2:
                    generarMenuBusquedaNombre();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarMenuBusquedaID() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenuBusquedaID();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    break;
                case 1:
                    busquedaIDEstudiante();
                    break;
                case 2:
                    busquedaIDProfesor();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarMenuBusquedaNombre() throws Exception {
        int opcionMenu = 0;
        do {
            imprimirMenuBusquedaNombre();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    break;
                case 1:
                    busquedaNombreEstudiante();
                    break;
                case 2:
                    busquedaNombreProfesor();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarMenuCaja() {
        int opcionMenu = 0;
        do {
            imprimirMenuCajas();
            System.out.println("Digite una opción del menú: ");
            opcionMenu = scanner.nextInt();
            switch (opcionMenu) {
                case 3:
                    System.out.println("¡Hasta Luego!");
                    break;
                case 1:
                    agregarPersona();
                    break;
                case 2:
                    atenderCliente();
                    break;
                default:
                    System.out.println("Digito una opción inválida [" + opcionMenu + "]");
                    break;
            }
        } while (opcionMenu != 3);
    }

    static void generarCursos() throws Exception {
        try {
            System.out.println("Bienvenido al ingreso de Cursos");
            System.out.println("Ingrese lo que se le solicite");
            boolean idValido = true;
            String id;
            do {
                System.out.println("Ingrese el id del curso: ");
                id = scanner.next();
                scanner.nextLine();
                boolean existe = objGestor.existeCurso(id);
                if (existe) {
                    System.out.println("El id que ingreso ya existe, intente otra vez");
                } else {
                    idValido = false;
                }
            } while (idValido);
            System.out.println("Ingrese el numero de grupo del curso: ");
            String numeroGrupo = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese la descripcion del curso: ");
            String descripcion = scanner.nextLine();

            objGestor.agregarCurso(id, numeroGrupo, descripcion);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema al registrar un curso");
        }
    }

    static void generarEstudiantes() throws Exception {
        try {
            System.out.println("Bienvenido al ingreso de Estudiantes");
            System.out.println("Ingrese lo que se le solicite");
            boolean idValido = true;
            String id;
            do {
                System.out.println("Ingrese el id del estudiante: ");
                id = scanner.next();
                scanner.nextLine();
                boolean existe = objGestor.existeEstudiante(id);
                if (existe) {
                    System.out.println("El id que ingreso ya existe, intente otra vez");
                } else {
                    idValido = false;
                }

            } while (idValido);
            System.out.println("Ingrese el nombre del estudiante: ");
            String nombre = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el apellido 1 del estudiante: ");
            String apellido1 = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el apellido 2 del estudiante: ");
            String apellido2 = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el id del curso por matricular: ");
            String idCurso = scanner.next();
            scanner.nextLine();
            objGestor.agregarEstudiante(id, nombre, apellido1, apellido2, idCurso);

        } catch (Exception e) {
            throw new Exception("Ha habido un problema al registrar estudiante" + e.getMessage());
        }

    }

    static void generarProfesor() throws Exception {
        try {
            System.out.println("Bienvenido al ingreso de Profesores");
            System.out.println("Ingrese lo que se le solicite");
            boolean idValido = true;
            String id;
            do {
                System.out.println("Ingrese el id del profesor: ");
                id = scanner.next();
                scanner.nextLine();
                boolean existe = objGestor.existeProfesor(id);
                if (existe) {
                    System.out.println("El id que ingreso ya existe, intente otra vez");
                } else {
                    idValido = false;
                }

            } while (idValido);
            System.out.println("Ingrese el nombre del profesor: ");
            String nombre = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el apellido 1 del profesor: ");
            String apellido1 = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el apellido 2 del profesor: ");
            String apellido2 = scanner.next();
            scanner.nextLine();
            System.out.println("Ingrese el id del curso por profesor: ");
            String idCurso = scanner.next();
            scanner.nextLine();
            objGestor.agregarProfesor(id, nombre, apellido1, apellido2, idCurso);

        } catch (Exception e) {
            throw new Exception("Ha habido un problema al registrar profesor" + e.getMessage());
        }

    }

    static void ingresoCursosEstudiante() throws Exception {
        try {
            System.out.println("Bienvenido al ingreso de Cursos a Estudiantes");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el ID del entities.Estudiante: ");
            String id = scanner.next();
            System.out.println("Ingrese el ID del entities.Curso: ");
            String idCurso = scanner.next();
            objGestor.agregarCursoEstudiante(id, idCurso);

        } catch (Exception e) {
            throw new Exception("Ha habido un problema al matricular el curso" + e.getMessage());
        }


    }

    static void ingresoCursoProfesor() throws Exception {
        try {
            System.out.println("Bienvenido al ingreso de Cursos a Profesores");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el ID del entities.Profesor: ");
            String id = scanner.next();
            System.out.println("Ingrese el ID del entities.Curso: ");
            String idCurso = scanner.next();
            objGestor.agregarCursoProfesor(id, idCurso);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema al asignar el curso" + e.getMessage());
        }


    }

    static void busquedaIDEstudiante() throws Exception {
        try {
            System.out.println("Bienvenido a la busqueda de entities.Estudiante por ID");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el ID del entities.Estudiante: ");
            String id = scanner.next();
            objGestor.busquedaIDEstudiante(id);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema en la busqueda del estudiante" + e.getMessage());
        }
    }

    static void busquedaIDProfesor() throws Exception {
        try {
            System.out.println("Bienvenido a la busqueda de entities.Profesor por ID");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el ID del entities.Estudiante: ");
            String id = scanner.next();
            objGestor.busquedaIDProfesor(id);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema en la busqueda del profesor" + e.getMessage());
        }
    }

    static void busquedaNombreEstudiante() throws Exception {
        try {
            System.out.println("Bienvenido a la busqueda de entities.Estudiante por Nombre");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el Nombre del entities.Estudiante: ");
            String nombre = scanner.next();
            objGestor.busquedaNombreEstudiante(nombre);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema en la busqueda del estudiante" + e.getMessage());
        }
    }

    static void busquedaNombreProfesor() throws Exception {
        try {
            System.out.println("Bienvenido a la busqueda de entities.Profesor por Nombre");
            System.out.println("Ingrese lo que se le solicite");
            System.out.println("Ingrese el Nombre del entities.Profesor: ");
            String nombre = scanner.next();
            objGestor.busquedaNombreProfesor(nombre);
        } catch (Exception e) {
            throw new Exception("Ha habido un problema en la busqueda del profesor" + e.getMessage());
        }
    }

    static void agregarPersona() {
        try {
            System.out.println("Ingrese la informacion del Cliente\n");
            String nombre;
            String id;
            System.out.println("Ingrese el ID del Cliente");
            id = scanner.next();
            System.out.println("Ingrese el nombre del Cliente");
            nombre = scanner.next();
            Persona objPersona = new Persona(id, nombre);
            personQueue.enqueue(String.valueOf(objPersona));
        } catch (Exception e) {
            e.getStackTrace();
            System.out.println("Ha ocurrido un error al registrar Cliente");
        }
    }

    static void atenderCliente() {
        try {
            boolean verificador = false;
            do {
                boolean yesOrNo = false;
                if (!yesOrNo) {
                    if (personQueue.isEmpty()) {
                        System.out.println("La cola esta vacia");
                        break;
                    } else {
                        String respuesta;
                        System.out.println("El cliente: " + personQueue.peek().getData() + " ha sido atendido?");
                        System.out.println("SI o NO");
                        respuesta = scanner.next();
                        if (respuesta.equalsIgnoreCase("SI")) {
                            personQueue.dequeue();
                            System.out.println("El siguiente cliente es: " + personQueue.peek().getData() + "\n");
                            verificador = true;

                        } else if (respuesta.equalsIgnoreCase("NO")) {
                            System.out.println("Atienda al cliente en la lista: " + personQueue.peek().getData());

                        }
                    }
                }
            } while (!verificador);
        } catch (Exception e) {
            e.getStackTrace();
            System.out.println("Ha ocurrido un error al atender cliente.");
        }

    }

}
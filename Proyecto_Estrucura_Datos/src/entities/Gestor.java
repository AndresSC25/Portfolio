package entities;

import java.util.ArrayList;

public class Gestor {
    //Se crea la instancia de las listas para poder almacenar los diferntes objetos en un arrayList
    private final ArrayList<Estudiante> listaEstudiantes = new ArrayList<>();
    private final ArrayList<Curso> listaCursos = new ArrayList<>();
    private final ArrayList<Profesor> listaProfesores = new ArrayList<>();

    //Motodo para agregar un curso a la lista.
    public void agregarCurso(String id, String numeroGrupo, String descripcion) throws Exception {
        String modificadorId = "CU-" + id;
        try {
            Curso nuevoCurso = new Curso(modificadorId, numeroGrupo, descripcion);
            listaCursos.add(nuevoCurso);
            System.out.println(nuevoCurso);
            System.out.println("Curso guardado con exito");

        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error al guardar el curso en el sistema" + e.getMessage());
        }

    }

    //Metodo para ver si se encuentra el curso y devuelve este mismo, para saber si ya esta registrado o no.
    public Curso encontrarCurso(String idCurso) throws Exception {
        try {
            Curso cursoEncontrado = null;
            for (Curso objCurso : listaCursos) {
                if (objCurso.getId().equals(idCurso)) {
                    cursoEncontrado = objCurso;
                }
            }
            return cursoEncontrado;

        } catch (Exception e) {
            throw new Exception("Ha habido un error encontrando el curso");
        }

    }

    //Metodo para validar si ya se registro un curso con el mismo id.
    public boolean existeCurso(String idCurso) throws Exception {
        try {
            String modificadorId = "CU-" + idCurso;
            boolean cursoEncontrado = false;
            for (Curso objCurso : listaCursos) {
                if (objCurso.getId().equals(modificadorId)) {
                    cursoEncontrado = true;
                    break;
                }
            }
            return cursoEncontrado;
        } catch (Exception e) {
            throw new Exception("Ha habido un error encontrando el curso");
        }
    }

    //Metodo para verificar que el estudiante no esta matriculado ya y nos devueleve el objeto encontrado.
    public Estudiante encontrarCursoMatriculadoEstudiante(String idEstudiante, String idCurso) throws Exception {
        try {
            Estudiante estudianteEncontrado = null;
            for (Estudiante objEstudiante : listaEstudiantes) {
                if (objEstudiante.getId().equals(idEstudiante)) {
                    for (int j = 0; j < objEstudiante.getListacursos().size(); j++) {
                        if (objEstudiante.getListacursos().get(j).getId().equals(idCurso)) {
                            estudianteEncontrado = objEstudiante;
                            return estudianteEncontrado;
                        }
                    }
                }
            }
            return estudianteEncontrado;

        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error al encontrar estudiante matriculado " + e.getMessage());
        }
    }

    //Metodo para verificar que el profesor no imparta el curso varias veces.
    public Profesor encontrarCursoProfesor(String idProfesor, String idCurso) throws Exception {
        try {
            Profesor profesorEncontrado = null;
            for (Profesor objProfesor : listaProfesores) {
                if (objProfesor.getId().equals(idProfesor)) {
                    for (int j = 0; j < objProfesor.getListacursos().size(); j++) {
                        if (objProfesor.getListacursos().get(j).getId().equals(idCurso)) {
                            profesorEncontrado = objProfesor;
                            return profesorEncontrado;
                        }
                    }
                }
            }
            return profesorEncontrado;

        } catch (Exception e) {
            throw new Exception("Ha habido un error encontrando el curso donde el profesor esta asignado " + e.getMessage());
        }

    }

    //Metodo para agregar al estudiante.
    public void agregarEstudiante(String id, String nombre, String apellido1, String apellido2, String idCurso) throws Exception {
        try {
            String modificadorId = "ES-" + id;
            String modificadorIdCurso = "CU-" + idCurso;
            Curso objCurso = encontrarCurso(modificadorIdCurso);
            if (!(encontrarEstudiante(modificadorId) == null)) {
                throw new Exception("No se registro el estudiante por que ya existe alguien con ese id: " + id);
            } else if (objCurso == null) {
                throw new Exception("No se registro el estudiante por que no existe un curso con ese id: " + modificadorIdCurso);
            }
            ArrayList<Curso> cursoTemp = new ArrayList<>();
            cursoTemp.add(objCurso);
            Estudiante objEstudiante = new Estudiante(modificadorId, nombre, apellido1, apellido2, cursoTemp);
            System.out.println(objEstudiante);
            System.out.println("entities.Estudiante registrado con exito");
            listaEstudiantes.add(objEstudiante);
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }
    }

    //Metodo que nos sirve para verificar si el estudiante existe.
    public boolean existeEstudiante(String idEstudiante) throws Exception {
        try {
            String modificadorId = "ES-" + idEstudiante;
            boolean estudianteEncontrado = false;
            for (Estudiante objEstudiante : listaEstudiantes) {
                if (objEstudiante.getId().equals(modificadorId)) {
                    estudianteEncontrado = true;
                    break;
                }
            }
            return estudianteEncontrado;

        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error encontrando los estudiantes");
        }

    }

    //Metodo que nos permite agregar un curso al estudiante.
    public void agregarCursoEstudiante(String idEstudiante, String idCurso) throws Exception {
        try {
            String modificadorId = "ES-" + idEstudiante;
            String modificadorIdCurso = "CU-" + idCurso;
            Curso objCurso = encontrarCurso(modificadorIdCurso);
            if (!(encontrarCursoMatriculadoEstudiante(modificadorId, modificadorIdCurso) == null)) {
                throw new Exception("No se puede matricular a dicha materia por que el estudiante ya esta matriculado a esa materia");
            } else if (objCurso == null) {
                throw new Exception("No se registro el curso por que no existe un curso con ese id: " + modificadorIdCurso);
            }
            Estudiante objEstudiante = encontrarEstudiante(modificadorId);
            ArrayList<Curso> tempCursos = objEstudiante.getListacursos();
            tempCursos.add(objCurso);
            objEstudiante.setListacursos(tempCursos);
            System.out.println(objEstudiante);
            System.out.println("entities.Estudiante entities.Curso registrado con exito");
        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error al agregar el curso al estudiante " + e.getMessage());
        }

    }

    //Metodo para encontrar si el estudiante existe y nos devuelve el objeto de la lista.
    public Estudiante encontrarEstudiante(String id_estudiante) throws Exception {
        try {
            Estudiante estudianteEncontrado = null;
            for (Estudiante objEstudiante : listaEstudiantes) {
                if (objEstudiante.getId().equals(id_estudiante)) {
                    estudianteEncontrado = objEstudiante;
                }
            }
            return estudianteEncontrado;

        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para agregar un curso al profesor
    public void agregarCursoProfesor(String idProfesor, String idCurso) throws Exception {
        try {
            String modificadorId = "PR-" + idProfesor;
            String modificadorIdCurso = "CU-" + idCurso;
            Curso objCurso = encontrarCurso(modificadorIdCurso);
            if (!(encontrarCursoProfesor(modificadorId, modificadorIdCurso) == null)) {
                throw new Exception("No se puede matricular a dicha materia por que el profesor ya esta asignado a esa materia");
            } else if (objCurso == null) {
                throw new Exception("No se registro el curso por que no existe un curso con ese id: " + modificadorIdCurso);
            }
            Profesor objProfesor = encontrarProfesor(modificadorId);
            ArrayList<Curso> tempCursos = objProfesor.getListacursos();
            tempCursos.add(objCurso);
            objProfesor.setListacursos(tempCursos);
            System.out.println(objProfesor);
            System.out.println("Profesor asignado a Curso con exito");
        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error al asignar un curso al profesor " + e.getMessage());
        }

    }

    //Metodo para agregar un profesor.
    public void agregarProfesor(String id, String nombre, String apellido1, String apellido2, String idCurso) throws Exception {
        try {
            String modificadorId = "PR-" + id;
            String modificadorIdCurso = "CU-" + idCurso;
            Curso objCurso = encontrarCurso(modificadorIdCurso);
            if (!(encontrarProfesor(modificadorId) == null)) {
                throw new Exception("No se registro el profesor por que ya existe alguien con ese id: " + id);
            } else if (objCurso == null) {
                throw new Exception("No se registro el profesor por que no existe un curso con ese id: " + idCurso);
            }
            ArrayList<Curso> cursoTemp = new ArrayList<>();
            cursoTemp.add(objCurso);
            Profesor objProfesor = new Profesor(modificadorId, nombre, apellido1, apellido2, cursoTemp);
            System.out.println(objProfesor);
            System.out.println("entities.Profesor registrado con exito");
            listaProfesores.add(objProfesor);
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para verificar si el profesor ya existe.
    public boolean existeProfesor(String idProfesor) throws Exception {
        try {
            String modificadorId = "PR-" + idProfesor;
            boolean profesorEncontrado = false;
            for (Profesor objProfesor : listaProfesores) {
                if (objProfesor.getId().equals(modificadorId)) {
                    profesorEncontrado = true;
                    break;
                }
            }
            return profesorEncontrado;

        } catch (Exception e) {
            throw new Exception("Ha ocurrido un error encontrando los profesores");
        }

    }

    //Metodo para encontrar en¿l profesor y devolverlo si este existe.
    public Profesor encontrarProfesor(String id_profesor) throws Exception {
        try {
            Profesor profesorEncontrado = null;
            for (Profesor objProfesor : listaProfesores) {
                if (objProfesor.getId().equals(id_profesor)) {
                    profesorEncontrado = objProfesor;
                }
            }
            return profesorEncontrado;
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para buscar al estudiante mediante el id
    public void busquedaIDEstudiante(String id_estudiante) throws Exception {
        try {
            String modificadorId = "ES-" + id_estudiante;
            Estudiante estudianteEncontrado = null;
            for (Estudiante objEstudiante : listaEstudiantes) {
                if (objEstudiante.getId().equals(modificadorId)) {
                    estudianteEncontrado = objEstudiante;
                }
            }
            if (estudianteEncontrado == null) {
                System.out.println("No hay ningun estudiante con ese id: " + modificadorId);
            } else {
                System.out.println("El estudiante con ese id: " + modificadorId + " es");
                System.out.println(estudianteEncontrado);

            }
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para buscar al profesor mediante el id
    public void busquedaIDProfesor(String id_Profesor) throws Exception {
        try {
            String modificadorId = "PR-" + id_Profesor;
            Profesor profesorEncontrado = null;
            for (Profesor objProfesor : listaProfesores) {
                if (objProfesor.getId().equals(modificadorId)) {
                    profesorEncontrado = objProfesor;
                }
            }
            if (profesorEncontrado == null) {
                System.out.println("No hay ningun profesor con ese id: " + modificadorId);
            } else {
                System.out.println("El profesor con ese id (" + modificadorId + ") es:");
                System.out.println(profesorEncontrado);

            }
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para buscar al estudiante mediante el nombre
    public void busquedaNombreEstudiante(String nombreEstudiante) throws Exception {
        try {
            Estudiante estudianteEncontrado = null;
            for (Estudiante objEstudiante : listaEstudiantes) {
                if (objEstudiante.getNombre().equals(nombreEstudiante)) {
                    estudianteEncontrado = objEstudiante;
                }
            }
            if (estudianteEncontrado == null) {
                System.out.println("No hay ningun estudiante con ese nombre: " + nombreEstudiante);
            } else {
                System.out.println("El estudiante con ese nombre (" + nombreEstudiante + ") es");
                System.out.println(estudianteEncontrado);

            }
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

    //Metodo para buscar al profesor mediante el nombre
    public void busquedaNombreProfesor(String nombreProfesor) throws Exception {
        try {
            Profesor profesorEncontrado = null;
            for (Profesor objProfesor : listaProfesores) {
                if (objProfesor.getNombre().equals(nombreProfesor)) {
                    profesorEncontrado = objProfesor;
                }
            }
            if (profesorEncontrado == null) {
                System.out.println("No hay ningun profesor con ese nombre: " + nombreProfesor);
            } else {
                System.out.println("El profesor con ese nombre (" + nombreProfesor + ") es:");
                System.out.println(profesorEncontrado);

            }
        } catch (Exception e) {
            throw new Exception(e.getMessage());
        }

    }

}

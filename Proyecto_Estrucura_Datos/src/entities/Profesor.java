package entities;

import java.util.ArrayList;

public class Profesor {
    private String id;
    private String nombre;
    private String apellido1;
    private String apellido2;
    private ArrayList<Curso> Listacursos;

    public Profesor() {
    }

    public Profesor(String id, String nombre, String apellido1, String apellido2, ArrayList<Curso> listacursos) {
        this.id = id;
        this.nombre = nombre;
        this.apellido1 = apellido1;
        this.apellido2 = apellido2;
        Listacursos = listacursos;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getApellido1() {
        return apellido1;
    }

    public void setApellido1(String apellido1) {
        this.apellido1 = apellido1;
    }

    public String getApellido2() {
        return apellido2;
    }

    public void setApellido2(String apellido2) {
        this.apellido2 = apellido2;
    }

    public ArrayList<Curso> getListacursos() {
        return Listacursos;
    }

    public void setListacursos(ArrayList<Curso> listacursos) {
        Listacursos = listacursos;
    }

    @Override
    public String toString() {
        return "entities.Profesor{" +
                "id='" + id + '\'' +
                ", nombre='" + nombre + '\'' +
                ", apellido1='" + apellido1 + '\'' +
                ", apellido2='" + apellido2 + '\'' +
                ", Listacursos=" + Listacursos +
                '}';
    }
}

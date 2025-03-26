package entities;

public class Persona {
    public String id;
    public String nombre;

    public Persona() {
    }

    public Persona(String id, String nombre) {
        this.id = id;
        this.nombre = nombre;
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

    @Override
    public String toString() {
        return "( con ID: " + id + ", " + " de nombre " + nombre + " )";
    }
}

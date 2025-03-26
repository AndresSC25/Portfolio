package entities;

public class Curso {
    private String id;
    private String numeroGrupo;
    private String descripcion;

    public Curso() {
    }

    public Curso(String id, String numeroGrupo, String descripcion) {
        this.id = id;
        this.numeroGrupo = numeroGrupo;
        this.descripcion = descripcion;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getNumeroGrupo() {
        return numeroGrupo;
    }

    public void setNumeroGrupo(String numeroGrupo) {
        this.numeroGrupo = numeroGrupo;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    @Override
    public String toString() {
        return "entities.Curso{" +
                "id='" + id + '\'' +
                ", numeroGrupo='" + numeroGrupo + '\'' +
                ", descripcion='" + descripcion + '\'' +
                '}';
    }
}

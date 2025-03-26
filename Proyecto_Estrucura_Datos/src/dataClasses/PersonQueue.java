package dataClasses;

public class PersonQueue {

    // principio de la cola

    private PersonNode front;

    // final de la cola

    private PersonNode rear;

    // tamaño de la cola

    private int size;

    public PersonQueue() {

        this.front = null;

        this.rear = null;

        this.size = 0;

    }

    // node ayuda a saber si la cola esta vacia

    public boolean isEmpty() {

        return this.size == 0;

    }


    // agregar un elemento a la cola

    public void enqueue(String name) {

        // Creamos un nuevo nodo

        PersonNode newNode = new PersonNode(name);

        // revisamos que no haya nada en el principio de la cola

        if (this.front == null) {

            this.front = newNode;

            this.rear = newNode;

            this.size++;

        } else {

            // agregamos el elemento al final de la cola

            this.rear.setNext(newNode);

            this.rear = newNode;

            size++;

        }

    }

    // desencolamos el primer elemento de la cola

    public PersonNode dequeue() {


        // el principio de la cola no esta vacio,

        // vamos a sacar el nodo al principio de la cola

        if (this.front != null) {


            // creamos el nodo con la informacion del principio de la cola

            PersonNode dequeueedNode = new PersonNode(front.getData());


            // el siguiente nodo en la cola se vuelve el principio de la cola

            this.front = this.front.getNext();


            // actualizamos el tamano de la cola

            size--;


            // retornamos el nodod con la informacion del principio de la cola

            return dequeueedNode;

        }

        return null;

    }

    // obtenemos el elemento al principio de la cola

    public PersonNode peek() {

        if (!this.isEmpty()) {

            return new PersonNode(front.getData());

        }

        return null;

    }

    public int getSize() {

        return this.size;

    }

}

package dataClasses;

public class LinkedList {

    Node head;

    int size = 0;

    public LinkedList insertNode(LinkedList list, String data) {

        Node newNode = new Node(data);

        if (list.head == null) {

            list.head = newNode;

            return list;
        }

        Node currentNode = list.head;

        while (currentNode.next != null) {

            currentNode = currentNode.next;

        }


        // agregamos el nuevo nodo

        currentNode.next = newNode;

        this.size++;

        return list;

    }

}

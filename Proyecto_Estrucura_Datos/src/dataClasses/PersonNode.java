package dataClasses;

public class PersonNode {

    private String data;

    private PersonNode next;

    public PersonNode(String data) {

        this.data = data;

        this.next = null;

    }

    public String getData() {

        return this.data;

    }

    public void setData(String data) {

        this.data = data;

    }

    public PersonNode getNext() {

        return this.next;

    }

    public void setNext(PersonNode next) {

        this.next = next;

    }

}

public class Queue<T> {
    private T[] buffer;
    private int head;
    private int tail;

    public Queue(int size) {
        buffer = new T[size];
        head = -1;
        tail = -1;
        Console.WriteLine("Queue dibuat!");
    }

    ~Queue() {
        //Perlu ga ya?
    }

    public void Enqueue(T element) {
        //coba ikutin alstrukdat
        if (head == -1 && tail == -1) {
            head = 0;
            tail = 0;
            buffer[head] = element;
        } else {
            tail = tail + 1;
            buffer[tail] = element;
        }
        Console.WriteLine("Elemen " + element + " ditambahkan!");
    }

    public T Dequeue() {
        //coba ikutin alstrukdat
        T element = buffer[head];
        if (head == tail) {
            head = -1;
            tail = -1;
        } else {
            head = head + 1;
        }
        Console.WriteLine("Elemen " + element + " dihapus!");
        return (element);
    }

    public void displayQueue(Queue<T> Q) {
        for (int i = head; i < tail+1; i++) {
            if (Q.buffer[i] != null) {
                Console.WriteLine(Q.buffer[i]);
            }
        }
    }
}
public class Queue<T> {
    public T[] buffer;
    public int head;
    public int tail;

    public Queue(int size) {
        buffer = new T[size];
        head = -1;
        tail = -1;
        // Console.WriteLine("Queue dibuat!");
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
        // Console.WriteLine("Elemen " + element + " ditambahkan!");
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
        // Console.WriteLine("Elemen " + element + " dihapus!");
        return (element);
    }

    public void displayQueue() {
        for (int i = head; i < tail+1; i++) {
            if (buffer[i] != null) {
                Console.WriteLine(buffer[i]);
            }
        }
    }

    public bool isEmpty() {
        if (head == -1 && tail == -1) {
            return (true);
        } else {
            return (false);
        }
    }
}
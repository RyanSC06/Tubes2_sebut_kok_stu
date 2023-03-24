public class Queue<T> {
    public T[] buffer;
    public int head;
    public int tail;

    public Queue(int size) {
        // membuat queue dengan ukuran size
        buffer = new T[size];
        head = -1;
        tail = -1;
    }

    public void Enqueue(T element) {
        // menambahkan element ke queue
        if (head == -1 && tail == -1) {
            // queue kosong
            head = 0;
            tail = 0;
            buffer[head] = element;
        } else {
            // queue tidak kosong
            tail = tail + 1;
            buffer[tail] = element;
        }
    }

    public T Dequeue() {
        // mengeluarkan element dari queue
        T element = buffer[head];
        if (head == tail) {
            // queue hanya memiliki 1 element
            head = -1;
            tail = -1;
        } else {
            // queue memiliki lebih dari 1 element
            head = head + 1;
        }
        return (element);
    }

    public void displayQueue() {
        // menampilkan isi queue
        for (int i = head; i < tail+1; i++) {
            if (buffer[i] != null) {
                System.Console.WriteLine(buffer[i]);
            }
        }
    }

    public bool isEmpty() {
        // mengecek apakah queue kosong
        if (head == -1 && tail == -1) {
            return (true);
        } else {
            return (false);
        }
    }
}
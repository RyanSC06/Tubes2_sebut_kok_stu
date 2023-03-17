//Sumber: https://www.w3schools.com/cs/index.php
//Sumber: https://www.c-sharpcorner.com/UploadFile/mahesh/how-to-read-a-text-file-in-C-Sharp/
//Sumber: https://davidboland.site/blog/how-to-implement-a-queue-in-c

using System;
using System.IO;

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



public class Maze {
    static string textFile;

    public static bool isMapValid (string[] map) {
        bool trueChar = true;
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                if (i == 0 && j == 0) {
                    if (map[i][j] != 'K') {
                        trueChar = false;
                        Console.WriteLine("Map ERROR code: 1");
                    }
                } else if (j % 2 == 0) {
                    if (map[i][j] != 'R' && map[i][j] != 'T' && map[i][j] != 'X') {
                        trueChar = false;
                        Console.WriteLine("Map ERROR code: 2");
                    }
                } else {
                    if (map[i][j] != ' ') {
                        trueChar = false;
                        Console.WriteLine("Map ERROR code: 3");
                    }
                }
            }
        }
        return (trueChar);
    }
    
    public static bool isIdxValid (string[] map, int idx_row, int idx_col) {
        if ((idx_row < map.Length) && (idx_col < map[0].Length)) {
            return (true);
        } else {
            return (false);
        }
    }

    public static bool isLocationReachable (string[] map, int idx_row, int idx_col) {
        if (map[idx_row][idx_col] != 'X') {
            return (true);
        } else {
            return (false);
        }
    }

    // public static char[,] makeMap (string[] map) {
    //     char[,] actualMap = new char[map.Length, (2*map[0].Length) - 1];
    //     int idx_col;

    //     for (int i = 0; i < map.Length; i++) {
    //         idx_col = 0;
    //         for (int j = 0; j < map[0].Length; i++) {
    //             if (map[i][j] != ' ') {
    //                 actualMap[i, idx_col] = map[i][j];
    //                 idx_col = idx_col + 1;
    //             }
    //         }
    //     }
    //     return (actualMap);
    // }

    public static Queue<int[]> checkAlive (Queue<int[]> Q, string[] map, int idx_row, int idx_col) {
        return (Q);
    }

    public static void findTreasureBFS (string[] map) {
        
    }

    public static void Main (string[] args) {
        Console.Write("Masukkan nama file: ");
        string fileName = Console.ReadLine();
        textFile = @"..\test\" + fileName + ".txt";

        if (File.Exists(textFile)) {
            string[] map = File.ReadAllLines(textFile);
            if (isMapValid(map)) {
                Console.WriteLine("\nPeta Anda:");
                foreach (string line in map) {
                    Console.WriteLine(line);
                }
            } else {
                Console.WriteLine("Maaf, peta Anda tidak valid.");
            }

        } else {
            Console.WriteLine("Maaf, file tersebut tidak ada.");
        }

        // Console.WriteLine("\nCOBA QUEUE");
        // Queue<int> Antrian = new Queue<int>(10);
        // Antrian.Enqueue(10);
    }
}

using System;
using System.IO;

public class Program2 {
    public static int isMapValid (string[] map) {
    //Fungsi untuk memeriksa validitas peta dan mengembalikan jumlah 'T' yang ada
        bool trueChar = true;
        bool isKrustyThere = false;
        int num_of_treasure = 0;

        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                if (j % 2 == 0) {
                    if (map[i][j] == 'K' && isKrustyThere == false) {
                        isKrustyThere = true;
                    } else if (map[i][j] == 'K' && isKrustyThere == true) {
                        isKrustyThere = false;
                        Console.WriteLine("Map ERROR code: 1");
                        break;
                    } else if (map[i][j] == 'T') {
                        num_of_treasure = num_of_treasure + 1;
                    } else if (map[i][j] != 'R' && map[i][j] != 'X') {
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

        if (trueChar && isKrustyThere) {
            return (num_of_treasure);
        } else {
            return (-1);
        }
    }

    public static string[] input() {
        string textFile;
        Console.Write("Masukkan nama file: ");
        string fileName = Console.ReadLine();
        textFile = @"..\test\" + fileName + ".txt";

        if (File.Exists(textFile)) {
            string[] map = File.ReadAllLines(textFile);
            if (isMapValid(map) != -1) {
                Console.WriteLine("\nPeta Anda:");
                foreach (string line in map) {
                    Console.WriteLine(line);
                }
                return (map);
            } else {
                string[] mapNotValid = new string[1];
                mapNotValid[0] = "";
                Console.WriteLine("Maaf, peta Anda tidak valid.");
                return (mapNotValid);
            }

        } else {
            string[] mapNotExist = new string[1];
            mapNotExist[0] = "";
            Console.WriteLine("Maaf, file tersebut tidak ada.");
            return (mapNotExist);
        }
    }

    public static void Main() {
        string[] x = input();
        Console.WriteLine("");
        foreach (string line in x) {
            Console.WriteLine(line);
        }
    }
}
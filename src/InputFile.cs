using System;
using System.IO;

public class InputFile {
    public static bool isMapValid (string[] map) {
    //Fungsi untuk memeriksa validitas peta dan mengembalikan jumlah 'T' yang ada
        bool trueChar = true;
        bool isKrustyThere = false;

        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                if (j % 2 == 0) {
                    if (map[i][j] == 'K' && isKrustyThere == false) {
                        isKrustyThere = true;
                    } else if (map[i][j] == 'K' && isKrustyThere == true) {
                        Console.WriteLine("Map ERROR code: 1");
                        return false;
                    } else if (map[i][j] != 'T' && map[i][j] != 'R' && map[i][j] != 'X') {
                        Console.WriteLine("Map ERROR code: 2");
                        return false;
                    }
                } else {
                    if (map[i][j] != ' ') {
                        Console.WriteLine("Map ERROR code: 3");
                        return false;
                    }
                }
            }
        }

        if (trueChar && isKrustyThere) {
            return (true);
        } else {
            return (false);
        }
    }


    public static bool isIdxValid (string[] map, int idx_row, int idx_col) {
    //Masukan "map" berupa peta masukan yang sudah dirapikan (tanpa spasi)
        if ((idx_row < map.Length && idx_row >= 0) && (idx_col < map[0].Length && idx_col >= 0)) {
            return (true);
        } else {
            return (false);
        }
    }

    public static bool isLocationReachable (string[] map, int idx_row, int idx_col) {
    //Masukan "map" berupa peta masukan yang sudah dirapikan (tanpa spasi)
        if ((map[idx_row][idx_col] != 'X')) {
            return (true);
        } else {
            return (false);
        }
    }

    public static string[] input() {
        Console.Write("Masukkan nama file: ");
        string filename = ("\\test\\" + Console.ReadLine() + ".txt");
        string textFile =  (Directory.GetCurrentDirectory() + filename);
        Console.WriteLine(textFile);

        if (File.Exists(textFile)) {
            string[] map = File.ReadAllLines(textFile);
            if (isMapValid(map)) {
                Console.WriteLine("\nPeta Anda:");
                foreach (string line in map) {
                    Console.WriteLine(line);
                }
                return (map);
            } else {
                string[] mapNotValid = new string[1];
                mapNotValid[0] = "-1";
                Console.WriteLine("Maaf, peta Anda tidak valid.");
                return (mapNotValid);
            }

        } else {
            string[] mapNotExist = new string[1];
            mapNotExist[0] = "-1";
            Console.WriteLine("Maaf, file tidak ditemukan.");
            return (mapNotExist);
        }
    }

    public static int findNodeIdx(List<Point> LP, int idx_row, int idx_col) {
        for (int i = 0; i < LP.Count; i++) {
            if (LP[i].X == idx_row && LP[i].Y == idx_col) {
                return (i);
            }
        }

        //Kondisi tidak mungkin
        return (-1);
    }

    public static string[] makeMap() {
        string[] actualMap = input();        
        string[] map = new string[actualMap.Length];
        for (int r = 0; r < actualMap.Length; r++) {
            for (int c = 0; c < actualMap[0].Length; c++) {
                if (actualMap[r][c] != ' ') {
                    map[r] = map[r] + actualMap[r][c];
                }
            }
        }
        return map;        
    }

    public static Graph makeGraph() {
        string[] actualMap = input();
        string[] map = new string[actualMap.Length];
        for (int r = 0; r < actualMap.Length; r++) {
            for (int c = 0; c < actualMap[0].Length; c++) {
                if (actualMap[r][c] != ' ') {
                    map[r] = map[r] + actualMap[r][c];
                }
            }
        }
        if (map[0] != "-1") {
            List<Point> nodes = new List<Point>();
            for (int i = 0; i < map.Length; i++) {
                for (int j = 0; j < map[0].Length; j++) {
                    Point P = new Point(-1, -1, TypeGrid.X);
                    if (map[i][j] == 'K') {
                        P = new Point(i, j, TypeGrid.KrustyKrab);
                        nodes.Add(P);
                    } else if (map[i][j] == 'R') {
                        P = new Point(i, j, TypeGrid.Lintasan);
                        nodes.Add(P);
                    } else if (map[i][j] == 'T') {
                        P = new Point(i, j, TypeGrid.Treasure);
                        nodes.Add(P);
                    }
                }
            }
            
            int k = 0;
            Graph G = new Graph(nodes);
            for (int i = 0; i < map.Length; i++) {
                for (int j = 0; j < map[0].Length; j++) {
                    if (map[i][j] != ' ' && map[i][j] != 'X') {
                        if (isIdxValid (map, i, j+1)) {
                            if (isLocationReachable (map, i, j+1)) {
                                G.addEdge(G.Nodes[k], G.Nodes[findNodeIdx(G.Nodes, i, j+1)]);
                            }
                        }

                        if (isIdxValid (map, i+1, j)) {
                            if (isLocationReachable (map, i+1, j)) {
                                G.addEdge(G.Nodes[k], G.Nodes[findNodeIdx(G.Nodes, i+1, j)]);
                            }
                        }

                        if (isIdxValid (map, i, j-1)) {
                            if (isLocationReachable (map, i, j-1)) {
                                G.addEdge(G.Nodes[k], G.Nodes[findNodeIdx(G.Nodes, i, j-1)]);
                            }
                        }

                        if (isIdxValid (map, i-1, j)) {
                            if (isLocationReachable (map, i-1, j)) {
                                G.addEdge(G.Nodes[k], G.Nodes[findNodeIdx(G.Nodes, i-1, j)]);
                            }
                        }
                        
                        k = k + 1;
                    }
                }
            }
            return (G);

        } else {
            List<Point> nodes = new List<Point>();
            Graph G = new Graph(nodes);
            // do nothing
            return (G);
        }
    }

    public static int findNumberOfTreasure (Graph g) {
        int num_T = 0;
        foreach (Point point in g.Nodes) {
            if (point.Type == TypeGrid.Treasure) {
                num_T = num_T + 1;
            }
        }
        return (num_T);
    }

    public static Point findStartingPoint (Graph g) {
        Point starting = new Point(-1, -1, TypeGrid.X);
        foreach (Point point in g.Nodes) {
            if (point.Type == TypeGrid.KrustyKrab) {
                starting = point;
                break;
            }
        }
        return (starting);
    }
    
    public static void Main (string[] args) {
        Graph g = makeGraph();
        
        List<List<Point>> solution = MazeBFS.findCheckedBFS(g);
        Console.WriteLine("\nJalur:");
        foreach (List<Point> lp in solution) {
            Graph.printListPath(lp);
        }

        List<Point> actualPath = MazeBFS.findPathBFS(g);
        Graph.printListPath(actualPath);
    }
}

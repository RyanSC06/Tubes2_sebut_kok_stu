using System;
using System.Collections.Generic;
using System.Diagnostics;

public class DFS{

    private Graph g;

    private List<Point> path = new List<Point>();

    private List<Point> tspPath = new List<Point>();

    private List<Point> pathVisited = new List<Point>();

    private List<Point> tspPathVisited = new List<Point>();

    private Stack<Point> pathStack = new Stack<Point>();

    private List<Point> backtrackPath = new List<Point>();

    private int treasureCount = 0;

    private int step = -1;

    private int tspStep = 0;

    private TimeSpan time = new TimeSpan();

    private TimeSpan tspTime = new TimeSpan();

    public DFS(Graph g, Point source, int numOfTreasure){
        // mencari path dari source ke semua treasure
            this.g = g;
            g.resetGraph();
            var watch = new Stopwatch();
            watch.Start();
            dfs(source, numOfTreasure);
            watch.Stop();
            time = watch.Elapsed;
            g.resetGraph();
    }

    private void dfs(Point source, int numOfTreasure){
        // Menandakan Point telah ditemukan, menambah step, dan menyimpan Point
        step++;
        source.pointFound();
        pathVisited.Add(source);
        path.Add(source);
        if (source.Type == TypeGrid.Treasure){
            // Jika Point berjenis Treasure
            treasureCount++;
        }
        if (treasureCount == numOfTreasure){
            // Jika semua treasure telah ditemukan
            return;
        }
        if (g.getNeighboursNotVisited(source) == 0){
            // Jika Point/simpul tidak memiliki tetangga yang belum dikunjungi
            addBacktrackPath(source);
            // Melakukan backtrack ke Point yang belum dikunjungi
            foreach (Point node in backtrackPath){
                path.Add(node);
                step++;
            }
            backtrackPath.Clear();
        } else {
            var neighbours = g.getNeighbours(source);
            // Menambah tetangga Point ke dalam stack
            for (int i = neighbours.Count-1; i >= 0; i--){
                if (!neighbours[i].Found){
                    pathStack.Push(neighbours[i]);
                }
            }
        }
        if (pathStack.Count == 0){
            System.Console.WriteLine("No path found");
            return;
        }
        Point next = pathStack.Pop();
        // Mencari Point yang belum dikunjungi dari stack
        while (next.Found){
            if (pathStack.Count == 0){
            System.Console.WriteLine("No path found");
            return;
            }
            next = pathStack.Pop();
        }
        // Rekursif ke Point selanjutnya
        dfs(next, numOfTreasure);
    }

    private void dfs(Point source){
        // sama seperti fungsi dfs sebelumnya tanpa batas treasure
        // fungsi tidak digunakan dalam program
        tspStep++;
        source.pointFound();
        tspPath.Add(source);
        tspPathVisited.Add(source);
        if (pathStack.Count == 0 || g.Nodes.Count == tspPathVisited.Count){
            return;
        }
        if (g.getNeighboursNotVisited(source) == 0){
            addBacktrackPathTSP(source);
            foreach (Point node in backtrackPath){
                tspPath.Add(node);
                tspStep++;
            }
            backtrackPath.Clear();
        } else {
            var neighbours = g.getNeighbours(source);
            for (int i = neighbours.Count-1; i >= 0; i--){
                if (!neighbours[i].Found){
                    pathStack.Push(neighbours[i]);
                }
            }
        }
        Point next = pathStack.Pop();
        while (next.Found){
            next = pathStack.Pop();
        }
        dfs(next);
    }

    private void tsp(){
        // fungsi tsp tidak digunakan dalam program
        var watch = new Stopwatch();
        watch.Start();
        for (int i = 0; i < path.Count; i++){
            tspPath.Add(path[i]);
            tspPath[i].pointFound();
        }
        for (int i = 0; i < pathVisited.Count; i++){
            tspPathVisited.Add(pathVisited[i]);
            tspPathVisited[i].pointFound();
        }
        tspStep = step;
        Point last = tspPath[tspPath.Count - 1];
        if (g.Nodes.Count != pathVisited.Count){
                if (g.getNeighboursNotVisited(last) == 0){
                    addBacktrackPathTSP(last);
                    // Graph.printListPath(backtrackPath);
                    foreach (Point node in backtrackPath){
                        tspPath.Add(node);
                        tspStep++;
                    }
                backtrackPath.Clear();
                } 
            Point next = pathStack.Pop();
            while (next.Found){
                next = pathStack.Pop();
            }
            dfs(next);
        }
        last = tspPathVisited[tspPathVisited.Count - 1];
        pathStack.Clear();
        g.resetGraph();
        // tspUtil(last);
        tspDFS(last);
        watch.Stop();
        tspTime = watch.Elapsed;
    }

    public void tspUtil(Point source){
        // fungsi rekursif tsp dengan menelusuri mundur dari path yang sudah didapat
        // tidak digunakan dalam program
        source.pointFound();
        tspPath.Add(source);
        tspStep++;
        if (source.Type == TypeGrid.KrustyKrab){
            return;
        }
        Point next = tspPath[tspPath.Count - 1];
        if (g.getNeighbours(source).Count > 2){
            next = tspPath[tspPath.IndexOf(source) - 1];
        }
        tspUtil(next);
    }

    public void tspDFS(Point source){
        // fungsi rekursif tsp dengan algoritma dfs
        // tidak digunakan dalam program
        source.pointFound();
        if (tspPathVisited[tspPathVisited.Count - 1] != source){
            tspPath.Add(source);
            tspStep++;
        }
        if (source.Type == TypeGrid.KrustyKrab){
            return;
        }
        if (g.getNeighboursNotVisited(source) == 0){
            addBacktrackPathTSP(source);
            foreach (Point node in backtrackPath){
                tspPath.Add(node);
                tspStep++;
            }
            backtrackPath.Clear();
        } else {
            foreach (Point node in g.getNeighbours(source)){
                if (!node.Found){
                    pathStack.Push(node);
                }
            }
        }
        Point next = pathStack.Pop();
        while (next.Found){
            next = pathStack.Pop();
        }
        tspDFS(next);
    }
    

    private void deleteBacktrackPath(){
        // menghapus path yang sudah dikunjungi
        List<Point> temp = new List<Point>();
        foreach (Point node in backtrackPath){
            temp.Add(node);
        }
        foreach (Point node in temp){
            path.Remove(node);
        }
    }

    private bool backtrackStop(Point source){
        // kasus saat backtrack berhenti
        if (pathVisited.IndexOf(source) == 0){
            // jika sudah kembali ke titik awal
            return true;
        }
        if (g.getNeighboursNotVisited(source) > 0){
            // jika masih ada tetangga yang belum dikunjungi
            return true;
        }
        return false;
    }

    private void addBacktrackPath(Point source){
        // prosedur utama backtrack
            var back = path[path.IndexOf(source)-1];
            // mundur satu langkah
            backtrackPath.Add(back);
            back.pointFound();
            if (backtrackStop(back)){
                return;
            }
            var next = path[path.IndexOf(back)-1];
            // rekursif memanggil backtrack util
            addBacktrackPathUtil(next);
    }

    private void addBacktrackPathTSP(Point source){
        // sama seperti backtrack biasa, namun khusus tsp
            var back = tspPath[tspPath.IndexOf(source)-1];
            backtrackPath.Add(back);
            back.pointFound();
            if (backtrackStop(back)){
                return;
            }
            var next = tspPath[tspPath.IndexOf(back)-1];
            addBacktrackPathUtilTSP(next);
    }

    private void addBacktrackPathUtil(Point back){
        // fungsi rekursif backtrack 
        backtrackPath.Add(back);
        back.pointFound();
        if (backtrackStop(back)){
            return;
        }       
        var next = path[path.IndexOf(back)-1];
        addBacktrackPathUtil(next);
    }

    private void addBacktrackPathUtilTSP(Point back){
        // fungsi rekursif backtrack tsp
        backtrackPath.Add(back);
        back.pointFound();
        if (backtrackStop(back)){
            return;
        }       
        var next = tspPath[tspPath.IndexOf(back)-1];
        addBacktrackPathUtilTSP(next);
    }

    // fungsi getter
    public List<Point> getFullPath(){
        return path;
    }

    public List<Point> getDFSPath(){
        return pathVisited;
    }

    public int getNodesVisited(){
        return pathVisited.Count;
    }

    public int getStep(){
        return step;
    }

    public double getTimeMicroS()
    {
        return time.TotalMicroseconds;
    }

    public double getTimeMs(){
        return time.TotalMilliseconds;
    }

    public TimeSpan getTime(){
        return time;
    }

    public List<Point> getTSPPath(){
        return tspPath;
    }

    public List<Point> getTSPPathVisited(){
        return tspPathVisited;
    }

    public int getTSPNodesVisited(){
        return tspPathVisited.Count;
    }

    public int getTSPStep(){
        return tspStep;
    }

    public TimeSpan getTimeTSP(){
        return tspTime + time;
    }

/*    public static void Main(string[] args){
        Graph input = InputFile.makeGraph();

        // input.printNodes();
        // input.printAdj();

        Point starting = InputFile.findStartingPoint(input);
        starting.print();
        int treasure = InputFile.findNumberOfTreasure(input);
        Console.WriteLine(treasure);
        DFS dfs2 = new DFS(input, starting, treasure);
        dfs2.tsp();
        Console.WriteLine("path visited: ");
        Graph.printListPath(dfs2.pathVisited);
        Console.WriteLine("path: ");
        Graph.printListPath(dfs2.path);
        Console.WriteLine("tsp path visited: ");
        Graph.printListPath(dfs2.tspPathVisited);
        Console.WriteLine("tsp path: ");
        // foreach (Point node in dfs2.tspPath){
        //     node.print();
        // }
        Console.WriteLine("n =" + dfs2.tspPath.Count);
        Graph.printListPath(dfs2.tspPath);

        Console.WriteLine("step: " + dfs2.step);
        Console.WriteLine("tsp step: " + dfs2.tspStep);

        // List<Point> bfs = MazeBFS.findTreasureBFS(input);
        // printListPath(bfs);
    }*/

}
using System.Collections.Generic;

public class DFS{

    private Graph g;

    private List<Point> path = new List<Point>();

    private List<Point> pathVisited = new List<Point>();

    private Stack<Point> pathStack = new Stack<Point>();

    private List<Point> backtrackPath = new List<Point>();

    private int treasureCount = 0;

    private int step = 0;

    public DFS(Graph g, Point source, int numOfTreasure){
            this.g = g;
            dfs(source, numOfTreasure);
            g.resetGraph();
    }

    private void dfs(Point source, int numOfTreasure){
        step++;
        source.pointFound();
        pathVisited.Add(source);
        path.Add(source);
        if (source.Type == TypeGrid.Treasure){
            treasureCount++;
        }
        if (treasureCount == numOfTreasure){
            return;
        }
        if (g.getNeighboursNotVisited(source) == 0){
            addBacktrackPath(source);
            printListPath(backtrackPath);
            foreach (Point node in backtrackPath){
                path.Add(node);
                step++;
            }
            backtrackPath.Clear();
        } else {
            var neighbours = g.getNeighbours(source);
            // printListPath(neighbours);
            for (int i = neighbours.Count-1; i >= 0; i--){
                if (!neighbours[i].Found){
                    pathStack.Push(neighbours[i]);
                }
            }
            // printListPath(pathStack.ToList());
        }
        Point next = pathStack.Pop();
        dfs(next, numOfTreasure);
    }

    private void deleteBacktrackPath(){
        List<Point> temp = new List<Point>();
        foreach (Point node in backtrackPath){
            temp.Add(node);
        }
        foreach (Point node in temp){
            path.Remove(node);
        }
    }

    private bool backtrackStop(Point source){
        if (pathVisited.IndexOf(source) == 0){
            return true;
        }
        if (g.getNeighboursNotVisited(source) > 0){
            return true;
        }
        return false;
    }

    private void addBacktrackPath(Point source){
            var back = pathVisited[pathVisited.IndexOf(source)-1];
            backtrackPath.Add(back);
            if (backtrackStop(back)){
                return;
            }
            var next = pathVisited[pathVisited.IndexOf(back)-1];
            addBacktrackPathUtil(next);
    }

    private void addBacktrackPathUtil(Point back){
        backtrackPath.Add(back);
        if (backtrackStop(back)){
            return;
        }       
        var next = pathVisited[pathVisited.IndexOf(back)-1];
        addBacktrackPathUtil(next);
    }

    public static void printListPath(List<Point> listPath){
        foreach (Point node in listPath){
                Console.Write(node.X + "," + node.Y);
                if (node != listPath[listPath.Count-1])
                Console.Write(" -> ");
        }
        Console.WriteLine();
    }


    public static void Main(string[] args){
        /*
        K R R 
        T R X
        R R T
        */
        Point p1 = new Point(0, 0, TypeGrid.KrustyKrab);
        Point p2 = new Point(1, 0, TypeGrid.Treasure);
        Point p3 = new Point(2, 0, TypeGrid.Lintasan);

        Point p4 = new Point(0, 1, TypeGrid.Lintasan);
        Point p5 = new Point(1, 1, TypeGrid.Lintasan);
        Point p6 = new Point(2, 1, TypeGrid.Lintasan);

        Point p7 = new Point(0, 2, TypeGrid.Lintasan);
        Point p9 = new Point(2, 2, TypeGrid.Treasure);

        List<Point> ps = new List<Point>();
        ps.Add(p1);
        ps.Add(p2);
        ps.Add(p3);
        ps.Add(p4);
        ps.Add(p5);
        ps.Add(p6);
        ps.Add(p7);
        ps.Add(p9);

        Graph g = new Graph(ps);

        g.addEdge(p1, p2);
        g.addEdge(p1, p4);
        g.addEdge(p2, p3);
        g.addEdge(p2, p5);
        g.addEdge(p3, p6);
        g.addEdge(p4, p5);
        g.addEdge(p4, p7);
        g.addEdge(p5, p6);
        g.addEdge(p6, p3);
        g.addEdge(p6, p9);


        // DFS dfs = new DFS(g, p1, 2);
        // printListPath(dfs.path);
        // printListPath(dfs.pathVisited);

        Graph input = InputFile.makeGraph();

        input.printNodes();
        input.printAdj();

        Point starting = InputFile.findStartingPoint(input);
        starting.print();
        int treasure = InputFile.findNumberOfTreasure(input);
        Console.WriteLine(treasure);
        DFS dfs2 = new DFS(input, starting, treasure);
        printListPath(dfs2.path);
        printListPath(dfs2.pathVisited);

        // List<Point> bfs = MazeBFS.findTreasureBFS(input);
        // printListPath(bfs);
    }

}
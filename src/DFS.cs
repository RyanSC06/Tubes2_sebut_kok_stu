using System.Collections.Generic;
using System.Diagnostics;

public class DFS{

    private Graph g;

    private List<Point> path = new List<Point>();

    private List<Point> pathVisited = new List<Point>();

    private Stack<Point> pathStack = new Stack<Point>();

    private List<Point> backtrackPath = new List<Point>();

    private int treasureCount = 0;

    private int step = -1;

    private TimeSpan time = new TimeSpan();

    public DFS(Graph g, Point source, int numOfTreasure){
            this.g = g;
            var watch = new Stopwatch();
            watch.Start();
            dfs(source, numOfTreasure);
            watch.Stop();
            time = watch.Elapsed;
            Console.WriteLine("Exec time: " + time.TotalMilliseconds + " ms");
            Console.WriteLine("Step: " + (path.Count - 1));
            Console.WriteLine("Step: " + step);
            Console.WriteLine("Node: " + pathVisited.Count);
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
            Graph.printListPath(backtrackPath);
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
        if (pathStack.Count == 0){
            System.Console.WriteLine("No path found");
            return;
        }
        Point next = pathStack.Pop();
        while (next.Found){
            next = pathStack.Pop();
        }
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
            var back = path[path.IndexOf(source)-1];
            backtrackPath.Add(back);
            back.pointFound();
            if (backtrackStop(back)){
                return;
            }
            var next = path[path.IndexOf(back)-1];
            addBacktrackPathUtil(next);
    }

    private void addBacktrackPathUtil(Point back){
        backtrackPath.Add(back);
        back.pointFound();
        if (backtrackStop(back)){
            return;
        }       
        var next = path[path.IndexOf(back)-1];
        addBacktrackPathUtil(next);
    }


    public static void Main(string[] args){
        Graph input = InputFile.makeGraph();

        input.printNodes();
        input.printAdj();

        Point starting = InputFile.findStartingPoint(input);
        starting.print();
        int treasure = InputFile.findNumberOfTreasure(input);
        Console.WriteLine(treasure);
        DFS dfs2 = new DFS(input, starting, treasure);
        Graph.printListPath(dfs2.path);
        Graph.printListPath(dfs2.pathVisited);

        // List<Point> bfs = MazeBFS.findTreasureBFS(input);
        // printListPath(bfs);
    }

}
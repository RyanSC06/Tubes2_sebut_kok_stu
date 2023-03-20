using System.Collections.Generic;

public class DFS{

    private Graph g;

    private List<Point> pathVisited = new List<Point>();

    private Stack<Point> pathStack = new Stack<Point>();

    private int treasureCount = 0;

    public DFS(Graph g, Point source, int numOfTreasure){
            this.g = g;
            dfs(source, numOfTreasure);
    }

    private void dfs(Point source, int numOfTreasure){
        foreach (Point p in g.getNeighbours(source)){
            pathStack.Push(p);
        }
        Point next = pathStack.Pop();
        pathVisited.Add(next);
        if (source.isAdjacent(next)){
            g.addPath(next);
        }
        if (next.Type == TypeGrid.Treasure){
            treasureCount++;
        }
        if (treasureCount == numOfTreasure){
            return;
        }
        dfs(next, numOfTreasure);
    }

    public void printPath(){
        g.printPath();
    }

    public void printPathVisited(){
        foreach (Point node in pathVisited){
                Console.Write(node.X + "," + node.Y);
                if (node != pathVisited[pathVisited.Count-1])
                Console.Write(" -> ");
            }
        Console.WriteLine();
    }


    public static void Main(string[] args){
        Point p1 = new Point(0, 0, TypeGrid.KrustyKrab);
        Point p2 = new Point(1, 0, TypeGrid.Lintasan);
        Point p3 = new Point(2, 0, TypeGrid.Lintasan);

        Point p4 = new Point(0, 1, TypeGrid.Lintasan);
        Point p5 = new Point(1, 1, TypeGrid.Lintasan);
        Point p6 = new Point(2, 1, TypeGrid.Lintasan);

        Point p7 = new Point(0, 2, TypeGrid.Lintasan);
        Point p8 = new Point(1, 2, TypeGrid.Lintasan);
        Point p9 = new Point(2, 2, TypeGrid.Treasure);

        List<Point> ps = new List<Point>();
        ps.Add(p1);
        ps.Add(p2);
        ps.Add(p3);
        ps.Add(p4);
        ps.Add(p5);
        ps.Add(p6);
        ps.Add(p7);
        ps.Add(p8);
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
        g.addEdge(p5, p8);
        g.addEdge(p6, p9);
        g.addEdge(p7, p8);
        g.addEdge(p8, p9);

        DFS dfs = new DFS(g, p1, 1);
        dfs.printPath();
        dfs.printPathVisited();
    }

}
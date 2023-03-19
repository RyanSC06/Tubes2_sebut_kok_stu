public class Graph{

    public List<Point> nodes {get; set;}

    public Dictionary<Point,List<Point>> adjList {get; set;}

    public Dictionary<Point, Boolean> visited {get; set;}

    public static int node_visited = 0;

    private List<Point> path = new List<Point>(); 

    public Graph(List<Point> nodes){
        this.nodes = nodes;
        adjList = new Dictionary<Point, List<Point>>();
        foreach (Point node in nodes){
            adjList.Add(node, new List<Point>());
        }
        visited = new Dictionary<Point, Boolean>();
        foreach (Point node in nodes){
            visited.Add(node, false);
        }
    }

    public void addEdge(Point source, Point destination){
        adjList[source].Add(destination);
    }

    public void addEdge(Point source, List<Point> destination){
        adjList[source].AddRange(destination);
    }

    public List<Point> getNeighbours(Point node){
        return adjList[node];
    }

    public void setVisited(Point node){
        visited[node] = true;
        node_visited++;
    }

    public bool isVisited(Point node){
        return visited[node];
    }

    public void resetVisited(){
        foreach (Point node in nodes){
            visited[node] = false;
        }
        node_visited = 0;
    }

    public void addPath(Point node){
        path.Add(node);
    }

    public void resetPath(){
        path.Clear();
    }

    public List<Point> getPath(){
        return path;
    }
    public void printPath(){
        foreach (Point node in path){
            Console.Write(node.X + "," + node.Y);
            if (node != path[path.Count-1])
            Console.Write(" -> ");
        }
        Console.WriteLine();
    }

}
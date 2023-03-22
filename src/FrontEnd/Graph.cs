using System;
using System.Collections.Generic;

public class Graph{

    public List<Point> nodes;

    public List<Point> Nodes {
        get { return nodes; }
        set { nodes = value; }
    }

    public Dictionary<Point,List<Point>> adjList;

    public Dictionary<Point, List<Point>> AdjList {
        get { return adjList; }
        set { adjList = value; }
    }

    public Graph(List<Point> nodes){
        this.nodes = nodes;
        adjList = new Dictionary<Point, List<Point>>();
        foreach (Point node in nodes){
            adjList.Add(node, new List<Point>());
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

    public int getNeighboursNotVisited(Point node){
        int n = 0;
        foreach (Point p in adjList[node]){
            if (!p.Found){
                n++;
            }
        }
        return n;
    }

    public void printNodes(){
        foreach (Point node in nodes){
            node.print();
        }
    }

    public void resetGraph(){
        foreach (Point node in nodes){
            node.resetPoint();
        }
    }

    public void printAdj(){
        foreach (Point key in nodes){
            Console.WriteLine("Adjacency List of ");
            key.print();
            Console.WriteLine(" -> ");
            foreach (Point p in adjList[key]){
                p.print();
            }
        }
    }

    public static void printListPath(List<Point> listPath){
        foreach (Point node in listPath){
                Console.Write(node.X + "," + node.Y);
                if (node != listPath[listPath.Count-1])
                Console.Write(" -> ");
        }
        Console.WriteLine();
    }

    /*public static void Main (string[] args){
        // point test
        Point p = new Point(1, 2, TypeGrid.KrustyKrab);
        Console.WriteLine(p.X);
        Console.WriteLine(p.Y);
        Console.WriteLine(p.Type);
        Console.WriteLine(p.Found);

        // set 
        p.X = 3;
        p.Y = 4;
        p.Type = TypeGrid.Treasure;
        p.pointFound();
        Console.WriteLine(p.X);
        Console.WriteLine(p.Y);
        Console.WriteLine(p.Type);
        Console.WriteLine(p.Found);

        // enum test
        Console.WriteLine(TypeGrid.KrustyKrab);
        Console.WriteLine(TypeGrid.Lintasan);
        Console.WriteLine(TypeGrid.Treasure);
        Console.WriteLine(TypeGrid.X);


        // graph test
        Point p1 = new Point(1, 2, TypeGrid.KrustyKrab);
        Point p2 = new Point(3, 4, TypeGrid.Treasure);
        Point p3 = new Point(5, 6, TypeGrid.Lintasan);
        Point p4 = new Point(7, 8, TypeGrid.X);

        List<Point> nodes = new List<Point>();
        nodes.Add(p1);
        nodes.Add(p2);
        nodes.Add(p3);
        nodes.Add(p4);

        Graph g = new Graph(nodes);
        g.addEdge(p1, p2);
        g.addEdge(p1, p3);
        g.addEdge(p1, p4);
        g.addEdge(p2, p3);
        g.addEdge(p2, p4);
        g.addEdge(p3, p4);

        Console.WriteLine(g.getNeighbours(p1).Count);
        Console.WriteLine(g.getNeighbours(p2).Count);
        Console.WriteLine(g.getNeighbours(p3).Count);
        Console.WriteLine(g.getNeighbours(p4).Count);

    }
*/
}
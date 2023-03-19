// See https://aka.ms/new-console-template for more information


// point test
Point p = new Point(1, 2, TypeGrid.KrustyKrab);
Console.WriteLine(p.X);
Console.WriteLine(p.Y);
Console.WriteLine(p.Type);
Console.WriteLine(p.found);

// set 
p.X = 3;
p.Y = 4;
p.Type = TypeGrid.Treasure;
p.pointFound();
Console.WriteLine(p.X);
Console.WriteLine(p.Y);
Console.WriteLine(p.Type);
Console.WriteLine(p.found);

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

Console.WriteLine(g.isVisited(p1));
Console.WriteLine(g.isVisited(p2));
Console.WriteLine(g.isVisited(p3));
Console.WriteLine(g.isVisited(p4));

g.setVisited(p1);
g.setVisited(p2);
g.setVisited(p3);
g.setVisited(p4);

Console.WriteLine(g.isVisited(p1));
Console.WriteLine(g.isVisited(p2));
Console.WriteLine(g.isVisited(p3));
Console.WriteLine(g.isVisited(p4));

g.resetVisited();

Console.WriteLine(g.isVisited(p1));
Console.WriteLine(g.isVisited(p2));
Console.WriteLine(g.isVisited(p3));
Console.WriteLine(g.isVisited(p4));


// path test
g.addPath(p1);
g.addPath(p2);
g.addPath(p3);
g.addPath(p4);

g.printPath();

g.resetPath();

g.printPath();





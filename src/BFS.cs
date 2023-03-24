using System;
using System.IO;

public class MazeBFS{
    public static List<List<Point>> findCheckedBFS (Graph g) {
        if (g.Nodes.Count != 0) {
            g.resetGraph();
            int num_T = InputFile.findNumberOfTreasure(g);
            int num_found = 0;

            Queue<Point> QP = new Queue<Point>(g.Nodes.Count * 10);
            Queue<List<Point>> QLP = new Queue<List<Point>>(g.Nodes.Count * 10);
            List<Point> checkedPath = new List<Point>();

            //INISIALISASI (PENGECEKAN PERTAMA)
            Point now = InputFile.findStartingPoint(g);
            now.Found = true;

            List<Point> path = new List<Point>();
            path.Add(now);
            checkedPath.Add(now);
            
            foreach (Point point in g.AdjList[now]) {
                if (point.Found == false) {
                    QP.Enqueue(point);
                    QLP.Enqueue(path);
                }
            }


            //PENGECEKAN BERIKUTNYA
            while ((num_found < num_T) && (QP.isEmpty() == false)) {
                now = QP.Dequeue();
                now.Found = true;

                List<Point> pathOther = new List<Point>(QLP.Dequeue());
                pathOther.Add(now);
                path = pathOther;
                checkedPath.Add(now);

                if (now.Type == TypeGrid.Treasure) {
                    for (int i = 0; i <= QLP.tail; i++) {
                        List<Point> pathTreasure = new List<Point>(pathOther);
                        QLP.buffer[i] = pathTreasure;
                    }
                    num_found = num_found + 1;
                }


                foreach (Point point in g.AdjList[now]) {
                    if (point.Found == false) {
                        QP.Enqueue(point);
                        List<Point> pathEnqueue = new List<Point>(pathOther);
                        QLP.Enqueue(pathEnqueue);
                    }
                }
            }

            List<List<Point>> returnValue = new List<List<Point>> ();
            returnValue.Add(path);
            returnValue.Add(checkedPath);
            return (returnValue);
        } else {
            List<Point> checkedPath = new List<Point>();
            List<Point> path = new List<Point>();
            List<List<Point>> returnValue = new List<List<Point>> ();
            returnValue.Add(path);
            returnValue.Add(checkedPath);
            return (returnValue);
        }
    }


    public static List<List<Point>> findPathBFS (Graph g) {
        if (g.Nodes.Count != 0) {
            g.resetGraph();
            int num_T = InputFile.findNumberOfTreasure(g);
            int num_found = 0;

            Queue<Point> QP = new Queue<Point>(g.Nodes.Count * 1000);
            Queue<List<Point>> QLP = new Queue<List<Point>>(g.Nodes.Count * 1000);
            List<Point> checkedPath = new List<Point>();

            //INISIALISASI (PENGECEKAN PERTAMA)
            Point now = InputFile.findStartingPoint(g);
            now.Found = true;

            List<Point> path = new List<Point>();
            path.Add(now);
            checkedPath.Add(now);
            
            foreach (Point point in g.AdjList[now]) {
                if (point.Found == false) {
                    QP.Enqueue(point);
                    QLP.Enqueue(path);
                }
            }


            //PENGECEKAN BERIKUTNYA
            while ((num_found < num_T) && (QP.isEmpty() == false)) {
                now = QP.Dequeue();
                now.Found = true;

                List<Point> pathOther = new List<Point>(QLP.Dequeue());
                pathOther.Add(now);
                path = pathOther;
                checkedPath.Add(now);

                if (now.Type == TypeGrid.Treasure) {
                    foreach (Point p in g.Nodes) {
                        if (p.Type != TypeGrid.Treasure) {
                            p.Found = false;
                        }
                    }
                    
                    while (QP.head != -1) {
                        Point trash = QP.Dequeue();
                        List<Point> otherTrash = QLP.Dequeue();
                    }
                    num_found = num_found + 1;
                }


                foreach (Point point in g.AdjList[now]) {
                    if (point.Found == false) {
                        QP.Enqueue(point);
                        List<Point> pathEnqueue = new List<Point>(pathOther);
                        QLP.Enqueue(pathEnqueue);
                    }
                }
            }

            List<List<Point>> returnValue = new List<List<Point>> ();
            returnValue.Add(path);
            returnValue.Add(checkedPath);
            return (returnValue);
        } else {
            List<Point> checkedPath = new List<Point>();
            List<Point> path = new List<Point>();
            List<List<Point>> returnValue = new List<List<Point>> ();
            returnValue.Add(path);
            returnValue.Add(checkedPath);
            return (returnValue);
        }
    }
}

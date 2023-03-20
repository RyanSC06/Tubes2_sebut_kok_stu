using System;
using System.IO;

public class MazeBFS{
    public static List<Point> findTreasureBFS (Graph g) {
        int num_T = InputFile.findNumberOfTreasure(g);
        int num_found = 0;

        Queue<Point> QP = new Queue<Point>(g.Nodes.Count + 50);
        Queue<List<Point>> QLP = new Queue<List<Point>>(g.Nodes.Count + 50);

        //INISIALISASI (PENGECEKAN PERTAMA)
        Point now = InputFile.findStartingPoint(g);
        now.Found = true;

        List<Point> path = new List<Point>();
        path.Add(now);
        
        foreach (Point point in g.AdjList[now]) {
            if (point.Found == false) {
                QP.Enqueue(point);
                Console.Write("[" + point.X + "," + point.Y + "]  melalui  ");
                QLP.Enqueue(path);
                foreach(Point x in path) {
                    Console.Write("[" + x.X + "," + x.Y + "], ");
                }
                Console.Write("\n\n");
            }
        }

        Console.WriteLine("ISI QUEUE 1:");
            for (int i = QP.head; i < QP.tail+1; i++) {
                if (QP.buffer[i] != null) {
                    Console.Write("[" + QP.buffer[i].X + "," + QP.buffer[i].Y + "], ");
                }
            }

            Console.WriteLine("\nISI QUEUE 2:");
            for (int i = QLP.head; i < QLP.tail+1; i++) {
                Console.Write("[");
                if (QLP.buffer[i] != null) {
                    foreach (Point x in QLP.buffer[i]) {
                        Console.Write("[" + x.X + "," + x.Y + "], ");
                    }
                }
                Console.Write("]\n\n");
            }
            Console.WriteLine("#Treasure found = " + num_found);
            Console.WriteLine("\n\n\n");


        //PENGECEKAN BERIKUTNYA
        while ((num_found < num_T) && (QP.isEmpty() == false)) {
            now = QP.Dequeue();
            now.Found = true;

            List<Point> path1 = new List<Point>(QLP.Dequeue());
            path1.Add(now);
            path = path1;

            if (now.Type == TypeGrid.Treasure) {
                for (int i = 0; i <= QLP.tail; i++) {
                    QLP.buffer[i] = path1;
                }
                num_found = num_found + 1;
            }


            foreach (Point point in g.AdjList[now]) {
                if (point.Found == false) {
                    QP.Enqueue(point);
                    Console.Write("[" + point.X + "," + point.Y + "]  melalui  ");
                    QLP.Enqueue(path1);
                    foreach(Point x in path1) {
                        Console.Write("[" + x.X + "," + x.Y + "], ");
                    }
                    Console.Write("\n\n");
                }
            }

            Console.WriteLine("ISI QUEUE 1:");
            for (int i = QP.head; i < QP.tail+1; i++) {
                if (QP.buffer[i] != null) {
                    Console.Write("[" + QP.buffer[i].X + "," + QP.buffer[i].Y + "], ");
                }
            }

            Console.WriteLine("\n\nISI QUEUE 2:");
            for (int i = QLP.head; i < QLP.tail+1; i++) {
                Console.Write("[");
                if (QLP.buffer[i] != null) {
                    foreach (Point x in QLP.buffer[i]) {
                        Console.Write("[" + x.X + "," + x.Y + "], ");
                    }
                }
                Console.Write("]\n\n");
            }
            Console.WriteLine("#Treasure found = " + num_found);
            Console.WriteLine("\n\n\n");
        }

        return (path);
    }
}

using System;

public enum TypeGrid {
    // enum tipe grid yang mungkin dari peta
    KrustyKrab,         // KrustyKrab (Start point)
    Lintasan,           // Lintasan
    Treasure,           // Treasure
    X                   // Bukan Lintasan
}

public class Point {
    private int x;

    public int X {
        get { return x; }
        set { x = value; }
    }

    private int y;

    public int Y {
        get { return y; }
        set { y = value; }
    }

    // contoh x dan y
    // p0 p1 
    // p2 p3 
    // p0 = (0,0)
    // p1 = (0,1)
    // p2 = (1,0)
    // p3 = (1,1)

    private TypeGrid type;

    public TypeGrid Type {
        get { return type; }
        set { type = value; }
    }

    private bool found;

    public bool Found {
        get { return found; }
        set { found = value; }
    }

    public Point(int x, int y, TypeGrid type) {
        X = x;
        Y = y;
        Type = type;
        found = false;
    }

    public void pointFound() {
        // menandakan point sudah ditemukan
        found = true;
    }

    public void resetPoint(){
        // mengembalikan point ke kondisi awal
        found = false;
    }

    public bool isLeft(Point p) {
        return (X == p.X - 1 && Y == p.Y);
    }

    public bool isRight(Point p) {
        return (X == p.X + 1 && Y == p.Y);
    }

    public bool isUp(Point p) {
        return (X == p.X && Y == p.Y - 1);
    }

    public bool isDown(Point p) {
        return (X == p.X && Y == p.Y + 1);
    }

    public bool isAdjacent(Point p) {
        return (isLeft(p) || isRight(p) || isUp(p) || isDown(p));
    }

    public void print(){
        Console.WriteLine("X: " + X + " Y: " + Y + " Type: " + Type + " Found: " + Found);
    }

}
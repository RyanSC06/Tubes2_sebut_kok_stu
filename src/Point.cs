public enum TypeGrid {
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
        found = true;
    }

    public void resetPoint(){
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
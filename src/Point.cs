public enum TypeGrid {
    KrustyKrab,         // KrustyKrab (Start point)
    Lintasan,           // Lintasan
    Treasure,           // Treasure
    X                   // Bukan Lintasan
}

public class Point {
    public int X { get; set; }

    public int Y { get; set; }

    public TypeGrid Type { get; set; }

    public bool found { get; set; }

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

}
using UnityEngine;

public class CubeColor
{
    public int r, g, b;
    public CubeColor(int r, int g, int b)
    { 
        this.r = r;
        this.g = g;
        this.b = b;
    }
    public CubeColor(Color color)
    {
        this.r = (int) (color.r * 255.0f);
        this.g = (int) (color.g * 255.0f);
        this.b = (int) (color.b * 255.0f);
    }
    /* ### Insert custom cube colors here for easy access in script### */
    public static readonly CubeColor red = new CubeColor(255, 0, 0);
    public static readonly CubeColor green = new CubeColor(0, 255, 0);
    public static readonly CubeColor blue = new CubeColor(0, 0, 255);
    public static readonly CubeColor black = new CubeColor(0, 0, 0);
    public static readonly CubeColor orange = new CubeColor(255, 80, 0);

}

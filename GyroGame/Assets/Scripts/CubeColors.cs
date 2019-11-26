using System.Collections;
using System.Collections.Generic;
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
    /* ### Insert custom cube colors here for easy access in script### */
    public static CubeColor red = new CubeColor(255, 0, 0);
    public static CubeColor green = new CubeColor(0, 255, 0);
    public static CubeColor blue = new CubeColor(0, 0, 255);
    public static CubeColor black = new CubeColor(0, 0, 0);
    public static CubeColor orange = new CubeColor(255, 100, 0);

}

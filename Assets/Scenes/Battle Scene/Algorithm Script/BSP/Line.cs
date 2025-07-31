using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    Orientation orientation;
    Vector2Int coordinates;

    public Line(Orientation Orientation, Vector2Int Coordinates)
    {
        orientation = Orientation;
        coordinates = Coordinates;
    }

    public Orientation Orientation
    {
        get => orientation;
        set => orientation = value;
    }
    public Vector2Int Coordinates
    {
        get => coordinates;
        set => coordinates = value;
    }
}

public enum Orientation
{
    horizontal = 0,
    vertical = 1
}

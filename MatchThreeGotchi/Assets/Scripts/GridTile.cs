using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    private GridTile leftNeighbour;
    private GridTile rightNeighbour;
    private GridTile upNeighbour;
    private GridTile downNeighbour;
    private int row;
    private int col;
    private Piece piece;

    public GridTile LeftNeighbour
    {
        get
        {
            return leftNeighbour;
        }

        set
        {
            leftNeighbour = value;
        }
    }

    public GridTile RightNeighbour
    {
        get
        {
            return rightNeighbour;
        }

        set
        {
            rightNeighbour = value;
        }
    }

    public GridTile UpNeighbour
    {
        get
        {
            return upNeighbour;
        }

        set
        {
            upNeighbour = value;
        }
    }

    public GridTile DownNeighbour
    {
        get
        {
            return downNeighbour;
        }

        set
        {
            downNeighbour = value;
        }
    }

    public int Row
    {
        get
        {
            return row;
        }

        set
        {
            row = value;
        }
    }

    public int Col
    {
        get
        {
            return col;
        }

        set
        {
            col = value;
        }
    }

    public Piece Piece {
        get {
            return piece;
        }

        set {
            piece = value;
        }
    }
}

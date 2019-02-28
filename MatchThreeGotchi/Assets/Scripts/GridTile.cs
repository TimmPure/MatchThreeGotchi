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
    private SpriteRenderer sr;

    private static GridTile previousSelected = null;
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private bool isSelected = false;

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

    private void Awake() {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        //TODO: Communicate to some sort of MouseListener? Delegate/Event?

        if (isSelected) {
            //This Piece is already selected and clicked again; deselect it
            Deselect();
        } else if (previousSelected == null) {
            //There is no Piece selected yet; select this
            Select();
        } else if (!BoardController.AreAdjacent(this.gameObject, previousSelected.gameObject)) {
            //The second selected Piece is not adjacent, we select it instead of the previous one
            previousSelected.Deselect();
            Select();
        } else {
            //This second Piece is adjacent; we deselect the first one and swap
            BoardController.instance.SwapPieces(this.gameObject, previousSelected.gameObject);
            previousSelected.Deselect();
        }
    }

    private void Select() {
        isSelected = true;
        sr.color = selectedColor;
        previousSelected = this;
    }

    private void Deselect() {
        isSelected = false;
        sr.color = Color.white;
        previousSelected = null;
    }
}

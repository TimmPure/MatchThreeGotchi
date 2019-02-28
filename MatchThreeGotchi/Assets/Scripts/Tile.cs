using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private Tile leftNeighbour;
    private Tile rightNeighbour;
    private Tile upNeighbour;
    private Tile downNeighbour;
    private int row;
    private int col;
    private Piece piece;
    private SpriteRenderer sr;

    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private bool isSelected = false;

    public delegate void OnTileClicked(Tile tile);
    public event OnTileClicked onTileClicked;

    public Tile LeftNeighbour {
        get {
            return leftNeighbour;
        }

        set {
            leftNeighbour = value;
        }
    }
    public Tile RightNeighbour {
        get {
            return rightNeighbour;
        }

        set {
            rightNeighbour = value;
        }
    }
    public Tile UpNeighbour {
        get {
            return upNeighbour;
        }

        set {
            upNeighbour = value;
        }
    }
    public Tile DownNeighbour {
        get {
            return downNeighbour;
        }

        set {
            downNeighbour = value;
        }
    }
    public int Row {
        get {
            return row;
        }

        set {
            row = value;
        }
    }
    public int Col {
        get {
            return col;
        }

        set {
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
        if(sr == null) {
            Debug.LogWarning("Could not find SpriteRenderer on " + this);
        }
    }

    private void OnMouseDown() {
        if(onTileClicked == null) {
            Debug.LogWarning(this + " Tried to run onTileClicked, but it is null.");
            return;
        }
        onTileClicked(this);
    }

    public void Select() {
        isSelected = true;
        sr.color = selectedColor;
    }

    public void Deselect() {
        isSelected = false;
        sr.color = Color.white;
    }

    public void SwapPieces(Tile other) {
        Piece temp = other.Piece;
        other.Piece = this.Piece;
        this.Piece = temp;

        Vector2 tempPos = other.Piece.transform.position;
        other.Piece.transform.position = this.Piece.transform.position;
        this.Piece.transform.position = tempPos;
    }

    public void DestroyPiece() {
        if (Piece != null) {
            Destroy(Piece.gameObject);
            Piece = null;
        }
    }
}

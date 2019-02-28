using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public Sprite[] flavours;
    public GameObject tilePrefab;
    public GameObject piecePrefab;
    public Tile[,] grid;
    public static int cols = 9;
    public static int rows = 9;

    private Tile selectedTile = null;
    private List<Tile> destroyListHorizontal = new List<Tile>();
    private List<Tile> destroyListVertical = new List<Tile>();

    void Start() {
        grid = new Tile[cols, rows];
        FillGridWithTiles();
        SetTileNeighbours();
        SpawnPieces();
    }

    void FillGridWithTiles() {
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                GameObject obj = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity, this.gameObject.transform) as GameObject;
                obj.name = "( " + i + " , " + j + " )";
                Tile objTile = obj.GetComponent<Tile>();
                objTile.onTileClicked += OnTileClicked;
                objTile.Row = j;
                objTile.Col = i;
                grid[i, j] = objTile;
            }
        }
    }

    void SetTileNeighbours() {
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                if (i > 0) {
                    grid[i, j].LeftNeighbour = grid[i - 1, j];
                }
                if (i < cols - 1) {
                    grid[i, j].RightNeighbour = grid[i + 1, j];
                }
                if (j < rows - 1) {
                    grid[i, j].UpNeighbour = grid[i, j + 1];
                }
                if (j > 0) {
                    grid[i, j].DownNeighbour = grid[i, j - 1];
                }
            }
        }
    }

    //void SpawnPiece(Tile tile) {
        
    //}

    void SpawnPieces() {
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                GameObject obj = Instantiate(piecePrefab, new Vector2(i, j), Quaternion.identity, this.transform) as GameObject;
                obj.name = "Piece ( " + i + " , " + j + " )";
                int flavourIndex = Random.Range(0, flavours.Length);
                obj.GetComponent<Piece>().Flavour = flavours[flavourIndex];
                obj.GetComponent<Piece>().FlavourIndex = flavourIndex;
                grid[i, j].Piece = obj.GetComponent<Piece>();
            }
        }
    }

    public static bool AreAdjacent(GameObject obj1, GameObject obj2) {
        if (obj1 == obj2) {
            Debug.LogWarning("BC.AreAdjacent() is checking if obj is adjacent to itsself...");
            return false;
        }
        return (obj1.transform.position.x == obj2.transform.position.x ||
                    obj1.transform.position.y == obj2.transform.position.y)
                    && Mathf.Abs(obj1.transform.position.x - obj2.transform.position.x) <= 1
                    && Mathf.Abs(obj1.transform.position.y - obj2.transform.position.y) <= 1;
    }

    void OnTileClicked(Tile tile) {
        if (tile.Piece == null) {
            Debug.Log("Empty tile selected.");
            return;
        }
        if (selectedTile == tile) {
            //This Tile is already selected and clicked again; deselect it
            tile.Deselect();
            selectedTile = null;
        } else if (selectedTile == null) {
            //There is no Tile selected yet; select this
            tile.Select();
            selectedTile = tile;
        } else if (!AreAdjacent(tile.gameObject, selectedTile.gameObject)) {
            //The second selected Tile is not adjacent, we select it instead of the previous one
            selectedTile.Deselect();
            tile.Select();
            selectedTile = tile;
        } else {
            //This second Piece is adjacent; we deselect the first one and swap

            selectedTile.SwapPieces(tile);

            bool matchFound = CheckMatches(tile) || CheckMatches(selectedTile);
            Debug.Log(matchFound);

            if (!matchFound) {

                //Check if the swap makes a match, if not we swap back/don't swap
                selectedTile.SwapPieces(tile);
                
            }

            selectedTile.Deselect();
            selectedTile = null;

            DestroyMatchedPieces();
        }
    }

    bool CheckMatches(Tile tile) {
        int flavour = tile.Piece.FlavourIndex;
        int horizontalCounter = 1;
        int verticalCounter = 1;
        Tile inspectedTile = tile;

        

        destroyListHorizontal.Add(tile);

        while (inspectedTile.RightNeighbour != null && inspectedTile.RightNeighbour.Piece != null && inspectedTile.RightNeighbour.Piece.FlavourIndex == flavour) {
            inspectedTile = inspectedTile.RightNeighbour;
            horizontalCounter++;
            destroyListHorizontal.Add(inspectedTile);
        }

        inspectedTile = tile;

        while (inspectedTile.LeftNeighbour != null && inspectedTile.LeftNeighbour.Piece != null && inspectedTile.LeftNeighbour.Piece.FlavourIndex == flavour) {
            inspectedTile = inspectedTile.LeftNeighbour;
            horizontalCounter++;
            destroyListHorizontal.Add(inspectedTile);
        }


        destroyListVertical.Add(tile);
        

        inspectedTile = tile;

        while (inspectedTile.DownNeighbour != null && inspectedTile.DownNeighbour.Piece != null && inspectedTile.DownNeighbour.Piece.FlavourIndex == flavour) {
            inspectedTile = inspectedTile.DownNeighbour;
            verticalCounter++;
            destroyListVertical.Add(inspectedTile);
        }

        inspectedTile = tile;

        while (inspectedTile.UpNeighbour != null && inspectedTile.UpNeighbour.Piece != null && inspectedTile.UpNeighbour.Piece.FlavourIndex == flavour) {
            inspectedTile = inspectedTile.UpNeighbour;
            verticalCounter++;
            destroyListVertical.Add(inspectedTile);
        }

        if (verticalCounter < 3) {
            destroyListVertical.Clear();
        }

        if (horizontalCounter < 3) {
            destroyListHorizontal.Clear();
        }

        return (verticalCounter >= 3 || horizontalCounter >= 3);

    }

    void DestroyMatchedPieces() {
        foreach (Tile tile in destroyListHorizontal) {
            tile.DestroyPiece();
        }

        foreach (Tile tile in destroyListVertical) {
            tile.DestroyPiece();
        }
        
    }

    //void CollapseColumn(int i) {
    //    //Determine upperTile
    //    Tile upperTile;

    //    do {
    //        Tile inspectedTile = upperTile;

    //        while (inspectedTile.UpNeighbour != null) {
    //            inspectedTile.SwapPieces(inspectedTile.UpNeighbour);
    //            inspectedTile = inspectedTile.UpNeighbour;
    //        }
    //        SpawnPiece(inspectedTile);
    //        if(upperTile.DownNeighbour != null) {
    //            upperTile = upperTile.DownNeighbour;
    //        }
    //    } while (upperTile.DownNeighbour != null && upperTile.DownNeighbour.Piece == null);

    //    destroyListHorizontal.Clear();
    //    destroyListVertical.Clear();
    //}

}

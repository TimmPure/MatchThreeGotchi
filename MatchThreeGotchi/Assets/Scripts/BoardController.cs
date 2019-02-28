using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public Sprite[] flavours;
    public GameObject tilePrefab;
    public GameObject piecePrefab;
    public GridTile[,] grid;
    public static int cols = 9;
    public static int rows = 9;

    private GridTile selectedTile = null;

    void Start() {
        grid = new GridTile[cols, rows];
        FillGridWithTiles();
        SetTileNeighbours();
        SpawnPieces();
    }

    void FillGridWithTiles() {
        for (int i = 0; i < cols; i++) {
            for (int j = 0; j < rows; j++) {
                GameObject obj = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity, this.gameObject.transform) as GameObject;
                obj.name = "( " + i + " , " + j + " )";
                GridTile objTile = obj.GetComponent<GridTile>();
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

    void OnTileClicked(GridTile tile) {
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
            selectedTile.Deselect();
            selectedTile = null;
        }
    }
}

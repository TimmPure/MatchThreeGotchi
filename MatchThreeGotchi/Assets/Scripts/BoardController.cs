using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public Sprite[] flavours;
    public GameObject tilePrefab;
    public GameObject piecePrefab;
    public GridTile[,] grid;
    public GameObject[,] pieces;
    public static int cols = 9;
    public static int rows = 9;
    public static BoardController instance;

    void Start() {
        instance = this;
        grid = new GridTile[cols, rows];
        pieces = new GameObject[cols, rows];
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
                GameObject obj = Instantiate(piecePrefab, new Vector2(i, j), Quaternion.identity, grid[i, j].transform) as GameObject;
                obj.name = "Piece ( " + i + " , " + j + " )";
                int flavourIndex = Random.Range(0, flavours.Length);
                obj.GetComponent<Piece>().Flavour = flavours[flavourIndex];
                obj.GetComponent<Piece>().FlavourIndex = flavourIndex;
                pieces[i, j] = obj;
            }
        }
    }

    public void SwapPieces(GameObject p1, GameObject p2) {
        if (!AreAdjacent(p1, p2)) {
            return;
        }

        Vector2 temp = p2.transform.position;
        Transform tempParent = p2.transform.parent;

        p2.transform.position = p1.transform.position;
        p2.transform.parent = p1.transform.parent;

        p1.transform.position = temp;
        p1.transform.parent = tempParent;
    }

    //Are TILES adjacent? I.e. is tileA a nieghbour of tileB?
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
}

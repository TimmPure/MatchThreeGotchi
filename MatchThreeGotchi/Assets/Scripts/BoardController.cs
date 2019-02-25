using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public Sprite[] flavours;
    public GameObject tilePrefab;
    public GameObject piecePrefab;
    public GameObject[,] grid;
    public GameObject[,] pieces;
    public static int cols = 9;
    public static int rows = 9;
    public static BoardController instance;

	void Start () {
        instance = this;
        grid = new GameObject[cols, rows];
        pieces = new GameObject[cols, rows];
        FillGridWithTiles();
        SetTileNeighbours();
        SpawnPieces();
	}
	
	void FillGridWithTiles()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector2(i, j), Quaternion.identity, this.gameObject.transform) as GameObject;
                obj.name = "( " + i + " , " + j + " )";
 //               obj.GetComponent<SpriteRenderer>().sprite = flavours[Random.Range(0, flavours.Length)];
                grid[i, j] = obj;
            }
        }
    }

    void SetTileNeighbours()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (i > 0)
                {
                grid[i, j].GetComponent<GridTile>().LeftNeighbour = grid[i - 1, j].GetComponent<GridTile>();
                }
                if(i < cols -1)
                {
                grid[i, j].GetComponent<GridTile>().RightNeighbour = grid[i + 1, j].GetComponent<GridTile>();
                }
                if(j < rows -1)
                {
                grid[i, j].GetComponent<GridTile>().UpNeighbour = grid[i, j + 1].GetComponent<GridTile>();
                }
                if(j > 0)
                {
                grid[i, j].GetComponent<GridTile>().DownNeighbour = grid[i, j - 1].GetComponent<GridTile>();
                }
            }
        }
    }

    void SpawnPieces()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(piecePrefab, new Vector2(i, j), Quaternion.identity, grid[i,j].transform) as GameObject;
                obj.name = "Piece ( " + i + " , " + j + " )";
                obj.GetComponent<Piece>().Flavour = flavours[Random.Range(0, flavours.Length)];
                pieces[i, j] = obj;
            }
        }
    }

    public void SwapPieces(GameObject p1, GameObject p2)
    {
        Vector2 temp = p2.transform.position;
        Transform tempParent = p2.transform.parent;

        p2.transform.position = p1.transform.position;
        p2.transform.parent = p1.transform.parent;

        p1.transform.position = temp;
        p1.transform.parent = tempParent;
    }
}

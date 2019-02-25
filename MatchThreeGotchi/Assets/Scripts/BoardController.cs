using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    public Sprite[] flavours;
    public static int cols = 9;
    public static int rows = 9;
    public GameObject[,] grid;
    public GameObject tilePrefab;
    public static BoardController instance;

	void Start () {
        instance = this;
        grid = new GameObject[cols, rows];

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {

                GameObject obj = Instantiate(tilePrefab,new Vector2(i,j),Quaternion.identity,this.gameObject.transform) as GameObject;
                obj.name = "( " + i + " , " + j + " )";
                obj.GetComponent<SpriteRenderer>().sprite = flavours[Random.Range(0, flavours.Length)];
                grid[i, j] = obj;
            }
        }
	}
	
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.orthographicSize = BoardController.rows / 2f;
        this.transform.position = new Vector3(BoardController.cols / 2f - 0.5f, BoardController.rows / 2f -0.5f, -10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

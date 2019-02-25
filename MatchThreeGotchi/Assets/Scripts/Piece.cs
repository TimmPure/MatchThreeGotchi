using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    private Sprite flavour;
    private SpriteRenderer sr;
    public int row;
    public int col;

    public Sprite Flavour
    {
        get
        {
            return flavour;
        }

        set
        {
            flavour = value;
            sr.sprite = flavour;
        }
    }

    private void Awake()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }
    
}

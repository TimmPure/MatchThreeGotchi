using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    private Sprite flavour;
    private SpriteRenderer sr;

    private static Piece previousSelected = null;
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private bool isSelected = false;

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

    private void OnMouseDown()
    {
        if (isSelected)
        {
            //This Piece is already selected and clicked again; deselect it
            Deselect();
        } else {
            if (previousSelected == null)
            {
                //There is no Piece selected yet; select it
                Select();
            } else {
                //This is the second piece selected; we deselect the first one and swap if adjacent
                //TODO: Swap here!
                previousSelected.Deselect();
            }
        }
    }

    private void Select()
    {
        isSelected = true;
        sr.color = selectedColor;
        previousSelected = this;
    }

    private void Deselect()
    {
        isSelected = false;
        sr.color = Color.white;
        previousSelected = null;
    }
}

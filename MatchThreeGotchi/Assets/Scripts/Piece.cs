using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    private int flavourIndex;
    private Sprite flavour;
    private SpriteRenderer sr;

    private static Piece previousSelected = null;
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private bool isSelected = false;

    public Sprite Flavour {
        get {
            return flavour;
        }

        set {
            flavour = value;
            sr.sprite = flavour;
        }
    }

    public int FlavourIndex {
        get {
            return flavourIndex;
        }

        set {
            flavourIndex = value;
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

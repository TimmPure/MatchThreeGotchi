using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    private int flavourIndex;
    private Sprite flavour;
    private SpriteRenderer sr;


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

}

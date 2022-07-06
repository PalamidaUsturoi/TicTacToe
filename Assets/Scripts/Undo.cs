using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Undo : MonoBehaviour {
    private void Start() {
        GetComponent<TMP_Text>().enabled = false;
    }

    private void OnMouseDown() {
        if ( !GameObject.Find("Square (0)").GetComponent<Cells>().IsGameOver() ) {
            int last = GameObject.Find("Square (0)").GetComponent<Cells>().GetLast();
            GameObject.Find("Square (" + last + ")").GetComponent<Cells>().Undo();
        }
        // Debug.Log( "UNDOOOOOOOOOOOOOOOOOOO" );
    }
}

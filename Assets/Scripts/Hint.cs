using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    private void OnMouseDown() {
        GameObject.Find("Square (0)").GetComponent<Cells>().ShowHint();
        // Debug.Log("oke");
    }
}

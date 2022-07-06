using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Restart : MonoBehaviour {
    private void Start() {
        GetComponent<TMP_Text>().enabled = false;
    }

    public void ShowText() {
        if (GameObject.Find("Square (0)").GetComponent<Cells>().IsGameOver())
            GetComponent<TMP_Text>().enabled = true;
    }

    private void OnMouseDown() {
        if (GameObject.Find("Square (0)").GetComponent<Cells>().IsGameOver()) {
            GameObject.Find("Square (0)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (1)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (2)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (3)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (4)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (5)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (6)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (7)").GetComponent<Cells>().SetUp();
            GameObject.Find("Square (8)").GetComponent<Cells>().SetUp();
        }
    }
}

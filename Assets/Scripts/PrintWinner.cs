using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintWinner : MonoBehaviour {

    private void Start() {
        GetComponent<TMP_Text>().enabled = false;
    }

    public void PrintResult(string msg) {
        GetComponent<TMP_Text>().text = msg;
        GetComponent<TMP_Text>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    private float timeLeft;
    private bool isOver;
    private void Start() {
        timeLeft = 3;
        isOver = false;
    }

    public void RestartTimer() {
        timeLeft = 3;
        isOver = false;
    }

    public void StopTimer() {
        timeLeft = 3;
        isOver = true;
    }

    private void Update() {
        if ( !isOver ) {
            if ( timeLeft > 0 ) 
                timeLeft -= Time.deltaTime;
            else {
                timeLeft = 3;
                isOver = true;
            }
        }
        else if ( !GameObject.Find("Square (0)").GetComponent<Cells>().IsGameOver() )
            GameObject.Find("Square (0)").GetComponent<Cells>().LoseGame();
        transform.GetComponent<TMP_Text>().text = "Timer\n" + timeLeft.ToString("F2");
    }
}

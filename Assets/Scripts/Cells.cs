using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cells : MonoBehaviour {
    private static int[,] matrix = new int[3,3];
    public static int count;
    private static int[] score = new int[2];
    private static char[] codif = new char[2];
    private static bool gameOver;

    private static int last;

    public int GetLast() {
        return last;
    }

    private void Start() {
        score[0] = score[1] = 0;
        SetUp();
    }

    public bool IsGameOver() {
        return gameOver;
    }

    public void SetUp() {
        codif['X' % 2] = 'X';
        codif['O' % 2] = 'O';
        transform.GetChild(0).GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("Score").GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("Restart").GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("Undo").GetComponent<TMP_Text>().enabled = false;
        for ( int l = 0; l < 3; l ++ )
            for ( int c = 0; c < 3; c ++ )
                matrix[l, c] = 15;
        count = 0;
        gameOver = false;
        last = -1;
        GameObject.Find("Timer").GetComponent<Timer>().RestartTimer();
    }

    private int WhoWon() {
        int prod;
        for ( int l = 0; l < 3; l ++ ) {
            prod = 1;
            for ( int c = 0; c < 3; c ++ )
                prod *= matrix[l, c];
            if ( prod == 1 )
                return 1;
            else if ( prod == 8 )
                return 2;
        }
        for ( int c = 0; c < 3; c ++ ) {
            prod = 1;
            for ( int l = 0; l < 3; l ++ )
                prod *= matrix[l, c];
            if ( prod == 1 )
                return 1;
            else if ( prod == 8 )
                return 2;
        }
        prod = 1;
        for ( int l = 0; l < 3; l ++ )
            prod *= matrix[l, l];
        if ( prod == 1 )
            return 1;
        else if ( prod == 8 )
            return 2;
        prod = 1;
        for ( int l = 0; l < 3; l ++ )
            prod *= matrix[l, 2 - l];
        if ( prod == 1 )
            return 1;
        else if ( prod == 8 )
            return 2;

        return -1;
    }

    public void Undo() {
        if ( last >= 0 ) {
            matrix[last / 3, last % 3] = 15;
            transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;
            count --;
        }
    }

    public void LoseGame() {
        gameOver = true;
        GameObject.Find("Score").GetComponent<PrintWinner>().PrintResult("Winner: " + codif[1 - count % 2]);
        GameObject.Find("Restart").GetComponent<Restart>().ShowText();
        // Debug.Log("lost");
    }

    private int GetHint() {
        int nr = Random.Range(0, 9);
        while ( matrix[nr / 3, nr % 3] != 15 )
            nr = Random.Range(0, 9);
        return nr;
    }

    public void ShowHint() {
        StartCoroutine(ShowHintAnimation());
        // ShowHintAnimation();
    }

    public IEnumerator ShowHintAnimation() {
        int nr = GetHint();
        GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "" + codif[count % 2];
        GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().enabled = true;
        
        yield return new WaitForSeconds(0.5f);

        GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "";
        GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().enabled = false;
    }

    // public void ShowHintAnimation() {
    //     int nr = GetHint();
    //     Debug.Log("ShowHintAnimation");
    //     // GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "" + codif[count % 2];
    //     GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "?";
    //     GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().enabled = true;
        
    //     // GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text = "";
    //     // GameObject.Find("Square (" + nr + ")" ).GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().enabled = false;
    // }

    private void OnMouseDown() {
        if ( !gameOver ) {
            string str = transform.GetChild(0).name;
            int nr = str[str.Length - 2] - '0';
            if (matrix[nr / 3, nr % 3] == 15) {
                transform.GetChild(0).GetComponent<TMP_Text>().text = "" + codif[count % 2];
                transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;
                matrix[nr / 3, nr % 3] = (count % 2) + 1;
                count ++;
                GameObject.Find("Timer").GetComponent<Timer>().RestartTimer();

                last = nr;
                
                int win = WhoWon();
                // Debug.Log(win + " " + count);
                if ( win != -1 ) {
                    score[win - 1] ++;
                    GameObject.Find("Score").GetComponent<PrintWinner>().PrintResult("Winner: " + codif[win - 1]);
                    GameObject.Find("Leaderboard").GetComponent<TMP_Text>().text = score[0] + " - " + score[1];
                    gameOver = true;
                }
                else if ( count >= 9 ) {
                    // Debug.Log("ajunge boss");
                    GameObject.Find("Score").GetComponent<PrintWinner>().PrintResult("Draw");
                    GameObject.Find("Leaderboard").GetComponent<TMP_Text>().text = score[0] + " - " + score[1];
                    gameOver = true;
                }

                if ( gameOver ) {
                    GameObject.Find("Restart").GetComponent<Restart>().ShowText();
                    GameObject.Find("Timer").GetComponent<Timer>().StopTimer();
                }
            }
            GameObject.Find("Undo").GetComponent<TMP_Text>().enabled = true;
        }
    }
}

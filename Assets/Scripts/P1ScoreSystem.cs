using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ScoreSystem : MonoBehaviour
{
    public static int p1Score;
    public Text p1Points;
    public Text winningText;

    void Start()
    {
        p1Score = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TheBall"))
        {
            StartCoroutine(WinningText());
            p1Score++;
            SetScore();
        }
    }

    void SetScore()
    {
        p1Points.text = p1Score.ToString();
    }

    IEnumerator WinningText()
    {
        winningText.gameObject.SetActive(true);
        winningText.GetComponent<Animator>().Play("WinningText");
        yield return new WaitForSeconds(5);
        winningText.gameObject.SetActive(false);
    }
}

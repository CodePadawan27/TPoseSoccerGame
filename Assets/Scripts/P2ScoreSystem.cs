using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ScoreSystem : MonoBehaviour
{
    public static int p2Score;
    public Text p2Points;
    public Text winningText;

    // Use this for initialization
    void Start()
    {
        p2Score = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TheBall"))
        {
            StartCoroutine(WinningText());
            p2Score++;
            SetScore();
        }
    }

    void SetScore()
    {
        p2Points.text = p2Score.ToString();
    }

    IEnumerator WinningText()
    {
        winningText.gameObject.SetActive(true);
        winningText.GetComponent<Animator>().Play("WinningText");
        yield return new WaitForSeconds(5);
        winningText.gameObject.SetActive(false);
    }
}

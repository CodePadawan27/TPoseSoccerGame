using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera p1Camera;
    public Camera p2Camera;
    public Text winningText;
    public Text playAgain;

    private int howManyRounds = MenuScript_howmanygoals.goalInput;
    private int p1Score = P1ScoreSystem.p1Score;
    private int p2Score = P2ScoreSystem.p2Score;

    public GameObject statTexts;
    public GameObject player1;
    public GameObject player2;

    public GameObject P1RunpowerBar;
    public GameObject P2RunpowerBar;

    void Start()
    {
        if (statTexts.activeInHierarchy)
        {
            statTexts.SetActive(false);
        }
        Cursor.visible = false;

        //var doo = Input.GetJoystickNames();

        //foreach (var item in doo)
        //{
        //    print(item.ToString());
        //}
        
    }

    void Update()
    {
        //print(howManyRounds.ToString());
        p1Score = P1ScoreSystem.p1Score;
        p2Score = P2ScoreSystem.p2Score;

        if ((p1Score == howManyRounds && howManyRounds > 0) ||
            (!GameObject.Find("man_02").GetComponent<Player2Controller>().heart1 &&
            !GameObject.Find("man_02").GetComponent<Player2Controller>().heart2 &&
            !GameObject.Find("man_02").GetComponent<Player2Controller>().heart3))
        {
            P1Wins();
        }
        else if ((p2Score == howManyRounds && howManyRounds > 0) ||
            (!GameObject.Find("man_01").GetComponent<PlayerController>().heart1 &&
            !GameObject.Find("man_01").GetComponent<PlayerController>().heart2 &&
            !GameObject.Find("man_01").GetComponent<PlayerController>().heart3))
        {
            P2Wins();
        }

    }

    void P1Wins()
    {
        P1RunpowerBar.SetActive(false);
        P2RunpowerBar.SetActive(false);
        GameObject.Find("StatisticsManager").GetComponent<Statistics>()._gameEnded = true;
        Vector3 topCam = new Vector3(0f, 30f, -9.53f);
        GetComponent<PauseGame>().enabled = false;
        statTexts.SetActive(true);
        DestroyHearts();
        GameObject.Find("man_01").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("man_02").GetComponent<Player2Controller>().enabled = false;
        GameObject.Find("P1points").GetComponent<Text>().enabled = false;
        GameObject.Find("P2points").GetComponent<Text>().enabled = false;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, topCam, 0.01f);
        mainCamera.GetComponent<BackgroundColorChanger>().enabled = true;

        //mainCamera.enabled = false;
        //p1Camera.enabled = true;
        //p2Camera.enabled = false;
        winningText.text = "Player 1 wins!";
        StartCoroutine(FadeInText(winningText, 6));
        PlayAgain();
    }

    void P2Wins()
    {
        P1RunpowerBar.SetActive(false);
        P2RunpowerBar.SetActive(false);
        GameObject.Find("StatisticsManager").GetComponent<Statistics>()._gameEnded = true;
        Vector3 topCam = new Vector3(0f, 30f, -9.53f);
        GetComponent<PauseGame>().enabled = false;
        statTexts.SetActive(true);
        DestroyHearts();
        GameObject.Find("man_01").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("man_02").GetComponent<Player2Controller>().enabled = false;
        GameObject.Find("P1points").GetComponent<Text>().enabled = false;
        GameObject.Find("P2points").GetComponent<Text>().enabled = false;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, topCam, 0.01f);
        mainCamera.GetComponent<BackgroundColorChanger>().enabled = true;

        //mainCamera.enabled = false;
        //p1Camera.enabled = false;
        //p2Camera.enabled = true;
        winningText.text = "Player 2 wins!";
        StartCoroutine(FadeInText(winningText, 6));
        PlayAgain();
    }

    //Fades text in with given time
    IEnumerator FadeInText(Text textToFadeIn, int time)
    {
        // loop over 3 second
        for (float i = 0; i <= time; i += Time.deltaTime)
        {
            // set color with i as alpha
            textToFadeIn.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    void PlayAgain()
    {
        playAgain.text = "Play again? y/n";
        StartCoroutine(FadeInText(playAgain, 12));
        if (Input.GetKeyDown(KeyCode.Y) || (Input.GetButtonDown("Submit")))
        {
            SceneManager.LoadScene("Game");
        }
        else if (Input.GetKeyDown(KeyCode.N) || (Input.GetButtonDown("Cancel")))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void DestroyHearts()
    {
        Destroy(GameObject.Find("P1heart1"));
        Destroy(GameObject.Find("P1heart2"));
        Destroy(GameObject.Find("P1heart3"));
        Destroy(GameObject.Find("P2heart1"));
        Destroy(GameObject.Find("P2heart2"));
        Destroy(GameObject.Find("P2heart3"));
    }
}

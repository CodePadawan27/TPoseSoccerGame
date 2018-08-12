using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{

    //Stats texts
    public Text statsGoalsP1;
    public Text statsGoalsP2;
    public Text statsHitsToTheBallP1;
    public Text statsHitsToTheBallP2;
    public Text statsMuggedBySupaBearP1;
    public Text statsMuggedBySupaBearP2;
    public Text timerText;
    public Text statusFallsP1;
    public Text statusFallsP2;
    private float _startTime;
    public bool _gameEnded = false;

    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        _startTime = Time.time;
        player1 = GameObject.Find("man_01");
        player2 = GameObject.Find("man_02");
    }

    void Update()
    {
        if (!_gameEnded)
        {
            Timer();
            UpdateStatTexts();
        }
    }

    void Timer()
    {
        float t = Time.time - _startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = minutes + " minutes " + seconds + " seconds";
    }

    void UpdateStatTexts()
    {
        statsHitsToTheBallP1.text = player1.GetComponent<PlayerController>().hitsToTheBall.ToString();
        statsHitsToTheBallP2.text = player2.GetComponent<Player2Controller>().hitsToTheBall.ToString();
        statsMuggedBySupaBearP1.text = player1.GetComponent<PlayerController>().hitsToTheBear.ToString();
        statsMuggedBySupaBearP2.text = player2.GetComponent<Player2Controller>().hitsToTheBear.ToString();
        statsGoalsP1.text = P1ScoreSystem.p1Score.ToString();
        statsGoalsP2.text = P2ScoreSystem.p2Score.ToString();
        statusFallsP1.text = player1.GetComponent<PlayerController>().falls.ToString();
        statusFallsP2.text = player2.GetComponent<Player2Controller>().falls.ToString();
    }
}

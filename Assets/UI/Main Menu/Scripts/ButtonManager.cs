using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject man01;
    public GameObject man02;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject howManyGoals;
    public GameObject howManyGoalsInput;
    public GameObject howManyGoalsStart;
    public bool playbuttonPressed = false;

    public void NewGameButton(string newGame)
    {
        playbuttonPressed = true;
        man01.GetComponent<Animator>().Play("Reverse");
        man02.GetComponent<Animator>().Play("Reverse");
        playButton.GetComponent<Animator>().Play("Reverse");
        exitButton.GetComponent<Animator>().Play("Reverse");
        howManyGoals.GetComponent<Animator>().Play("HowManyGoals");
        howManyGoalsInput.GetComponent<Animator>().Play("HowManyGoalsInput");

    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void StartGame(string newGame)
    {
        SceneManager.LoadScene(newGame);
    }
}

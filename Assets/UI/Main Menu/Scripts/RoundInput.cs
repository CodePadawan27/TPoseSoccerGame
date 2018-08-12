using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundInput : MonoBehaviour
{
    public static int goalInput;
    public Text goals;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    private GameObject startButton;
    private GameObject buttonManager;

    public GameObject man01;
    public GameObject man02;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject howManyGoals;
    public GameObject howManyGoalsInput;
    public GameObject howManyGoalsStart;


    private bool axisInUse = false;

    void Start()
    {
        startButton = GameObject.Find("StartGame");
        buttonManager = GameObject.Find("ButtonManager");
        startButton.SetActive(false);
    }

    void Update()
    {
        if (buttonManager.GetComponent<ButtonManager>().playbuttonPressed)
            SetGoals();

        //else if(Input.GetButtonDown("Cancel"))
        //{
        //    PlayAnimationIfBackToRoot();
        //}
    }

    //If player presses cancel, scene goes back to root level
    void PlayAnimationIfBackToRoot()
    {
        print("joy");
        buttonManager.GetComponent<ButtonManager>().playbuttonPressed = false;
        man01.GetComponent<Animator>().Play("Man_01");
        man02.GetComponent<Animator>().Play("Man_02");
        playButton.GetComponent<Animator>().Play("Button_play");
        exitButton.GetComponent<Animator>().Play("Button_exit");
        howManyGoals.GetComponent<Animator>().Play("Reverse");
        howManyGoalsInput.GetComponent<Animator>().Play("Reverse");
    }

    //Sets the goal amount for the match
    void SetGoals()
    {

        //for (int i = 0; i < keyCodes.Length; i++)
        //{
        //    if (Input.GetKeyDown(keyCodes[i]))
        //    {
        //        startButton.SetActive(true);
        //        startButton.GetComponent<Button>().Select();
        //        int numberPressed = i + 1;
        //        goals.text = numberPressed.ToString();
        //        goalInput = numberPressed;
        //    }

        //}

        if (Input.GetKeyDown("up") || Input.GetAxisRaw("VerticalP1") == 1)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                goalInput += 1;
                goals.text = goalInput.ToString();
                startButton.SetActive(true);
                startButton.GetComponent<Button>().Select();
            }
        }

        else if (Input.GetAxisRaw("VerticalP1") == 0)
        {
            axisInUse = false;
        }

        else if ((Input.GetKeyDown("down") || Input.GetAxisRaw("VerticalP1") == -1) && !(goalInput <= 0))
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                goalInput -= 1;
                goals.text = goalInput.ToString();
                startButton.SetActive(true);
                startButton.GetComponent<Button>().Select();
            }

        }

        else
        {
            startButton.SetActive(false);
        }
    }


}

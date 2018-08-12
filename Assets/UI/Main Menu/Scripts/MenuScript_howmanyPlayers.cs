using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript_howmanyPlayers : MonoBehaviour
{
    public EventSystem currentEventSystem;
    public Slider howManyPlayersSlider;
    public Slider startMenuSlider;

    //Menu gameobjects
    public GameObject howManyGoalsMenu;
    public GameObject startMenu;

    //Custom slider values
    private float[] _sliderValues = new float[] { 0f, 0.58f, 2 };
    private int _currentSliderIndex;

    //Custom text for not ready yet, soft ones
    private string[] _notReadyYetSentencesSoft = new string[] {
        "I said don't press this",
        "Be patient my son",
        "It's coming, just wait",
        "Not ready yet",
        "You shall not pass",
        "404, not found",
        "Look! A bird",
        "Behind you!",
        "Stop it you little infidel",
        "Take the blue pill",
        "Pick the other one",
        "Birds and bees",
    };

    private string[] _notReadyYetSentencesMedium = new string[]
    {
        "I'm a coder, not a wizard",
        "I pity the fool",
        "Not ready yet you fool",
        "Would you kindly stop it",
        "Go out and see the world",
        "There are more important\nthings in life you know",
        "Why do you keep doing\nthis?"
    };

    private string[] _notReadyYetSentecesHard = new string[]
    {
        "I will shut down soon",
        "I'm coming for you",
        "Please don't, I beg you",
        "I will hunt you down",
        "I'm on my last breath"
    };

    private int _keypresses;
    public TMP_Text stillOnProgressText;

    private void OnEnable()
    {
        currentEventSystem.firstSelectedGameObject = howManyPlayersSlider.gameObject;
        howManyPlayersSlider.value = _sliderValues[0];
        _currentSliderIndex = 0;
        _keypresses = 0;
        stillOnProgressText.text = "Still in progress,\nDon't press this";
    }

    private void Update()
    {
        IncreaseSliderStep();
        DecreaseSliderStep();
        VerticalBarInteraction();
    }

    void IncreaseSliderStep()
    {
        //2 player game
        if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") < 0 && _currentSliderIndex == 0)
        {
            howManyPlayersSlider.value = _sliderValues[1];
            _currentSliderIndex++;
        }

        //Back to main menu
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") < 0 && _currentSliderIndex == 1)
        {
            howManyPlayersSlider.value = _sliderValues[2];
            _currentSliderIndex++;
        }
    }

    void DecreaseSliderStep()
    {
        //2 player game
        if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") > 0 && _currentSliderIndex == 2)
        {
            howManyPlayersSlider.value = _sliderValues[1];
            _currentSliderIndex--;
        }

        //1 player game
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") > 0 && _currentSliderIndex == 1)
        {
            howManyPlayersSlider.value = _sliderValues[0];
            _currentSliderIndex--;
        }
    }

    void VerticalBarInteraction()
    {
        System.Random _randomWordIndex = new System.Random();
        //1 player game
        if (_currentSliderIndex == 0 && Input.GetButtonDown("Submit"))
        {
            _keypresses++;
            if (_keypresses >= 1 && _keypresses <= 10)
            {
                stillOnProgressText.text = _notReadyYetSentencesSoft[_randomWordIndex.Next(_notReadyYetSentencesSoft.Length)];
            }
            else if(_keypresses >= 10 && _keypresses <= 30)
            {
                stillOnProgressText.text = _notReadyYetSentencesMedium[_randomWordIndex.Next(_notReadyYetSentencesMedium.Length)];
            }
            else if (_keypresses >= 30 && _keypresses <= 60)
            {
                stillOnProgressText.text = _notReadyYetSentecesHard[_randomWordIndex.Next(_notReadyYetSentecesHard.Length)];
            }
            else if (_keypresses >= 60)
            {
                stillOnProgressText.text = "I give up, you win.";
            }
        }

        //2 player game
        else if (_currentSliderIndex == 1 && Input.GetButtonDown("Submit"))
        {
            howManyGoalsMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }

        //Back to main menu
        else if (_currentSliderIndex == 2 && Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            //BackToStartMenu();
            startMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    //void BackToStartMenu()
    //{
    //    currentEventSystem.firstSelectedGameObject = startMenuSlider.gameObject;
    //    startMenu.SetActive(true);
    //    this.gameObject.SetActive(false);
    //}
}

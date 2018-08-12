using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript_howmanygoals : MonoBehaviour
{
    public EventSystem currentEventSystem;
    public static int goalInput;
    public TMP_Text howManyGoalsNumber;
    public Slider howmanyGoalsSlider;
    public GameObject howmanyPlayersMenu;
    private int _currentSliderIndex;
    private bool _goalInputEntered = false;

    void OnEnable()
    {
        currentEventSystem.SetSelectedGameObject(howmanyGoalsSlider.gameObject);
        howmanyGoalsSlider.value = 0;
        _currentSliderIndex = 0;
        goalInput = 1;
    }

    void Update()
    {
        GoalInput();
        Interactions();
    }

    void GoalInput()
    {
        if (howmanyGoalsSlider.value == 0 && Input.GetAxisRaw("HorizontalP1") == 1)
        {
            if (_goalInputEntered == false)
            {
                goalInput += 1;
                howManyGoalsNumber.text = goalInput.ToString();
                _goalInputEntered = true;
            }
        }

        else if (howmanyGoalsSlider.value == 0 && Input.GetAxisRaw("HorizontalP1") == -1)
        {
            if (_goalInputEntered == false)
            {
                if (goalInput >= 2)
                {
                    goalInput -= 1;
                    howManyGoalsNumber.text = goalInput.ToString();
                    _goalInputEntered = true;
                }
            }
        }

        else if (Input.GetAxisRaw("HorizontalP1") == 0)
        {
            _goalInputEntered = false;
        }
    }

    void Interactions()
    {
        if (howmanyGoalsSlider.value == 1 && Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Game");
        }
        else if(howmanyGoalsSlider.value == 2 && Input.GetButtonDown("Submit") || Input.GetButton("Cancel"))
        {
            howmanyPlayersMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

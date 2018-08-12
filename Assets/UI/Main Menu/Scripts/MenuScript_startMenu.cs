using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript_startMenu : MonoBehaviour {

    public GameObject introMenu;
    public GameObject optionsMenu;
    public GameObject howManyPlayersMenu;
    public EventSystem currentEventSystem;
    public Slider _startMenuSlider;
    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        currentEventSystem.SetSelectedGameObject(_startMenuSlider.gameObject);  
    }

    private void Update()
    {
        if(_startMenuSlider.value == 0 && Input.GetButtonDown("Submit"))
        {
            StartGame();
        }

        else if(_startMenuSlider.value == 1 && Input.GetButtonDown("Submit"))
        {
            Options();
        }
        else if (_startMenuSlider.value == 2 && Input.GetButtonDown("Submit"))
        {
            ExitGame();
        }
        //TODO
        else if(Input.GetButton("Cancel"))
        {
            introMenu.SetActive(true);
            this.gameObject.SetActive(false);
            
        }
    }

    public void StartGame()
    {
        this.gameObject.SetActive(false);
        howManyPlayersMenu.SetActive(true);
    }

    public void Options()
    {
        //currentEventSystem.SetSelectedGameObject(null);
        this.gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        print("Game quits!");
        Application.Quit();
    }

}

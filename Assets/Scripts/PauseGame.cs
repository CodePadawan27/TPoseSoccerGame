using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public Transform canvas;
    public Transform pauseMenu;
    public Transform controlsMenu;
    public GameObject player1;
    public GameObject player2;
    private bool _gameEnded = false;
    void Update()
    {
        if (Input.GetButtonDown("PauseGame") && !_gameEnded)
        {
            Pause();
        }
    }

    public void Pause()
    {
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<Player2Controller>().enabled = false;
        Cursor.visible = true;
        if (canvas.gameObject.activeInHierarchy == false)
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                controlsMenu.gameObject.SetActive(false);
            }
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            player1.GetComponent<PlayerController>().enabled = true;
            player2.GetComponent<Player2Controller>().enabled = true;
            Cursor.visible = false;
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Controls(bool Open)
    {
        if(Open)
        {
            controlsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            controlsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

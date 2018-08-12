using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuScript_options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource sfxTestAudioSource;
    public AudioClip sfxTestAudioClip;
    public GameObject startMenu;
    public Slider optionsSlider;
    public Slider startMenuSlider;
    public Slider soundVolumeSlider;
    public Sprite handleCurrentImage;
    public Sprite handleNewImage;
    public Slider musicVolumeSlider;
    public EventSystem currentEventSystem;
    
    private float[] _sliderValues = new float[] { 0f, 0.88f, 1.77f, 3f };
    private int _currentSliderIndex;
    private bool sfxSliderisOn = false;
    private bool musicSliderisOn = false;
    private List<Transform> _soundVolumeSliderChildren = new List<Transform>();
    private List<Transform> _musicVolumeSliderChildren = new List<Transform>();

    void Start()
    {
        currentEventSystem.firstSelectedGameObject = optionsSlider.gameObject;
        _currentSliderIndex = 0;
        optionsSlider.value = _sliderValues[0];
        PopulateSliderLists();
    }

    void Update()
    {
        IncreaseSliderStep();
        DecreaseSliderStep();
        VerticalBarInteraction();
    }

    void PopulateSliderLists()
    {
        foreach (Transform character in soundVolumeSlider.transform)
        {
            _soundVolumeSliderChildren.Add(character);
        }

        foreach (Transform character in musicVolumeSlider.transform)
        {
            _musicVolumeSliderChildren.Add(character);
        }
    }

    void IncreaseSliderStep()
    {
        //Music volume
        if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") < 0 && _currentSliderIndex == 0 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[1];
            _currentSliderIndex++;
        }

        //Graphics quality
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") < 0 && _currentSliderIndex == 1 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[2];
            _currentSliderIndex++;
        }

        //Back
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") < 0 && _currentSliderIndex == 2 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[3];
            _currentSliderIndex++;

        }

        //Back to start menu if Cancel button is pressed
        else if (Input.GetButtonDown("Cancel") && !musicSliderisOn && !sfxSliderisOn)
        {
            BackToStartMenu();
        }
    }

    void VerticalBarInteraction()
    {
        //Sound volume
        if (_currentSliderIndex == 0 && Input.GetButtonDown("Submit") && optionsSlider.IsInteractable())
        {
            sfxSliderisOn = true;
            currentEventSystem.SetSelectedGameObject(soundVolumeSlider.gameObject);
            _soundVolumeSliderChildren[2].GetComponentInChildren<Image>().sprite = handleNewImage;
            optionsSlider.interactable = false;

        }
        else if (sfxSliderisOn && Input.GetButtonDown("Cancel"))
        {
            sfxSliderisOn = false;
            currentEventSystem.SetSelectedGameObject(optionsSlider.gameObject);
            _soundVolumeSliderChildren[2].GetComponentInChildren<Image>().sprite = handleCurrentImage;
            optionsSlider.interactable = true;
        }

        //Music volume
        else if (_currentSliderIndex == 1 && Input.GetButtonDown("Submit") && optionsSlider.IsInteractable())
        {
            musicSliderisOn = true;
            currentEventSystem.SetSelectedGameObject(musicVolumeSlider.gameObject);
            _musicVolumeSliderChildren[2].GetComponentInChildren<Image>().sprite = handleNewImage;
            optionsSlider.interactable = false;
        }
        else if (musicSliderisOn && Input.GetButtonDown("Cancel"))
        {
            print("WORKSSSS");
            musicSliderisOn = false;
            currentEventSystem.SetSelectedGameObject(optionsSlider.gameObject);
            _musicVolumeSliderChildren[2].GetComponentInChildren<Image>().sprite = handleCurrentImage;
            optionsSlider.interactable = true;
        }

        else if (_currentSliderIndex == 2 && Input.GetButtonDown("Submit") && optionsSlider.IsInteractable())
        {

        }

        //Back
        else if (_currentSliderIndex == 3 && Input.GetButtonDown("Submit") && optionsSlider.IsInteractable())
        {
            BackToStartMenu();
        }
    }

    void DecreaseSliderStep()
    {
        //Back to main menu
        if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") > 0 && _currentSliderIndex == 3 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[2];
            _currentSliderIndex--;
        }

        //Graphics quality
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") > 0 && _currentSliderIndex == 2 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[1];
            _currentSliderIndex--;
        }

        //Music volume
        else if (Input.GetButtonDown("VerticalP1") && Input.GetAxisRaw("VerticalP1") > 0 && _currentSliderIndex == 1 && optionsSlider.IsInteractable())
        {
            optionsSlider.value = _sliderValues[0];
            _currentSliderIndex--;
        }

        //Back to start menu if Cancel button is pressed
        else if (Input.GetButtonDown("Cancel") && !musicSliderisOn && !sfxSliderisOn)
        {
            BackToStartMenu();
        }
    }

    void BackToStartMenu()
    {
        currentEventSystem.firstSelectedGameObject = startMenuSlider.gameObject;
        optionsSlider.value = _sliderValues[0];
        _currentSliderIndex = 0;
        startMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    //Change the game's SFX volume
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);
        sfxTestAudioSource.PlayOneShot(sfxTestAudioClip);
    }

    //Change the game's music volume
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
}

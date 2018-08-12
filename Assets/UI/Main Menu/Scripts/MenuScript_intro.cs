using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript_intro : MonoBehaviour {

    public TextMeshProUGUI blinkingText;
    public GameObject startMenu;
    public AudioSource tposeMainVoice;

	void Start () {
        Cursor.visible = false;
        //StartCoroutine(BlinkText());
	}
	
	void Update () {
		if(Input.anyKey)
        {
            this.gameObject.SetActive(false);
            startMenu.SetActive(true);
        }
	}

    //If re-enabled, blinking starts
    private void OnEnable()
    {
        StartCoroutine(BlinkText());
    }

    //Blinks text for half seconds at time
    public IEnumerator BlinkText()
    {
        while(true)
        {
            blinkingText.SetText("");
            yield return new WaitForSeconds(.5f);
            blinkingText.SetText("Press start");
            yield return new WaitForSeconds(.5f);
        }
    }
}

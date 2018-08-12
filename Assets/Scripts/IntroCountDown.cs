using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a script for intro countdown beep (IntroCounterText UI element).
public class IntroCountDown : MonoBehaviour {

    public AudioSource introCountDownAudio;
    public AudioClip countdownBeep;
    private GameObject sb;

    void Start () {
        sb = GameObject.Find("BannerBoard_scrolling_text");
        introCountDownAudio = GetComponent<AudioSource>();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        Time.timeScale = 0;
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().enabled = false;
        introCountDownAudio.PlayOneShot(countdownBeep);
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = true;
        yield return new WaitForSecondsRealtime(1.5f);
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = false;
        introCountDownAudio.PlayOneShot(countdownBeep);
        GameObject.Find("IntroCounterText").GetComponent<Text>().text = "2";
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = true;
        yield return new WaitForSecondsRealtime(1.5f);
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = false;
        GameObject.Find("IntroCounterText").GetComponent<Text>().text = "1";
        introCountDownAudio.PlayOneShot(countdownBeep);
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = true;
        yield return new WaitForSecondsRealtime(1.5f);
        GameObject.Find("IntroCounterText").GetComponent<Text>().enabled = false;
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().enabled = true;
        Time.timeScale = 1;

        //Stars PlayAnimationWithDifferentLenth() coroutine from BannerBoard_Scrolling class
        StartCoroutine(sb.GetComponent<BannerBoard_ScrollingText>().PlayAnimationWithDifferentLength());
    }

}

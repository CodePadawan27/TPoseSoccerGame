using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject man_default;
    public GameObject man_bdozer;

    void Start()
    {
        SwitchToBasic();
    }


    public void SwitchToBullDozer()
    {
        if (man_default.gameObject)
        {
            man_default.gameObject.SetActive(false);
        }
        man_bdozer.gameObject.SetActive(true);
    }

    public void SwitchToBasic()
    {
        if (man_bdozer)
        {
            man_bdozer.gameObject.SetActive(false);
        }
        man_default.gameObject.SetActive(true);
    }
}

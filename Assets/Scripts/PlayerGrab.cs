using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{

    private bool _isGrabbed = false;
    public GameObject grabbedPlayer;
    float distance;

    private void Update()
    {
        distance = Vector3.Distance(this.transform.position, grabbedPlayer.transform.position);

        if (_isGrabbed && !Input.GetButton("GrabP1") || distance > 1f)
            grabbedPlayer.transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (Input.GetButton("GrabP1") && obj.gameObject.name == "man_02")
        {
            grabbedPlayer.transform.SetParent(this.transform);
            _isGrabbed = true;
        }
    }
}


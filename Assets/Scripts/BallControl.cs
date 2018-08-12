using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Vector3 _ballPosition;
    private Quaternion _ballRotation;
    public int p1HitsToTheBall;
    public int p2HitsToTheBall;
    private AudioSource ballHitAudio;
    public AudioClip ballHitSfx;



    void Start()
    {
        ballHitAudio = this.GetComponent<AudioSource>();
        _ballPosition = this.transform.position;
        _ballRotation = this.transform.rotation;

    }

    void Update()
    {
        if (this.transform.position.y < 0)
        {
            this.transform.position = _ballPosition;
            this.transform.rotation = _ballRotation;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ballHitAudio.PlayOneShot(ballHitSfx);
        }

    }
}

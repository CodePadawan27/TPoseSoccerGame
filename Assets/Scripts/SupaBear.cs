using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupaBear : MonoBehaviour
{
    public AudioSource supaBearAudio;
    public AudioSource supaBearExplosion;
    public AudioClip sfx_supaBearExplosion;
    public Transform player;
    public float speed = 5f;
    public bool _bearDropped;
    public int p1MuggedTimes;
    public int p2MuggedTimes;



    private void Start()
    {
        supaBearAudio.Play();
        _bearDropped = false;
    }

    void Update()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.y < -20)
        {
            supaBearExplosion.PlayOneShot(sfx_supaBearExplosion);
            _bearDropped = true;
        }
    }

    //Logs player bear mugs
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "man_01")
        {
            p1MuggedTimes++;
        }
        else if (collision.gameObject.name == "man_02")
        {
            p2MuggedTimes++;
        }
    }
}

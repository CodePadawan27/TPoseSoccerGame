using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public AudioSource heartAudio;
    public AudioClip pickUp;

    private GameObject _player1;
    private GameObject _player2;

    List<Transform> p1Characters = new List<Transform>();
    List<Transform> p2Characters = new List<Transform>();

    void Start()
    {
        heartAudio = GetComponent<AudioSource>();
        _player1 = GameObject.Find("man_01");
        _player2 = GameObject.Find("man_02");
        PopulateCharactersLists();
    }

    void PopulateCharactersLists()
    {
        foreach (Transform character in _player1.gameObject.transform)
        {
            p1Characters.Add(character);
        }

        foreach (Transform character in _player2.gameObject.transform)
        {
            p2Characters.Add(character);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            heartAudio.PlayOneShot(pickUp);
            GiveHealth(other.gameObject);
        }
    }

    private void GiveHealth(GameObject player)
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;

        foreach (Transform character in p1Characters)
        {
            if (player.name.Equals(character.gameObject.name))
            {
                _player1.GetComponent<PlayerController>().HealthIncrease();
            }
        }

        foreach (Transform character in p2Characters)
        {
            if (player.name.Equals(character.gameObject.name))
            {
                _player2.GetComponent<Player2Controller>().HealthIncrease();
            }
        }

        Destroy(this.gameObject, 1f);
        GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().heartSpawned = false;
        //ObjectSpawnManager.heartSpawned = false;
    }
}

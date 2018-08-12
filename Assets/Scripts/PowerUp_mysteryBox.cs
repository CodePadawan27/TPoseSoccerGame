using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_mysteryBox : MonoBehaviour
{
    public AudioSource mysteryBoxAudio;
    public AudioClip pickUp;
    public GameObject powerupExplosion;
    public GameObject supabearExplosion;
    public GameObject _supaBear;

    private GameObject _player1;
    private GameObject _player2;


    //List<Transform> p1Characters = new List<Transform>();
    //List<Transform> p2Characters = new List<Transform>();

    void Start()
    {
        //mysteryBoxAudio = GetComponent<AudioSource>();
        _player1 = GameObject.Find("man_01");
        _player2 = GameObject.Find("man_02");
        //PopulateCharactersLists();
    }

    //void PopulateCharactersLists()
    //{
    //    foreach (Transform character in _player1.gameObject.transform)
    //    {
    //        p1Characters.Add(character);
    //    }

    //    foreach (Transform character in _player2.gameObject.transform)
    //    {
    //        p2Characters.Add(character);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mysteryBoxAudio.PlayOneShot(pickUp);
            GameObject intantiatedExplosion = Instantiate(powerupExplosion, transform.position, transform.rotation);
            Destroy(intantiatedExplosion, powerupExplosion.GetComponent<ParticleSystem>().main.duration);
            int randomCoroutine = Random.Range(1, 5);
            switch (randomCoroutine)
            {
                //Player getting bigger
                case 1:
                    StartCoroutine(GettingBigger(other.gameObject));
                    break;
                //Player getting smaller
                case 2:
                    StartCoroutine(GettingSmaller(other.gameObject));
                    break;
                //Bulldozer switch
                case 3:
                    StartCoroutine(BullDozer(other.gameObject));
                    break;
                //Supabear attacks
                case 4:
                    StartCoroutine(SupaBear(other.gameObject));
                    break;
                default:
                    break;
            }
        }
    }
    //TODO
    public IEnumerator SupaBear(GameObject player)
    {
        Debug.Log("nyt tulee karhu");
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        GameObject intantiatedSupaBear = Instantiate(_supaBear, transform.position, Quaternion.identity);
        _supaBear.GetComponent<SupaBear>().player = player.transform;
        print(_supaBear.GetComponent<SupaBear>().player.ToString());

        //yield return new WaitForSecondsRealtime(10);
        var elapsedTime = 0f;
        while (elapsedTime <= 10)
        {
            elapsedTime += Time.deltaTime;
            
            if(GameObject.Find("Supabear(Clone)").GetComponent<SupaBear>()._bearDropped)
            {
                print("nalle tippui");
                break;
            }
            else
            {
                yield return null;
            }
        }

        Destroy(intantiatedSupaBear);
        GameObject intantiatedExplosion = Instantiate(supabearExplosion, intantiatedSupaBear.transform.position, intantiatedSupaBear.transform.rotation);
        Destroy(intantiatedExplosion, supabearExplosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(this.gameObject);
        GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().mysteryboxSpawned = false;
    }

    IEnumerator BullDozer(GameObject player)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        if (player.name.Equals("man_01_default"))
        {
            _player1.GetComponent<CharacterSwitch>().SwitchToBullDozer();
            yield return new WaitForSecondsRealtime(10);
            _player1.GetComponent<CharacterSwitch>().SwitchToBasic();
            Destroy(this.gameObject, 1f);
            GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().mysteryboxSpawned = false;
        }
        else if (player.name.Equals("man_02_default"))
        {
            _player2.GetComponent<CharacterSwitch>().SwitchToBullDozer();
            yield return new WaitForSecondsRealtime(10);
            _player2.GetComponent<CharacterSwitch>().SwitchToBasic();
            Destroy(this.gameObject, 1f);
            GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().mysteryboxSpawned = false;
        }
    }

    IEnumerator GettingBigger(GameObject player)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        if (!player.name.Equals("man_01_bdozer") && !player.name.Equals("man_02_bdozer"))
        {
            player.transform.localScale = new Vector3(5, 5, 5);
        }
        else
        {
            player.transform.localScale = new Vector3(3, 3, 3);
        }
        yield return new WaitForSecondsRealtime(10);
        player.transform.localScale = new Vector3(1, 1, 1);
        Destroy(this.gameObject);
        GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().mysteryboxSpawned = false;
    }

    IEnumerator GettingSmaller(GameObject player)
    {
        player.GetComponentInParent<AudioSource>().pitch = 2.0f;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        yield return new WaitForSecondsRealtime(10);
        player.transform.localScale = new Vector3(1, 1, 1);
        player.GetComponent<AudioSource>().pitch = 1.0f;
        Destroy(this.gameObject);
        GameObject.Find("SpawnManager").GetComponent<ObjectSpawnManager>().mysteryboxSpawned = false;
    }
}
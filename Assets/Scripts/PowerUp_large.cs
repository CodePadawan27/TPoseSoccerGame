using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_large : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject;
            //gameObject.GetComponent<Renderer>().enabled = false;
            
            StartCoroutine(Resizer(player));
        }
    }


    IEnumerator Resizer(GameObject player)
    {
        player.transform.localScale = new Vector3(5, 5, 5);
        Debug.Log("Nyt kasvoi");
        yield return new WaitForSecondsRealtime(10);
        player.transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("Nyt takas");
        Destroy(this.gameObject);

    }


}

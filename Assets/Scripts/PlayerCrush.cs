using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is meant to track if other player is bigger than the other.
  If this is the case and other player jumps to other player top, the player
  under the bigger player crashes and loses life.*/
public class PlayerCrush : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //CheckIfBigger();
	}

    //void CheckIfBigger()
    //{
    //    if (player1.transform.localScale.x > player2.transform.localScale.x)
    //}
}

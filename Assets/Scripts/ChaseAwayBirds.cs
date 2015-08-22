using UnityEngine;
using System.Collections;

public class ChaseAwayBirds : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Bird")
		{
			collider.GetComponent<BirdMovement>().ChaseAway();
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.tag == "Bird")
		{
			collider.GetComponent<BirdMovement>().ChaseAway();
		}
	}
}

using UnityEngine;
using System.Collections;

public class FarmerMovement : MonoBehaviour
{
	public float speed = 10.0f;
	public float reachedTolerance = 0.1f;

	[HideInInspector]
	public Vector2 targetPosition;

	public static FarmerMovement farmerMovement;

	void Awake()
	{
		farmerMovement = this;
	}

	// Use this for initialization
	void Start ()
	{
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 targetVector = targetPosition - (Vector2)transform.position;
		float touchDistance = targetVector.magnitude;
		if(touchDistance > reachedTolerance)
		{
			Vector2 direction = targetVector.normalized;
			transform.position += (Vector3)direction * speed * Time.deltaTime;
		}
	}
}

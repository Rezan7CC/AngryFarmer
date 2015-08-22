using UnityEngine;
using System.Collections;

public class FarmerMovement : MonoBehaviour
{
	public float speed = 10.0f;
	public float reachedTolerance = 0.1f;

	[HideInInspector]
	public Vector2 targetPosition;

	public GameObject head;
	public GameObject armLeft;
	public GameObject armRight;

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

			Vector3 oldPosition = armRight.transform.localPosition;
			oldPosition.y = Mathf.Sin(Time.time * 20.0f) * 0.1f + 0.6f;
			armRight.transform.localPosition = oldPosition;

			Vector3 oldPosition2 = armLeft.transform.localPosition;
			oldPosition2.y = -Mathf.Sin(Time.time * 20.0f) * 0.1f + 0.5f;
			armLeft.transform.localPosition = oldPosition2;
		}

		Vector3 tempHeadPosition = head.transform.localPosition;
		tempHeadPosition.y = Mathf.Sin(Time.time * 10.0f) * 0.05f + 0.75f;
		head.transform.localPosition = tempHeadPosition;
	}
}

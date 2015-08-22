using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
	public float speed = 10.0f;
	public float reachedTolerance = 0.1f;
	bool chasedAway = false;
	bool onField = false;

	[HideInInspector]
	public BirdHandler targetField;

	Vector2 targetPosition;

	// Use this for initialization
	void Start ()
	{
		BoxCollider2D targetFieldCollider = targetField.GetComponent<BoxCollider2D>();
		targetPosition = (Vector2)targetField.transform.position + new Vector2(
			Random.Range(targetFieldCollider.size.x * -0.5f, targetFieldCollider.size.x * 0.5f),
			Random.Range(targetFieldCollider.size.y * -0.5f, targetFieldCollider.size.y * 0.5f));
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 targetVector = (Vector2)targetPosition - (Vector2)transform.position;
		float targetDistance = targetVector.magnitude;
		if(targetDistance > reachedTolerance)
		{
			Vector2 direction = targetVector.normalized;
			transform.position += (Vector3)direction * speed * Time.deltaTime;
			if(direction.x > 0)
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			else
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				
		}
		else
		{
			if(!chasedAway && !onField)
			{
				onField = true;
				targetField.birdCount++;
			}
		}
	}

	public void ChaseAway()
	{
		if(!chasedAway && onField)
		{
			chasedAway = true;
			targetPosition = BirdSpawner.birdSpawner.GeneratePosition();
			targetField.birdCount--;
		}
	}
}

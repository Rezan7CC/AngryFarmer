using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
	public GameObject wing;
	public GameObject head;
	public float speed = 10.0f;
	public float reachedTolerance = 0.1f;
	bool chasedAway = false;
	bool onField = false;

	[HideInInspector]
	public BirdHandler targetField;

	Vector2 targetPosition;

	bool animationIsRunning = false;

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

			if(!animationIsRunning)
			{
				StartCoroutine(AnimateWings());
			}

			if(direction.x > 0)
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			else
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				
		}
		else
		{
			if(!chasedAway && !onField)
			{
				wing.SetActive(false);
				onField = true;
				targetField.birdCount++;
				StartCoroutine(AnimateHead());
			}
			else if(chasedAway && onField)
			{
				Destroy(this);
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

	IEnumerator AnimateWings()
	{
		animationIsRunning = true;
		wing.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		wing.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		animationIsRunning = false;
	}

	IEnumerator AnimateHead()
	{
		float headDownDuration = 0.25f;
		float headUpDuration = 0.25f;

		while(onField && !chasedAway)
		{
			StartCoroutine(HeadDown(headDownDuration));
			yield return new WaitForSeconds(headDownDuration);
			StartCoroutine(HeadUp(headUpDuration));
			yield return new WaitForSeconds(headUpDuration);
		}
		StartCoroutine(HeadUp(headUpDuration));
	}

	IEnumerator HeadUp(float duration)
	{
		float startAngle = head.transform.localRotation.eulerAngles.z;
		float currentDuration = 0.0f;

		while(currentDuration <= duration)
		{
			currentDuration += Time.deltaTime;

			Vector3 tempRotation = head.transform.localRotation.eulerAngles;
			tempRotation.z = Mathf.LerpAngle(startAngle, 0.0f, currentDuration / duration);
			head.transform.localRotation = Quaternion.Euler(tempRotation);

			yield return new WaitForEndOfFrame();
		}
		head.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
	}

	IEnumerator HeadDown(float duration)
	{
		float startAngle = head.transform.localRotation.eulerAngles.z;
		float currentDuration = 0.0f;
		
		while(currentDuration <= duration)
		{
			currentDuration += Time.deltaTime;
			
			Vector3 tempRotation = head.transform.localRotation.eulerAngles;
			tempRotation.z = Mathf.LerpAngle(startAngle, 30.0f, currentDuration / duration);
			head.transform.localRotation = Quaternion.Euler(tempRotation);
			
			yield return new WaitForEndOfFrame();
		}
		head.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 30.0f);
	}
}

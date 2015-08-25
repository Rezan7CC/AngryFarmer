using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointGenerator : MonoBehaviour
{
	public int maxPoints = 100;
	public float timeIntervall = 0.5f;
	public int pointsPerIntervall = 1;

	bool pause = false;
	[HideInInspector]
	public bool collectPoints = false;
	public int currentPoints;

	public Image UIPoints;
	public Image UIPointsInactive;
	public GameObject submitPointsPSPrefab;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(GeneratePoints());
		SetPoints(50);
	}
	
	// Update is called once per frame
	void Update()
	{
	}

	IEnumerator GeneratePoints()
	{
		while(true)
		{
			yield return new WaitForSeconds(timeIntervall);
			if(!pause)
				SetPoints(currentPoints + pointsPerIntervall);
		}
	}

	void SetPoints(int points)
	{
		currentPoints = Mathf.Clamp(points, 0, maxPoints);
		UIPoints.fillAmount = (float)currentPoints / maxPoints;
//		Debug.Log(currentPoints);
//		debugText.text = currentPoints.ToString();
	}

	public void StartCollectPoints()
	{
		collectPoints = true;
//		StartCoroutine(CollectPoints());
		FarmerMovement.farmerMovement.targetPosition = transform.position;
	}

//	IEnumerator CollectPoints()
//	{
//		while(collectPoints)
//		{
//			yield return new WaitForEndOfFrame();
//		}
//	}

	public void SubmitPoints()
	{
		if(currentPoints > 0)
		{
			StartCoroutine(SubmitPointsEffect(2.0f));
			StartCoroutine(FontSizeEffect(1.0f));
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			pause = true;
			Color tempColor = UIPointsInactive.color;
			tempColor.a = 0.5f;
			UIPointsInactive.color = tempColor;

			if(collectPoints)
			{
				SubmitPoints();
				collectPoints = false;
			}
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			if(collectPoints)
			{
				SubmitPoints();
				collectPoints = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			pause = false;
			Color tempColor = UIPointsInactive.color;
			tempColor.a = 0.0f;
			UIPointsInactive.color = tempColor;
		}
	}

	IEnumerator SubmitPointsEffect(float duration)
	{
		Vector3 startPositon = transform.position;
		Vector3 endPosition = new Vector3(8f, 5f, 0.0f);
		float currentDuration = 0.0f;
		GameObject submitPointsPS = Instantiate(submitPointsPSPrefab, startPositon, Quaternion.identity) as GameObject;
		int pointsToAdd = currentPoints;
		SetPoints(0);

		while(currentDuration <= duration)
		{
			currentDuration += Time.deltaTime;
			
			Vector3 tempPosition = gameObject.transform.position;
			tempPosition = Vector3.Lerp(startPositon, endPosition, currentDuration / duration);
			submitPointsPS.transform.position = tempPosition;
			
			yield return new WaitForEndOfFrame();
		}

		Destroy(submitPointsPS);
		GameStats.gameStats.AddPoints(pointsToAdd);
	}

	IEnumerator FontSizeEffect(float duration)
	{
		GameStats.gameStats.UIScore.fontSize = 80;
		float currentDuration = 0.0f;
		
		while(currentDuration <= duration)
		{
			currentDuration += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		GameStats.gameStats.UIScore.fontSize = 65;
	}
}

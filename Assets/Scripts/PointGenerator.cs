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
		GameStats.gameStats.AddPoints(currentPoints);
		SetPoints(0);
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
}

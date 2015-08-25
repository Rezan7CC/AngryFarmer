using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
//	float inputRadius = 1.0f;
	FarmerMovement farmerMovement;
	// Use this for initialization
	void Awake()
	{
		farmerMovement = GetComponent<FarmerMovement>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
		{
			if(Input.touchCount > 0)
			{
				PositionInput(Input.GetTouch(0).position);
			}
			
			if(Input.GetMouseButtonDown(0))
			{
				PositionInput(Input.mousePosition);
			}
		}
	}

	void PositionInput(Vector2 screenPosition)
	{
		farmerMovement.targetPosition = Camera.main.ScreenToWorldPoint((Vector3)screenPosition);

//		Collider2D collider = Physics2D.OverlapCircle(farmerMovement.targetPosition, inputRadius);

		GameObject[] fields = GameObject.FindGameObjectsWithTag("Field");
		foreach(GameObject field in fields)
		{
			field.GetComponent<PointGenerator>().collectPoints = false;
		}
	}
}

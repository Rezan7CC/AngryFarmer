using UnityEngine;
using System.Collections;

public class PointCollector : MonoBehaviour
{
	public PointGenerator pointGenerator;
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{			
			if(pointGenerator.collectPoints)
			{
				pointGenerator.SubmitPoints();
				pointGenerator.collectPoints = false;
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			if(pointGenerator.collectPoints)
			{
				pointGenerator.SubmitPoints();
				pointGenerator.collectPoints = false;
			}
		}
	}
}

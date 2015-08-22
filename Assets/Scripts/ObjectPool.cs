using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	public int objectsAtStart = 0;
	public bool willGrow = true;
	public bool useMaximumGrowth = false;
	public int maximumGrowth = 0;
	public bool ui = false;
	public GameObject prefab;
	public List<GameObject> objects = new List<GameObject>();
	
	[HideInInspector]
	public Transform TF;

	void Awake()
	{
		TF = transform;
	}
	
	void Start()
	{
		// Instantiate and prepare x (objectsAtStart) objects
		for(int i = 0; i < objectsAtStart; ++i)
		{
			GameObject obj = Instantiate(prefab) as GameObject;
			obj.SetActive(false);
			obj.transform.SetParent(TF, !ui);
			objects.Add(obj);
		}
	}
	
	public GameObject RequestObject()
	{
		// Search for inactive object and return it
		for(int i = 0; i < objects.Count; ++i)
		{
			if(!objects[i].activeInHierarchy)
			{
				return objects[i];
			}
		}
		
		// If there isn't a inactive object, willgrow is true and the object count is < maximumGrowth
		if(willGrow && (!useMaximumGrowth || (useMaximumGrowth && objects.Count +1 <= maximumGrowth)))
		{
			// Instantiate, prepare and return a object
			GameObject obj = Instantiate(prefab) as GameObject;
			obj.SetActive(false);
			obj.transform.SetParent(TF, !ui);
			objects.Add(obj);
			return obj;
		}
		
		return null;
	}
	
	public int GetActiveObjectsCount()
	{
		int tempCount = 0;
		
		foreach(GameObject gO in objects)
		{
			if(gO.activeSelf == true)
				tempCount++;
		}
		return tempCount;
	}
}
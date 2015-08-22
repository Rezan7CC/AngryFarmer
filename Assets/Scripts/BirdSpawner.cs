using UnityEngine;
using System.Collections;

public class BirdSpawner : MonoBehaviour
{
	public float spawnTime = 10.0f;
	public float minSpawnTime = 1.0f;
	public float timeToDecreaseAfterSpawn = 0.1f;

	public ObjectPool objectPool;

	public static BirdSpawner birdSpawner;

	float currentSpawnTime = 0.0f;

	void Awake()
	{
		birdSpawner = this;
	}

	// Use this for initialization
	void Start ()
	{
		currentSpawnTime = spawnTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(currentSpawnTime);
		currentSpawnTime -= Time.deltaTime;
		if(currentSpawnTime <= 0)
		{
			SpawnBird();
		}
	}

	void SpawnBird()
	{
		spawnTime -= timeToDecreaseAfterSpawn;
		spawnTime = Mathf.Clamp(spawnTime, minSpawnTime, spawnTime);
		currentSpawnTime = spawnTime;

		GameObject[] fields = GameObject.FindGameObjectsWithTag("Field");
		int randomField = Random.Range(0, fields.Length);
		GameObject targetField = fields[randomField];

		

		GameObject bird = objectPool.RequestObject();
		bird.transform.position =  (Vector3)GeneratePosition();
		bird.SetActive(true);
		bird.GetComponent<BirdMovement>().targetField = targetField.GetComponent<BirdHandler>();
	}

	public Vector2 GeneratePosition()
	{
		float x = 0;
		float y = 0;
		int spawnRegion = Random.Range(0, 4);
		
		float halfWidth = Camera.main.orthographicSize * 2.5f;
		float halfHeight = Camera.main.orthographicSize * 2.5f / Camera.main.aspect;
		
		switch(spawnRegion)
		{
		case 0:
		{
			x = Random.Range(-halfWidth, halfWidth);
			y = halfHeight;
			break;
		}
		case 1:
		{
			x = Random.Range(-halfWidth, halfWidth);
			y = -halfHeight;
			break;
		}
		case 2:
		{
			x = -halfWidth;
			y = Random.Range(-halfHeight, halfHeight);
			break;
		}
		case 3:
		{
			x = halfWidth;
			y = Random.Range(-halfHeight, halfHeight);
			break;
		}
		}

		return new Vector2(x, y);
	}
}

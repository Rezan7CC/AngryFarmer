using UnityEngine;
using System.Collections;

public class AutoDestroyPS : MonoBehaviour
{
	ParticleSystem PS;

	void Awake()
	{
		PS = GetComponent<ParticleSystem>();
		PS.IsAlive();
	}

	void Update()
	{
		if(PS != null && !PS.IsAlive())
			Destroy(gameObject);
	}
}

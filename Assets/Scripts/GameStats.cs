using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GameStats : MonoBehaviour
{
	[HideInInspector]
	public int score;

	public int bonusIncreasePerIntervall = 5;
	public float bonusIntervall = 20.0f;
	
	public Text UIScore;
	public Text UIBonus;
	public Image UIHealth;
	public GameObject UIGameOver;

	public GameObject[] gameObjectsToDeactivateOnGameOver;

	public float birdCheckIntervall = 1.0f;
	public float damagePerBird = 0.001f;

	bool gameOver = false;

	float currentHealth = 100.0f;
	int currentBonus = 0;
//	public Text UITime;

	public static GameStats gameStats;

	void Awake()
	{
		gameStats = this;
	}

	// Use this for initialization
	void Start()
	{
		StartCoroutine(BonusIncrease());
		StartCoroutine(CheckBirds());
		SetBonus(0);
		SetHealth(100.0f);
	}
	
	// Update is called once per frame
	void Update()
	{
		UIScore.text = score.ToString();
//		UITime.text = Time.time.ToString();
	}

	public void AddPoints(int points)
	{
		SetScore(score + points + Mathf.RoundToInt(points * ((float)currentBonus * 0.01f)));
	}

	void SetScore(int score)
	{
		this.score = score;
	}

	IEnumerator BonusIncrease()
	{
		while(!gameOver)
		{
			yield return new WaitForSeconds(bonusIntervall);
			SetBonus(currentBonus + bonusIncreasePerIntervall);
		}
	}

	void SetBonus(int bonus)
	{
		currentBonus = bonus;
		UIBonus.text = "+" + currentBonus.ToString() + "%";
	}

	IEnumerator CheckBirds()
	{
		while(!gameOver)
		{
			yield return new WaitForSeconds(birdCheckIntervall);
			GameObject[] fields = GameObject.FindGameObjectsWithTag("Field");
			foreach(GameObject field in fields)
			{
				SetHealth(currentHealth - field.GetComponent<BirdHandler>().birdCount * damagePerBird);
			}
		}
	}

	void SetHealth(float health)
	{
		currentHealth = health;
		UIHealth.fillAmount = currentHealth * 0.01f;
		if(currentHealth <= 0.0f)
		{
			UIGameOver.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			Camera.main.GetComponent<Blur>().enabled = true;
			foreach(GameObject gameObjectToDeactivateOnGameOver in gameObjectsToDeactivateOnGameOver)
			{
				gameObjectToDeactivateOnGameOver.SetActive(false);
			}
		}
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale = 1.0f;
		
	}
}

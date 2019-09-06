using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
	// HUD
    public GameObject scoreText;
    public GameObject livesText;
	public GameObject rockOnText;
	public GameObject rockGodText;
	public GameObject progress;					// rock on progress bar
	public GameObject rgProgress;				// rock god progress bar

    public int maxEnemies = 10;					// Maximum number of enemies allowed in play; if currentEnemies equals or exceeds this, no more enemies can spawn until some are destroyed.
	public float startSpawnTime = 2.0f;			// Seconds between each spawn at game start. Implemented as minimum wait in seconds per spawn
	public float minSpawnTime = 0.1f;			// Maximum spawn rate. This is the point where the difficulty plateaus.
	public Rect spawnZone;						// The rectangle that defines where enemies can spawn
	public GameObject[] enemies;				// List of every possible enemy or projectile to spawn.
	public float spawnChangeTime = 10.0f;		// How often should we increase the spawn rate? In seconds.

	public int health = 3;						// Remaining HP. If the player being hit drops it to 0, the player is destroyed.

	private PlayerShoot playerShooting;			// The player's shooting script. Used to determine if the player is in Rock On or Rock GOD mode.

	private int score;							// Current score. Currently represented via enemies destroyed.

	private int currentEnemies;					// Number of enemies currently in play
	private float currentTick;					// Time in seconds from last enemy spawn. Should it exceed spawnTime, spawning is now possible.
	private float spawnTime;					// Seconds between each spawn at game start. Implemented as minimum wait in seconds per spawn. Changes as game progresses.
	private float prevSpawnChangeTime;			// Time since the spawn rate was last changed.

	private float rockOnMeter;					// A value between 0 and 100 determining how full the "Rock On" meter is.
	private float rockGodMeter;					// A value between 0 and 100 determining how full the "Rock GOD" meter is.

	// Use this for initialization
	void Start ()
	{
		currentEnemies = 0;
		currentTick = 0.0f;
		score = 0;

		rockOnMeter = 0.0f;
		rockGodMeter = 0.0f;

        livesText.GetComponent<TextMesh>().text = "Lives: " + health;
        scoreText.GetComponent<TextMesh>().text = "Score: " + score;
		rockOnText.GetComponent<TextMesh>().text = "Rock On: " + rockOnMeter.ToString("P1");
		rockGodText.GetComponent<TextMesh>().text = "Rock GOD: " + rockGodMeter.ToString("P1");
		spawnTime = startSpawnTime;
		prevSpawnChangeTime = Time.time;

		playerShooting = GameObject.Find("Rocker Dude").GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update ()
	{
		currentTick += Time.deltaTime;
		if(currentTick > spawnTime && currentEnemies < maxEnemies)
		{
			// Spawn an enemy at a random location, given our range
			Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(spawnZone.xMin, spawnZone.xMax), Random.Range(spawnZone.yMin, spawnZone.yMax), 0.0f), Quaternion.identity);
			currentTick = 0.0f;
			currentEnemies++;
		}

		// Increase the spawn rate when necessary
		if(Time.time - prevSpawnChangeTime > spawnChangeTime && spawnTime > minSpawnTime)
		{
			spawnTime -= 0.01f;
			prevSpawnChangeTime = Time.time;
		}

		if(playerShooting.isRockOn)
		{
			rockOnMeter -= 0.2f * Time.deltaTime;

			if(rockOnMeter <= 0.0f)
			{
				rockOnMeter = 0.0f;
			}

			rockOnText.GetComponent<TextMesh>().text = "Rock On: " + rockOnMeter.ToString("P1");
		}

		else if(rockOnMeter == 1.0f)
		{
			rockGodMeter += 0.01f * Time.deltaTime;

			if(rockGodMeter >= 1.0f)
			{
				rockGodMeter = 1.0f;
			}

			rockGodText.GetComponent<TextMesh>().text = "Rock GOD: " + rockGodMeter.ToString("P1");
		}

		if(playerShooting.isRockGod)
		{
			rockGodMeter -= 0.05f * Time.deltaTime;
			if(rockGodMeter <= 0.0f)
			{
				rockGodMeter = 0.0f;
			}

			rockGodText.GetComponent<TextMesh>().text = "Rock GOD: " + rockGodMeter.ToString("P1");
		}

		progress.transform.localScale = new Vector3(3.0f * rockOnMeter, 1.0f, 1.0f);
		rgProgress.transform.localScale = new Vector3(5.0f * rockGodMeter, 0.5f, 1.0f);
	}

	// Updates the currentEnemies count, as well as the score.
	public void enemyHit()
	{
		currentEnemies--;
		score++;

        changeScore();

		if(rockOnMeter < 1.0f && !(playerShooting.isRockOn || playerShooting.isRockGod))
		{
			rockOnMeter += 0.05f;

			// increasing size of progress bar
			Vector3 barScale = progress.transform.localScale;
			barScale += new Vector3 (0.15f, 0.0f, 0.0f);
			progress.transform.localScale = barScale;

			if(rockOnMeter >= 1.0f)
			{
				rockOnMeter = 1.0f;

				rockGodMeter += 0.333f;

				// increasing size of progress bar
				Vector3 rgBarScale = rgProgress.transform.localScale;
				rgBarScale += new Vector3 (1.0f, 0.0f, 0.0f);
				rgProgress.transform.localScale = rgBarScale;

				if(rockGodMeter >= 0.999f)
				{
					rockGodMeter = 1.0f;
				}

				rockGodText.GetComponent<TextMesh>().text = "Rock GOD: " + rockGodMeter.ToString("P1");
			}
		}

		rockOnText.GetComponent<TextMesh>().text = "Rock On: " + rockOnMeter.ToString("P1");
    }

    // Subtracts 1 HP from the player. Returns whether or not the player is still alive.
    public bool playerHit()
	{
		health--;

        changeLives();

        return health > 0;
	}

	// Returns the current score.
	public int getScore()
	{
		return score;
	}

    public void changeScore()
    {
        scoreText.GetComponent<TextMesh>().text = "Score: " + getScore()*10;
    }

    public void changeLives()
    {
        livesText.GetComponent<TextMesh>().text = "Lives: " + health;
    }

	public void cullEnemy()
	{
		currentEnemies--;
	}

	public float getRockOn()
	{
		return rockOnMeter;
	}

	public float getRockGod()
	{
		return rockGodMeter;
	}

	public void godBulletFired()
	{
		rockGodMeter = 0.0f;
		rockGodText.GetComponent<TextMesh>().text = "Rock GOD: " + rockGodMeter.ToString("P1");
	}
}

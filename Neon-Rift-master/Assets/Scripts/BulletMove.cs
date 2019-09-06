using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public float speed;

	private static float[] pitches = { 1f/1f, 9f/8f, 6f/5f, 4f/3f, 3f/2f, 8f/5f, 16f/9f, 15f/8f };	// The pitches at which the note can ring while still sounding good
	// From left to right: Tonic, Major 2nd, Minor 3rd, Perfect 4th, Perfect 5th, Minor 6th, Minor 7th
	// There's also a Major 7th in there for the special case of the dominant V at the end of the song

	private static Spawning spawning;
	private AudioSource myAudio;
	private static AudioSource BGM;

    // Use this for initialization
    void Start()
    {
		spawning = GameObject.Find("Game Manager").GetComponent<Spawning>();
		BGM = GameObject.Find("Game Manager").GetComponent<AudioSource>();
		myAudio = GetComponent<AudioSource>();

		// Break the song into its 3 sections, then each section into its chords
		// Then, randomly select one of the chord tones to play
		int[] trio;

		// First part
		if(BGM.time < (21.0f + (1.0f / 3.0f)))
		{
			// Mod (10 + 2/3)
			float musicTime = BGM.time % (10.0f + (2.0f / 3.0f));

			if(musicTime < (2.0f + (2.0f / 3.0f)))
			{
				// i chord
				trio = new int[] { 0, 2, 4 };
			}
			else if(musicTime < 4.0f)
			{
				// VI chord
				trio = new int[] { 5, 0, 2 };
			}
			else if (musicTime < (5.0f + (1.0f / 3.0f)))
			{
				// VII chord
				trio = new int[] { 6, 1, 3 };
			}
			else if (musicTime < 8.0f)
			{
				// i chord
				trio = new int[] { 0, 2, 4 };
			}
			else if (musicTime < (9.0f + (1.0f / 3.0f)))
			{
				// III chord
				trio = new int[] { 2, 4, 6 };
			}
			else
			{
				// iv chord
				trio = new int[] { 3, 5, 0 };
			}
		}

		// Second part
		else if(BGM.time < (37.0f + (1.0f / 3.0f)))
		{
			// Mod (5 + 1/3)
			float musicTime = BGM.time % (5.0f + (1.0f / 3.0f));

			if(musicTime < (2.0f + (2.0f / 3.0f)))
			{
				// iv chord
				trio = new int[] { 3, 5, 0 };
			}
			else
			{
				// i chord
				trio = new int[] { 0, 2, 4 };
			}
		}

		// End part
		else
		{
			float musicTime = BGM.time % (5.0f + (1.0f / 3.0f));

			if(musicTime < (1.0f + (1.0f / 3.0f)))
			{
				// VI chord
				trio = new int[] { 5, 0, 2 };
			}
			else if (musicTime < (2.0f + (2.0f / 3.0f)))
			{
				// VII chord
				trio = new int[] { 6, 1, 3 };
			}
			else
			{
				// V chord
				trio = new int[] { 4, 7, 1 };
			}
		}

		// Randomly pull this bullet's note from the list of valid pitches
		myAudio.pitch = pitches[trio[Random.Range(0, trio.Length)]];
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);

        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            Destroy(gameObject);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		// Bullets destroy enemies, but they don't destroy enemy A, which is a projectile
		if(other.gameObject.name.Substring(0, 5) == "Enemy" && other.gameObject.name[6] != 'A' && other.gameObject.tag != "Invunrable")
		{
			//animation code
			if (other.gameObject.name[6] == 'B')
			{
				//trigger animation
				other.gameObject.GetComponent<Animator>().SetTrigger("enBDead");
			}
			else if (other.gameObject.name[6] == 'C') {
				//trigger animation
				other.gameObject.GetComponent<Animator>().SetTrigger("enCDead");
			}

			Destroy(other.gameObject);
			spawning.enemyHit();
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorC : MonoBehaviour {
	public bool isJumping;
	private Rigidbody2D enemyCRigidbody;
	public float groundSpeed = -0.3f;
	public float jumpForce = 350.0f;
	public bool hitStage;
	public float jumpEndPos = 0;
	public float jumpSpeed = -0.02f;


	// Use this for initialization
	void Start()
	{
		jumpEndPos = this.transform.position.y - 0.2f;
		enemyCRigidbody = GetComponent<Rigidbody2D>();
		enemyCRigidbody.gravityScale = 0.25f;
		isJumping = true;
		hitStage = false;
	}

	// Update is called once per frame
	void Update()
	{

		if (isJumping)
		{
			enemyCRigidbody.constraints = RigidbodyConstraints2D.FreezeAll ^ RigidbodyConstraints2D.FreezePositionY;  // Unfreeze Y
			enemyCRigidbody.AddForce(new Vector2(0.0f, jumpForce));

		}
		if (enemyCRigidbody.velocity.y < 0 && this.transform.position.y > jumpEndPos - 0.1f && this.transform.position.y < jumpEndPos + 0.1f)
		{
			enemyCRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;   
		}
		if(this.transform.position.y > jumpEndPos - 0.1f && this.transform.position.y < jumpEndPos + 0.1f && enemyCRigidbody.velocity.y == 0)
		{
			GetComponent<Animator>().SetTrigger("enCRun");
			transform.Translate(new Vector3(groundSpeed, 0, 0) * Time.deltaTime);
			this.tag = "Invunrable";
		}
		else
		{
			transform.Translate(new Vector3(jumpSpeed, 0, 0) * Time.deltaTime);
		}
		isJumping = false;
	}
}
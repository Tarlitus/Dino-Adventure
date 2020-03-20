using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float maxSpeed = 5f;		
    public float speed = 2f;
    public bool grounded;
    public bool hit;
    public bool death;
    public float jumpPower = 6.5f;

    public AudioClip jumpSound;		//used to reproduce sounds
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip enemydefSound;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;		//used to change player color
    private bool jump;			//jump variable, true when player jumps
    private bool hop;			//used to create a little jump when an enemy is defeated
    private bool movement = true;	//used to disable movement when player is hit
    private int hitCount = 0;		//if its three, the player dies and respawn in the beginning

    AudioSource Audio;


    // Start is called before the first frame update
    void Start(){	
	 rb2d = GetComponent<Rigidbody2D>();
	 anim = GetComponent<Animator>();
	 spr = GetComponent<SpriteRenderer>();
	 Audio = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update(){
       anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));	//variables used in player animations
       anim.SetBool("Grounded", grounded);
       anim.SetBool("Hit", hit);
       anim.SetBool("Death", death);

       if(Input.GetKeyDown(KeyCode.UpArrow) && grounded)	//the player jumps if Up arrow is pressed and the player is in the ground
	{
		jump = true;
	}
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate(){

	Vector3 fixedVelocity = rb2d.velocity;
	fixedVelocity.x *= 0.75f;

	if(grounded)
	{
		rb2d.velocity = fixedVelocity;	//decrease aceleration of the player when is grounded
	}

    	float h = Input.GetAxis("Horizontal");
	if(!movement)
		h = 0;

	rb2d.AddForce(Vector2.right * speed * h);

	float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

	//switch direction of the sprite
	if(h > 0.1f){
		transform.localScale = new Vector3(1f, 1f, 1f);
	}
	if(h < -0.1f){
		transform.localScale = new Vector3(-1f, 1f, 1f);
	}


	if (jump){
		Audio.clip = jumpSound;
		Audio.Play ();
		rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
		rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
		jump = false;
	}

	if (hop){
		
		rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
		rb2d.AddForce(Vector2.up * jumpPower / 1.5f, ForceMode2D.Impulse);	//hop impulse is smaller than jump impulse
		hop = false;
	}

    }

    void OnBecameInvisible(){
	Invoke("Respawn", 0.3f);
    }

    public void EnemyHop(){
	Audio.clip = enemydefSound;
	Audio.Play ();

	hop = true;

    }

    public void EnemyKnockBack(float enemyPosX){
	
	hitCount++;
	if(hitCount == 3){
		Audio.clip = deathSound;
		Audio.Play ();
		rb2d.velocity = new Vector3(0f, 0f,0f);	//speed null
		spr.color = Color.white;
		death = true;
		movement = false;
		Invoke("DeathDelay", 0.5f);
		Invoke("Respawn", 0.7f);
		Invoke("EnableMovement", 0.8f);
	}else{
		Audio.clip = hitSound;
		Audio.Play ();

		hop = true;
		hit = true;
	
		float side = Mathf.Sign(enemyPosX - transform.position.x);
		rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
	
		movement = false;
		Invoke("EnableMovement", 0.4f);		//enable movement after a delay
		}
	
	if(hitCount == 1){
		spr.color = Color.yellow;
	}
	if(hitCount == 2){

		Color color = new Color(255/255f, 120/255f, 0/255f);	//orange in float code
		spr.color = color;
	}
     
    }

    //method where enable movement after the player is hit
    void EnableMovement(){
	movement = true;
	hit = false;
    }

    void DeathDelay(){
	death = false;

    }

    void Respawn(){
	hitCount = 0;
	spr.color = Color.white;
	rb2d.velocity = new Vector3(0f, 0f,0f);		//speed null
	transform.position = new Vector3(-12.657f,-3.331f,0);	//respawn coordinates
    }
}

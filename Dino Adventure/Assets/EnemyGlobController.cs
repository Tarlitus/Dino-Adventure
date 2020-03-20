using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobController : MonoBehaviour
{   public float maxSpeed = 0.75f;
    public float speed = 0.75f;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { rb2d.AddForce(Vector2.right * speed);
      float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
       rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

	//call collision method when speed is null
	if(rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)	{
       Invoke("Collision", 0f);
	}
    }

	//change direction when collide method
    void Collision (){

	speed = -speed;
	rb2d.velocity = new Vector2(speed, rb2d.velocity.y);

	//change direction of sprite
      if(speed < 0){
		transform.localScale = new Vector3(1f, 1f, 1f);
	 }
	 if(speed > 0){
		transform.localScale = new Vector3(-1f, 1f, 1f);
    	}

  
    }

     void OnTriggerEnter2D(Collider2D col){
	if(col.gameObject.tag == "Player"){

       float yOffset = 0.8f;
       if(transform.position.y + yOffset < col.transform.position.y){
		col.SendMessage("EnemyHop");	//call hop jump method
		Destroy(gameObject);
       } else 	{	col.SendMessage("EnemyKnockBack",transform.position.x);
			}
     }

	//change direction if touch other enemy
	if(col.gameObject.tag == "Enemy"){

		Invoke("Collision", 0f);

	}
    }
}

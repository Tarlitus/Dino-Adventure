using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBugController : MonoBehaviour
{
    public Transform Goal;
    public float speed;		//speed of the enemy
    
    private Rigidbody2D rb2d;
    private float fixedSpeed;
    private Vector3 start, end; //vectors for start and end position
    // Start is called before the first frame update
    void Start()
    { 
		rb2d = GetComponent<Rigidbody2D>();
    
		if(Goal != null){
		Goal.parent = null;		//Makes Goal independant of the parent
		start = transform.position;
		end = Goal.position;

	 }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    { 
	 if(Goal != null) {
      fixedSpeed = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, Goal.position, fixedSpeed);					//MoveTowards vector uses three values: start position, end position and distanceDelta
      }

	 if(transform.position == Goal.position){
		Goal.position = (Goal.position == start) ? end : start;		//if the enemy reachs Goal´s position, the position of the Goal switch
		
         //change direction of sprite
         if(Goal.position == end){
		transform.localScale = new Vector3(1f, 1f, 1f);
	     }
	    if(Goal.position == start){
		transform.localScale = new Vector3(-1f, 1f, 1f);
          }

	 }
    }

    void OnTriggerEnter2D(Collider2D col){
	if(col.gameObject.tag == "Player"){

       float yOffset = 0f;
	  if(transform.position.y + yOffset < col.transform.position.y){
		col.SendMessage("EnemyHop");	//call hop jump method
	Destroy(gameObject);
       } else 	{	col.SendMessage("EnemyKnockBack",transform.position.x);
			}
     }
    }


}


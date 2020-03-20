using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour

{   private Player_Controller player;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start(){
	player = GetComponentInParent<Player_Controller>();
	rb2d = GetComponentInParent<Rigidbody2D>();  
    }

    void OnCollisionEnter2D(Collision2D col){
	if(col.gameObject.tag == "Platform"){
	rb2d.velocity = new Vector3(0f, 0f,0f);	//when player touch the platform, speed is null
	player.transform.parent = col.transform;
	player.grounded = true;
	}	
    }

    void OnCollisionStay2D(Collision2D col){
	if(col.gameObject.tag == "Ground"){
	player.grounded = true;
	}
	if(col.gameObject.tag == "Platform"){
	player.transform.parent = col.transform;	//player follow the movement of the platform, making col.transform the new parent
	player.grounded = true;
	}

    }
    void OnCollisionExit2D(Collision2D col){
	if(col.gameObject.tag == "Ground"){
	player.grounded = false;
	}
	if(col.gameObject.tag == "Platform"){
	player.transform.parent = null;		//undo parent col.transform
	player.grounded = false;
	}

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{   
    public float fallDelay = 1f;			//time before platform falls
    public float respawnDelay = 3f;		//respawn time

    private Rigidbody2D rb2d;
    private Vector3 start;

    // Start is called before the first frame update
    void Start()
    {	rb2d = GetComponent<Rigidbody2D>();
	start = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
	if(col.gameObject.CompareTag("Player")){		//check if its the player	
	 Invoke("Fall", fallDelay);				//call Fall method after fallDelay
	 Invoke("Respawn", fallDelay + respawnDelay);	//call Respawn method after fallDelay and respawnDelay
	}
    }

    void Fall(){
	rb2d.isKinematic = false;		//is affected by gravity

    }
    void Respawn(){				//respawn method
	transform.position = start;	//respawn in initial position
	rb2d.isKinematic = true;		//gravity doesn´t affect
	rb2d.velocity = Vector3.zero;	//speed  zero
	

    }
    
}

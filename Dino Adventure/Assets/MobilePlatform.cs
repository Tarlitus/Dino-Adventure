using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{

    public Transform Goal;
    public float speed;		//speed of the platform

    private Vector3 start, end; //vectors for start and end position

    // Start is called before the first frame update
    void Start()
    { if(Goal != null){
	Goal.parent = null;		//Makes Goal independant of the mobile platform
	start = transform.position;
	end = Goal.position;

      }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    { if(Goal != null) {
      float fixedSpeed = speed * Time.deltaTime;
	transform.position = Vector3.MoveTowards(transform.position, Goal.position, fixedSpeed);	//MoveTowards vector uses three values: start position, end position and distanceDelta
      }
      if(transform.position == Goal.position){
	Goal.position = (Goal.position == start) ? end : start;		//if the platform reach Goal´s position, the position of the Goal switch
      }
        
    }

}

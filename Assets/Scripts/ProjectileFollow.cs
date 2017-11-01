using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour {

    public Transform projectile;
    public Transform farLeft;
    public Transform farRight;
    //public Transform top;
    //public Transform bottom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = transform.position;
        newPosition.x = projectile.position.x;
        //newPosition.y = projectile.position.y + 1.88f;
        newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x);
        //newPosition.y = Mathf.Clamp(newPosition.y, bottom.position.y, top.position.y);
        transform.position = newPosition;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = new Vector2(-4, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

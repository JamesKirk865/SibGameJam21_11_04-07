using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tape : MonoBehaviour{
public AudioSource Auf;
private Rigidbody2D tRB;
private bool fall;
    void Start(){
        tRB=gameObject.GetComponent<Rigidbody2D>();
		fall=false;
    }

    void FixedUpdate(){
		if(tRB.velocity.y==0){
			if(fall){
				Auf.Play();
				fall=false;
			}
		}else{
			fall=true;
		}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour{
public GameObject L,R,mm;
public bool alive;
private BoxCollider2D playerCol,col;

private int dT;
[SerializeField] private LayerMask pl;
    void Start(){
		dT=3;
        alive=false;
		playerCol=GameObject.Find("player").GetComponent<BoxCollider2D>();
		col=gameObject.GetComponent<BoxCollider2D>();
		mm=GameObject.Find("mainMenu");
    }

    void FixedUpdate(){
		if(alive){
			--dT;
			if(dT==0){
				gameObject.AddComponent<Rigidbody2D>();
				L.GetComponent<beam>().alive=true;
				R.GetComponent<beam>().alive=true;
			}
		}
		if(Physics2D.IsTouching(playerCol,col)){
			alive=true;
			mm.GetComponent<Canvas>().enabled=false;
			mm.GetComponent<AudioSource>().Stop();
			GameObject.Find("Most_upal").GetComponent<AudioSource>().Play();
		}
		if(col.IsTouchingLayers(pl)){
			Destroy(gameObject);
		}
		
    }
}
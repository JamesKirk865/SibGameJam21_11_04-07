using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMoving : MonoBehaviour{
	private float Ymin,dY;
	private GameObject player;
    void Start(){
        player=GameObject.Find("player");
		Ymin=0;
		dY=0;
    }

    void FixedUpdate(){
        gameObject.transform.position=new Vector3(gameObject.transform.position.x,player.transform.position.y<Ymin?Ymin:player.transform.position.y,gameObject.transform.position.z);
		if(gameObject.transform.position.y>dY){
			dY+=9f;
			Instantiate(GameObject.Find("wall"),new Vector3(0,dY,-10),Quaternion.identity);
			print("placed");
		}
    }
}
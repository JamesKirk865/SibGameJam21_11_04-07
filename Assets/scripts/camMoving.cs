using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMoving : MonoBehaviour{
	private float Ymin,dY,Ymax;
	private GameObject player;
	private bool doWalls;
    void Start(){
        player=GameObject.Find("player");
		Ymin=0;
		dY=0;
		Ymax=35f;
		doWalls=true;
    }

    void FixedUpdate(){
        gameObject.transform.position=new Vector3(gameObject.transform.position.x,player.transform.position.y<Ymin?Ymin:player.transform.position.y,gameObject.transform.position.z);
		Camera.main.backgroundColor=new Color((145+(int)569*(gameObject.transform.position.y/42))/1000f,(800+(int)141*(gameObject.transform.position.y/42))/1000f,(863+(int)86*(gameObject.transform.position.y/42))/1000f);
		//from RGBA(0.145, 0.800, 0.863, 0.000)
		//to RGBA(0.714, 0.941, 0.949, 0.000)
		if(gameObject.transform.position.y>dY&doWalls){
			dY+=9f;
			Instantiate(GameObject.Find("wall"),new Vector3(0,dY,-10),Quaternion.identity);
		}
		if(gameObject.transform.position.y>Ymax){
			doWalls=false;
		}
    }
}
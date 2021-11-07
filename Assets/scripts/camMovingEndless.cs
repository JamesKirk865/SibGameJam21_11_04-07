using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovingEndless : MonoBehaviour{
	private float Ymin,dY,Y,X;
	private GameObject player,dp;
	public GameObject plat,tape;
	private int k;
    void Start(){
        player=GameObject.Find("player");
		Ymin=0;
		dY=0;
		Y=0;
		k=0;
		for(float i=3;i<9;i+=1.5f){
			Instantiate(plat,new Vector3(UnityEngine.Random.Range(-5,5),-4+i,0),Quaternion.identity);
			dp=GameObject.Find("newPlatform(Clone)");
			dp.GetComponent<platform>().alive=true;
			dp.transform.name="sukablyat";
		}
		Instantiate(tape,new Vector3(dp.transform.position.x,dp.transform.position.y+0.5f,0),Quaternion.identity);
    }

    void FixedUpdate(){
        gameObject.transform.position=new Vector3(gameObject.transform.position.x,player.transform.position.y<Ymin?Ymin:player.transform.position.y,gameObject.transform.position.z);
		if(gameObject.transform.position.y>dY){
			dY+=9f;
			Instantiate(GameObject.Find("wall"),new Vector3(0,dY,-10),Quaternion.identity);
		}
		if(gameObject.transform.position.y>Y){
			if(k%2==0){
				X=UnityEngine.Random.Range(-3,3);
			}else{
				X=UnityEngine.Random.Range(-7,7);
			}
			Instantiate(plat,new Vector3(X,Y+5f,0),Quaternion.identity);
			dp=GameObject.Find("newPlatform(Clone)");
			dp.GetComponent<platform>().alive=true;
			dp.transform.name="sukablyat";
			Y+=UnityEngine.Random.Range(1.5f,2.2f);
			++k;
		}
		if(k>4){
				Instantiate(tape,new Vector3(X,Y+6f,0),Quaternion.identity);
				GameObject.Find("tape(Clone)").transform.name="plsno";
				k-=5;
			}
    }
}
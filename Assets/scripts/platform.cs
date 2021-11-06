using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour{
	public float ticks;
	private float dT;
	private int was,cur;
	private bool crack;
	private GameObject player;
	public float r;
	public bool alive;
	public Material red,blue;
	public Sprite[]list=new Sprite[5];
	private SpriteRenderer Sren;
	public int stage=5;
    void Start(){
		crack=false;
		Sren=GetComponent<SpriteRenderer>();
		player=GameObject.Find("player");
		if(stage!=5){
			was=5-stage;
			Sren.sprite=list[was-1];
		}else{
			was=0;
		}
        dT=(int)(ticks+1-was*ticks/5);
    }

    void FixedUpdate(){
		if(alive){
			if(crack){
				--dT;
				cur=(int)((ticks-dT)/(ticks/6));
				if(cur>was){
					Sren.sprite=list[cur-1];
					was=cur;
				}
				if(dT==1){
					Destroy(gameObject);
				}
			}else{
				if((gameObject.transform.position.y-player.transform.position.y)<r){
					crack=true;
				}
			}
		}
    }
	
	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.tag=="move"){
			player.GetComponent<player>().ren.material=red;
			player.GetComponent<player>().can=false;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if(coll.gameObject.tag=="move"){
			player.GetComponent<player>().ren.material=blue;
			player.GetComponent<player>().can=true;
		}
	}
}
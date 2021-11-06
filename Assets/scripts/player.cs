using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour{
public float speed,jump;
private Rigidbody2D pRB;
private BoxCollider2D leg;
[SerializeField] private LayerMask fl;
private int tape=0,Jlen=0;
private Vector3 mouse;
private GameObject newPlat;
private bool place,triggered,boostX2,jumped,jumpedX2;
public Renderer ren;
public bool can;
public Texture2D curT;
private Animator anim;
public Material blue,red,defaultM;
public Image[]icon=new Image[5];
public Sprite on,off;

	void Start(){
        pRB=gameObject.GetComponent<Rigidbody2D>();
		leg=gameObject.GetComponent<BoxCollider2D>();
		place=false;
		can=false;
		Cursor.SetCursor(curT,Vector2.zero,CursorMode.Auto);
		anim=gameObject.GetComponent<Animator>();
		for(int i=0;i<5;++i){
			icon[i].sprite=off;
		}
    }

    void FixedUpdate(){
		triggered=false;
		if(Input.GetAxis("Horizontal")!=0){
			anim.SetBool("walk",true);
			if(Input.GetAxis("Horizontal")>0){
				pRB.velocity=new Vector2(speed,pRB.velocity.y);
				gameObject.GetComponent<SpriteRenderer>().flipX=false;
			}else{
				pRB.velocity=new Vector2(-speed,pRB.velocity.y);
				gameObject.GetComponent<SpriteRenderer>().flipX=true;
			}
		}else{
			pRB.velocity=new Vector2(0,pRB.velocity.y);
			anim.SetBool("walk",false);
		}
		if(Input.GetKey(KeyCode.Space)){
			if(leg.IsTouchingLayers(fl)){
				pRB.velocity=new Vector2(pRB.velocity.x,jump/4);
				++Jlen;
				jumped=true;
				jumpedX2=false;
				anim.SetBool("jump",true);
			}else{
				if(Jlen>0){
					++Jlen;
					pRB.velocity=new Vector2(pRB.velocity.x,jump/20*Jlen);
					if(Jlen==20){
						Jlen=0;
					}
				}else{
					if(!jumped){
						if(boostX2){
							if(!jumpedX2){
								pRB.velocity=new Vector2(pRB.velocity.x,jump);
								jumped=true;
								jumpedX2=true;
							}
						}
					}
				}
			}
		}else{
			Jlen=0;
			jumped=false;
		}
		if(leg.IsTouchingLayers(fl)&(pRB.velocity.y==0)){
			anim.SetBool("jump",false);
		}
		if(place){
			mouse=Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newPlat.transform.position=new Vector3(mouse.x,mouse.y,0);
			if(Input.GetMouseButtonDown(0)){
				if(can){
					newPlat.GetComponent<BoxCollider2D>().isTrigger=false;
					newPlat.GetComponent<platform>().alive=true;
					Destroy(newPlat.GetComponent<Rigidbody2D>());
					newPlat.transform.name="platform";
					newPlat.tag="flat";
					newPlat.layer=8;
					place=false;
					--tape;
					icon[tape].sprite=off;
					can=false;
					//Cursor.visible=true;
					ren.material=defaultM;
				}
			}
		}else{
			if(tape>0){
				mouse=Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Instantiate(GameObject.Find("newPlatform"),new Vector3(mouse.x,mouse.y,0),Quaternion.identity);
				newPlat=GameObject.Find("newPlatform(Clone)");
				newPlat.AddComponent<Rigidbody2D>().gravityScale=0;
				newPlat.GetComponent<BoxCollider2D>().isTrigger=true;
				newPlat.tag="move";
				ren=newPlat.GetComponent<Renderer>();
				ren.material=blue;
				place=true;
				can=true;
				//Cursor.visible=false;
			}
		}
		
    }
	
	void OnTriggerEnter2D(Collider2D coll){
		if((coll.gameObject.tag=="tape")&(!triggered)){
			icon[tape].sprite=on;
			++tape;
			Destroy(coll.gameObject);
			triggered=true;
		}
		if(coll.gameObject.tag=="boostX2jump"){
			boostX2=true;
			Destroy(coll.gameObject);
		}
	}
	
	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.tag=="move"){
			ren.material=red;
			can=false;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if(coll.gameObject.tag=="move"){
			ren.material=blue;
			can=true;
		}
	}
}
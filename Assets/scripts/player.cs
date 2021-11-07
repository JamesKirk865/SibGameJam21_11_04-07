using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour{
public float speed,jump;
private Rigidbody2D pRB;
private BoxCollider2D leg;
[SerializeField] private LayerMask fl;
private int Jlen=0,tape=0;
private Vector3 mouse;
private GameObject newPlat;
private bool triggered,boostX2,jumped,jumpedX2,playd,pld;
public Renderer ren;
public bool place,can,end;
public Texture2D curT;
private Animator anim;
public Material blue,red,defaultM;
public Image[]icon=new Image[5];
public Sprite on,off;
public AudioSource Awalk,Ajump1,Ajump2,Aequip;
public GameObject Prun,Pjump,Ptake,Pbuild;

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
		playd=false;
		pld=false;
    }

    void FixedUpdate(){
		triggered=false;
		if(Input.GetAxis("Horizontal")!=0){
			anim.SetBool("walk",true);
			Instantiate(Prun,gameObject.transform.position,Quaternion.identity);
			if(!pld&playd){
				Awalk.Play();
				pld=true;
			}
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
			Awalk.Stop();
			pld=false;
		}
		if(Input.GetKey(KeyCode.Space)){
			if(leg.IsTouchingLayers(fl)){
				pRB.velocity=new Vector2(pRB.velocity.x,jump/4);
				++Jlen;
				jumped=true;
				jumpedX2=false;
				anim.SetBool("jump",true);
				Destroy(GameObject.Find("Run(Clone)"));
				Instantiate(Pjump,gameObject.transform.position,Quaternion.identity);
				Ajump1.Play();
				Awalk.Stop();
				pld=false;
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
								Ajump1.Play();
								Awalk.Stop();
								pld=false;
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
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		if(leg.IsTouchingLayers(fl)&(pRB.velocity.y==0)){
			anim.SetBool("jump",false);
			if(!playd){
				Ajump2.Play();
				playd=true;
			}
		}else{
			playd=false;
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
					ren.material=defaultM;
					Instantiate(Pbuild,newPlat.transform.position,Quaternion.identity);
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
			}
		}
		/*if(end){
			Destroy(newPlat);
			tape=0;
		}*/
    }
	
	void OnTriggerEnter2D(Collider2D coll){
		if((coll.gameObject.tag=="tape")&(!triggered)){
			icon[tape].sprite=on;
			++tape;
			Destroy(coll.gameObject);
			triggered=true;
			Aequip.Play();
			Instantiate(Ptake,coll.gameObject.transform.position,Quaternion.identity);
		}
		if(coll.gameObject.tag=="boostX2jump"){
			boostX2=true;
			Destroy(coll.gameObject);
		}
		if(coll.gameObject.tag=="trigger"){
			GameObject.Find("ost").GetComponent<AudioSource>().Stop();
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
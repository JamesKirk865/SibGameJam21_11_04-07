using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class toLevel1 : MonoBehaviour{
private Collider2D coll;
public int dTick;
private int dStick;
private bool go;
[SerializeField] private LayerMask pl;
private Image blackS;
private bool lvl1;
    void Start(){
        coll=gameObject.GetComponent<Collider2D>();
		go=false;
		dTick=50;
		if(GameObject.Find("UI")){
			lvl1=true;
		}
		if(lvl1){
			GameObject.Find("UI").GetComponent<Canvas>().enabled=false;
			blackS=GameObject.Find("startScreen").GetComponent<Image>();
			dStick=25;
		}
	}

    void FixedUpdate(){
        if(coll.IsTouchingLayers(pl)){
			Cursor.visible=false;
			if(lvl1){
				blackS.color=Color.black;
			}else{
				gameObject.transform.position=new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,-2);
				GameObject.Find("mainMenu").GetComponent<Canvas>().enabled=false;
			}
			go=true;
		}
		if(go){
			--dTick;
		}
		if(dTick==0){
			if(GameObject.Find("mainMenu")){
				GameObject.Find("ost").GetComponent<AudioSource>().Play();
			}
			SceneManager.LoadScene("level1");
		}
		if(dStick>0){
			blackS.color=new Color(0,0,0,(float)dStick/25);
			--dStick;
		}
		if(lvl1){
			if(dStick==0){
				Cursor.visible=true;
				GameObject.Find("UI").GetComponent<Canvas>().enabled=true;
			}
		}
    }
}
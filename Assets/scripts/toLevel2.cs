using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class toLevel2 : MonoBehaviour{
private Collider2D coll;
public int dTick;
private Image blckS;
[SerializeField] private LayerMask pl;
    void Start(){
        coll=gameObject.GetComponent<Collider2D>();
		if(GameObject.Find("ending")){
			GameObject.Find("ending").GetComponent<Canvas>().enabled=false;
			dTick=30;
			blckS=GameObject.Find("startScreen2").GetComponent<Image>();
		}
    }

    void FixedUpdate(){
        if(coll.IsTouchingLayers(pl)){
			gameObject.transform.position=new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,-2);
			SceneManager.LoadScene("level2");
		}
		if(dTick>0){
			--dTick;
			blckS.color=new Color(0,0,0,(float)dTick/25);
			if(dTick==0){
				GameObject.Find("ending").GetComponent<Canvas>().enabled=true;
			}
		}
    }
}
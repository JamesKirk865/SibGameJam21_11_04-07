using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toLevel2 : MonoBehaviour{
private Collider2D coll;
public int dTick;
private bool go;
[SerializeField] private LayerMask pl;
    void Start(){
        coll=gameObject.GetComponent<Collider2D>();
		go=false;
    }

    void FixedUpdate(){
        if(coll.IsTouchingLayers(pl)){
			gameObject.transform.position=new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,-2);
			if(GameObject.Find("mainMenu")){
				GameObject.Find("mainMenu").GetComponent<Canvas>().enabled=false;
			}
			go=true;
		}
		if(go){
			--dTick;
		}
		if(dTick==0){
			SceneManager.LoadScene("level2");
		}
    }
}
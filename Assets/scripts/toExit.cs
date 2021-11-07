using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toExit : MonoBehaviour{
[SerializeField] private LayerMask pl;
private BoxCollider2D coll;
    void Start(){
        coll=gameObject.GetComponent<BoxCollider2D>();
    }

    void Update(){
        if(coll.IsTouchingLayers(pl)){
			Application.Quit();
			print("exit");
		}
    }
}

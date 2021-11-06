using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakingPlat : MonoBehaviour{
private Collider2D coll;
[SerializeField] private LayerMask pl;
    void Start(){
        coll=gameObject.GetComponent<Collider2D>();
    }

    void FixedUpdate(){
        if(coll.IsTouchingLayers(pl)){
			Destroy(gameObject);
		}
    }
}
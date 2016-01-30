using UnityEngine;
using System.Collections;

public class ActivateByCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision){
        Debug.LogFormat("{0}",collision.collider.name);
        collision.collider.SendMessage("ActivateCollision",SendMessageOptions.DontRequireReceiver);
    }
}

using UnityEngine;
using System.Collections;

public class ActivateControl : MonoBehaviour {

    public float activationMaxDistance;

	void Start () {
	
	}
	
	void Update () {
	    if( Input.GetButtonDown("Fire1") ){
            RaycastHit hit;
            if( Physics.Raycast( transform.position, transform.forward, out hit, activationMaxDistance ) ){
                Debug.Log("A hit, a very papable hit!");

                hit.transform.SendMessage("Activate",SendMessageOptions.DontRequireReceiver);
            }
        }
	}
}

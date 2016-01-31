using UnityEngine;
using System.Collections;

public class Constellation : MonoBehaviour {

    ReturnToHub returnToHub;

	void Start () {
        returnToHub = FindObjectOfType<ReturnToHub>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
	}
	
	void Update () {
	
	}


}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToHub : MonoBehaviour {

    public string hub = "Hub";

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if( Input.GetKeyDown(KeyCode.Escape) ){
            Activate();
        }
	}

    void Activate(){
       SceneManager.LoadScene(hub);
    } 
}

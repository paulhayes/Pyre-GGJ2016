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
	    if( Input.GetButtonDown("Cancel") ){
            Activate();
        }
	}

    void RitualComplete(){
        GameObject.FindObjectOfType<RitualControl>().SendMessage("RitualComplete",SceneManager.GetActiveScene().name);
        Activate();
    }

    void Activate(){

       SceneManager.LoadScene(hub);
    } 
}

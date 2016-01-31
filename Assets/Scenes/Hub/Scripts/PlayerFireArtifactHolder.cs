using UnityEngine;
using System.Collections;

public class PlayerFireArtifactHolder : MonoBehaviour {

    public Artifact[] artifacts;

    Fire fire;

    GameObject currentlyHolding;

    public bool IsHolding {
        get {
            return currentlyHolding != null;
        }
    }

	void Start () {
	    fire = GameObject.FindObjectOfType<Fire>();
	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetButtonDown("Fire1") && currentlyHolding!=null){
            RaycastHit hit;
            if( fire.GetComponent<Collider>().Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hit, 1f ) ){
                currentlyHolding.SetActive(false);
                currentlyHolding = null;
                fire.SendMessage("Boost");
                if( fire.GetComponent<RitualControl>().IsGameComplete ){
                    fire.SendMessage("OnGameComplete");
                }     
            }
         
        }  
	}

    public void RitualComplete(string name){
        Debug.LogFormat("{0} ritual complete, going to hold reward",name);

        for(int i=0;i<artifacts.Length;i++){
            if( artifacts[i].ritual == name ){
                currentlyHolding = artifacts[i].artifact;
                currentlyHolding.SetActive(true);
                Debug.LogFormat("Found {0}",currentlyHolding.name);

                return;
            }
        }
        fire.SendMessage("Boost");
        if( fire.GetComponent<RitualControl>().IsGameComplete ){
                    fire.SendMessage("OnGameComplete");
                }

    }
}

[System.Serializable]
public class Artifact{
    public string ritual;
    public GameObject artifact;
}

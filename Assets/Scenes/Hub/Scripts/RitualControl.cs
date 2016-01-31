using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualControl : MonoBehaviour {

    Dictionary<string,bool> enabledRituals = new Dictionary<string, bool>();
    Dictionary<string, ActivateGame> ritualsRegistered = new Dictionary<string, ActivateGame>();
    List<string> completedRituals = new List<string>();
    void Awake(){
        
    }

	void Start () {
	    Debug.Log("Start Ritual Control");
	}
	
	void Update () {
	    
	}

    public void EnableRituals(){
        

        foreach(var ritual in ritualsRegistered.Values)
        {
            ritual.enabled = true;
            enabledRituals[ritual.sceneName] = true;
        }
    }

    public void RegisterRitual(ActivateGame ritual){

        ritualsRegistered[ritual.sceneName] = ritual;
        ritual.enabled = enabledRituals.ContainsKey(ritual.sceneName) && enabledRituals[ritual.sceneName];
        if( completedRituals.IndexOf(ritual.sceneName)!=-1 ){
            ritual.SendMessage("RitualComplete",SendMessageOptions.DontRequireReceiver);
        }
        else {
            ritual.SendMessage("RitualIncomplete",SendMessageOptions.DontRequireReceiver);

        }
    }

   public IEnumerator RitualComplete(string name){
        enabledRituals[name] = false;
        completedRituals.Add(name);
        Debug.LogFormat("Marking {0} as complete",name);
        yield return new WaitForSeconds(1f);
        PlayerFireArtifactHolder artifactHolder = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<PlayerFireArtifactHolder>();
        artifactHolder.RitualComplete(name);
    }




}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RitualControl : MonoBehaviour {

    public Image completeOverlay;
    public Text textOverlay;

    Dictionary<string,bool> enabledRituals = new Dictionary<string, bool>();
    Dictionary<string, ActivateGame> ritualsRegistered = new Dictionary<string, ActivateGame>();
    List<string> completedRituals = new List<string>();

    void Awake(){
        
    }

    public bool IsGameComplete {
        get {
            return completedRituals.Count == ritualsRegistered.Values.Count;
        }
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

    IEnumerator OnGameComplete(){
        yield return StartCoroutine( FadeInOverlay() );
        yield return StartCoroutine( FadeInText() );
    }

    IEnumerator OnGameOver(){
        yield return StartCoroutine( FadeInOverlay() );
        RememberPositonAndDirection.Reset();
        Destroy(gameObject);
        SceneManager.LoadScene("Hub");
    }

    IEnumerator FadeInOverlay(){
        float t = 0;
        while( t<1f ){
            yield return null;
            t+=Time.deltaTime;
            Color c = completeOverlay.color;
            c.a = t;
            completeOverlay.color = c;
        }

    }

    IEnumerator FadeInText(){
        float t = 0;
        while( t<2f ){
            yield return null;
            t+=Time.deltaTime;
            Color c = textOverlay.color;
            c.a = t;
            textOverlay.color = c;
        }
    }


}

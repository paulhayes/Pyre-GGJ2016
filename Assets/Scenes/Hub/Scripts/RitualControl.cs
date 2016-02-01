using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RitualControl : MonoBehaviour {

    public Image completeOverlay;
    public Text textOverlay;
    public Text introText;

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
        introText.enabled = true;
        StartCoroutine(FadeOutIntro());
	}
	
	void Update () {
	    
	}

    public void EnableRituals(){
        

        foreach(var ritual in ritualsRegistered.Values)
        {
            Debug.LogFormat("Enabling {0}",ritual.sceneName);
            ritual.activatable = true;
            enabledRituals[ritual.sceneName] = true;
        }
    }

    public bool IsRitualComplete(string sceneName){
        return completedRituals.IndexOf(sceneName)!=-1;
    }

    public void RegisterRitual(ActivateGame ritual){

        ritualsRegistered[ritual.sceneName] = ritual;
        bool isComplete = (completedRituals.IndexOf(ritual.sceneName)!=-1);
        Debug.LogFormat("RegisterRitual: {0} is {1}",ritual.sceneName,isComplete?"complete":"not complete");
        ritual.activatable = !isComplete && (enabledRituals.ContainsKey(ritual.sceneName) && enabledRituals[ritual.sceneName]);
        if( isComplete ){
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

        while(true){
            if( Input.GetButtonDown("Fire1") ){
                RememberPositonAndDirection.Reset();
                Destroy(gameObject);
                SceneManager.LoadScene("Hub");
            }
            yield return null;
        }
    }

    IEnumerator OnGameOver(){
        yield return StartCoroutine( FadeInOverlay() );
        RememberPositonAndDirection.Reset();
        Destroy(gameObject);
        SceneManager.LoadScene("Hub");
    }

    IEnumerator FadeOutIntro(){
        float t = 0;
        float d = 3f;
        Color textColor = introText.color;
        textColor.a = 0;
        introText.color = textColor;
        while( t<d ){
            yield return null;
            textColor = introText.color;
            t+=Time.deltaTime;
            textColor.a = t/d;
            introText.color = textColor;

        }

        t = 0;
        d = 10f;
        while( t<d ){
            yield return null;
            t+=Time.deltaTime;
            Color c = completeOverlay.color;
            textColor = introText.color;
            c.a = 1-t/d;
            textColor.a = 1-t/d;
            completeOverlay.color = c;
            introText.color = textColor;
            if( Input.GetButtonDown("Fire1") ){
                break;
            }

        }
        introText.enabled = false;
        completeOverlay.enabled = false;
    }

    IEnumerator FadeInOverlay(){
        completeOverlay.enabled = true;
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
        float d = 2f;
        Color c = textOverlay.color;
        while( t<d ){
            yield return null;
            t+=Time.deltaTime;
            c = textOverlay.color;
            c.a = t/d;
            textOverlay.color = c;
        }
        c.a = 1f;
        textOverlay.color = c;
    }


}

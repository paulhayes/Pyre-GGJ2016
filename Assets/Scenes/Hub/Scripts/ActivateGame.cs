using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActivateGame : MonoBehaviour {

    public string sceneName;

    void Awake(){
        FindObjectOfType<RitualControl>().RegisterRitual(this);
    }

    void Activate(){
        if(!enabled) return;
        PlayerFireArtifactHolder artifactHolder = GameObject.FindObjectOfType<PlayerFireArtifactHolder>();
        if( artifactHolder.IsHolding ){
            
        }
        else {
            RememberPositonAndDirection.Store();
            SceneManager.LoadScene(sceneName);          
        }
    }
}

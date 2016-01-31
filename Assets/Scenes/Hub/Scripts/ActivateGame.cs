using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActivateGame : MonoBehaviour {

    public string sceneName;
    public bool activatable;

    void Awake(){
        activatable = true;
        FindObjectOfType<RitualControl>().RegisterRitual(this);
    }

    void Update(){
        
    }

    void Activate(){
        if(!activatable) return;
        PlayerFireArtifactHolder artifactHolder = GameObject.FindObjectOfType<PlayerFireArtifactHolder>();
        if( artifactHolder.IsHolding ){
            
        }
        else {
            RememberPositonAndDirection.Store();
            SceneManager.LoadScene(sceneName);          
        }
    }
}

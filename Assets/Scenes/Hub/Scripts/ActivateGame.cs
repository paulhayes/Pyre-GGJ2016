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
        SceneManager.LoadScene(sceneName);
        RememberPositonAndDirection.Store();
    }
}

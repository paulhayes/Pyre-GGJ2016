using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActivateGame : MonoBehaviour {

    public string sceneName;

    void Start(){
        FindObjectOfType<RitualControl>().RegisterRitual(this);
    }

    void Activate(){
        if(!enabled) return;
        SceneManager.LoadScene(sceneName);
        RememberPositonAndDirection.Store();
    }
}

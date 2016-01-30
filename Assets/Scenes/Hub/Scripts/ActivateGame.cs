using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ActivateGame : MonoBehaviour {

    public string sceneName;

    void Activate(){
        SceneManager.LoadScene(sceneName);
        RememberPositonAndDirection.Store();
    }
}

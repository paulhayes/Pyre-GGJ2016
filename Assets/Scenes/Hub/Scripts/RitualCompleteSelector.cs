using UnityEngine;
using System.Collections;

public class RitualCompleteSelector : MonoBehaviour {

    public GameObject completeRitualObject;
    public GameObject incompleteRitualObject;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void RitualComplete(){
       completeRitualObject.SetActive(true);
       incompleteRitualObject.SetActive(false);
    }  

    void RitualIncomplete(){
        completeRitualObject.SetActive(false);
       incompleteRitualObject.SetActive(true);        
    }
}

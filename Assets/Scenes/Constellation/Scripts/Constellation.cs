using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constellation : MonoBehaviour {

    public Transform[] holes;
    public ConstellationData gameData;
    public Rigidbody[] marbles;
    public int marbleCount;
    public Transform marbleStartPosition;
     
    ReturnToHub returnToHub;

    List<int> filledHoles = new List<int>();

	void Start () {
        returnToHub = FindObjectOfType<ReturnToHub>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


	}
	
	void Update () {
	    if( Input.GetButton("Fire1") ){
            RaycastHit hit;
            if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2f)){
                int index = System.Array.IndexOf(holes,hit.transform);
                if( index != -1 ){
                    int filledHoldIndex = filledHoles.IndexOf(index);
                    if( filledHoldIndex==-1 ){
                        Add(index);
                    }
                    else{
                        Remove(index);
                    }

                }
            }
        }
	}

    void Add(int index){
        
        //holes[index]
        //filledHoles.Add(index);

    }

    void Remove(int index){
        //filledHoles.Remove(index);

    }


}

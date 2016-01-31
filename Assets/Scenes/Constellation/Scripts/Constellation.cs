using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Constellation : MonoBehaviour {

    public Transform[] holes;
    public ConstellationData gameData;
    public Rigidbody[] marbles;
    public int marbleCount;
    public Transform marbleStartPosition;
    public float offsetZ;
     
    ReturnToHub returnToHub;

    List<int> filledHoles = new List<int>();

	void Start () {
        returnToHub = FindObjectOfType<ReturnToHub>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


	}
	
	void Update () {
	    if( Input.GetButtonDown("Fire1") ){
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
        CancelInvoke("OnComplete");
        if(filledHoles.Count==marbles.Length){
            return;
        }
        Rigidbody marble = TopMarble();
        marble.isKinematic = true;
        marble.GetComponent<Collider>().enabled = false;
        marble.transform.parent = holes[index];
        marble.transform.localPosition = new Vector3(0,0,offsetZ);
        filledHoles.Add(index);
        CheckList();
    }

    void Remove(int index){
        CancelInvoke("OnComplete");
        Rigidbody marble = holes[index].GetChild(0).GetComponent<Rigidbody>();
        marble.transform.parent = marbleStartPosition;
        marble.transform.localPosition = Vector3.zero;
        marble.isKinematic = false;
        marble.GetComponent<Collider>().enabled = true;
        filledHoles.Remove(index);

        CheckList();
    }

    Rigidbody TopMarble(){
        return marbles.Where(r=>!r.isKinematic).Aggregate((m1,m2)=>m1.transform.position.y > m2.transform.position.y ? m1 : m2 );
    }

    void CheckList(){
        if( Enumerable.SequenceEqual(filledHoles.OrderBy(m=>m), gameData.holdIndexes.OrderBy(m=>m)) ){
            Invoke("OnComplete",1f);
        }
    }

    void OnComplete(){
        SendMessage("RitualComplete");

    }

}

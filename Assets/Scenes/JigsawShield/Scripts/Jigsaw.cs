using UnityEngine;
using System.Collections;

public class Jigsaw : MonoBehaviour {

    public Collider[] pieces;
    public Transform depthPosition;
    public float zMovement;

    Collider currentlyDragging;
    bool isDragging;

    void Start () {
	
	}
	
	void Update () {
	    if( Input.GetButtonDown("Fire1") ){
            RaycastHit hit;
            int index;
            if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2f ) && ((index=System.Array.IndexOf( pieces,hit.collider)) != -1 ) ){
                StartDrag( pieces[index] );
            }

        }
        if( Input.GetButtonUp("Fire1") ){
            StopDrag();
        }

        if( isDragging ) {
            
        }
	}

    void StartDrag(Collider piece){
        currentlyDragging = piece;
        Vector3 p = currentlyDragging.transform.position;
        p += Camera.main.transform.forward * zMovement;
        isDragging = true;
        
    }

    void StopDrag(){
        if( currentlyDragging != null ){
            Vector3 p = currentlyDragging.transform.position;
            p -= Camera.main.transform.forward * zMovement;
            currentlyDragging = null;
        }
        isDragging = false;
    }
}

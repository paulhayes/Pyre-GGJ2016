using UnityEngine;
using System.Collections;
using System.Linq;

public class Jigsaw : MonoBehaviour {

    public Collider[] pieces;
    public float zMovement;
    public BoxCollider dragPlane;
    public float snapPosition;
    Collider currentlyDragging;
    bool isDragging;
    Vector3 startPos;
    Vector3 dragPosDiff;


    void Start () {
        //Cursor.visible = true;
       // Cursor.lockState = CursorLockMode.None;

	}
	
	void Update () {
        RaycastHit hit;
	    if( Input.GetButtonDown("Fire1") ){
            int index;
            if( Physics.Raycast(Camera.main.ScreenPointToRay(MouseOverload.intelligentMousePosition), out hit, 2f ) && ((index=System.Array.IndexOf( pieces,hit.collider)) != -1 ) ){
                StartDrag( pieces[index], hit );
            }

        }
        if( Input.GetButtonUp("Fire1") ){
            StopDrag();
        }

        if( isDragging ) {
            if( dragPlane.Raycast(Camera.main.ScreenPointToRay(MouseOverload.intelligentMousePosition), out hit, 10f) ){
                currentlyDragging.transform.position = (hit.point) + dragPosDiff;

            }
        }
	}

    void StartDrag(Collider piece, RaycastHit hit){
        currentlyDragging = piece;
        Vector3 p = currentlyDragging.transform.position;
        p += Camera.main.transform.forward * zMovement;
        isDragging = true;
        dragPosDiff = currentlyDragging.transform.position - hit.point;
        startPos = hit.point;        
    }

    void StopDrag(){
        if( currentlyDragging != null ){
            Vector3 p = currentlyDragging.transform.position;
            p -= Camera.main.transform.forward * zMovement;
            currentlyDragging.transform.position = p;
            Snap();
            currentlyDragging = null;
            Check();
        }
        isDragging = false;

    }

    void Snap(){
        foreach( var piece in pieces ){
            if( Vector3.Distance( piece.transform.position,currentlyDragging.transform.position ) < snapPosition ){
                currentlyDragging.transform.position = piece.transform.position;

            } 
        }
    }

    void Check(){
        Vector3 pos = pieces[0].transform.position;
        bool isAllDone = pieces.Where(p=>p.transform.position==pos).Count() == pieces.Length;
        if( isAllDone ) {
            Debug.Log("Done");
            Invoke("OnComplete",1f);
        }

    }

    void OnComplete(){
        SendMessage("RitualComplete");
    }


}

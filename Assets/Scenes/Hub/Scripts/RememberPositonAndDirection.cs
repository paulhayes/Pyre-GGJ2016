using UnityEngine;
using System.Collections;

public class RememberPositonAndDirection : MonoBehaviour {

    static Quaternion rotation1;
    static Quaternion rotation2;
    static Vector3 position;
    static bool stored;
    static RememberPositonAndDirection instance;
    public Transform targetPosition;
    public Transform targetRotation1;
    public Transform targetRotation2;

    void Awake(){
        instance = this;
    }

	void Start () {
        if( stored ){
            targetRotation1.rotation = rotation1;
            targetRotation2.rotation = rotation2;
            targetPosition.position = position;
        }
	}
	
	void Update () {
	    
	}

    public static void Store(){
        rotation1 = instance.targetRotation1.rotation;
        rotation2 = instance.targetRotation2.rotation;

        position = instance.targetPosition.position;

        stored = true;
    }

    public static void Reset(){
        stored = false;
    }

   
}

using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

    public Transform source;
    public Transform target;
    public Vector3 multipler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        target.localRotation = Quaternion.Euler( Vector3.Scale( source.localRotation.eulerAngles,multipler ) );
	}
}

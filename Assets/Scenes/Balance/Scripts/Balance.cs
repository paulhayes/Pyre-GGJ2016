using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Linq;

public class Balance : MonoBehaviour {

    public float springiness;
    public float dampening;
    public Transform target;
    public float maxRotation;

    public BalanceData[] allData;
    BalanceData currentData;

    float goalBalance;
    float rotationSpeed;
    float currentRotation;

    public Toggle[] togglesUI;

    public Collider[] sources;
    public Collider[] destinations;

    ReturnToHub returnToHub;

	void Start () {
        returnToHub = FindObjectOfType<ReturnToHub>();
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

        currentData = allData[ PlayerPrefs.GetInt("BalanceProgress",0) % allData.Length ]; 
        for(int i=0;i<currentData.weights.Length;i++){
            togglesUI[i].GetComponentInChildren<Text>().text = currentData.weights[i].ToString();

            GameObject obj = Instantiate(currentData.objects[i]) as GameObject;
            obj.transform.position = sources[i].transform.position;
            obj.transform.parent = sources[i].transform;
            obj.name =  currentData.weights[i].ToString();
        }
        CalculateBalance();
	}

    void IncrementBalance(){
        PlayerPrefs.SetInt("BalanceProgress", PlayerPrefs.GetInt("BalanceProgress")+1);
    }
	
	void Update () {
        currentRotation = target.transform.localRotation.eulerAngles.x;
        if( currentRotation > 180 ) currentRotation-=360;
        rotationSpeed += springiness * ( goalBalance - currentRotation );
        rotationSpeed *= dampening;
        currentRotation += Time.deltaTime * rotationSpeed;
        if( currentRotation > maxRotation ){ 
            currentRotation = maxRotation;
            rotationSpeed = -rotationSpeed;
        }
        if( currentRotation < -maxRotation ){
            currentRotation = -maxRotation;
            rotationSpeed = -rotationSpeed;
        }

        RaycastHit hit;
        if(Input.GetButtonDown("Fire1")&& Physics.Raycast(Camera.main.ScreenPointToRay(MouseOverload.intelligentMousePosition),out hit,10f)){
            int sourceIndex = System.Array.IndexOf(sources,hit.collider);
            int destinationIndex = System.Array.IndexOf(destinations,hit.collider);

            //Debug.LogFormat("{0}, {1}, {2}",sourceIndex,destinationIndex,hit.transform.name);

            if( sourceIndex != -1 ){
                if( sources[sourceIndex].transform.childCount != 0 ){
                    Debug.Log(sources[sourceIndex].transform.childCount);
                    Transform obj = sources[sourceIndex].transform.GetChild(0);
                    if( obj!=null){
                        Add(obj.gameObject,sourceIndex);
                    }    
                }
            }
            if( destinationIndex != -1 ){
                if( destinations[destinationIndex].transform.childCount != 0 ){
                    Transform obj = destinations[destinationIndex].transform.GetChild(0);
                    if( obj!=null){
                        Remove(obj.gameObject,destinationIndex);
                    }    

                }

            }
        }

        target.transform.localRotation = Quaternion.Euler(currentRotation,0,0);



	}

    public void CalculateBalance(){
        CancelInvoke("OnComplete");
        List<Collider> currentWeights = destinations.Where(d=>d.transform.childCount>0).ToList();


        float currentRightWeight = 0;
        if(currentWeights.Count>0){
            currentRightWeight = currentWeights.Select(d=>float.Parse( d.transform.GetChild(0).name)).Aggregate((m,t)=>m+t);
        }
        goalBalance =  Mathf.Clamp( currentData.goalWeight - currentRightWeight, -1, 1 ) * maxRotation ;
        //Debug.LogFormat("{0}, {1}-{2}",goalBalance,currentRightWeight,currentData.goalWeight);
        if( goalBalance == 0 ){
            Invoke("OnComplete",2.5f);
        }
    }

    void OnComplete(){
        
        returnToHub.SendMessage("RitualComplete");
    }


    public void Add(GameObject obj, int index){

        

        Transform[] emptyDestination = destinations.Select(d=>d.transform).Where(t=>t.childCount == 0).ToArray();

        if(emptyDestination.Length>0){
            emptyDestination[0].GetComponent<Collider>().enabled = true;
            obj.transform.parent = emptyDestination[0];
            obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = Quaternion.identity;
        }
        CalculateBalance();
    }

    public void Remove(GameObject obj, int index){
        Transform[] emptySources = sources.Select(d=>d.transform).Where(t=>t.childCount == 0).ToArray();
        if(emptySources.Length>0){
            obj.transform.parent.GetComponent<Collider>().enabled = false;
            obj.transform.parent = emptySources[0];
            obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = Quaternion.identity;
        }
        CalculateBalance();
    }
}

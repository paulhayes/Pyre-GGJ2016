using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

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

	void Start () {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        currentData = allData[ PlayerPrefs.GetInt("BalanceProgress",0) % allData.Length ]; 
        for(int i=0;i<currentData.weights.Length;i++){
            togglesUI[i].GetComponentInChildren<Text>().text = currentData.weights[i].ToString();

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

        target.transform.localRotation = Quaternion.Euler(currentRotation,0,0);



	}

    public void CalculateBalance(){
        List<Toggle> onElements = togglesUI.Where(u=>u.isOn).ToList();
        float currentRightWeight = 0;
        if( onElements.Count > 0 ){
            currentRightWeight = onElements.Select(u=>float.Parse( u.GetComponentInChildren<Text>().text)).Aggregate((m,t)=>m+t);
        }
        goalBalance =  Mathf.Clamp( currentData.goalWeight - currentRightWeight, -1, 1 ) * maxRotation ;
        Debug.LogFormat("{0}, {1}-{2}",goalBalance,currentRightWeight,currentData.goalWeight);

    }
}

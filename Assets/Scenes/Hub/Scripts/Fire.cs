﻿using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    public float maxScale;
    public float minScale;
    public Color maxColor;
    public Color minColor;

    public Transform flames;

    public float timeRemaining;
    public float boostTime;

    public AudioSource fireCrackle;
    public AudioSource fireBoost;

    float startTime;

    public Light light;
    public Material material;

    static Fire instance;
    bool countingDown;

    MeshRenderer[] fireMeshes;
    Animator mAnimator;

    void Awake(){

        fireMeshes = GetComponentsInChildren<MeshRenderer>();
        mAnimator = GetComponentInChildren<Animator>();

        if( instance != null && instance != this ){
            Destroy(gameObject);
            return;
        } else {
            DontDestroyOnLoad( gameObject );
        }
        light.enabled = false;
        DisableMeshes();
        if( instance == null ){
            instance = this;
            //BeginCountdown();

        }

    }

	// Use this for initialization
	void Start () {
	    
        
	}
	
	// Update is called once per frame
	void Update () {
        if( countingDown ){
            timeRemaining-=Time.deltaTime;
            if( timeRemaining < 0 ){
                timeRemaining = 0;
                light.enabled = false;
                fireCrackle.volume = 0;
                DisableMeshes();
                countingDown = false;
                Debug.Log("GameOver");
                SendMessage("OnGameOver");
            }
            float t = Mathf.InverseLerp(0,startTime,timeRemaining);
            fireCrackle.volume = t;
            material.SetColor("_EmissionColor",Color.Lerp(minColor,maxColor,t));
            light.color = Color.Lerp(minColor,maxColor,t);

            flames.localScale = Vector3.one * ( light.intensity = Mathf.Lerp(minScale,maxScale,Mathf.Pow(t,0.9f)) );
        }
	}

    void Activate(){
        StartCoroutine( BeginCountdown() );
    }

    void ActivateCollision(){
        StartCoroutine( BeginCountdown() );
    }

    IEnumerator BeginCountdown(){
        fireCrackle.Play();
        SendMessage("EnableRituals");
        Debug.Log("Countdown starting");
        if( countingDown )  yield break;
        EnableMeshes();
        countingDown = true;
        light.enabled = true;
        float max = timeRemaining;
        timeRemaining = 1f;
        startTime = max;
        Update();
        float fillUpDuration = 5f;
        while(timeRemaining<max){
            timeRemaining += (max/fillUpDuration) * Time.deltaTime;

            yield return null;
         }

    }

    void StopCoundown(){
        countingDown = false;
    }

    void EnableMeshes(){
        mAnimator.enabled = true;
        foreach(MeshRenderer m in fireMeshes ){
            m.enabled = true;
        }

    }

    void DisableMeshes(){
        mAnimator.enabled = false;
        foreach(MeshRenderer m in fireMeshes ){
            m.enabled = false;
        }
    }

    void Boost(){
        fireBoost.Play();
        timeRemaining+=boostTime;
    }

    void OnGameComplete(){
        countingDown = false;
        timeRemaining = startTime;
    }
}

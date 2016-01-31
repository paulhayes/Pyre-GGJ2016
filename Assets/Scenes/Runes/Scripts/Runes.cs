using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Runes : MonoBehaviour {

    public Collider[] runesSources;
    public Collider[] runeSlots;
    public RuneData gameData;
	public AudioSource runePlaced;
	public AudioSource rightRune;
	public AudioSource rightRuneRightPlace;
    List<Transform> usedRunes = new List<Transform>();
    List<int> usedRuneIndexes = new List<int>();

	void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

	}
	
	void Update () {
        RaycastHit hit;
        if(  Input.GetButtonDown("Fire1") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2f ) ){
            
            int source = System.Array.IndexOf( runesSources, hit.collider );
            int dest = System.Array.IndexOf( runeSlots, hit.collider );
            if( source != -1 ){
                Add( runesSources[source], source );
            }
               /*
            else {
                Remove( runeSlots[] );
            }
            */
        }
	}

    void Add(Collider runeSlot, int index){
        if( runeSlots.Length == usedRunes.Count ){
            return;
        }

        if( runeSlot.transform.childCount == 0 ){
            return;
        }

        Transform rune = runeSlot.transform.GetChild(0);
        runePlaced.Play();

        usedRunes.Add( rune );
        usedRuneIndexes.Add( index );

        rune.parent = runeSlots[usedRunes.Count-1].transform;
        rune.localPosition = Vector3.zero;

        if( runeSlots.Length == usedRunes.Count ){
            Check();
        }
    }

    void Remove(){

    }

    void Check(){
        
        int rightNumberRightPlace = 0;
        int rightNumber = 0;
        for(int i=0;i<gameData.runeIndexes.Length;i++){
            int usedIndex = usedRuneIndexes.IndexOf( gameData.runeIndexes[i] );
            if( usedIndex == i ){
                rightNumberRightPlace++;
            }
            else if( usedIndex != -1 ){
                rightNumber++;
            } 
            Debug.LogFormat("{0},{1}",usedIndex,i);
        }

        Debug.LogFormat("RRRP={0} RRWP={1}",rightNumberRightPlace,rightNumber);

        if( rightNumberRightPlace == runeSlots.Length ){
            Invoke("OnComplete",2f);
        }
        else {
            Invoke("Reset",2f);
        }

        StartCoroutine( PlayResult(rightNumberRightPlace,rightNumber) );

        // ( , usedRuneIndexes.OrderBy(r=>r) );
    }

    IEnumerator PlayResult(int rightNumber, int rightNumberRightPlace){
        for(int j=0;j<rightNumberRightPlace;j++){
            rightRune.Play();
            yield return new WaitForSeconds(rightRuneRightPlace.clip.length);
        }
		for(int i=0;i<rightNumber;i++){
			rightRune.Play();
			yield return new WaitForSeconds(rightRune.clip.length);
		}
       yield break;
    }

    void OnComplete(){
        SendMessage("RitualComplete");
    }

    void Reset(){
        for(int i=0;i<usedRuneIndexes.Count;i++){
            usedRunes[i].parent = runesSources[usedRuneIndexes[i]].transform;
            usedRunes[i].localPosition = Vector3.zero;
        }

        usedRuneIndexes.Clear();
        usedRunes.Clear();
    }
}

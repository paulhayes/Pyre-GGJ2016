using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class BalanceData : ScriptableObject {
    public float[] weights;
    public float goalWeight;
    public GameObject[] objects;
}

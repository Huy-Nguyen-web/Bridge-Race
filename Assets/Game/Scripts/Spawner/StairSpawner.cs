using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    [SerializeField] private GameObject stairsPrefab;
    [SerializeField] private int amount;
    [SerializeField] private float horizontalSpaceBetweenStair;
    [SerializeField] private float verticalSpaceBetweenStair;
    private void Awake() {
        for(int i = 0; i < amount; i++){
            GameObject stair = Instantiate(stairsPrefab, transform.position + new Vector3(0, verticalSpaceBetweenStair * i, horizontalSpaceBetweenStair * i), Quaternion.identity);
            stair.name = i.ToString();
        }
    }
}
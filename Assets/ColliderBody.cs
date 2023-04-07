using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBody : MonoBehaviour
{
    [SerializeField] private Character parentObject;
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Character")) return;
        Character character = other.transform.GetComponent<Character>();
        if(character == parentObject) return;
        if(character.collectedBrick.Count > parentObject.collectedBrick.Count){
            parentObject.CharacterGotHit();
        } 
    }
}

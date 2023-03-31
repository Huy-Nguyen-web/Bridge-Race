using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevel : MonoBehaviour
{
    public UnityAction<Vector3> OnEndLevelAction;
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Character"){
            OnEndLevelAction?.Invoke(transform.position);
            other.transform.position = transform.position;
            other.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

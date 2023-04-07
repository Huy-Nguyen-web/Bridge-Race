using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevel : MonoBehaviour
{
    public UnityAction<Vector3> OnEndLevelAction;
    public UnityAction<bool> OnEndUIPopUp;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(Constant.TAG_CHARACTER)){
            other.GetComponent<Character>().isWin = true;
            OnEndLevelAction?.Invoke(transform.position);
            OnEndUIPopUp?.Invoke(other.GetComponent<Player>() != null);
            other.transform.position = transform.position;
            other.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

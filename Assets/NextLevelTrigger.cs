using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private BrickSpawner currentBrickSpawner;
    [SerializeField] private BrickSpawner previousBrickSpawner;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Character"){
            Enemy enemyScript = other.GetComponent<Enemy>();
            if(enemyScript != null){
                enemyScript.currentLevel += 1;
                enemyScript.CreateBrickList();
                enemyScript.MoveToBrick();
            }
            foreach(GameObject brick in currentBrickSpawner.bricks){
                if (brick.GetComponent<Brick>().brickColor == other.GetComponent<Character>().characterColor){
                    brick.SetActive(true);
                }
            }
            foreach(GameObject brick in previousBrickSpawner.bricks){
                if (brick.GetComponent<Brick>().brickColor == other.GetComponent<Character>().characterColor){
                    brick.SetActive(false);
                }
            }
        }
    }
}

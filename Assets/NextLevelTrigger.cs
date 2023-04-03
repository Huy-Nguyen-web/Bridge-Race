using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private BrickSpawner currentBrickSpawner;
    [SerializeField] private BrickSpawner previousBrickSpawner;
    [SerializeField] private GameObject wallBlock;
    [SerializeField] private int currentLevel;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Character"){
            Character characterScript = other.GetComponent<Character>();
            characterScript.currentLevel = currentLevel;
            Enemy enemyScript = other.GetComponent<Enemy>();
            wallBlock.SetActive(false);
            Invoke(nameof(CloseWallBlock), 0.5f);
            if(enemyScript != null){
                enemyScript.currentLevel = currentLevel;
                enemyScript.SwitchState(enemyScript.SeekBrickState);
                // enemyScript.CreateBrickList();
                // enemyScript.MoveToBrick();
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
    private void CloseWallBlock(){
        wallBlock.SetActive(true);
    }
}

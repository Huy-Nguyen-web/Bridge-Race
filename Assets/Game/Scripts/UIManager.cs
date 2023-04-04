using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private EndLevel endLevel;
    [SerializeField] private GameObject endLevelScreen;
    [SerializeField] private GameObject nextLevelButton;
    private void Start() {
        endLevelScreen.SetActive(false);
        endLevel = FindObjectOfType<EndLevel>();
        endLevel.OnEndUIPopUp += ShowEndScreen;
    }
    public void NextLevel(){
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevel > SceneManager.sceneCount - 1){
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);
   }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowEndScreen(bool isPlayer){
        endLevelScreen.SetActive(true);
        nextLevelButton.SetActive(isPlayer);
    }
}

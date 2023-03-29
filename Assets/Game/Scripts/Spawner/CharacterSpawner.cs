using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private ColorData colorData;
    [SerializeField] private float distanceBetweenCharacter;
    public UnityAction<GameObject> OnPlayerSpawn;
    private List<int> characterIndexes = new List<int>();
    private void Start() {
        // Instantiate player, and assign first color to player
        int playerIndex = Random.Range(0, 4);
        GameObject player = Instantiate(playerPrefab, transform.position + new Vector3(playerIndex * distanceBetweenCharacter, 1f, 0f), Quaternion.identity);
        OnPlayerSpawn?.Invoke(player);
        player.GetComponent<Player>().ChangeColor(playerIndex);
        characterIndexes.Add(playerIndex);
        
        // Instantiate enemy, and make sure the enemy index needs to different that previous index
        while(characterIndexes.Count < 4 ){
            int enemyIndex = Random.Range(0, 4);
            if (characterIndexes.Contains(enemyIndex)) continue;
            GameObject enemy = Instantiate(enemyPrefab, transform.position + new Vector3(enemyIndex * distanceBetweenCharacter, 1f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().ChangeColor(enemyIndex);
            characterIndexes.Add(enemyIndex);
        }
    }
}

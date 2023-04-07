using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    [SerializeField] private CharacterSpawner characterSpawner;
    [SerializeField] private EndLevel endLevel;
    private Vector3 endLevelPosition;
    public bool endGame = false;
    private void Awake(){
        characterSpawner.OnPlayerSpawn += OnPlayerSpawn;
        endLevel.OnEndLevelAction += EndGame;
    }
    private void LateUpdate() {
        if(!endGame){
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);
        } else {
            // TODO: Move the camera to the end level pillar
            Vector3 offset = new Vector3(0, 5, -5);
            transform.position = Vector3.Lerp(transform.position, endLevelPosition + offset, speed * Time.deltaTime);
        }
    }
    private void OnPlayerSpawn(GameObject player){
        this.player = player;
    }
    public void EndGame(Vector3 endLevelPosition){
        // Move the camera to the end pillar
        this.endLevelPosition = endLevelPosition;
        endGame = true;
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    [SerializeField] private CharacterSpawner characterSpawner;
    private void Awake(){
        characterSpawner.OnPlayerSpawn += OnPlayerSpawn;
    }
    private void Update() {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);
    }
    private void OnPlayerSpawn(GameObject player){
        this.player = player;
    }
}

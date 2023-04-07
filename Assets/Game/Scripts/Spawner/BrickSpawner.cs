using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private int column;
    [SerializeField] private int row;
    [SerializeField] private float spaceBetweenBrick;
    [SerializeField] private ColorData colorData;
    [SerializeField] private bool enableOnStart;
    public List<GameObject> bricks = new List<GameObject>();
    private void Awake() {
        for(int i = 0; i < column; i++){
            for (int j = 0; j < row; j++){
                GameObject brick = Instantiate(brickPrefab, new Vector3(transform.position.x + i * spaceBetweenBrick, transform.position.y + 0.25f,transform.position.z + j * spaceBetweenBrick), Quaternion.identity, transform);
                if(!enableOnStart){
                    brick.SetActive(false);
                }
                bricks.Add(brick);
            }
        }
    }
}

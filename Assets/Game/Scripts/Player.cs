using UnityEngine;

public class Player : Character 
{
    [SerializeField] private LayerMask groundLayer;
    private float deltaX, deltaY;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector2 startPosition;
    private float gravity = -10f;
    private DynamicJoystick joystick;
    private GroundBound currentGroundBound;
    private bool isMoving;
    private void Start() {
        joystick = FindObjectOfType<DynamicJoystick>();
        endLevel.OnEndLevelAction += EndGame;
        gotHit = false;
        UpdateGroundBound();
    }
    private void Update() {
        if(gameEnd) return;
        if(gotHit) return;
        direction = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized;
        RaycastHit stair;
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out stair, Mathf.Infinity, stairLayer)){
            Stair stairScript = stair.transform.gameObject.GetComponent<Stair>();
            Material stairMaterial = stair.transform.gameObject.GetComponent<MeshRenderer>().material;
            if(stairScript.stairColor != characterColor || stairMaterial == stairScript.defaultMaterial){
                if (direction.z >= 0f) direction.z = 0;
            }
        }
        if(transform.position.x > currentGroundBound.groundBoundRight){
            if(direction.x > 0){
                direction.x = 0;
            }
        }
        if(transform.position.x < currentGroundBound.groundBoundLeft){
            if(direction.x < 0){
                direction.x = 0;
            }
        }
        if(transform.position.z < currentGroundBound.groundBoundBottom){
            if(direction.z < 0){
                direction.z = 0;
            }
        }

        if(Vector3.Distance(Vector3.zero, direction) > 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }else{
            animator.SetBool("isRunning", false);
        }
        // Vector3 clampPosition = new Vector3(Mathf.Clamp(transform.position.x, currentGroundBound.groundBoundLeft, currentGroundBound.groundBoundRight), transform.position.y, Mathf.Clamp(transform.position.z, currentGroundBound.groundBoundBottom, Mathf.Infinity)); 
        // transform.position = clampPosition;

        RaycastHit ground;
        if(!Physics.Raycast(transform.position, Vector3.down, out ground, 1f, groundLayer)){
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }else{
            velocity.y = 0;
        }
    }
    public void UpdateGroundBound(){
        GameObject currentGroundLevel = GameObject.Find("GroundLevel" + currentLevel);
        currentGroundBound = currentGroundLevel.GetComponent<GroundBound>();
    }
}



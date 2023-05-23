using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 360;
    public CameraFollower CameraFollower;

    private Joystick joystick;
    private Entity player;
    private Rigidbody rb;
    private Vector3 movement;

    public Vector3 GetDirection()
    {
        return movement;
    }

    public void TakePlayer(EntityPlayer player)
    {
        this.player = player;
        CameraFollower.TakePlayer(this.player.gameObject);
        rb = this.player.GetComponent<Rigidbody>();
        joystick = IngameUI.instance.joystick;
        rb.maxAngularVelocity = player.Speed;
        rb.maxDepenetrationVelocity = player.Speed;
    }

    void FixedUpdate()
    {
        MovementLogic();
    }

    private void Update()
    {
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
            player.transform.rotation = rotation;
        }
    }

    private void MovementLogic()
    {
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;
        movement = new(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * player.Speed;
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameSettings gameSettings;

    private Animator animator;
    private Rigidbody rb;

    private void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate(){
        if (GetComponent<GrassCutting>().GetSlashingStatus())
            return;

        rb.velocity = new Vector3(joystick.Horizontal * gameSettings.moveSpeed, rb.velocity.y, joystick.Vertical * gameSettings.moveSpeed);

        animator.SetFloat("Movement", Mathf.Abs(joystick.Horizontal) + Mathf.Abs(joystick.Vertical));

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            transform.rotation = Quaternion.LookRotation(rb.velocity);

        else if (GetComponent<GrassCutting>().CheckGrass())
            GetComponent<GrassCutting>().SlashGrass();
    }
}
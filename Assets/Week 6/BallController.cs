using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] float speed = 2.0f;
    [SerializeField] [Range(1, 100)] public float lookYSensitivity;
    [SerializeField] [Range(1, 100)] public float lookXSensitivity;
    [SerializeField] private Camera fpCamera;


    private Vector3 moveBuffer;
    private float lookDeltaX = 0;
    private float lookDeltaY = 0;
    private float camDist;

    private float camAngleX = 45f;
    private float camAngleY = 0;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camDist = Vector3.Distance(fpCamera.transform.position, transform.position);
    }

    void SetCamera() {
        fpCamera.transform.rotation = Quaternion.Euler(camAngleX, camAngleY, 0);
        fpCamera.transform.position = transform.position - fpCamera.transform.forward * camDist;
    }

    // Update is called once per frame
    void Update()
    {

        if (moveBuffer.magnitude > 0)
        {
            rb.velocity =
               Quaternion.LookRotation(
                    Vector3.ProjectOnPlane(fpCamera.transform.forward, Vector3.up),
                    Vector3.up)
                * moveBuffer.normalized * speed;
        } else
        {
            rb.velocity = Vector3.zero;
        }

        SetCamera();

        lookDeltaX = 0;
        lookDeltaY = 0;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        moveBuffer = new Vector3(v.x, 0, v.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var lookDelta = context.ReadValue<Vector2>();
        // Debug.Log($"LOOK {lookDelta}");
        if (lookDelta.magnitude < 0.1f) return;
        lookDeltaX = lookDelta.x * Mathf.Sqrt(lookXSensitivity) * .01f;
        lookDeltaY = -lookDelta.y * Mathf.Sqrt(lookYSensitivity) * .01f;
        camAngleX += lookDeltaY;
        camAngleY += lookDeltaX;
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Pick Up"))
            Destroy(c.gameObject);
    }
}

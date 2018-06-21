using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{

    private float change = 50f;
    private float maxChange = 100f;
    public TextMeshProUGUI changeText;

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float mouseSensitivity = 2f;
    [SerializeField]
    private Camera firstPersonCamera;
    private Quaternion m_CameraTargetRot;
    private Rigidbody rb;

    public float jumpStrength = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool isGrounded = false;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        m_CameraTargetRot = firstPersonCamera.transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        changeText.text = change.ToString();

        // Character movement
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 velocity = ((transform.right * xMovement) + (transform.forward * zMovement)).normalized * movementSpeed;

        if (velocity != Vector3.zero)
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // Camera Movement
        float yRotation = Input.GetAxisRaw("Mouse X");
        float xRotation = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * mouseSensitivity;
        m_CameraTargetRot *= Quaternion.Euler (-xRotation, 0f, 0f);
        

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        firstPersonCamera.transform.localRotation = m_CameraTargetRot;
        // Jump
        if (!isGrounded)
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
                isGrounded = true;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.up * jumpStrength;
            isGrounded = false;
        }
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            change++;
            Destroy(other.gameObject);
        }
    }

    public void AddChange(float addedChange)
    {
        change = (addedChange + change > maxChange) ? change = 100 : change += addedChange;
    }

    public void loseChange(float lostChange)
    {
        change -= lostChange;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -90f, 90f);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}

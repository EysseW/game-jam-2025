using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Transform player;
    public float mouseSenstivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -100f, 100f);

        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0f);
        player.transform.rotation = Quaternion.Euler(0, yRotation, 0.0f);
    }
}

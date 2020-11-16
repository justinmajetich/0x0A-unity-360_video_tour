using UnityEngine;

public class DevCameraController : MonoBehaviour
{
    private float lookSpeed = 2.5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {

            float horizontal = -Input.GetAxis("Mouse X") * lookSpeed;
            float vertical = -Input.GetAxis("Mouse Y") * lookSpeed;

            Quaternion rot = transform.rotation;

            transform.rotation = Quaternion.Euler(rot.eulerAngles.x + vertical, rot.eulerAngles.y + horizontal, 0f);
        }
    }
}

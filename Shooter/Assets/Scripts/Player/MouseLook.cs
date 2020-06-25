using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 1f;
    public Transform player;

    private float rotationX = 0;
    /*private float rotationY = 0;
    private float minimumVert = -45.0f; 
    private float maximumVert = 45.0f;*/

    private void FixedUpdate()
    {
        /*rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

        var delta = Input.GetAxis("Mouse X") * sensitivity;
        rotationY = transform.localEulerAngles.y + delta;
        
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);*/
        var mouseX = Input.GetAxis("Mouse X") * sensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}

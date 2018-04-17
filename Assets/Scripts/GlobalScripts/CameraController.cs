using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float scrollSpeed = 500.0f;
    public float moveSpeed = 40;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.mousePosition.y >= Screen.height * 0.95) // Mouse on top of the screen, move camera forward.
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }
        if (Input.mousePosition.y <= 0.05)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width * 0.95)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }
        if (Input.mousePosition.x <= 0.05)
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.up * 2 * Mathf.Sin(60) * Time.deltaTime * scrollSpeed * Input.GetAxis("Mouse ScrollWheel"), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed * Input.GetAxis("Mouse ScrollWheel"), Space.World);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.up * 2 * Mathf.Sin(60) * Time.deltaTime * scrollSpeed * Input.GetAxis("Mouse ScrollWheel"), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed * Input.GetAxis("Mouse ScrollWheel"), Space.World);
        }
    }
}


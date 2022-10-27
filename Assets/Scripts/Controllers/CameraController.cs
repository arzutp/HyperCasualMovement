using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float speed = 0.05f;
    float zoomSpeed = 10f;
    float rotateSpeed = 0.01f;

    float maxHeight = 30f;
    float minHeight = 3f;

    Vector2 mousePosition1;
    Vector2 mousePosition2;

    Vector3 temp;
    public GameObject player;
    void Start()
    {
        temp = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x+temp.x, transform.position.y, player.transform.position.z + temp.z);

        float horizontal = speed * Input.GetAxis("Horizontal");
        float vertical = speed * Input.GetAxis("Vertical");
        float scrool = -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if ((transform.position.y >= maxHeight) && (scrool > 0))
        {
            scrool = 0;
        }
        else if ((transform.position.y <= minHeight) && (scrool < 0))
        {
            scrool = 0;
        }
        if ((transform.position.y + scrool) > maxHeight)
        {
            scrool = maxHeight - transform.position.y;
        }
        else if ((transform.position.y + scrool) < minHeight)
        {
            scrool = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrool, 0);
        Vector3 lateralMove = horizontal * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vertical;

        Vector3 move = verticalMove + lateralMove + forwardMove;
        transform.position += move;

        GetCameraRotationUpdate();
    }

    void GetCameraRotationUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePosition1 = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            mousePosition2 = Input.mousePosition;

            float dx = (mousePosition2 - mousePosition1).x * rotateSpeed;
            float dy = (mousePosition2 - mousePosition1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            mousePosition1 = mousePosition2;
        }
    }
}

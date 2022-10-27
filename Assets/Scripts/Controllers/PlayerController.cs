using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] PlayerModel playerModel;
    [SerializeField] Rigidbody rb;
    Vector3 direction;

    public void Start()
    {
        transform.position = Vector3.zero;
        playerModel.SetAnimReset();
    }

    private void Update()
    {
        playerMovement();
    }

    #region movement
    private void playerMovement()
    {
            if (Input.GetMouseButton(0))  
            {
                playerModel.SetRun(true);
                Plane plane = new Plane(Vector3.up, transform.position);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out var distance))
                    direction = ray.GetPoint(distance);

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(direction.x, 0f, direction.z), playerSpeed * Time.deltaTime);
                transform.LookAt(direction);
                Vector3 offset = direction - transform.position;
                if (offset.magnitude > 1f)
                    transform.LookAt(direction);
            }
            if (Input.GetMouseButtonUp(0))
            {
                playerModel.SetRun(false);
            }

    }
    #endregion
}

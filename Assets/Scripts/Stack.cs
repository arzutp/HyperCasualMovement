using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{   
    Vector3 currentSpawnObject;
    int spawnListIndexCounter = 0;
    public List<GameObject> spawnableObjects = new List<GameObject>();
    public List<GameObject> playerFollowers = new List<GameObject>();
    float zPos = 0f;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Spawner":
                spawnableObjects.Add(other.transform.gameObject);
                other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

                if (spawnableObjects.Count == 1)
                {
                    currentSpawnObject = new Vector3(other.transform.position.x, transform.position.y + 1f, other.transform.position.z);
                    other.gameObject.transform.localPosition = currentSpawnObject;
                    other.gameObject.GetComponent<SpawnableObject>().UpdateCubePosition(transform, true);
                }
                else if (spawnableObjects.Count > 1)
                {
                    other.gameObject.transform.position = currentSpawnObject;
                    currentSpawnObject = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.2f, other.transform.position.z);
                    other.gameObject.GetComponent<SpawnableObject>().UpdateCubePosition(spawnableObjects[spawnListIndexCounter].transform, true);
                    spawnListIndexCounter++;
                }
                break;
            case "PlayerFollower":
                playerFollowers.Add(other.transform.gameObject);
                other.transform.SetParent(transform);
                other.transform.GetComponent<PlayerFollowerModel>().SetRun(true);
                other.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                zPos -= other.transform.localScale.z - 0.2f;
                other.gameObject.transform.localPosition = new Vector3(0, 0, zPos);
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    
    private Vector3 _firstCubePos;
    private Vector3 _currentCubePos;
    private int _cubeListIndexCounter = 0;
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
                    _firstCubePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                    _currentCubePos = new Vector3(other.transform.position.x, _firstCubePos.y, other.transform.position.z);
                    other.gameObject.transform.localPosition = _currentCubePos;
                    //other.gameObject.transform.localPosition = new Vector3(0, transform.position.y, 0);
                    _currentCubePos = new Vector3(other.transform.position.x, base.transform.position.y, other.transform.position.z);
                    other.gameObject.GetComponent<SpawnableObject>().UpdateCubePosition(base.transform, true);
                }
                else if (spawnableObjects.Count > 1)
                {
                    other.gameObject.transform.position = _currentCubePos;
                    _currentCubePos = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.2f, other.transform.position.z);
                    other.gameObject.GetComponent<SpawnableObject>().UpdateCubePosition(spawnableObjects[_cubeListIndexCounter].transform, true);
                    _cubeListIndexCounter++;
                }
                break;
            case "PlayerFollower":
                playerFollowers.Add(other.transform.gameObject);
                other.transform.GetComponent<PlayerFollowerModel>().SetRun(true);
                //other.transform.SetParent(stackParent);
                other.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                other.gameObject.transform.localPosition = new Vector3(0, -0.35f, zPos);
                zPos -= other.transform.localScale.z - 0.2f;
                break;
            default:
                break;
        }
    }
    private void LerpMove(List<GameObject> lerpList)
    {
        
        for (int i = 0; i < lerpList.Count; i++)
        {
            lerpList[i].transform.localPosition = new Vector3(Mathf.Lerp(lerpList[i].transform.localPosition.x, playerTransform.position.x, Time.deltaTime*10 * (lerpList.Count - i)),
            lerpList[i].transform.localPosition.y,
            Mathf.Lerp(lerpList[i].transform.localPosition.z, playerTransform.localPosition.z, Time.deltaTime * 10 * (lerpList.Count - i)));
        }
    }
}

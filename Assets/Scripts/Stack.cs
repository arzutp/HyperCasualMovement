using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Transform playerTransform;
    float yPos = 0f;
    float zPos=0f;
    List<GameObject> spawnableObjects = new List<GameObject>();
    List<GameObject> playerFollowers = new List<GameObject>();
    private void Update()
    {
        LerpMove(spawnableObjects);
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Spawner":
                other.transform.SetParent(parent);
                spawnableObjects.Add(other.transform.gameObject);
                other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                other.gameObject.transform.localPosition = new Vector3(0, parent.position.y + yPos, 0);
                other.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                yPos += 0.2f;
                break;
            case "PlayerFollower":
                playerFollowers.Add(other.transform.gameObject);
                other.transform.GetComponent<PlayerFollowerModel>().SetRun(true);
                other.transform.SetParent(parent);
                other.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                other.gameObject.transform.localPosition = new Vector3(0, -0.35f, zPos);
                zPos -= other.transform.localScale.z-0.2f;
                break;
            default:
                break;
        }
    }

    private void LerpMove(List<GameObject> lerpList)
    {
        for (int i = 0; i < lerpList.Count; i++)
        {
            lerpList[i].transform.localPosition = new Vector3(Mathf.Lerp(lerpList[i].transform.localPosition.x, playerTransform.localPosition.x, Time.deltaTime * 2 * (lerpList.Count - i)),
            lerpList[i].transform.localPosition.y,
            lerpList[i].transform.localPosition.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public GameObject blueRobotPrefab;
    public GameObject yellowRobotPrefab;
    public GameObject catPrefab;
    public Transform attachPoint;
    private GameObject petInstance;
    /*
    void Start()
    {
        if (GameManager.Instance == null) return;
        PetType petType = GameManager.Instance.equippedPet;

        if(petType == PetType.None) return;
        if (!GameManager.Instance.petUnlocks[petType]) return;

        GameObject prefabToSpawn = null;
        switch (petType)
        {
            case PetType.BlueRobot:
                prefabToSpawn = blueRobotPrefab;
                break;
            case PetType.YellowRobot:
                prefabToSpawn = yellowRobotPrefab;
                break;
            case PetType.Cat:
                prefabToSpawn = catPrefab;
                break;
        }

        if(prefabToSpawn != null)
        {
            var petInstance = Instantiate(prefabToSpawn);
            petInstance.transform.SetParent(attachPoint, false);
            petInstance.transform.localPosition = Vector3.zero;
            petInstance.transform.localRotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}

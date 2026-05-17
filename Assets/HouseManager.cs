using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public Camera mainCamera;

    private Dictionary<string, GameObject> housesDict = new Dictionary<string, GameObject>();
    private GameObject currentHouse;

    void Start()
    {
        LoadAllHouses();
    }

    void LoadAllHouses()
    {
        GameObject[] houses = Resources.LoadAll<GameObject>("Houses");

        foreach (GameObject house in houses)
        {
            housesDict.Add(house.name, house);
        }

        Debug.Log("Loaded Houses: " + housesDict.Count);
    }

    // 👇 هذا هو المهم (Flutter بترسل له ID)
    public void ShowHouseById(string houseId)
    {
        if (currentHouse != null)
        {
            Destroy(currentHouse);
        }

        if (housesDict.ContainsKey(houseId))
        {
            currentHouse = Instantiate(housesDict[houseId]);

            // وضعه في مركز المشهد
            currentHouse.transform.position = Vector3.zero;

            // ربطه بالكاميرا (اختياري حسب تصميمك)
            mainCamera.transform.LookAt(currentHouse.transform);
        }
        else
        {
            Debug.LogError("House ID not found: " + houseId);
        }
    }
}
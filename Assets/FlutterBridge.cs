using UnityEngine;

public class FlutterBridge : MonoBehaviour
{
    public HouseManager houseManager;

    // Flutter رح ينادي هذه الدالة
    public void OnMessageReceived(string message)
    {
        Debug.Log("Received from Flutter: " + message);

        houseManager.ShowHouseById(message);
    }
}
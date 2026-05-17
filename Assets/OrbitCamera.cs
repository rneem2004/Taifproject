using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        // 🖱️ Laptop (Mouse)
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * speed;
            transform.RotateAround(target.position, Vector3.up, x);
        }

        // 📱 Mobile (Touch)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float x = touch.deltaPosition.x * speed * 0.1f;
                transform.RotateAround(target.position, Vector3.up, x);
            }
        }
    }
}

using UnityEngine;

public class RotateHouse : MonoBehaviour
{
    public float speed = 0.2f;

    void Update()
    {
        // 👆 موبايل (Touch)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotX = touch.deltaPosition.x * speed;
                transform.Rotate(Vector3.up, -rotX);
            }
        }

        // 🖱️ لابتوب (Mouse)
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * speed * 100;
            transform.Rotate(Vector3.up, -rotX);
        }
    }
}
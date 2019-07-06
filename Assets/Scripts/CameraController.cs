
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 100f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 200f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;

        //if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)

        if (Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        //if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            rot.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            rot.y += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
        transform.eulerAngles = rot;

    }
}

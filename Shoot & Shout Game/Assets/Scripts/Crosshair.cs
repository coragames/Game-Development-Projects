using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}

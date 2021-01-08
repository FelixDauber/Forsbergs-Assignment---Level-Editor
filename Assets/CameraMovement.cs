using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.position -= (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed);
    }
    public void Center()
    {
        transform.position = new Vector2(TileSet.tileSet.offset, TileSet.tileSet.offset);
    }
}

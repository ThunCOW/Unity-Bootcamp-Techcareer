using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform Player;

    public float MouseSpeed;

    Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;

        rotation.x += Input.GetAxis("Mouse X") * MouseSpeed;
        rotation.y -= Input.GetAxis("Mouse Y") * MouseSpeed;

        //Quaternion QT = Quaternion.Euler(localRot.x, localRot.y, 0);
        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }
}

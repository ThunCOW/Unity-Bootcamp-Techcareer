using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hareket : MonoBehaviour
{
    public float hareketHizi;

    private void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 hareketVek = new Vector3(Horizontal, 0, Vertical) * Time.deltaTime * hareketHizi;
        transform.Translate(hareketVek);
    }
}
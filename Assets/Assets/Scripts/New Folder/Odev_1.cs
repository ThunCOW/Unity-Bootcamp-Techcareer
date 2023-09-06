using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odev_1 : MonoBehaviour
{
    public int Can = 3;

    public float Odev;

    public string KarakterIsim;

    // Start is called before the first frame update
    void Start()
    {
        Metod_1();
        Metod_2();
        Metod_3();
    }

    private void Metod_1()
    {
        if (Can > 0)
        {
            Debug.Log("Karakter Hayatta");
        }
        else
        {
            Debug.Log("Karakter Öldü");
        }
    }

    private void Metod_2()
    {
        switch (Odev)
        {
            case 0:
                Debug.Log("Ödeve Baþlanmadý");
                break;
            case .25f:
                Debug.Log("Ödevin Çeyreði Tamamlandý");
                break;
            case .5f:
                Debug.Log("Ödevin Yarýsý Tamamlandý");
                break;
            case 1:
                Debug.Log("Ödev Tamamlandý");
                break;
        }
    }

    private void Metod_3()
    {
        while(KarakterIsim.Length > 0)
        {
            Debug.Log(KarakterIsim[0]);
            KarakterIsim = KarakterIsim.Substring(1);
        }
    }
}

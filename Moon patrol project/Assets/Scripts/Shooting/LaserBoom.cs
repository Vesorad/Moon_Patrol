using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoom : MonoBehaviour
{
    [SerializeField] bool destory;
    private void Update()
    {
        if (destory == true)
        {
            Destroy(gameObject); 
        }
    }

}

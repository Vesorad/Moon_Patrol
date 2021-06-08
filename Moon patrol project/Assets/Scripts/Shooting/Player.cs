using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speedLaser=2;
    [SerializeField] float movmentSpeed = 30;

    void Start()
    {
        
    }

    void Update()
    {
        //RUCH GRACZA//
        float translation = Input.GetAxis("Vertical") * movmentSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * movmentSpeed * Time.deltaTime;       
        transform.Translate(rotation, translation, 0); 

        //STRZELANIE GRACZA//
        if (Input.GetKeyDown("x"))
        {

            
           
        }
      
    }
}

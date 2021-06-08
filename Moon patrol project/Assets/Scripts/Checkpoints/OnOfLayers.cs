using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOfLayers : MonoBehaviour
{
    [SerializeField] GameObject[] offLayers;
    [SerializeField] GameObject[] onLayers;
    [SerializeField] GameObject[] offObstacles;
    public float groundResetX;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var item in offLayers)
            {
                item.SetActive(false);
            }

            foreach (var item in onLayers)
            {
                item.SetActive(true);
            }
        }
    }

    public void OffObstacles()
    {
        for (int i = 0; i < offObstacles.Length; i++)
        {
            if (offObstacles[i] != null)
            {
                Destroy(offObstacles[i]);
            }
        }
    }
}

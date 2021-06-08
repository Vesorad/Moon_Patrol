using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersSpriteChanger : MonoBehaviour
{
    Ground ground;

    [SerializeField] Sprite state0;
    [SerializeField] Sprite state1;
    [SerializeField] Sprite state2;

    private void Awake()
    {
        ground = FindObjectOfType<Ground>();

        switch (ground.numberOfMapState)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = state0;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = state1;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = state2;
                break;
        }

    }
}

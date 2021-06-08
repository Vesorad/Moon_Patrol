using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInHole : MonoBehaviour
{
    [SerializeField] float deltaX;
    [SerializeField] float deltaY;

    [HideInInspector] public float newPosX;
    [HideInInspector] public float newPosY;

    [HideInInspector] public Vector3 lastPosition;

    private void Update()
    {
        newPosX = lastPosition.x + deltaX;
        newPosY = lastPosition.y + deltaY;
    }
}

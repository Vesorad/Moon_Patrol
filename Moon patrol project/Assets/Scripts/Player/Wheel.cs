using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Wheel : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
}

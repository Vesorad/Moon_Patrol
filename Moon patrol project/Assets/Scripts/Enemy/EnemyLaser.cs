using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [Header("Ustawienia lasera")]
    [SerializeField] float laserSpeed = 5;
    [SerializeField] List<GameObject> DestroyPos;

    AudioManager audioManager;
    int random;
    Vector3 targetPos;
    Vector3 vectorToTarget;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        random = UnityEngine.Random.Range(0, DestroyPos.Count);
    }
    void Update()
    {
        MoveToPoint();
        RotateToPoint();
    }

    public void MoveToPoint()
    {
        var targetPos = DestroyPos[random].transform.position;
        var speedLaser = laserSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speedLaser);
    }

    private void RotateToPoint()
    {
        Vector3 vectorToTarget = DestroyPos[random].transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * laserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            audioManager.Play("EnemyMissle");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_Hole : MonoBehaviour
{
    [SerializeField] GameObject hole;
    [SerializeField] GameObject Bum;

    Ground ground;

    private void Awake()
    {
        ground = FindObjectOfType<Ground>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            var holePos = new Vector2(transform.position.x, transform.position.y - 0.4f);
            var holeObject = Instantiate(hole, holePos, Quaternion.Euler(0, 0, 0), ground.gameObject.transform);
            holeObject.AddComponent<DestroyHole>();
            var BumPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
            GameObject BumAnim = Instantiate(Bum, BumPos, Quaternion.Euler(0, 0, 0), ground.gameObject.transform);
            Destroy(BumAnim, 0.3f);

            Destory();
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "LaserUp")
        {
            Destory();
        }
        if (collision.gameObject.tag == "HoleCheck")
        {
            Destory();
        }
    }

    private void Destory()
    {
        Destroy(gameObject);
    }
}

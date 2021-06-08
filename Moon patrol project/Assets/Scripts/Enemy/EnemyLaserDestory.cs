using UnityEngine;

public class EnemyLaserDestory : MonoBehaviour
{
   [SerializeField] GameObject groundBum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            MakeHole();
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "LaserUp")
        {
            Destroy();
        }

    }

    private void MakeHole()
    {
        var pos = new Vector2(transform.position.x, transform.position.y - 0.4f);
        var rot = Quaternion.Euler(0, 0, 0);

        var newGroundBum = Instantiate(groundBum, pos, rot);
        Destroy();
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}

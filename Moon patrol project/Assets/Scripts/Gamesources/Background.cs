using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Layers Prifabs")]
    [Tooltip("Warstawa tylnia")] [SerializeField] GameObject layer1Prifab;
    [Tooltip("Warstwa przednia")] [SerializeField] GameObject layer2Prifab;

    [Header("Bacground seetings")]
    [Tooltip("Odległość w osi X dla tworzenia nowego obiektu bacground")]
    [SerializeField] float deltaX = 17.8f;

    [HideInInspector] public float actualSpeed;
    GameObject lastLayer1;
    GameObject lastLayer2;
    GameObject newLayer1;
    GameObject newLayer2;
    Ground ground;

    void Awake()
    {
        ground = FindObjectOfType<Ground>();

        actualSpeed = ground.actualSpeed;

        newLayer1 = Instantiate(layer1Prifab, transform);
        newLayer2 = Instantiate(layer2Prifab, transform);

    }

    void Update()
    {
            actualSpeed = ground.actualSpeed;

        CreateLayers();

        LayerMovement();
    }

    void LayerMovement()
    {
        if (newLayer1 != null && newLayer2 != null)
        {
            newLayer1.transform.Translate(Vector3.left * actualSpeed / 3 * Time.deltaTime);
            newLayer2.transform.Translate(Vector3.left * actualSpeed / 2 * Time.deltaTime);
        }

        if (lastLayer1 != null && lastLayer2 != null)
        {
            lastLayer1.transform.Translate(Vector3.left * actualSpeed / 3 * Time.deltaTime);
            lastLayer2.transform.Translate(Vector3.left * actualSpeed / 2 * Time.deltaTime);
        }
    }
    void CreateLayers()
    {
        if (newLayer1.transform.position.x < 0.1 && newLayer1.transform.position.x > -0.1)
        {
            Destroy(lastLayer1);
            lastLayer1 = newLayer1;
            newLayer1 = Instantiate(layer1Prifab, transform);

            Vector3 newPos = newLayer1.transform.position;
            newPos.x += deltaX;
            newLayer1.transform.position = newPos;
        }
        if (newLayer2.transform.position.x < 0.1 && newLayer2.transform.position.x > -0.1)
        {
            Destroy(lastLayer2);
            lastLayer2 = newLayer2;
            newLayer2 = Instantiate(layer2Prifab, transform);

            Vector3 newPos = newLayer2.transform.position;
            newPos.x += deltaX;
            newLayer2.transform.position = newPos;
        }
    }
}

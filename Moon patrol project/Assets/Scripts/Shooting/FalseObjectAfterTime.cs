using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalseAfterTime : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestroyLasers());
    }
    IEnumerator DestroyLasers()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

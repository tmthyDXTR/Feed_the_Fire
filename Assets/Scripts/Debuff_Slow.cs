using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Slow : MonoBehaviour
{
    public float factor;
    public float duration;

    void OnEnable()
    {
        StartCoroutine(WaitAndDestroy(0.02f));
    }

    IEnumerator WaitAndDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}

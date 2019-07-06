using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Material originalMaterial;
    public Material newMaterial;

    public void ChangeToMinableMat()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = newMaterial;
    }

    public void ChangeToOriginalMat()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = originalMaterial;
    }

    void Start()
    {
        originalMaterial = this.gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
    }

    void Update()
    {

    }
}

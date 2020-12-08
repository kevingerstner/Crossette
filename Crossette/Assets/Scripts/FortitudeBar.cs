using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortitudeBar : MonoBehaviour
{
    private Transform bar;
    public Fortitude linkedFortitude;

    private float max_amt;

    private void Start()
    {
        if (linkedFortitude != null) SetLinkedFortitude(linkedFortitude);
        bar = transform.Find("Bar");
    }

    public void SetLinkedFortitude(Fortitude fortitude)
    {
        linkedFortitude = fortitude;
        max_amt = linkedFortitude.maxFortitude;
        linkedFortitude.onFortitudeChange += UpdateBar;
    }

    private void UpdateBar(float amt)
    {
        SetSize(amt / max_amt);
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}

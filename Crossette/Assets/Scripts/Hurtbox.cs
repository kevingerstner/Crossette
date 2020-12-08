using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    Fortitude fortitude;

    // Start is called before the first frame update
    void Start()
    {
        fortitude = GetComponentInParent<Fortitude>();
    }

    public void GetHitBy(float dmg)
    {
        fortitude.LoseFortitude(dmg);
    }
}

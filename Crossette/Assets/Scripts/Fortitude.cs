using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortitude : MonoBehaviour
{
    public float maxFortitude = 100f;
    public float startFortitude = 100f;
    private float fortitude;

    public delegate void OnFortitudeChange(float amt);
    public event OnFortitudeChange onFortitudeChange;

    public delegate void OnLoseFortitude();
    public event OnLoseFortitude onLoseFortitude;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        fortitude = startFortitude;
        isDead = false;
    }

    public void LoseFortitude(float amt)
    {
        fortitude = Mathf.Clamp(fortitude -= amt, 0, maxFortitude);
        if (fortitude == 0) isDead = true;
        onFortitudeChange?.Invoke(fortitude);
        onLoseFortitude?.Invoke();
    }

    public void GainFortitude(float amt)
    {
        fortitude = Mathf.Clamp(fortitude += amt, 0, maxFortitude);
        if (fortitude > 0) isDead = false;
        onFortitudeChange?.Invoke(fortitude);
    }
}

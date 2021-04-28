using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{
    public Text ichorUI;
    
    void Start()
    {
        ZPlayerPrefs.Initialize("Lemon", "SLStan0001!");
        InitializeZPP();
    }

    void Update()
    {
        ichorUI.text = ZPlayerPrefs.GetFloat("Ichor", 0).ToString() + " Ichor";
    }

    public void Reset()
    {
        ZPlayerPrefs.DeleteAll();
        InitializeZPP();
    }

    public void InitializeZPP()
    {
        if (!ZPlayerPrefs.HasKey("Ichor"))
        {
            ZPlayerPrefs.SetFloat("Ichor", 0);
        }
        if (!ZPlayerPrefs.HasKey("HighScore"))
        {
            ZPlayerPrefs.SetFloat("HighScore", 0);
        }
    }
}

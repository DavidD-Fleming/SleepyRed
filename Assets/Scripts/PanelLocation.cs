using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelLocation : MonoBehaviour
{
    private Vector3 panelLocation;
    void Start()
    {
        panelLocation = transform.position;
        // x = 298.5, y = 224, z = 0
    }

    public void GoToTreasury ()
    {
        transform.position = new Vector3(-298.5f, 224f, 0);
        panelLocation = transform.position;
    }

    public void GoToHome ()
    {
        transform.position = new Vector3(298.5f, 224f, 0);
        panelLocation = transform.position;
    }
}

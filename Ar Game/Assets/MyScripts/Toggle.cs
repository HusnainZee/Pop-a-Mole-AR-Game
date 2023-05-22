using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEditor.UI;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    bool istoggled = false;
    public GameObject Info;

    public void Toggled()
    {
        istoggled = !istoggled;

        if (istoggled)
            Info.SetActive(true);
        else
            Info.SetActive(false);

    }
}

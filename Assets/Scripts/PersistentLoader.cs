using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentLoader : MonoBehaviour
{
    public static PersistentLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) //If there are no other instances of this object
        {
            Instance = this; //Set this to the Instance
            DontDestroyOnLoad(gameObject); //Don't destroy
        }
        else //If there ARE other instances of this object
        {
            Destroy(gameObject); //Destroy them
        }
    }
}

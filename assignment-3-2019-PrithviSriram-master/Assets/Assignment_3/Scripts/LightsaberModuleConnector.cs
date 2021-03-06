﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberModuleConnector : MonoBehaviour
{
    //The GameObject this connector is going to connect
    public GameObject objectToConnect;

    //Does connecting this object requires glue
    public bool requiresGlue;

    //The boolean shows if the object is connected. Would be accessed from the LightsaberBehavior script
    private bool m_isConnected;
    public bool IsConnected
    {
        get
        {
            return m_isConnected;
        }

        set
        {
            m_isConnected = value;
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (!requiresGlue)
        {
            //check if the other obejct is objectToConnect
            //if so change IsConnected to true
            Debug.Log("on trigger enter");
            //IsConnected = true;
            if (other.gameObject == objectToConnect) //other == objectToConnect)//other.GameObject == objectToConnect)//GameObject.ReferenceEquals(other, objectToConnect))
            {
                IsConnected = true;
                Debug.Log("entered");
            }
        }

        if(requiresGlue)
        {
            //check if the other object is GlueZone
            //if so switch off requiresGlue
            if(other.gameObject.name == "GlueZone")
            {
                requiresGlue = false;
            }
        }
    }
}

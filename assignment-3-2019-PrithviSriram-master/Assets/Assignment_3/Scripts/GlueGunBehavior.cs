using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;

public class GlueGunBehavior : MonoBehaviour
{
    OculusSampleFramework.DistanceGrabbable grabState;

    //The interactive area that would be activated when pressing down the trigger while grabbing the gluegun
    [SerializeField]
    GameObject glueZone;
    [SerializeField]
    GameObject particle;
    //bool particle_on = false;
    //ParticleSystem ps;
    private void Awake()
    {
        grabState = this.GetComponent<OculusSampleFramework.DistanceGrabbable>();
        //Get component of the OVRGrabbable
        //ps = particle.GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        //If the gluegun is being grabbed, the gluezone is active while the trigger is pressed
        if (grabState.grabbedBy.Get_Controller() == OVRInput.Controller.LTouch && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.0f)// && particle_on ==false)
        {
            glueZone.SetActive(true);
            particle.SetActive(true);
            //particle_on = true;
            //ps.Play();
        }
        else
        {
            glueZone.SetActive(false);
            particle.SetActive(false);
            //particle_on = false;
            //ps.Stop();
        }
    }
}
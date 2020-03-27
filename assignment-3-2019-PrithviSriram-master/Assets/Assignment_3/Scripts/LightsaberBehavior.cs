using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;

public class LightsaberBehavior : MonoBehaviour
{
    //Accessing the script that take care of lightsaber's grabbing state
    OVRGrabbable grabState;

    //The Quillon that already installed on the handle. Should be inactive at the begginning of the game
    [SerializeField]
    GameObject lightsaberQuillonInstalled;
    //The Quillon module that has not been installed yet.
    [SerializeField]
    GameObject lightsaberQuillonModule;
    //The active area to snap the quillon module to the handle
    [SerializeField]
    GameObject quillonConnectZone;
    bool quillonIsInstalled = false;

    //The Power that already installed on the handle. Should be inactive at the beginning of the game
    [SerializeField]
    GameObject lightsaberPowerInstalled;
    //The Power module that has not been installed yet
    [SerializeField]
    GameObject lightsaberPowerModule;
    //The active area to snap the power module to the handle
    [SerializeField]
    GameObject powerConnectZone;
    bool powerIsInstalled = false;

    //bool to signal if the lightsaber is done assembling
    bool lightsaberIsAssembled;

    //The blade that already installed on the handle
    [SerializeField]
    GameObject lightsaberBlade;
    [SerializeField]
    float lightsaberLength = 1f;
    [SerializeField]
    float bladeSmooth = 1f;
    bool bladeIsActivated;
    private float final_pos = 0f;
    private void Awake()
    {
        //[TODO]Getting the info of OVRGrabbable
        Debug.Log("awake");
        grabState = this.GetComponent<OVRGrabbable>();

    }
    private void start()
    {
        Debug.Log("start");
    }

    private void FixedUpdate()
    {
        //[TODO]Step one: check if the power is connected.
        //if(powerIsInstalled == false)
        //{
        Debug.Log("before power is installed");
        ConnectingPower();
        Debug.Log("after power is installed");
        //}

        //[TODO]Step two: check in the Quillon is connected.
        //if (quillonIsInstalled == false)
        //{
        ConnectingQuillon();
        if (quillonIsInstalled == true)
        {
            lightsaberIsAssembled = true;
        }
        //}

        //[TODO]Once the lightsaber is done assembling, set the blade GameObject active.
        if (lightsaberIsAssembled)
        {
            //yield return new WaitForSeconds(0.5f);
            lightsaberBlade.SetActive(true);
        }

        //[TODO]If the lightsaber is done assembled, change bladeIsActivated after pressing the A button on the R-Controller while the player is grabbing it
        if (lightsaberIsAssembled)
        {
            if (grabState.isGrabbed && (OVRInput.Get(OVRInput.Button.One)))
            {
                bladeIsActivated = !bladeIsActivated;
                SetBladeStatus(bladeIsActivated);
            }

            if (grabState.isGrabbed && (OVRInput.Get(OVRInput.Button.Two)))
            {
                bladeIsActivated = !bladeIsActivated;
                SetBladeStatus_down();
            }
        }
    }

    void ConnectingPower()
    {

        //get the connector state of power
        if (powerConnectZone.GetComponent<LightsaberModuleConnector>().IsConnected == true)
        {
            lightsaberPowerInstalled.SetActive(true);
            lightsaberPowerModule.GetComponent<MeshRenderer>().enabled = false;
            powerConnectZone.SetActive(false);
            powerIsInstalled = true;
        }
        //if it is connected:

        //activate the pre-installed power part on the handle

        //simply make the power module "invisible" by switching off its mesh renderer

        //we dont need the connect area anymore so switch it off



    }

    void ConnectingQuillon()
    {

        if (quillonConnectZone.GetComponent<LightsaberModuleConnector>().IsConnected)
        {
            lightsaberQuillonInstalled.SetActive(true);
            lightsaberQuillonModule.GetComponent<MeshRenderer>().enabled = false;
            quillonConnectZone.SetActive(false);
            quillonIsInstalled = true;
        }
        //same process as in power connection        

    }

    void SetBladeStatus(bool bladeStatus)
    {
        /*if (!bladeStatus)
        {
            //Lightsaber goes back
            while (lightsaberBlade.activeSelf)
            {
                final_pos = Mathf.Lerp(final_pos, 0f, 2.0f * Time.deltaTime);
                lightsaberBlade.transform.localScale = new Vector3(final_pos, 1.345002f, 1.345f);
                if (lightsaberBlade.transform.localScale.x < 0.01f)
                {
                    lightsaberBlade.SetActive(false);
                }

            }
        }*/

        if (bladeStatus)
        {
            //Lightsaber pulls out
            lightsaberBlade.SetActive(true);
            //final_pos = new Vector3(lightsaberBlade.transform.position.x, lightsaberBlade.transform.position.y + lightsaberLength, lightsaberBlade.transform.position.z);
            //lightsaberBlade.transform.position = Vector3.Lerp(lightsaberBlade.transform.position, final_pos, 1.0f * Time.deltaTime);
            final_pos = Mathf.Lerp(final_pos, 1.5f, 2.0f * Time.deltaTime);
            lightsaberBlade.transform.localScale = new Vector3(final_pos, 1.345002f, 1.345f);
        }
    }

    void SetBladeStatus_down()
    {
        while (lightsaberBlade.transform.localScale.x > 0.01f)
        {
            final_pos = Mathf.Lerp(final_pos, 0f, 2.0f * Time.deltaTime);
            lightsaberBlade.transform.localScale = new Vector3(0, 1.345002f, 1.345f);
            if (lightsaberBlade.transform.localScale.x < 0.02f)
            {
                lightsaberBlade.SetActive(false);
            }

        }
    }
}


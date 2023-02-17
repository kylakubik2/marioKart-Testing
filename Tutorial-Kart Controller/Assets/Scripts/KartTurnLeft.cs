using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KartTurnLeft : MonoBehaviour
{
    public SteamVR_Action_Boolean turnLeft;
    public SteamVR_Input_Sources handType;
    public GameObject kart;
    private Rigidbody rb;

    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform backLeftTire;
    public Transform backRightTire;

    public float MaxSpeed;
    private float RealSpeed;

    private float steerDirection;

    Vector3 steerDirVect;
    float steerAmount;

    void Start()
    {
        turnLeft.AddOnStateDownListener(TriggerDown, handType);
        turnLeft.AddOnStateUpListener(TriggerUp, handType);

        rb = GetComponent<Rigidbody>();

        steerDirection = Input.GetAxisRaw("Horizontal");
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RealSpeed = transform.InverseTransformDirection(rb.velocity).z;
        steerAmount = RealSpeed > 30 ? RealSpeed / 4 * steerDirection : steerAmount = RealSpeed / 1.5f * steerDirection;
        steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 3 * Time.deltaTime);
    }
}

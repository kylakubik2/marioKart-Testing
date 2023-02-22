using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class KartGoScript : MonoBehaviour
{
    public SteamVR_Action_Boolean kartGo;
    public SteamVR_Input_Sources rightHand;

    public SteamVR_Action_Boolean kartReverse;
    public SteamVR_Input_Sources leftHand;

    public SteamVR_Action_Boolean kartTurnLeft;
    public SteamVR_Action_Boolean kartTurnRight;
    public Transform frontLeftTire;
    public Transform frontRightTire;
    public Transform backLeftTire;
    public Transform backRightTire;

    public GameObject kart;
    public Rigidbody rb;

    private float CurrentSpeed = 0;
    public float MaxSpeed;
    private float RealSpeed;
    public float originalMaxSpeed;

    private float steerDirection;
    Vector3 steerDirVect;
    float steerAmount;

    private int coinCount;
    private int numLaps;
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI lapCountText;
    private float startEffectTime;

    public AudioSource source;
    public AudioClip coinClip;
    public AudioClip boostClip;
    public AudioClip bombClip;
    public AudioClip cheer;

    void Start()
    {
        kartGo.AddOnStateDownListener(GoTriggerDown, rightHand);
        kartGo.AddOnStateUpListener(GoTriggerUp, rightHand);

        kartReverse.AddOnStateDownListener(ReverseTriggerDown, leftHand);

        kartTurnLeft.AddOnStateDownListener(LeftTurnDown, leftHand);
        kartTurnLeft.AddOnStateUpListener(LeftTurnUp, leftHand);

        kartTurnRight.AddOnStateDownListener(RightTurnDown, rightHand);
        kartTurnRight.AddOnStateUpListener(RightTurnDown, rightHand);

        coinCount = 0;
        numLaps = 0;
        originalMaxSpeed = MaxSpeed;

        SetCountText();
        SetLapText();
    }


    public void GoTriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f);
    }
    public void GoTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f);
    }


    public void ReverseTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, -MaxSpeed / 1.75f, 1f * Time.deltaTime);
    }


    public void LeftTurnUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);

        steerDirection = 0.0f;
    }
    public void LeftTurnDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);

        steerDirection = -1.0f;
    }


    public void RightTurnUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);

        steerDirection = 0.0f;
    }
    public void RightTurnDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);

        steerDirection = 1.0f;
    }


    void SetCountText()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
    }
    void SetLapText()
    {
        lapCountText.text = "Lap: " + numLaps.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coinCount = coinCount + 1;

            SetCountText();
        }
        else if (other.gameObject.CompareTag("Mushroom"))
        {
            other.gameObject.SetActive(false);
            startEffectTime = Time.time;
            MaxSpeed = MaxSpeed * 1.25f;
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            other.gameObject.SetActive(false);
            startEffectTime = Time.time;
            MaxSpeed = MaxSpeed * 0.5f;
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            numLaps = numLaps + 1;

            SetLapText();
        }
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;

        RealSpeed = transform.InverseTransformDirection(rb.velocity).z;
        steerAmount = RealSpeed > 30 ? RealSpeed / 4 * steerDirection : steerAmount = RealSpeed / 1.5f * steerDirection;
        steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 3 * Time.deltaTime);

        if (Time.time >= (startEffectTime + 5.0f))
        {
            MaxSpeed = originalMaxSpeed;
        }
    }
}
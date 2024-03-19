using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockPick : MonoBehaviour
{
    public Transform UpperPin, BottomPin;
    public float RotationSpeed;
    public AudioSource clickSound, TurningSound;
    public Transform UpperPinTargetPoint;
    public Transform BottomPinTargetPoint;
    public float successGap = 5f;
    private bool _lockPicked = false;
    public Transform Lock;
    bool pin1set = false;
    bool pin2set = false;
    private Quaternion _upperpinStartRotation;
    private Quaternion _bottompinStartRotation;
    public float OpenSpeed = 2;
    public Door door;
    public void BeginPicking()
    {
        SetRandomTargetRotations();
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.playerInput.enabled = false;
        gameManager.PlayerCamera.SetActive(false);
        gameManager.playerInput.gameObject.GetComponent<InteractionScript>().enabled = false;
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!_lockPicked)
        {
            Movement();
            Check();
        }
        else
        {
            Lock.localRotation = Quaternion.Slerp(Lock.localRotation, Quaternion.Euler(0,0,-90f), OpenSpeed*Time.deltaTime);
        }

    }
    void SetRandomTargetRotations()
    {
        _upperpinStartRotation = UpperPin.localRotation;
        _bottompinStartRotation = BottomPin.localRotation;
        UpperPinTargetPoint.localRotation = Quaternion.Euler(0f, 0f, Random.Range(10f, 360f));
        BottomPinTargetPoint.localRotation = Quaternion.Euler(0f, 0f, Random.Range(120f, 360f));
    }
    void Check()
    {
        float pin1Rotation = Quaternion.Angle(UpperPin.localRotation, UpperPinTargetPoint.localRotation);
        float pin2Rotation = Quaternion.Angle(BottomPin.localRotation, BottomPinTargetPoint.localRotation);
        Debug.LogError("pin1Rotation " + pin1Rotation);

        if (!_lockPicked)
        {
            if (pin1Rotation < successGap)
            {
                if(!pin1set)
                {
                    clickSound.Play();
                    Debug.Log("Pin 1 is correct. Adjust Pin 2.");
                    pin1set = true;
                }
            }
            else
            {
                pin1set = false;
            }
            if(pin2Rotation < successGap)
            {
                if (!pin2set)
                {
                    clickSound.Play();
                    Debug.Log("Pin 2 is correct. Adjust Pin 2.");
                    pin2set = true;
                }
            }
            else
            {
                pin2set = false;
            }
            if (!_lockPicked && pin2set&& pin1set)
            {
                OpenLock();
            }
        }
    }
    void Movement()
    {
        Vector2 Inputs;
        Inputs.x = Input.GetAxis("Horizontal");
        Inputs.y = Input.GetAxis("Vertical");
        if(Inputs.x != 0 || Inputs.y !=0)
        {
            TurningSound.Play();
        }
        UpperPin.Rotate(0, 0, Inputs.y * RotationSpeed * Time.deltaTime);
        BottomPin.Rotate(0, 0, Inputs.x * RotationSpeed * Time.deltaTime);
    }

    void OpenLock()
    {
        _lockPicked = true;
        clickSound.Play();
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.playerInput.enabled = true;
        gameManager.PlayerCamera.SetActive(true);
        gameManager.playerInput.gameObject.GetComponent<InteractionScript>().enabled = true;
        this.gameObject.SetActive(false);
        door.UnlockDoor();
    }

}

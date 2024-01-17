using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayScript : MonoBehaviour
{
    [SerializeField] PlayerMovment playerMovment;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] Rigidbody rb;
    [SerializeField] GunScript gunScript;

    public bool sway = true;
    public bool swayRotation = true;
    public bool bobOffset = true;
    public bool bobSway;



    Vector2 lookInput;
    Vector2 walkInput;

    private void Update()
    {
        BobRotation();
        BobOffset();
        GetInput();
        SwayRotation();
        Sway();
        SwayRotation();
        ComposidePositionRotation();

    }
    void GetInput()
    {
        lookInput.x = playerCam.inputX;
        lookInput.y = playerCam.inputY;

        walkInput.y = playerMovment.hozInput;
        walkInput.x = playerMovment.vertInput;
    }


    public float step = 0.01f;
    public float maxStepDistance = 0.06f;
    Vector3 swayPos;
    void Sway()
    {

        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;  

    }


    public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    Vector3 swayEulerRot;
    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }


    float smooth = 10f;
    float smoothRot = 12f;
    void ComposidePositionRotation()
    {



        transform.localPosition =
            Vector3.Lerp(transform.localPosition,
            swayPos + bobPosition,
            Time.deltaTime * smooth);

        if (gunScript.isAiming)
        {
            transform.localRotation =
            Quaternion.Slerp(gunScript.aimPoint.transform.localRotation,
            Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation),
            Time.deltaTime * smoothRot);

            /*transform.localPosition =
            Vector3.Lerp(gunScript.weaponHolderAim.transform.position,
            swayPos + bobPosition,
            Time.deltaTime * smooth);*/
            return;
        }


        transform.localRotation =
            Quaternion.Slerp(transform.localRotation,
            Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation),
            Time.deltaTime * smoothRot);
    }


    [Header("BOBBING")]
    public float speedCurve;

    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;

    Vector3 bobPosition;

    void BobOffset()
    {
        speedCurve += Time.deltaTime * (playerMovment.isGrounded ? rb.velocity.magnitude : 1f) + 0.01f;

        bobPosition.x =
            (curveCos * bobLimit.x * (playerMovment.isGrounded ? 1 : 0))
            - (walkInput.x * travelLimit.x);

        bobPosition.y =
            (curveSin * bobLimit.y)
            - (rb.velocity.y * travelLimit.y);
        bobPosition.z =
            -(walkInput.y * travelLimit.z);
    }

    [Header("Bob rotate")]
    public Vector3 multiplier;
    Vector3 bobEulerRotation;

    void BobRotation()
    {
        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) :
                                                          multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));

        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * curveCos                    : 0);
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * curveCos * walkInput.x      : 0);
    }
}

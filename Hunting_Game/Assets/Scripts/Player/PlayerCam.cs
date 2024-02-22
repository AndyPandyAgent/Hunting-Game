using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public S_WorldStateManager worldManager;

    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camPos;

    public float xRotation;
    public float yRotation;

    public float inputX;
    public float inputY;

    public GunScript gunScript;

    private bool isAiming;

    private Vector3 currentRot;
    private Vector3 targetRot;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float aimRecoilX;
    [SerializeField] private float aimRecoilY;
    [SerializeField] private float aimRecoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    void Start()
    {

    }

    // Update is called once per frame

    private void Update()
    {
        if(worldManager.hasStarted)
            Engage();
    }
    void Engage()
    {

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        inputX = mouseX;
        inputY = mouseY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0) * Quaternion.Euler(currentRot);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);

        //Recoil
        isAiming = gunScript.isAiming;

        targetRot = Vector3.Lerp(targetRot, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRot = Vector3.Slerp(currentRot, targetRot, snappiness * Time.fixedDeltaTime);
        //transform.localRotation = Quaternion.Euler(currentRot);

        //transform.position = camPos.position;
    }

    public void RecoilFire()
    {
        if (isAiming) targetRot += new Vector3(transform.rotation.x + aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
        else targetRot += new Vector3(transform.rotation.x + recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}

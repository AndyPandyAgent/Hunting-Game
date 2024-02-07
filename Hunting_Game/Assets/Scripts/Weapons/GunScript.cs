using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class GunScript : MonoBehaviour
{

    public GameObject bullet;
    public WeaonAnimScript animScript;
    private Animator anim;

    private const bool setTrue = true;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, relodTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public LayerMask heartLayer;

    int bulletsLeft, bulletsShot;

    bool readyToShoot, reloading;

    public bool shooting;
    public float range = 1000;
    public float damage;

    public Camera fpsCam;
    public GameObject camHolder;
    public Transform attackPoint;

    public GameObject muzzleFlash;
    public GameObject smoke;
    public GameObject shootLight;
    public GameObject aimPoint;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;


    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip shootSound;
    private float pitchMax = 1.5f;
    private float pitchMin = 1f;



    [Header("Aiming")]
    public bool isAiming;
    public Transform weaponHolderAim;
    public Transform weaponHolder;

    public float aimTime = 1;
    private float fov;

    public PlayerCam playerCam;

    private Vector3 currentRot;
    private Vector3 targetRot;
    [Header("Recoil")]
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;
    [SerializeField] private float aimRecoilX;
    [SerializeField] private float aimRecoilY;
    [SerializeField] private float aimRecoilZ;
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;



    private void Awake()
    {
        isAiming = false;
        fov = Camera.main.fieldOfView;

        bulletsLeft = magazineSize;
        readyToShoot = true;
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {


        Engage();

    }

    private void Aiming()
    {
        float newFov = Mathf.Lerp(fov, fov / 2, 2);

        //transform.SetPositionAndRotation(weaponHolderAim.position, fpsCam.transform.rotation);
        //transform.position = weaponHolderAim.position;
        //transform.rotation = aimPoint.transform.rotation;
        transform.SetParent(weaponHolderAim);


        Camera.main.fieldOfView = newFov;

    }

    private void NotAiming()
    {
        gameObject.transform.SetParent(weaponHolder);
        Camera.main.fieldOfView = fov;
    }

    public void Engage()
    {
        MyInput();

        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        if (isAiming)
        {
            Aiming();
        }
        else
        {
            NotAiming();
        }


        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);

        /*targetRot = Vector3.Lerp(targetRot, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRot = Vector3.Slerp(currentRot, targetRot, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRot);*/
    }

    private void MyInput()
    {

        if (readyToShoot && Input.GetButtonDown("Fire1") && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            print("pew");

            Shoot();


        }

        if (readyToShoot && Input.GetButtonDown("Fire1") && !reloading && bulletsLeft <= 0)
            Reload();

        else if (readyToShoot && Input.GetKeyDown(KeyCode.R) && !reloading)
            Reload();
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward * 10000, out hit, Mathf.Infinity))
        {
            print(hit.transform.gameObject.name);
            print("Shoot");
            S_Heart heart = hit.transform.GetComponent<S_Heart>();

            if (heart != null)
            {
                print("hit");
                heart.TakeDamage(damage);
            }
        }



        //animScript.PlayAnim();
        //anim.SetTrigger("Shoot");

        playerCam.RecoilFire();
        RecoilFire();

        Instantiate(smoke, new Vector3(attackPoint.position.x, attackPoint.position.y, attackPoint.position.z), fpsCam.transform.rotation);

        shootLight.gameObject.SetActive(true);
        Invoke("TurnOffLight", 0.05f);


        audioSource.pitch = Random.Range(pitchMin, pitchMax);

        audioSource.clip = shootSound;

        audioSource.Play();

        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


     



        /*GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);*/

        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", relodTime);
    }

    void ShotTimer()
    {
        shooting = false;
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void TurnOffLight()
    {
        shootLight.gameObject.SetActive(false);
    }

    public void RecoilFire()
    {
        if (isAiming) targetRot += new Vector3(aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
        else targetRot += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10000);
    }
}

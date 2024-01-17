using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ViewBob : MonoBehaviour
{
    [SerializeField] bool Isenabled = true;

    [SerializeField, Range(0, 0.1f)] float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] float frequency = 10f;
    [SerializeField, Range(0, 0.1f)] float idleAmplitude = 0.015f;
    [SerializeField, Range(0, 30)] float idleFrequency = 10f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform cam_holder = null;
    [SerializeField] Transform campos = null;

    float togglespeed = 1.0f;
    Vector3 startpos;
    Rigidbody controller;
    PlayerMovment playerMovment;

    void Start()
    {
        startpos = campos.localPosition;
        controller = GetComponent<Rigidbody>();
        playerMovment = GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam_holder.position = campos.position;
        cam_holder.position = campos.localPosition + cam_holder.position;
        CheckMotion();
        cam.LookAt(FocusTarget());
    }
    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude / 100f;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude / 50f;
        return pos;
    }

    Vector3 IdleMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * idleFrequency) * idleAmplitude / 100f;
        return pos;
    }

    void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0f, controller.velocity.z).magnitude;
        ResetPosition();
        if (speed < togglespeed)
        {
            PlayMotion(IdleMotion());
            return;
        }
        if (!playerMovment.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    void ResetPosition()
    {
        if (cam.localPosition == startpos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startpos, 1 * Time.deltaTime);
    }
    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + campos.localPosition.y, transform.position.z);
        pos += cam.forward * 15f;
        return pos;
    }
    void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }
}
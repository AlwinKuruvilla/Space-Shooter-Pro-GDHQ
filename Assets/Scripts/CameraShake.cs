using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public bool shakeOn;
    public float shakeWaitTime = 0.05f;
    public float shakeDuration = 20.0f;
    public float shakeDistAmount = 0.2f;
    private int numOfShakes;
    private float yPos;


    public void StartCameraShake() {
        yPos = transform.position.y;
        StartCoroutine(CameraShakeRoutine(shakeWaitTime, shakeDistAmount, true));
    }

    IEnumerator CameraShakeRoutine(float shakeTime, float shakeDistance, bool canShake) {
        numOfShakes = 0;
        while (canShake) {
            transform.position = new Vector3(transform.position.x, yPos + shakeDistance, transform.position.z);
            yield return new WaitForSeconds(shakeTime);
            transform.position = new Vector3(transform.position.x, yPos - shakeDistance, transform.position.z);
            yield return new WaitForSeconds(shakeTime);
            numOfShakes++;

            if (numOfShakes >= shakeDuration) {
                canShake = false;
                transform.position = new Vector3(transform.position.x, yPos,
                    transform.position.z);
            }
        }
    }
    
}

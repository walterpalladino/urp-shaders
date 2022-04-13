using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDrift : MonoBehaviour
{

    [Header("Up / Down")]
    [SerializeField] private float verticalDriftPeriod = 1.0f;
    [SerializeField] private float driftStrenght = 0.25f;

    [Header("Angles")]
    [SerializeField] private float angleDriftPeriod = 1.0f;
    [SerializeField] private float maxDriftAngleZ = 0.5f;
    [SerializeField] private float maxDriftAngleX = 0.5f;

    //[Range(0.0f,360.0f)]
    //[SerializeField] 
    private float driftPeriodOffset = 0.0f;
    private float originalY;
    private Quaternion originalRotation;


    // Start is called before the first frame update
    void Start()
    {
        driftPeriodOffset = UnityEngine.Random.Range(0.0f, 360.0f);
        originalY = transform.localPosition.y;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        DoDrift();
    }

    private void DoDrift()
    {

        float driftY = Mathf.Sin(Time.realtimeSinceStartup * verticalDriftPeriod + driftPeriodOffset);
        Vector3 position = transform.position;
        position.y = originalY + driftY * driftStrenght;

        transform.localPosition = position;
        //transform.localRotation = Quaternion.Euler(Mathf.Sin(Time.realtimeSinceStartup * driftPeriod) * maxDriftAngleX, 0, Mathf.Sin(Time.realtimeSinceStartup * driftPeriod) * maxDriftAngleZ);
        //transform.localRotation = originalRotation * Quaternion.Euler(Mathf.Sin(Time.realtimeSinceStartup * angleDriftPeriod) * maxDriftAngleX, 0, Mathf.Sin(Time.realtimeSinceStartup * angleDriftPeriod) * maxDriftAngleZ);
        originalRotation = transform.localRotation;
        transform.localRotation = Quaternion.Euler(Mathf.Sin(Time.realtimeSinceStartup * angleDriftPeriod) * maxDriftAngleX, originalRotation.eulerAngles.y, Mathf.Sin(Time.realtimeSinceStartup * angleDriftPeriod) * maxDriftAngleZ);

    }
}

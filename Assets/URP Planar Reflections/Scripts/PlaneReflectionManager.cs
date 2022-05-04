using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaneReflectionManager : MonoBehaviour
{

    Camera reflectionCamera;
    Camera mainCamera;

    [SerializeField]
    GameObject reflectionPlane;

    RenderTexture renderTexture;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    float reflectionStrength = 0.5f;

    [Range(1, 16)]
    [SerializeField]
    int reflectionTextureGrain = 1;


    // Start is called before the first frame update
    void Start()
    {
        GameObject reflectionCameraGO = new GameObject("ReflectionCamera");
        reflectionCamera = reflectionCameraGO.AddComponent<Camera>();

        mainCamera = Camera.main;

        renderTexture = new RenderTexture(Screen.width / reflectionTextureGrain, Screen.height / reflectionTextureGrain, 24);
        renderTexture.antiAliasing = 2;


        reflectionCamera.targetTexture = renderTexture;
        reflectionPlane.GetComponentInChildren<Renderer>().material.SetTexture("_ReflectionTexture", renderTexture, RenderTextureSubElement.Default);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupReflectionCamera()
    {

        Vector3 cameraDirectionWorldSpace = mainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = mainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = mainCamera.transform.position;

        //  Transform the vectors to the reflection plane space
        Vector3 cameraDirectionPlaneSpace = reflectionPlane.transform.InverseTransformDirection(cameraDirectionWorldSpace);
        Vector3 cameraUpPlaneSpace = reflectionPlane.transform.InverseTransformDirection(cameraUpWorldSpace);
        Vector3 cameraPositionPlaneSpace = reflectionPlane.transform.InverseTransformPoint(cameraPositionWorldSpace);

        //  Mirror the vectors
        cameraDirectionPlaneSpace.y *= -1.0f;
        cameraUpPlaneSpace.y *= -1.0f;
        cameraPositionPlaneSpace.y *= -1.0f;

        //  Transform the vectors back to World Space
        cameraDirectionWorldSpace = reflectionPlane.transform.TransformDirection(cameraDirectionPlaneSpace);
        cameraUpWorldSpace = reflectionPlane.transform.TransformDirection(cameraUpPlaneSpace);
        cameraPositionWorldSpace = reflectionPlane.transform.TransformDirection(cameraPositionPlaneSpace);

        //  Set the reflection camera
        reflectionCamera.transform.position = cameraPositionWorldSpace;
        reflectionCamera.transform.LookAt(cameraPositionWorldSpace + cameraDirectionWorldSpace, cameraUpWorldSpace);

    }



    // Unity calls this method automatically when it enables this component
    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += EndCamerarendering;
        RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
    }

    // Unity calls this method automatically when it disables this component
    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= EndCamerarendering;
        RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
    }

    private void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == "ReflectionCamera")
        {
            SetupReflectionCamera();
            reflectionPlane.GetComponentInChildren<Renderer>().material.SetFloat("_ReflectionStrength", reflectionStrength);
        }
    }



    // When this method is a delegate of RenderPipeline.beginCameraRendering event, Unity calls this method every time it raises the beginCameraRendering event
    void EndCamerarendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.tag == "MainCamera")
        {
        }
    }
}

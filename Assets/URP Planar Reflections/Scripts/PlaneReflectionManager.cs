using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaneReflectionManager : MonoBehaviour
{

    [SerializeField]
    Camera reflectionCamera;
    Camera mainCamera;

    [SerializeField]
    GameObject reflectionPlane;


    //[SerializeField]
    RenderTexture renderTexture;



    // Start is called before the first frame update
    void Start()
    {
        //GameObject reflectionCameraGO = new GameObject("ReflectionCamera");
        //reflectionCamera = reflectionCameraGO.AddComponent<Camera>();
        //reflectionCamera.enabled = false;

        mainCamera = Camera.main;
        /*
        //renderTexture = new RenderTexture(Screen.width, Screen.height, 24); //  Includes the stencil buffer
        if (reflectionCamera.targetTexture != null)
        {
            reflectionCamera.targetTexture.Release();
        }

        reflectionCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);*/
        
        //  Set the reflection texture properties
        /*renderTexture.Release();

        renderTexture.width = Screen.width / 4;
        renderTexture.height = Screen.height / 4;
        renderTexture.depth = 24;
        renderTexture.antiAliasing = 2;
        renderTexture.Create();
        */
        renderTexture = new RenderTexture(Screen.width / 4, Screen.height / 4, 24);
        renderTexture.antiAliasing = 2;


        reflectionCamera.targetTexture = renderTexture;
//        reflectionPlane.GetComponentInChildren<Renderer>().material.SetTexture("_BaseTexture", renderTexture, RenderTextureSubElement.Default);
        reflectionPlane.GetComponentInChildren<Renderer>().material.SetTexture("_ReflectionTexture", renderTexture, RenderTextureSubElement.Default);
        //        reflectionPlane.GetComponent<Renderer>().material.SetTexture(Shader.PropertyToID("_ReflectionTexture"), renderTexture);
        /*Renderer reflectionPlaneRenderer = reflectionPlane.GetComponent<Renderer>().material.SetTexture(Shader.PropertyToID("_ReflectionTexture"), renderTexture);
        Material material = reflectionPlaneRenderer.material;
        material.SetTexture(Shader.PropertyToID("_ReflectionTexture"), renderTexture);
        reflectionPlaneRenderer.materials[0] = material;*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnPostRender()
    {
        Debug.Log("OnPostRender");
        RenderReflection();
    }
    */

    void SetupReflectionCamera()
    {
        //reflectionCamera.CopyFrom(mainCamera);

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

  //      //  Set the reflection texture dimensions

        /*
        //  Set render target for the reflection camera
        reflectionCamera.targetTexture = renderTexture;

        //  Render the reflection camera
        //reflectionCamera.Render();
        */
    }



    // Unity calls this method automatically when it enables this component
    private void OnEnable()
    {
        // Add WriteLogMessage as a delegate of the RenderPipelineManager.beginCameraRendering event
        RenderPipelineManager.endCameraRendering += EndCamerarendering;

        RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
    }

    // Unity calls this method automatically when it disables this component
    private void OnDisable()
    {
        // Remove WriteLogMessage as a delegate of the  RenderPipelineManager.beginCameraRendering event
        RenderPipelineManager.endCameraRendering -= EndCamerarendering;
        RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
    }

    private void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        Debug.Log($"Beginning rendering the camera: {camera.name}");
        if (camera.name == "ReflectionCamera")
        {
            SetupReflectionCamera();
        }
    }



    // When this method is a delegate of RenderPipeline.beginCameraRendering event, Unity calls this method every time it raises the beginCameraRendering event
    void EndCamerarendering(ScriptableRenderContext context, Camera camera)
    {
        Debug.Log($"Ending rendering the camera: {camera.name}");
        if (camera.tag == "MainCamera")
        {
            //RenderReflection();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  https://www.youtube.com/watch?v=1tHhGa7gi8o
public class BillboardSprite : MonoBehaviour
{

    public enum AnimationAxis
    {
        Rows,
        Columns
    }

    MeshRenderer meshRenderer;

    [SerializeField]
    string rowPropertyName = "_CurRow";
    [SerializeField]
    string colPropertyName = "_CurCol";

    [SerializeField]
    AnimationAxis axis = AnimationAxis.Columns;
    [SerializeField]
    float animationSpeed = 5.0f;
    [SerializeField]
    int animationIdx = 0;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        string clipKey, frameKey;
        if (axis == AnimationAxis.Rows)
        {
            clipKey = rowPropertyName;
            frameKey = colPropertyName;
        }
        else
        {
            clipKey = colPropertyName;
            frameKey = rowPropertyName;
        }

        int frame = (int)(Time.time * animationSpeed) % 4;
        meshRenderer.material.SetFloat(clipKey, animationIdx);
        meshRenderer.material.SetFloat(frameKey, frame);

    }
}

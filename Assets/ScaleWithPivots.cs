using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleWithPivots : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject cylinder;
    private Vector3 initialScale;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = cylinder.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        cylinder.transform.localScale = new Vector3(initialScale.x, distance / 2f, initialScale.z);

        Vector3 middlePoint = (startPoint.transform.position + new Vector3(endPoint.transform.position.x,endPoint.transform.position.y +offset, endPoint.transform.position.z) / 2f);
        cylinder.transform.localPosition = middlePoint;

        startPoint.transform.position = new Vector3(transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CubeMovement : MonoBehaviour
{

    public float tumblingDuration = 0.2f;
    bool isRotate = false;

    string ColliderName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter : " + other.gameObject.transform.parent.name);

        ColliderName = other.gameObject.transform.parent.name;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnCollisionExit : " + other.gameObject.transform.parent.name);
    }

    void Update()
    {
        var dir = Vector3.zero;

        if (Input.GetKey(KeyCode.I))
            dir = Vector3.forward;

        if (Input.GetKey(KeyCode.J))
            dir = Vector3.back;

        if (Input.GetKey(KeyCode.U))
            dir = Vector3.left;

        if (Input.GetKey(KeyCode.K))
            dir = Vector3.right;

        if (dir != Vector3.zero && !isRotate)
        {
            StartCoroutine(Rotate(dir));
        }
    }

    
    IEnumerator Rotate(Vector3 direction)
    {
        isRotate = true;

        var rotAxis = Vector3.Cross(Vector3.up, direction);
        var pivot = (transform.position + Vector3.down) + direction;

        var startRotation = transform.rotation;
        var endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

        var startPosition = transform.position;
        var endPosition = transform.position + direction * transform.localScale.x;

        var rotSpeed = 90.0f / tumblingDuration;
        var t = 0.0f;

        while (t < tumblingDuration)
        {
            t += Time.deltaTime;
            if (t < tumblingDuration)
            {
                transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
                yield return null;
            }
            else
            {
                transform.rotation = endRotation;
                transform.position = endPosition;
            }
        }

        isRotate = false;
    }
}
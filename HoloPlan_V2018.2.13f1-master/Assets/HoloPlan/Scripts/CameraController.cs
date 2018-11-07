using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float rotationSpeed;

    private Vector3 offset;
    private float smooth = 5.0f;

    // Use this for initialization
    void Start() {

        offset = transform.position - player.transform.position;
    }

    /* private void FixedUpdate()
    {

        while (Input.GetKey("1"))
        {
            Quaternion targetRot = Quaternion.Euler(0, rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smooth);
        }
        while (Input.GetKey("2"))
        {
            Quaternion targetRot = Quaternion.Euler(0, -rotationSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smooth);
        }
    }*/

    // Update is called once per frame
    void LateUpdate () {

        transform.position = player.transform.position + offset;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }
}

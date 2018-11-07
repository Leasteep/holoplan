using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    private int count;

    public float speed;
    public Text countText;
    public Text win;
    
	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Count:" + count.ToString();
        win.gameObject.SetActive(false);
        win.text = "You WIN!";

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
	}
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count = count + 1;
            countText.text = "Count:" + count.ToString();
        }
        if (count == 10)
        {
            countText.gameObject.SetActive(false);
            win.gameObject.SetActive(true);
        }
    }

}

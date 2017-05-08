using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{
    Transform headtrans;
	Vector3 startPos;
    // Use this for initialization
    void Start()
    {
        headtrans = gameObject.GetComponentInChildren<Camera>().transform;
		startPos = transform.position;
    }

    // Update is called once per frame
    public float thrust = 1.7f;
    bool on = false;
    void Update()
    {
        if (Cardboard.SDK.Triggered)
        {
            on = !on;
        }

        if (on)
        {
            transform.position += headtrans.rotation * base.transform.forward * thrust * Time.deltaTime;
        }
		if (transform.position.y <= -100f) {
			transform.position = startPos;
		}
    }
}

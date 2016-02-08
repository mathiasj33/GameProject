using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float speed;
    public float height;
    [Range(0.0f, 50)]
    public float angle;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, height, 0);
        transform.Rotate(new Vector3(angle, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = Vector3.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime;
        delta += Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        delta.Normalize();
        delta /= 50;
        delta *= speed;
        transform.position += delta;
    }
}

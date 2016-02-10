using UnityEngine;
using System.Collections;

namespace Scripts
{
    public class CameraControl : MonoBehaviour
    {
        public float speed;
        public float height;
        [Range(0.0f, 50)]
        public float angle;

        // Use this for initialization
        void Start()
        {
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 delta = Vector3.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            delta += Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            delta.Normalize();
            delta /= 50;
            delta *= speed;
            transform.position += delta;
        }

        void OnValidate()
        {
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
        }
    }
}
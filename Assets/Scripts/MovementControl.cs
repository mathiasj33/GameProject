using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

    public float speed = 1;
    private const int Scale = 2;
    private Vector3 target;
    private Vector3 direction;
    private bool moving;

    public void moveTo(Vector3 target)
    {
        this.target = target;
        direction = target - transform.position;
        direction.Normalize();
        moving = true;
    }
	
	void Update () {
        if(moving)
        {
            transform.position += direction * speed * Time.deltaTime / Scale;
            transform.LookAt(transform.position + direction);
            if(Vector3.Distance(target, transform.position) < 0.2f)
            {
                moving = false;
            }
        }
	}
}

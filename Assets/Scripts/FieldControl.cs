using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieldControl : MonoBehaviour
{

    private List<Vector3> positions = new List<Vector3>();
    private List<GameObject> fields = new List<GameObject>();

    void Start()
    {
        initPositions();
        removeCollidingPositions();
        addFields();
        batchFields();
    }

    private void initPositions()
    {
        int size = (int)gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                positions.Add(new Vector3(x - size / 2 + 0.5f, 0.05f, z - size / 2 + 0.5f));
            }
        }
    }

    private void removeCollidingPositions()
    {
        positions.RemoveAll(pos => isRaycast(pos + new Vector3(0, -.3f, 0)));
    }

    private bool isRaycast(Vector3 pos)
    {
        Ray[] rays = new Ray[9];
        rays[0] = new Ray(pos + new Vector3(-0.5f, 0, 0.5f), Vector3.up);
        rays[1] = new Ray(pos + new Vector3(0, 0, 0.5f), Vector3.up);
        rays[2] = new Ray(pos + new Vector3(0.5f, 0, 0.5f), Vector3.up);
        rays[3] = new Ray(pos + new Vector3(-0.5f, 0, 0), Vector3.up);
        rays[4] = new Ray(pos + new Vector3(0, 0, 0), Vector3.up);
        rays[5] = new Ray(pos + new Vector3(0.5f, 0, 0), Vector3.up);
        rays[6] = new Ray(pos + new Vector3(-0.5f, 0, -0.5f), Vector3.up);
        rays[7] = new Ray(pos + new Vector3(0, 0, -0.5f), Vector3.up);
        rays[8] = new Ray(pos + new Vector3(0.5f, 0, -0.5f), Vector3.up);

        foreach (Ray ray in rays)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.3f))
            {
                Debug.DrawLine(hit.point, hit.point + new Vector3(0, -1f, 0), Color.red, 20, false);
                return true;
            }
        }
        return false;
    }

    private void addFields()
    {
        foreach (Vector3 pos in positions)
        {
            GameObject field = (GameObject)Instantiate(Resources.Load("Prefabs/field"));
            field.transform.position = pos;
            fields.Add(field);
        }
    }

    private void batchFields()
    {
        BatchUtility.BatchQuads(fields, 1024);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameLogic;

namespace Scripts
{
    public class GameFieldControl : MonoBehaviour
    {
        private List<Vector3> positions = new List<Vector3>();
        private List<GameObject> fields = new List<GameObject>();
        private GameField gameField;
        private GameObject redField;

        void Start()
        {
            initPositions();
            removeCollidingPositions();
            addFields();
            batchFields();
            initGameField();
            redField = (GameObject)Instantiate(Resources.Load("Prefabs/selectedField"));
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

        private void initGameField()
        {
            gameField = new GameField();
            positions.ForEach(v => gameField.AddField(ConvertTo2D(v)));
        }

        private void batchFields()
        {
            BatchUtility.BatchQuads(fields, 1024);
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit result;
            if (Physics.Raycast(ray, out result))
            {
                MarkRed(result.point);
            }
        }

        private void MarkRed(Vector3 pos)
        {
            Vector3 fieldPos = ConvertToFieldPosition(pos);
            if (gameField.ContainsField(ConvertTo2D(fieldPos)))
            {
                redField.transform.position = fieldPos;
                redField.SetActive(true);
            }
            else
            {
                redField.SetActive(false);
            }
        }

        private Vector3 ConvertToFieldPosition(Vector3 pos)
        {
            float x = Mathf.Floor(pos.x);
            x += 0.5f;
            float z = Mathf.Floor(pos.z);
            z += 0.5f;
            return new Vector3(x, 0.1f, z);
        }

        private Vector2 ConvertTo2D(Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }
    }
}
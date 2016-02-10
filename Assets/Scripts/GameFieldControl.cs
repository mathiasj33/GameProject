using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameLogic;
using Utils;

namespace Scripts
{
    public class GameFieldControl : MonoBehaviour
    {
        private float boundSize;
        public int size;
        private float squareLength;
        private TwoWayDictionary<Vector3, Vector2> positions = new TwoWayDictionary<Vector3, Vector2>();
        private List<GameObject> fields = new List<GameObject>();
        private GameField gameField;

        void Start()
        {
            boundSize = (int)gameObject.GetComponent<MeshRenderer>().bounds.size.x;
            squareLength = boundSize / size;
            InitGameField();
            InitPositions();
            RemoveCollidingPositions();
            AddFields();
            BatchFields();
        }

        private void InitPositions()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float x = squareLength * i;
                    float z = squareLength * j;
                    positions.Add(new Vector3(x - boundSize / 2 + squareLength / 2, 0.01f, z - boundSize / 2 + squareLength / 2),
                        new Vector2(i, j));
                }
            }
        }

        private void RemoveCollidingPositions()
        {
            for(int i = positions.Count - 1; i >= 0; i--)
            {
                Vector3 pos = positions.GetKeyAt(i);
                if(IsRaycast(pos + new Vector3(0, -.3f, 0)))
                {
                    gameField.RemoveField(positions.GetValueAt(i));
                    positions.RemoveKeyAndValue(pos);
                }
            }
        }

        private bool IsRaycast(Vector3 pos)
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

        private void AddFields()
        {
            foreach (Vector3 pos in positions.Keys)
            {
                GameObject field = (GameObject)Instantiate(Resources.Load("Prefabs/field"));
                field.transform.position = pos;
                field.transform.localScale = new Vector3(squareLength, squareLength, 1);
                fields.Add(field);
            }
        }

        private void InitGameField()
        {
            gameField = new GameField(size);
        }

        private void BatchFields()
        {
            BatchUtility.BatchQuads(fields, 1024);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit result;
                if (Physics.Raycast(ray, out result))
                {
                    Vector2 target = ConvertToFieldPosition(result.point);
                    List<Vector2> path = gameField.FindPath(new Vector2(0, 0), target);
                    path.ForEach(MarkRed);
                }
            }
        }

        private void MarkRed(Vector2 pos)
        {
            GameObject redField = (GameObject)Instantiate(Resources.Load("Prefabs/selectedField"));
            Vector3 fieldPos = ConvertToWorldPosition(pos);
            redField.transform.position = fieldPos;
            redField.transform.localScale = new Vector3(squareLength, squareLength, 0);
            redField.SetActive(true);
        }

        private Vector2 ConvertToFieldPosition(Vector3 pos)
        {
            float x = pos.x - pos.x % squareLength;
            if(x > 0) x += squareLength / 2;
            else x -= squareLength / 2;
            float z = pos.z -  pos.z % squareLength;
            if(z > 0) z += squareLength / 2;
            else z -= squareLength / 2;
            Vector3 newPos = new Vector3(x, 0.01f, z);
            
            return positions.GetValue(newPos);
        }

        private Vector3 ConvertToWorldPosition(Vector2 pos)
        {
            return positions.GetKey(pos);
        }

        private Vector2 ConvertTo2D(Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        private Vector3 ConvertTo3D(Vector2 vec)
        {
            return new Vector3(vec.x, 0.01f, vec.y);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{
    class BatchUtility
    {
        public static void BatchQuads(IList<GameObject> batchList, int batchSize)
        {
            GameObject batchRoot = CreateBatchRoot();
            int verticesAmount = 0;
            foreach (GameObject obj in batchList)
            {
                obj.transform.parent = batchRoot.transform;
                verticesAmount += 4;
                if (verticesAmount >= batchSize)
                {
                    CombineMeshes(batchRoot);
                    verticesAmount = 0;
                    batchRoot = CreateBatchRoot();
                }
            }
        }

        private static GameObject CreateBatchRoot()
        {
            GameObject batchRoot = new GameObject();
            batchRoot.AddComponent<MeshFilter>();
            batchRoot.AddComponent<MeshRenderer>();
            return batchRoot;
        }

        private static void CombineMeshes(GameObject obj)
        {
            MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length - 1];
            var index = 0;
            for (var i = 0; i < meshFilters.Length; i++)
            {
                if (meshFilters[i].sharedMesh == null)
                {
                    continue;
                }
                combine[index].mesh = meshFilters[i].sharedMesh;
                combine[index++].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }
            obj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            obj.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true);
            obj.transform.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/FX/field");
            obj.transform.gameObject.SetActive(true);
        }
    }
}

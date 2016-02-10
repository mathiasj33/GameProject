using System;
using UnityEngine;
using System.Collections;

class LoadBundles : MonoBehaviour
{
    public string BundleURL;
    public string AssetName;

    IEnumerator Start()
    {
        // Start a download of the given URL
        WWW www = WWW.LoadFromCacheOrDownload(BundleURL, 1);

        // Wait for download to complete
        yield return www;

        // Load and retrieve the AssetBundle
        AssetBundle bundle = www.assetBundle;

        // Load the object asynchronously
        AssetBundleRequest request = bundle.LoadAssetAsync("test", typeof(GameObject));

        // Wait for completion
        yield return request;

        // Get the reference to the loaded object
        GameObject obj = request.asset as GameObject;

        // Unload the AssetBundles compressed contents to conserve memory
        bundle.Unload(false);

        // Frees the memory from the web stream
        www.Dispose();

        Instantiate(obj);
    }
}
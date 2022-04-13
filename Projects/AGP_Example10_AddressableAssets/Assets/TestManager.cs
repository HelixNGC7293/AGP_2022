using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TestManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Method1());

        Method2();
    }

    void OnDestroy()
    {
        Addressables.Release(handle_Character1);

        assetRefMember.ReleaseAsset();
    }


    ////////////////////Method 1
    SimpleScriptableObject character1;
    AsyncOperationHandle<SimpleScriptableObject> handle_Character1;
    IEnumerator Method1()
    {
        string key = "Assets/Lily.asset";
        handle_Character1 = Addressables.LoadAssetAsync<SimpleScriptableObject>(key);
        yield return handle_Character1;

        if (handle_Character1.Status == AsyncOperationStatus.Succeeded)
        {
            character1 = handle_Character1.Result;
            //GameObject obj = handle_Character1.Result;
            //Instantiate(obj, transform);
            Debug.Log(character1.playerName);
        }
    }




    void Character1Loaded(AsyncOperationHandle<SimpleScriptableObject> obj)
    {
        if (obj.Status != AsyncOperationStatus.Succeeded) return;

        character2 = obj.Result;

        Debug.Log(character2.playerName);
    }


    ////////////////////Method 2

    [SerializeField]
    AssetReference assetRefMember;
    SimpleScriptableObject character2;
    void Method2()
    {

        var handle = assetRefMember.LoadAssetAsync<SimpleScriptableObject>();
        handle.Completed += Character1Loaded;
    }
}

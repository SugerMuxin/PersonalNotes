using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAlot : MonoBehaviour {

    public GameObject model;
    public int MaxNum = 100;
    public int numInFrame = 1;
    public int currentStepCount = 0;
    public int currentTotalCount = 0;
    public List<GameObject> all = new List<GameObject>();
    public UnityEngine.UI.InputField inputFieldMax;
    public UnityEngine.UI.InputField inputFieldNumPerFrame;

    private MaterialPropertyBlock _mpb = null;//与buffer交换数据
    public List<Material> animMat = new List<Material>();
    public bool useGpuInstancing = true;
    // Use this for initialization
    public void ReCreate () {
        _mpb = null;
        if(inputFieldMax.text != "")
        {
            MaxNum = int.Parse(inputFieldMax.text);
        }
        if(inputFieldNumPerFrame.text != "")
        {
            numInFrame = int.Parse(inputFieldNumPerFrame.text);
        }

        if(all.Count > 0)
        {
            for (int i = 0; i < all.Count; i++)
            {
                Destroy(all[i]);
            }
            all.Clear();
        }
        currentStepCount = 0;
        currentTotalCount = 0;
        StopAllCoroutines();
        StartCoroutine(CreateSoldier());

    }

    public void ToggleGpu(bool use)
    {
        useGpuInstancing = use;//useGpuInstancing;
        if(useGpuInstancing)
        {
            animMat[0].EnableKeyword("INSTANCING_ON");
            animMat[1].EnableKeyword("INSTANCING_ON");
        }
        else
        {
            animMat[0].EnableKeyword("INSTANCING_OFF");
            animMat[1].EnableKeyword("INSTANCING_OFF");   
        }
       
    }

    IEnumerator CreateSoldier()
    {
        while(currentTotalCount < MaxNum)
        {
            {
                var obj = Instantiate(model, new Vector3(currentTotalCount % 100 -50, 0, currentTotalCount / 100 - 50), model.transform.rotation);
                SetPropertyBlockByGameObject(obj);
                all.Add(obj);
                currentTotalCount++;
                currentStepCount++;
                if (currentStepCount > numInFrame)
                {
                    currentStepCount = 0;
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
       


     private bool SetPropertyBlockByGameObject(GameObject pGameObject)
    {
        if(pGameObject == null)
        {
            return false;
        }
        if(_mpb == null)
        {
            _mpb = new MaterialPropertyBlock();
        }

        //随机每个对象的颜色
        _mpb.SetColor("_TimeOffset", new Vector4(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f));

        MeshRenderer meshRenderer = pGameObject.GetComponentInChildren<MeshRenderer>();
        if (meshRenderer == null)
        {
            return false;         
        }

        meshRenderer.SetPropertyBlock(_mpb);

        return true;
    }

}
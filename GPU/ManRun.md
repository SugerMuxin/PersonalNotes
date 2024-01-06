using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManRun : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0;
    public Material[]mat;
    private int index = 0;
    float nextRunTime;
    private MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        Think();
    }

    void Think()
    {
        nextRunTime = Random.Range(3, 10f);
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(index == 1)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if(time > nextRunTime)
        {
            ChangeAnim();
            time = 0;
        }
    }
    void ChangeAnim()
    {
        if(++index >= mat.Length){
            index = 0;
        }
        if(index == 1)
        {
            Think();            
        }
        else{            
            transform.forward = new Vector3(Random.Range(-20,20),0,Random.Range(-20,20));
        }
        mesh.sharedMaterial = mat[index];
    }
}
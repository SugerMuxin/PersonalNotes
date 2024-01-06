using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEditor.Animations;
using System.Runtime.CompilerServices;

public class BakeAnimTextureWindow : EditorWindow {

    // Use this for initialization
    //public Animator anim;
    public SkinnedMeshRenderer render;
    public Animator animator;
    public AnimationClip clip;

    [MenuItem("我的/烘焙动画图")]
    static void BakeAnim() {
        GetWindow<BakeAnimTextureWindow>().Show();


    }

    Texture2D tex;
    Texture2D normalTex;
    private void OnGUI()
    {
        render = EditorGUILayout.ObjectField(render, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;
        animator= EditorGUILayout.ObjectField(animator, typeof(Animator), true) as Animator;
        clip= EditorGUILayout.ObjectField(clip, typeof(AnimationClip), true) as AnimationClip;
        if (GUILayout.Button("创建")){
            CreateTexture();
            BakeModelData();
            SaveTex();
        }

    }

    private void BakeModelData()
    {
        Mesh mesh = new Mesh();
        var anCon = animator.runtimeAnimatorController as AnimatorController;       

        for (int x = 0; x <100; x++)
        {
            clip.SampleAnimation(animator.gameObject, x /100.0f);

            render.BakeMesh(mesh);
            Vector3[] vertes = mesh.vertices;
            Vector3[] normals=   mesh.normals;
            for (int i = 0; i < vertes.Length; i++)
            {
                Vector3 pos = vertes[i] / 2.5f * 0.5f + Vector3.one * 0.5f;

                tex.SetPixel(i,x,new Color( pos.x,pos.y,pos.z));

                Vector3 normal = normals[i];

                normalTex.SetPixel(i, x,new Color( normal.x, normal.y, normal.z));
            }
            tex.Apply();
            normalTex.Apply();
        }
    }

    private void CreateTexture()
    {
        
        tex = new Texture2D(render.sharedMesh.vertexCount,100);
        normalTex = new Texture2D(render.sharedMesh.vertexCount,100);


    }

    void SaveTex() {
        File.WriteAllBytes("Assets/" + clip.name + "Anim.png", tex.EncodeToPNG());
        
        AssetDatabase.Refresh();

        TextureImporter im = AssetImporter.GetAtPath("Assets/" + clip.name + "Anim.png") as TextureImporter;

        im.filterMode = FilterMode.Point;
        im.npotScale = TextureImporterNPOTScale.None;
        im.maxTextureSize = 8192;
        im.SaveAndReimport();


        File.WriteAllBytes("Assets/" + clip.name + "Normal.png", normalTex.EncodeToPNG());
        AssetDatabase.Refresh();

        TextureImporter imN = AssetImporter.GetAtPath("Assets/" + clip.name + "Normal.png") as TextureImporter;
        
        
        imN.filterMode = FilterMode.Point;
        imN.npotScale = TextureImporterNPOTScale.None;
        imN.maxTextureSize = 8192;
        imN.SaveAndReimport();


    }
}
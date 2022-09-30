/*using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectReplaceEditorWindow : EditorWindow
{
    [MenuItem("Tools/�滻�����е�����")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ObjectReplaceEditorWindow));
    }
    public GameObject newPrefab;
    static GameObject tonewPrefab;
    private string replaceName = "";
    private bool isChangeName = false;
    public class ReplaceData
    {
        public GameObject old;
        public GameObject replace;
        public int index = 0;
    }
    void OnGUI()
    {
        EditorGUILayout.LabelField("ѡ��һ���µ�����");
        newPrefab = (GameObject)EditorGUILayout.ObjectField(newPrefab, typeof(GameObject), true, GUILayout.MinWidth(100f));
        tonewPrefab = newPrefab;
        //isChangeName = EditorGUILayout.ToggleLeft("���ı䳡�������������", isChangeName);
        if (GUILayout.Button("�滻ѡ�е�����"))
        {
            ReplaceObjects();
        }
        EditorGUILayout.LabelField("----------------------");
        replaceName = EditorGUILayout.TextField("��Ҫ�滻����������", replaceName);
        if (GUILayout.Button("�滻��ͬ���ֵ�"))
        {
            ReplaceObjectsByName(replaceName, false);
        }
        if (GUILayout.Button("�滻�������ֵ� ����"))
        {
            ReplaceObjectsByName(replaceName, true);
        }
        EditorGUILayout.LabelField("----------------------");
        if (GUILayout.Button("�����޸�"))
        {
            EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
    }
    void ReplaceObjects()
    {
        if (tonewPrefab == null) return;
        Object[] objects = Selection.objects;
        List<ReplaceData> replaceDatas = new List<ReplaceData>();
        foreach (Object item in objects)
        {
            GameObject temp = (GameObject)item;
            ReplaceData replaceData = new ReplaceData();
            replaceData.old = temp;
            ReplaceOne(replaceData);
            replaceDatas.Add(replaceData);
        }
        HandleReplaceData(replaceDatas);
    }
    void HandleReplaceData(List<ReplaceData> replaceDatas)
    {
        foreach (var replaceData in replaceDatas)
        {
            replaceData.replace.transform.SetSiblingIndex(replaceData.index);
            if (null != replaceData.old && null != replaceData.old.gameObject)
                DestroyImmediate(replaceData.old.gameObject);
        }
    }
    void ReplaceObjectsByName(string name, bool isContain)
    {
        if (string.IsNullOrEmpty(name)) return;
        List<ReplaceData> replaceDatas = new List<ReplaceData>();
        Transform[] all = Object.FindObjectsOfType<Transform>();
        foreach (var item in all)
        {
            //Debug.LogError(item.name);
            ReplaceData replaceData = new ReplaceData();
            replaceData.old = item.gameObject;
            if (!isContain && item.gameObject.name == name)
            {
                ReplaceOne(replaceData);
                replaceDatas.Add(replaceData);
            }
            else if (isContain && item.gameObject.name.Contains(name))
            {
                ReplaceOne(replaceData);
                replaceDatas.Add(replaceData);
            }
        }
        HandleReplaceData(replaceDatas);
    }

    public void ReplaceOne(ReplaceData replaceData)
    {
        GameObject replace = (GameObject)PrefabUtility.InstantiatePrefab(tonewPrefab);
        replace.transform.SetParent(replaceData.old.transform.parent);
        replace.transform.localPosition = replaceData.old.transform.localPosition;
        replace.transform.localRotation = replaceData.old.transform.localRotation;
        replace.transform.localScale = replaceData.old.transform.localScale;
        replaceData.replace = replace;
        replaceData.index = replaceData.old.transform.GetSiblingIndex();
    }
}*/
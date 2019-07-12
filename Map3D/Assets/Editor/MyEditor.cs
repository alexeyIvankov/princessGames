using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dom), true)]
public class MyEditor : Editor
{
    public SceneAsset sceneName;
}

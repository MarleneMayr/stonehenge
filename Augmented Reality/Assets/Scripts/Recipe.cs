using UnityEngine;
using Bricks;
using UnityEditor;

public class Recipe : ScriptableObject
{
    // More information on serialization and what to look out for...
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/SerializeField.html
    public RecipeBrick[] ingredients;
}
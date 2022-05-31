using UnityEditor;
using UnityEngine;
public class Menu : MonoBehaviour
{
    [MenuItem("DebugMode/All")]
    static void DoAll()
    {
        Shader.SetGlobalInt("_Mode", 0);
    }

/*    [MenuItem("DebugMode/Depth")]
    static void DoDepth()
    {
        Shader.SetGlobalInt("_Mode", 1);
    }*/

    [MenuItem("DebugMode/Normal")]
    static void DoNormal()
    {
        Shader.SetGlobalInt("_Mode", 2);
    }

    [MenuItem("DebugMode/Thickness")]
    static void DoThickness()
    {
        Shader.SetGlobalInt("_Mode", 3);
    }

    [MenuItem("DebugMode/Floor Textured with Refraction")]
    static void DoFloortTextured()
    {
        Shader.SetGlobalInt("_Mode", 4);
    }

    [MenuItem("DebugMode/Reflection")]
    static void DoReflection()
    {
        Shader.SetGlobalInt("_Mode", 5);
    }

    [MenuItem("DebugMode/Refraction")]
    static void DoRefraction()
    {
        Shader.SetGlobalInt("_Mode", 6);
    }

    [MenuItem("DebugMode/Phong Specular Highlight")]
    static void DoHighlight()
    {
        Shader.SetGlobalInt("_Mode", 7);
    }

    [MenuItem("DebugMode/Blend Scene and Fluid Color")]
    static void DoBlend()
    {
        Shader.SetGlobalInt("_Mode", 8);
    }

/*    [MenuItem("DebugMode/Test Walls")]
    static void DoWalls()
    {
        Shader.SetGlobalInt("_Mode", 9);
    }*/
}

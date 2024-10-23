using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform))] // 扩展 Transform 组件的 Inspector
public class TransformInspectorExtension : Editor
{
    public override void OnInspectorGUI()
    {
        // 显示默认的 Transform Inspector 面板
        base.OnInspectorGUI();

        // 获取当前选中的 Transform 对象
        Transform transform = (Transform)target;

        // 获取物体的绝对大小
        Vector3 absoluteSize = GetObjectSize(transform);

        // 在 Inspector 中显示对象的绝对大小信息
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Object Absolute Size Info", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("绝对大小 (World Size):", $"X = {absoluteSize.x} 单位, Y = {absoluteSize.y} 单位, Z = {absoluteSize.z} 单位");
    }

    // 获取物体的绝对大小
    private Vector3 GetObjectSize(Transform transform)
    {
        Renderer renderer = transform.GetComponent<Renderer>();
        Collider collider = transform.GetComponent<Collider>();

        if (renderer != null) // 使用 Renderer 的边界计算大小
        {
            Bounds bounds = renderer.bounds;
            return bounds.size;
        }
        else if (collider != null) // 如果没有 Renderer，使用 Collider 的边界计算大小
        {
            Bounds bounds = collider.bounds;
            return bounds.size;
        }
        else
        {
            // 如果没有 Renderer 或 Collider，返回局部缩放
            Debug.LogWarning($"物体 '{transform.name}' 没有 Renderer 或 Collider 组件，无法计算准确大小。");
            return Vector3.Scale(transform.localScale, Vector3.one);
        }
    }
}

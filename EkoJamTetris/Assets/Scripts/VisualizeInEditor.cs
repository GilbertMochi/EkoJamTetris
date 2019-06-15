using UnityEngine;

public class VisualizeInEditor : MonoBehaviour
{
    public bool drawInEditor;

    private void OnDrawGizmos() {
        if(drawInEditor)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position, 0.4f);
        }
    }
}

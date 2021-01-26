using UnityEngine;

//This is for Orthographic Camera Only
[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    internal Camera _Camera;

    private float pOrthosize => _Camera.orthographicSize;

    private float pWorldScreenHalfWidth => CUtility.RoundingTofloat(pOrthosize * _Camera.aspect);

    public float UpY => CUtility.RoundingTofloat(pOrthosize + transform.position.y);

    public float DownY => CUtility.RoundingTofloat(-pOrthosize + transform.position.y);

    public float RightX => CUtility.RoundingTofloat(pWorldScreenHalfWidth + transform.position.x);

    public float LeftX => CUtility.RoundingTofloat(-pWorldScreenHalfWidth + transform.position.x);

    private float pWidth => CUtility.RoundingTofloat(pWorldScreenHalfWidth * 2);

    private float pHeight => CUtility.RoundingTofloat(pOrthosize * 2);

    void Awake()
    {
        _Camera = gameObject.GetComponent<Camera>();
    }

    public void DebugAllValues()
    {
        DebugUtils.Log("m_orthosize =>" + pOrthosize);
        DebugUtils.Log("m_Width =>" + pWidth);
        DebugUtils.Log("m_Height =>" + pHeight);
        DebugUtils.Log("m_UpY =>" + UpY);
        DebugUtils.Log("m_DownY =>" + DownY);
        DebugUtils.Log("m_RightX =>" + RightX);
        DebugUtils.Log("m_LeftX =>" + LeftX);
    }

    public void SetOrthoGraphicSize(float size)
    {
        _Camera.orthographicSize = size;
        // DebugAllValues();
    }
}

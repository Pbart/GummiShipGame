using UnityEngine;
using System.Collections;

public class DemoSceneScript : MonoBehaviour
{
    private Transform _cachedTransform;
    private float _phase = 0f;
    private int _skyBoxIndex = 0;
    private Skybox _cachedSkyBox;

    [SerializeField]
    private float _distance = 40f;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private Material[] _skyBoxList;

    [SerializeField]
    private Texture2D _frameTexture;

    [SerializeField]
    private Vector2 textPos;

    private int SkyBoxIndex
    {
        get{ return _skyBoxIndex;}
        set
        { 
            _skyBoxIndex = value;
            if(_skyBoxIndex < 0)
                _skyBoxIndex = _skyBoxList.Length;
            if(_skyBoxIndex >= _skyBoxList.Length)
                _skyBoxIndex = 0;
            _cachedSkyBox.material = _skyBoxList[_skyBoxIndex];
        }
    }

    void Awake()
    {
        _cachedTransform = transform;
        _cachedSkyBox = GetComponent<Skybox>();
        _cachedSkyBox.material = _skyBoxList[0];
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(textPos.x + 10f, textPos.y, 50, 30), "Last"))
            SkyBoxIndex--;
        if (GUI.Button(new Rect(textPos.x + 80, textPos.y, 50, 30), "Next"))
            SkyBoxIndex++;
        GUI.Label(new Rect(textPos.x + 200, textPos.y + 10f, 300, 30), _skyBoxList[_skyBoxIndex].name);
        GUI.DrawTexture(new Rect(0, 0, 1024, 768), _frameTexture, ScaleMode.StretchToFill, true, 10.0F);
    }

    void Update()
    {
        _cachedTransform.position = new Vector3(Mathf.Sin(_phase), 0f, Mathf.Cos(_phase)) * _distance;
        _cachedTransform.localRotation = Quaternion.Euler(_cachedTransform.position);
        _phase += _speed * Time.deltaTime;
    }
}

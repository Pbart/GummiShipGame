using UnityEngine;
using System.Collections;

public class DemoScriptSceneLoader : MonoBehaviour {

    [SerializeField]
    private Transform _cachedTransform;
    private float _phase = 0f;

    [SerializeField]
    private float _distance = 40f;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private int _skyBoxIndex = 0;

    [SerializeField]
    private Texture2D _frameTexture;

    [SerializeField]
    private Vector2 textPos;

    [SerializeField]
    private GameObject starParticles;

    private int SkyBoxIndex
    {
        get { return _skyBoxIndex; }
        set
        {
            _skyBoxIndex = value;
            if (_skyBoxIndex > Application.levelCount - 1)
                _skyBoxIndex = 1;
            if (_skyBoxIndex < 1)
                _skyBoxIndex = Application.levelCount - 1;
            StartCoroutine(LoadAsyncLevel(_skyBoxIndex));
        }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
        SkyBoxIndex = 1;
	}


    void OnGUI()
    {
        if (GUI.Button(new Rect(textPos.x + 10f, textPos.y, 50, 30), "Last"))
            SkyBoxIndex--;
        if (GUI.Button(new Rect(textPos.x + 80, textPos.y, 50, 30), "Next"))
            SkyBoxIndex++;
        GUI.Label(new Rect(textPos.x + 200, textPos.y + 10f, 300, 30), Application.loadedLevelName);
        GUI.DrawTexture(new Rect(0, 0, 1024, 768), _frameTexture, ScaleMode.StretchToFill, true, 10.0F);
    }

    void Update()
    {
        if (_cachedTransform == null && Camera.main != null) {
            _cachedTransform = Camera.main.transform;
        }

        if (_cachedTransform != null) {
            _cachedTransform.position = new Vector3(Mathf.Sin(_phase), 0f, Mathf.Cos(_phase)) * _distance;
            _cachedTransform.localRotation = Quaternion.Euler(_cachedTransform.position);
            _phase += _speed * Time.deltaTime;
        }
    }

    IEnumerator LoadAsyncLevel(int levelIndex)
    {
        AsyncOperation async = Application.LoadLevelAsync(levelIndex);
        yield return async;
        Debug.Log("Loading complete");
    }
}

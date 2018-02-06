using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewProvider : MonoBehaviour
{
    private Canvas _canvas;
    private Config _config;
    private readonly Dictionary<string, GameObject> _uiPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        GD.ScreenWidth = GetCanvasWidth();
        GD.ScreenHeight = GetCanvasHeight();
    }

    public void Init(Config config)
    {
        _config = config;
    }

    public void Get<ViewT>(Action<ViewT> finishCallback) where ViewT : MonoBehaviour
    {
        string prefabName = "UI/" + _config.GetViewPrefabName(typeof(ViewT));

        GameObject go = null;
        _uiPrefabs.TryGetValue(prefabName, out go);
        UnityEngine.Object prefab = null;
        if (go == null)
        {
            prefab = Resources.Load(prefabName, typeof(GameObject));
            _uiPrefabs[prefabName] = prefab as GameObject;
        }
        else
        {
            prefab = go;
        }

        GameObject view = Instantiate(prefab, _canvas.transform) as GameObject;
        ViewT viewObject = view.GetComponent<ViewT>();
        if (viewObject != null)
        {
            view.SetActive(false);

            if (finishCallback != null)
            {
                finishCallback(viewObject);
            }
        }
    }

    public int GetCanvasWidth()
    {
        return (int)_canvas.GetComponent<CanvasScaler>().referenceResolution.x;
    }
    public int GetCanvasHeight()
    {
        return (int)_canvas.GetComponent<CanvasScaler>().referenceResolution.y;
    }
}

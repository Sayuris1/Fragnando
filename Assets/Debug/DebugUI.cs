using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DebugUI : MonoBehaviour
{
    public static DebugUI Instance;


    [SerializeField]
    private GameObject _debugText;
    
    private Dictionary<string, TextMeshProUGUI> _debugs = new();
    private Transform _grid;

    private void Awake()
    {
        Instance = this;

        _grid = GetComponentInChildren<GridLayoutGroup>().transform;
    }

    public void AddOrUpdateDebug(string key, string value)
    {
        if(_debugs.ContainsKey(key))
            _debugs[key].text = value;

        else
        {
            GameObject newGO = Instantiate(_debugText);
            newGO.transform.SetParent(_grid);

            _debugs.Add(key, newGO.GetComponent<TextMeshProUGUI>());
            _debugs[key].text = value;
        }
    }

    public void RemoveFromDebug(string key)
    {
        if(!_debugs.ContainsKey(key))
            return;

        TextMeshProUGUI tmp = _debugs[key];
        _debugs.Remove(key);

        Destroy(tmp.gameObject);
    }

    public void ShowHideDebug()
    {
        _grid.gameObject.SetActive(!_grid.gameObject.activeSelf);
    }

    public void EnableDisableOcclusion()
    {
        AROcclusionManager manager = Camera.main.gameObject.GetComponent<AROcclusionManager>();
        manager.enabled = !manager.enabled;
    }
}

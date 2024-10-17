using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}

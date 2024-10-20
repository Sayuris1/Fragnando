using System.Collections;
using System.Collections.Generic;
using Niantic.Lightship.AR.WorldPositioning;
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

    private ARWorldPositioningManager _positioningManager;

    private void Awake()
    {
        Instance = this;

        _grid = GetComponentInChildren<GridLayoutGroup>().transform;
        StartCoroutine(GetPositioningManager());
    }

    private void Update()
    {
        if(Camera.main.TryGetComponent<ARWorldPositioningCameraHelper>(out var helper))
            AddOrUpdateDebug("Cam", $"Cam: Latitude: {helper.Latitude},\n Cam Longitude: {helper.Longitude},\n Cam Altitude: {helper.Altitude}");

        if(_positioningManager != null)
            AddOrUpdateDebug("Status", $"Status: {_positioningManager.Status}\n GPS Enabled: {Input.compass.enabled}\n GPS Status: {Input.location.status}");
        else
            AddOrUpdateDebug("Status", $"Status: null\n GPS Enabled: {Input.compass.enabled}\n GPS Status: {Input.location.status}");

        //AddOrUpdateDebug("GPSPos", "GPS Latitude: " + Input.location.lastData.latitude + "\n GPS Longitude: " + Input.location.lastData.longitude);

        if(Input.location.status != LocationServiceStatus.Running)
        {
            Input.compass.enabled = true;
            Input.location.Start(5, 3);
        }
    }

    private IEnumerator GetPositioningManager()
    {
        yield return new WaitUntil(() => Camera.main.gameObject.GetComponentInParent<ARWorldPositioningManager>() != null);

        _positioningManager = Camera.main.gameObject.GetComponentInParent<ARWorldPositioningManager>();
        AddOrUpdateDebug("Status", $"Status: {_positioningManager.Status}");
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

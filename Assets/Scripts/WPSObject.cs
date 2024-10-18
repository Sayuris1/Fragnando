using System;
using System.Collections;
using Niantic.Lightship.AR.WorldPositioning;
using TMPro;
using UnityEngine;

[Serializable]
public struct WPSPosStruct
{
    public double LA;
    public double LO;
    public double ALT;

    public WPSPosStruct(double la = 37.795349, double lo = -122.39237, double alt = 0)
    {
        this.LA = la;
        this.LO = lo;
        this.ALT = alt;
    }
}

public class WPSObject : MonoBehaviour
{
    public WPSPosStruct WPSPos;

    [SerializeField] 
    private ARWorldPositioningObjectHelper _positioningHelper;

    public void Setup(PersonalitySheet.Row row)
    {
        gameObject.name = row.Id;

        gameObject.GetComponentInChildren<TextMeshPro>().text = row.Text;
        gameObject.GetComponentInChildren<TextMeshPro>().fontSize = row.FontSize;

        gameObject.transform.localScale = Vector3.one * row.Scale;

        WPSPos = new(row.Latitude, row.Longitude, row.Altitude);

        StartCoroutine(GetPositioningHelper());
    }

    private IEnumerator GetPositioningHelper()
    {
        yield return new WaitUntil(() => Camera.main.gameObject.GetComponentInParent<ARWorldPositioningObjectHelper>() != null);

        _positioningHelper = Camera.main.gameObject.GetComponentInParent<ARWorldPositioningObjectHelper>();
        _positioningHelper.AddOrUpdateObject(gameObject, WPSPos.LA, WPSPos.LO, WPSPos.ALT, Quaternion.identity);
    }

    private void Update()
    {
        if(_positioningHelper == null)
            return;

        transform.LookAt(Camera.main.transform, transform.up);

        // Z rot always 0
        Vector3 currentRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, 0);

        DebugUI.Instance.AddOrUpdateDebug(gameObject.name, $"{gameObject.name}:\n Latitude: {WPSPos.LA}\n Longitude: {WPSPos.LO}\n Altitude: {WPSPos.ALT}");
    }

    private void OnDestroy()
    {
        DebugUI.Instance.RemoveFromDebug(gameObject.name);
    }
}

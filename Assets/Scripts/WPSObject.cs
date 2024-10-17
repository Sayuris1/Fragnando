using Niantic.Lightship.AR.WorldPositioning;
using UnityEngine;

public class WPSObject : MonoBehaviour
{
    //LA:37.7953541872301
    //LO:-122.392329630534
    //ALT:32.8621559143066
    //37.79537982860326, -122.39239660353809

    [SerializeField] 
    private ARWorldPositioningObjectHelper positioningHelper;

    public double LA = 37.795349;
    public double LO = -122.39237;
    public double ALT = 0;

    private void OnEnable()
    {
        if(positioningHelper == null)
            positioningHelper = Camera.main.gameObject.GetComponentInParent<ARWorldPositioningObjectHelper>();
        
        positioningHelper.AddOrUpdateObject(gameObject, LA, LO, ALT, Quaternion.identity);
    }

    #if UNITY_EDITOR
        void Update()
        {
            transform.LookAt(Camera.main.transform, transform.up);

            var helper = Camera.main.GetComponent<ARWorldPositioningCameraHelper>();
            DebugUI.Instance.AddOrUpdateDebug("Latitude", $"Latitude: {helper.Latitude}");
            DebugUI.Instance.AddOrUpdateDebug("Longitude", $"Longitude: {helper.Longitude}");
            DebugUI.Instance.AddOrUpdateDebug("Altitude", $"Altitude: {helper.Altitude}");
        }
    #endif
}

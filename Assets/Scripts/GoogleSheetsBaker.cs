using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Niantic.Lightship.AR.WorldPositioning;
using UnityEngine;

public class GoogleSheetsBaker : MonoBehaviour
{
    public GameObject CreatePrefab;

    private static readonly string _sheetID = "1GdLLxtKPpudC-sWthOs0QJQGNbCUi5UHQozi7rhjnuA";
    //private static readonly string _googleCredential = File.ReadAllText("C:/Users/Admin/Desktop/Cred.json");
    private static readonly string _googleCredential = @"{}";
    private List<WPSObject> _createdObjects = new();

    private async void Start()
    {
        await PullDataFromSheet();
    }

    public async Task PullDataFromSheet()
    {
        // pass logger to receive logs
        var sheetContainer = new SheetContainer();

        // bake sheets from google converter
        await sheetContainer.Bake(new Cathei.BakingSheet.GoogleSheetConverter(_sheetID, _googleCredential));

        foreach (WPSObject createdObj in _createdObjects)
            Destroy(createdObj.gameObject);
        _createdObjects = new();

        foreach (PersonalitySheet.Row row in sheetContainer.PersonalitySheet)
        {
            WPSObject newGo = Instantiate(CreatePrefab).GetComponent<WPSObject>();

            newGo.Setup(row);
            
            _createdObjects.Add(newGo);
        }
    }

    public async void DebugPull()
    {
        await PullDataFromSheet();
    }
}

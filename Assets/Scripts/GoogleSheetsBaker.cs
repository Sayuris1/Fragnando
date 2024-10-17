using System.Threading.Tasks;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using UnityEngine;

public class GoogleSheetsBaker : MonoBehaviour
{
    private static readonly string _sheetID = "1GdLLxtKPpudC-sWthOs0QJQGNbCUi5UHQozi7rhjnuA";

    private static readonly string _googleCredential = @"{
        ""type"": ""service_account"",
        ""project_id"": ""duckt-438919"",
        ""private_key_id"": ""dfb046b9f180fb540d3cc375fae227f9b78454d8"",
        ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDNPYerot2kEj5C\nS0Kub1FE3bEbkaY+WVoPPxiKmW7+hCT0gqotqnPyNX6PTU24bicVGOZ3Wt9MPM67\n/g2vYjlMvqwDudw1MutacHstFggRAzaXaiMpqT9sG8wkFvZpJafmd5lZ9dMriHAx\nP5tNban06/S5MXVl3LtNjofMm6mGywWfS2fUGD4IATrSUgb656XWNPO8cDNQb+1D\ntCsksvI5OZspAuLoRc6Z+nDBL/R9+ewD65hepF0wUKPGzJH0VsGCIK5Nfztbyqhz\nGnWFGyE71PgoPDDyzmyz4snGOGUmtk5BDv0W0vde1YBAt0n+y32aDTJvTQamNv/Q\nj83hKO+xAgMBAAECggEAA74ANzpkpHn0eo5EPjyt3h27sOpYMWSJx/1JLYSSk0Xy\n2L/fXS5+Z2qfYhRrAYNsQRrfeUqMVHSUYWjTTfQYyHcxv24+kpXypO2OaJoXyUJs\nsHIuB3Ovghr32MQDBK18CpX6LOHjhcFUW48ffgss/hf/GF3x2DEs4hzIvMvGzMjm\nnVrYwXJC6NTYI5i4D7fCgD9wFMFu1T9v6CDzELatryh6spxtogYD3ugsH+X8bokv\n9s2qX4A2/vIcNdgQjtanZpIRSxgAdxwQVOVDLZABwkEyxNrIbD2h9gjGn/8BccS/\n/JF8OCsOO1lXWP5FAjv0hc9bBpC6DVOv6NEhesMEpQKBgQDqeNvMUZtY5S7FPZwV\n9ZgDjUhYpgGMgLs3DYWr0RTyNo+TOL6K1STBJENxzIjedJjfmLGc3OosZy4RxBmS\nVv4amDkuhIa9ZbevtFIX9g6kWPkHXcu6yvWpCF9Ycp9vqNurrdto09VWfRs2Vdjh\nBkyjBkHmADsSjtbYrzNTsOgGlQKBgQDgFZg8H4S2oI5GT9qYaoAZvZlWVJFso9ZF\nS7X91FztOZHXt2HNeZ6YcKORmxFtAuT2son424FKXPHyzQynxxOjeFAjo+M87YJa\nsQI27UiDCv6ZRnLjIcLj+9J7j/DfTWqvfU1Pt2wLjF8fLlEko8B+cT4hrcazIm6z\ny8xEDYxJrQKBgHWjd5u2Yfui2Olq9NHrq340t1SKzYIh4ExJw4Ql3Z18lGJn+OmM\n8OjaeeAxn4YE9+JPKOWrzO3EQs/1FGNgtBLBxwa4xSVnUFaeYQXWlRgN77urapjo\nVYKeScAFmkqabMRg8RFSHtpK4IlNXwffcjnk3NE50mmIMvD6TgUyX+dVAoGAYUQe\nW2KW+arBdvWsfeIkjofE6js1EIIhPNz7Nx2Z5ILLsmXwNv9mfubNqSM1MitCdW7U\nAshM25lykCC/MIhyFs/fgUjfFxBFN9aDjpH6mp8IGkIBFmQOM2WYXHgDZvo0p83s\n5ABLDsw0cdVp9Ux6/qdq/7VtfU4h3TtGA/gZf9UCgYEAp+1WWXWRjg4Nd00OIR1M\nsABsR6/uTB5bHrYx9Pu1gInfBsE5lUBABfb5scozMgidAG+ip/dFjcu1HaFUuKXo\neh1VDXkfLoOcGHZ7tzXVudQMNtPPu28vje91qF5FwDsPSvzGVMssswC0NlRbxL2o\nv1+tXURACQh8L1R1LPlC2RM=\n-----END PRIVATE KEY-----\n"",
        ""client_email"": ""admin-572@duckt-438919.iam.gserviceaccount.com"",
        ""client_id"": ""106103188599557196355"",
        ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
        ""token_uri"": ""https://oauth2.googleapis.com/token"",
        ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
        ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/admin-572%40duckt-438919.iam.gserviceaccount.com"",
        ""universe_domain"": ""googleapis.com""
    }";

    private async void Start()
    {
        await PullDataFromSheet();
    }

    private async Task PullDataFromSheet()
    {
        // pass logger to receive logs
        var sheetContainer = new SheetContainer();

        // bake sheets from google converter
        await sheetContainer.Bake(new GoogleSheetConverter(_sheetID, _googleCredential));

        foreach (var row in sheetContainer.PersonalitySheet)
            Debug.Log(row.Text);
    }
}

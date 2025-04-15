using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ButtonClickSender : MonoBehaviour
{
    [Header("Configurações da API")]
    public string apiURL = "https://localhost:7215/api/Button"; // Substitua pela sua URL
    public string buttonID = "meuBotao"; // Identificador único

    [Header("Dados Adicionais")]
    public bool sendSceneName = true;
    public bool sendPlatformInfo = true;

    private Button unityButton;
    public class AcceptAllCertificates : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    void Start()
    {
        unityButton = GetComponent<Button>();
        if (unityButton != null)
        {
            unityButton.onClick.AddListener(() => StartCoroutine(SendClickData()));
        }
    }

    IEnumerator SendClickData()
    {
        // Prepara os dados adicionais
        var additionalData = new Dictionary<string, string>();

        if (sendSceneName)
        {
            additionalData["scene"] = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        if (sendPlatformInfo)
        {
            additionalData["platform"] = Application.platform.ToString();
        }

        // Cria o objeto de dados
        var clickData = new ButtonClickInput
        {
            id = System.Guid.NewGuid().ToString(),
            buttonName = buttonID,
            clickTime = System.DateTime.UtcNow.ToString("o"), // Formato ISO 8601
            additionalData = additionalData
        };

        // Converte para JSON
        string jsonData = JsonUtility.ToJson(clickData);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        // Configura a requisição
        var request = new UnityEngine.Networking.UnityWebRequest(apiURL, "POST");
        request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(postData);
        request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envia a requisição
        yield return request.SendWebRequest();

        // Verifica o resultado
        if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Erro ao enviar click: {request.error}");
        }
        else
        {
            Debug.Log($"Click registrado! Resposta: {request.downloadHandler.text}");
        }
    }
}

// Modelo para serialização
[System.Serializable]
public class ButtonClickInput
{
    public string id;
    public string buttonName;
    public string clickTime;
    public Dictionary<string, string> additionalData;
}
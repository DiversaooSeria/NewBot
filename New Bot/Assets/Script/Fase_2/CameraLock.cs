using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLock : MonoBehaviour
{
    public bool isCameraLocked = false; // Variável que controla o estado da câmera
    private Vector3 lockedPosition;
    public Transform player;         // Referência ao transform do jogador
    public float smoothSpeed = 0.005f;  // Velocidade de suavização (quanto menor, mais suave)

    void Start()
    {
        // Armazena a posição inicial da câmera (ou a posição atual ao travar)
        lockedPosition = transform.position;
    }

    void Update()
    {
        if (isCameraLocked)
        {
            // Trava a posição da câmera na posição armazenada
            transform.position = lockedPosition;
        }
        else
        {
           
            HandleCameraMovement();
        }
    }

    void HandleCameraMovement()
    {
        // Exemplo simples de movimentação da câmera (ajuste conforme necessário)


        Vector3 smoothedPosition = Vector3.Lerp( transform.position, player.position , smoothSpeed );
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    public void LockCamera()
    {
        isCameraLocked = true;
        lockedPosition = transform.position; // Armazena a posição no momento do bloqueio
    }

    public void UnlockCamera()
    {
        isCameraLocked = false;
    }
    
}


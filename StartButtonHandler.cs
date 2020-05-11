using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    public void StartGame()
    {
        CubeBase.isGameStarted = true;
        GameManagerBase.isGameStarted = true;
    }
}

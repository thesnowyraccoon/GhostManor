using UnityEngine;

// Add a Pause System to your Game! - Top Down Unity 2D #17
// Game Code Library
// 11 Feb 2025
// Code Version: Unknown
// Available at: https://youtu.be/fspxIduosYQ?si=_vm6Td2PG3Pnm0PY 

public class PauseController : MonoBehaviour
{
    public static bool isPaused { get; private set; } = false;

    public static void SetPause(bool pause)
    {
        isPaused = pause;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EchoRecorder : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Echo Settings")]
    public GameObject echoPrefab;

    public List<PlayerSnapshot> playerSnapshots = new List<PlayerSnapshot>();

    private List<EchoPlayback> echoes = new List<EchoPlayback>();

    private float recordingTime = 0f;

    private void Update()
    {
        recordingTime += Time.deltaTime;

        playerSnapshots.Add(new PlayerSnapshot
        {
            position = transform.position,
            timestamp = recordingTime
        });
    }

    public void Do(InputAction.CallbackContext context)
    {
        // Had to add, or else it made 5 Echoes at once for some reason.
        if (!context.performed) return;

        Debug.Log("Repeat!");

        // Replay all previously created Echoes.
        foreach (EchoPlayback echo in echoes)
        {
            if (echo != null)
                echo.Replay();
        }

        // Create a new Echo.
        GameObject newEcho = Instantiate(
            echoPrefab,
            playerSnapshots[0].position,
            Quaternion.identity 
        );

        EchoPlayback echoScript =
            newEcho.GetComponent<EchoPlayback>();

        // Give the Echo its own copy of the recorded path.
        echoScript.SetSnapshots(
            new List<PlayerSnapshot>(playerSnapshots)
        );

        echoScript.Replay();

        echoes.Add(echoScript);

        // Start recording a new path.
        playerSnapshots.Clear();
        recordingTime = 0f;
    }
}

[System.Serializable]
public class PlayerSnapshot
{
    public Vector2 position;
    public float timestamp;
}

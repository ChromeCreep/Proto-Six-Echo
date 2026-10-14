//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class EchoPlayer : MonoBehaviour
//{
//    public List<PlayerSnapshot> playerSnapshots;
//    private PlayerInput playerInput;
//    public Rigidbody2D rb;

//    [Header("Echo Settings")]
//    public Transform echo;
//    public Rigidbody2D echoRb;
//    private float recordingTime = 0f;

//    private bool isRewinding = false;

//    private void Start()
//    {
//        playerInput = GetComponent<PlayerInput>();
//    }

//    private void Update()
//    {
//        if (isRewinding)
//            return;

//        recordingTime += Time.deltaTime;

//        playerSnapshots.Add(new PlayerSnapshot
//        {
//            position = transform.position,
//            timestamp = recordingTime
//        });
//    }

//    public void Do(InputAction.CallbackContext context)
//    {
//        Debug.Log("Initiate Echo Procedure.");

//        if (isRewinding)
//            return;

//        StartCoroutine(EchoCoroutine());
//    }

//    public IEnumerator EchoCoroutine()
//    {
//        isRewinding = true;

//        // Replay snapshots from first to last.
//        for (int i = 0; i < playerSnapshots.Count - 1; i++)
//        {
//            echo.position = playerSnapshots[i].position;

//            // Calculate the time until the next snapshot.
//            float waitTime = playerSnapshots[i + 1].timestamp - playerSnapshots[i].timestamp;

//            yield return new WaitForSeconds(waitTime);
//        }

//        // Move to the final recorded position.
//        echo.position = playerSnapshots[playerSnapshots.Count - 1].position;

//        isRewinding = false;

//        playerSnapshots.Clear();
//        recordingTime = 0f;
//    }

//}

//[System.Serializable]

//public class PlayerSnapshot
//{
//    public Vector2 position;
//    public float timestamp;
//}
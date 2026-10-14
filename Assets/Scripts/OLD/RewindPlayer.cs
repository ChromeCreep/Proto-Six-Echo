using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//public class RewindPlayer : MonoBehaviour
//{
//    public List<PlayerSnapshot> playerSnapshots;
//    private PlayerInput playerInput;
//    public Rigidbody2D rb;
//    private bool isRewinding = false;

//    private void Start()
//    {
//        playerInput = GetComponent<PlayerInput>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isRewinding)
//            return;

//        playerSnapshots.Add(new PlayerSnapshot
//        {
//            position = transform.position,
//            velocity = rb.linearVelocity
//        });
//    }


//    public void Do(InputAction.CallbackContext context)
//    {
//        Debug.Log("Initiate Echo Procedure.");

//        if(isRewinding)
//            return;
//        StartCoroutine(RewindCoroutine());
//    }

//    public IEnumerator RewindCoroutine()
//    {
//        rb.simulated = false;
//        isRewinding = true;

//        for (int i = playerSnapshots.Count - 1; i >=0; i--)
//        {
//            rb.linearVelocity = playerSnapshots[i].velocity;
//            transform.position = playerSnapshots[i].position;

//            yield return null;
//        }

//        rb.simulated = true;
//        isRewinding = false;

//        playerSnapshots.Clear();
//    }
//}

//[System.Serializable]

//public class PlayerSnapshot
//{
//    public Vector2 position;
//    public Vector2 velocity;
//}
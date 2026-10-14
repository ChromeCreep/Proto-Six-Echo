using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoPlayback : MonoBehaviour
{
    private List<PlayerSnapshot> playerSnapshots = new List<PlayerSnapshot>();

    private Coroutine playbackCoroutine;
    private SpriteRenderer echoSprite;

    private float originalAlpha;

    private void Awake()
    {
        echoSprite = GetComponent<SpriteRenderer>();

        // Sets the Instantiated Echoes color to a random one.
        Color randomColor = Random.ColorHSV(
            0f, 1f,
            0.6f, 1f,
            0.8f, 1f,
            0.4f, 0.7f
        );

        echoSprite.color = randomColor;

        originalAlpha = echoSprite.color.a;
    }

    public void SetSnapshots(List<PlayerSnapshot> snapshots)
    {
        playerSnapshots = snapshots;
    }

    public void Replay()
    {
        // Stop this Echo's previous playback, if any.
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
        }

        // Restore the original alpha when playback starts.
        Color color = echoSprite.color;
        color.a = originalAlpha;
        echoSprite.color = color;

        // Start playing its path from the beginning.
        playbackCoroutine = StartCoroutine(EchoCoroutine());
    }

    public IEnumerator EchoCoroutine()
    {
        // Same logic as the Rewind Script, except starts at the beginning then goes to the end.

        for (int i = 0; i < playerSnapshots.Count - 1; i++)
        {
            transform.position = playerSnapshots[i].position;

            float waitTime = playerSnapshots[i + 1].timestamp - playerSnapshots[i].timestamp;

            yield return new WaitForSeconds(waitTime);
        }

        // Move to the final recorded position.
        if (playerSnapshots.Count > 0)
        {
            transform.position = playerSnapshots[playerSnapshots.Count - 1].position;
        }

        // Lowers the alpha to indicate the end of a Echo Playback.
        Color color = echoSprite.color;
        color.a = 0.05f;
        echoSprite.color = color;

        playbackCoroutine = null;
    }
}
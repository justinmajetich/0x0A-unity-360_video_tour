using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] videoRooms = new GameObject[4];

    private Material fade;

    private VideoPlayer activeVideoPlayer;

    [SerializeField]
    private float fadeToBlackDuration = 2f;

    [SerializeField]
    private float fadeFromBlackDuration = 1.5f;

    private int currentRoom = 0;

    private void Start() {

        // Set all rooms inactive, except starting room.
        for (int i = 0; i < 4; i++)
        {
            videoRooms[i].SetActive(i == currentRoom);
        }

        // Get active video player.
        activeVideoPlayer = videoRooms[currentRoom].GetComponent<VideoPlayer>();

        // Get fade material and initialize alpha.
        fade = GetComponentInChildren<MeshRenderer>().material;
        fade.color = new Color(0, 0, 0, 0);
    }

    public void TransitionToVideoRoom(int roomIndex) {

        // Fade out audio/video.
        StartCoroutine(FadeToBlack(roomIndex));
    }

    IEnumerator FadeToBlack(int roomIndex) {

        // Lerp between 0 and 1 alpha over time.
        // Variable t tracks interpolation value of Lerp.
        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeToBlackDuration) {

            // Fade out material alpha.
            Color newColor = new Color(0, 0, 0, t);
            fade.color = newColor;

            // Fade out audio.
            activeVideoPlayer.SetDirectAudioVolume(0, 1f - t);

            yield return null;
        }

        fade.color = new Color(0, 0, 0, 1);

        // Disable current video and enable new.
        videoRooms[currentRoom].SetActive(false);
        videoRooms[roomIndex].SetActive(true);
        currentRoom = roomIndex;

        // Get active video player.
        activeVideoPlayer = videoRooms[currentRoom].GetComponent<VideoPlayer>();

        // Yeild null till new player is prepared.
        while(!activeVideoPlayer.isPrepared) {
            yield return null;
        }

        // Fade in audio/video.
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack() {

        // Lerp from 1 to 0 alpha over time.
        // Variable t tracks interpolation value of Lerp.
        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeFromBlackDuration) {

            float newAlpha = Mathf.Lerp(1, 0, t);
            Color newColor = new Color(0, 0, 0, newAlpha);
            fade.color = newColor;

            // Fade in audio.
            activeVideoPlayer.SetDirectAudioVolume(0, t);

            yield return null;
        }

        fade.color = new Color(0, 0, 0, 0);

    }
}

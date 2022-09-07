using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MP4Loader : MonoBehaviour
{
    string FilePath = "";

    [SerializeField]
    private RawImage rawImage;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private AudioSource audioSource;


    void Start()
    {
        FilePath = Application.dataPath + "/Intro.mp4";
        LoadVideo(FilePath);
    }

    private void LoadVideo(string file_name)
    {
        videoPlayer.url = "file://" + file_name;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        audioSource.clip = null;

        rawImage.texture = videoPlayer.targetTexture;

        MP4_Play();
    }

    public void MP4_Play()
    {
        videoPlayer.Play();
        audioSource.Play();
    }

    public void OnStart_Click()
    {
        videoPlayer.Stop();
        audioSource.Stop();
        SceneManager.LoadScene("ChildRoom");
    }

    public void OnQuit_Click()
    {
        Application.Quit();
    }
}

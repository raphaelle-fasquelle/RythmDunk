using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class MusicInfo : MonoBehaviour
{
    string path, jsonString;
    public static List<float> startTimes;
    public static float musicDuration;

    private void Start()
    {
        path = Application.streamingAssetsPath + "/tfr_unity_1m14s_jeu_3.glm.bytes";
        jsonString = File.ReadAllText(path);
        GPGameLevelMakerFile musicEvents = JsonUtility.FromJson<GPGameLevelMakerFile>(jsonString);
        startTimes = new List<float>();

        foreach(GPGameEvent gpe in musicEvents.events)
        {
            startTimes.Add(gpe.startTime);
        }
        musicDuration = musicEvents.duration;
    }

    [Serializable]
    public class GPGameEvent
    {
        public float startTime;
        public float endTime;
        public int type;
        public string typeParameters;
    }

    [Serializable]
    public class GPGameLevelMakerFile
    {
        public float bpm;
        public float duration;
        public List<GPGameEvent> events;
    }
}

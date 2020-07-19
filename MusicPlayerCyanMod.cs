using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;

public class MusicPlayerCyanMod : UnityEngine.MonoBehaviour
{
    public static MusicPlayerCyanMod instance;
    List<FileInfo> playList;
    AudioSource currentClip;
    AudioListener audioListen;
    string path;
    float sound_value;
    Camera mainCameraFiled;
    Camera mainCamera
    {
        get
        {
            if (mainCameraFiled == null)
            {
                mainCameraFiled = UnityEngine.Object.FindObjectOfType(typeof(Camera)) as Camera;
            }
            return mainCameraFiled;
        }
    }
    void Awake()
    {
        instance = this;
    }
    void OnDestroy()
    {
        instance = null;
    }
    void Start()
    {
        sound_value = 1f;
        path = Application.dataPath + "/Music/";
        UpdatePlayerList();
    }
    public void UpdatePlayerList()
    {
        DirectoryInfo ifodir = new DirectoryInfo(path);
        if (!ifodir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        playList = new List<FileInfo>();
        FileInfo[] info = ifodir.GetFiles();
        foreach (FileInfo files in info)
        {
            if (files != null && files.FullName.EndsWith(".mp3"))
            {
                playList.Add(files);
            }
        }
    }
    public void Play(FileInfo info)
    {
        PlaySound(FromMp3Data(info.ReadAllBytes()), 1f, 1f, info.Name);
    }
    void OnGUI()
    {
        if (currentClip != null)
        {
            string str = "Clip:" + currentClip.name + "\nTime:" + currentClip.time.ToString("F") + "\nTime samples:" + currentClip.timeSamples + "\nTime2:" + currentClip.clip.length;
            GUI.Label(new Rect(Screen.width / 2, 0, 200, 200), str);
        }
        if (GUI.Button(new Rect(Screen.width / 2, 60, 100, 20), "play"))
        {
            Play(playList[0]);
        }
    }

    public AudioSource PlaySound(AudioClip clip, float volume, float pitch, string audiname)
    {
        volume *= sound_value;
        if ((clip != null) && (volume > 0.01f))
        {
            if (audioListen == null)
            {
                audioListen = UnityEngine.Object.FindObjectOfType(typeof(AudioListener)) as AudioListener;
                if (audioListen == null)
                {
                    if (mainCamera != null)
                    {
                        audioListen = mainCamera.gameObject.AddComponent<AudioListener>();
                    }
                }
            }
            if ((audioListen != null) && audioListen.enabled)
            {
                currentClip = audioListen.audio;
                if (currentClip == null)
                {
                    currentClip = audioListen.gameObject.AddComponent<AudioSource>();
                }
                currentClip.pitch = pitch;
                currentClip.PlayOneShot(clip, volume);
                currentClip.name = audiname;
                return currentClip;
            }
        }
        return null;
    }

    public static AudioClip FromMp3Data(byte[] data)
    {
        MemoryStream mp3stream = new MemoryStream(data);
        Mp3FileReader mp3audio = new Mp3FileReader(mp3stream);
        WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(mp3audio);
        WAV wav = new WAV(AudioMemStream(waveStream).ToArray());
        UnityEngine.Debug.Log(wav.ToString());
        AudioClip audioClip = AudioClip.Create("testSound", wav.SampleCount, 1, wav.Frequency, false, false);
        audioClip.SetData(wav.LeftChannel, 0);
        return audioClip;
    }

    private static MemoryStream AudioMemStream(WaveStream waveStream)
    {
        MemoryStream outputStream = new MemoryStream();
        using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, waveStream.WaveFormat))
        {
            byte[] bytes = new byte[waveStream.Length];
            waveStream.Position = 0;
            waveStream.Read(bytes, 0, Convert.ToInt32(waveStream.Length));
            waveFileWriter.Write(bytes, 0, bytes.Length);
            waveFileWriter.Flush();
        }
        return outputStream;
    }

    public class WAV
    {
        static float bytesToFloat(byte firstByte, byte secondByte)
        {
            short s = (short)((secondByte << 8) | firstByte);
            return s / 32768.0F;
        }
        static int bytesToInt(byte[] bytes, int offset = 0)
        {
            int value = 0;
            for (int i = 0; i < 4; i++)
            {
                value |= ((int)bytes[offset + i]) << (i * 8);
            }
            return value;
        }
        public float[] LeftChannel { get; internal set; }
        public float[] RightChannel { get; internal set; }
        public int ChannelCount { get; internal set; }
        public int SampleCount { get; internal set; }
        public int Frequency { get; internal set; }

        public WAV(byte[] wav)
        {
            ChannelCount = wav[22];
            Frequency = bytesToInt(wav, 24);
            int pos = 12;
            while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
            {
                pos += 4;
                int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
                pos += 4 + chunkSize;
            }
            pos += 8;
            SampleCount = (wav.Length - pos) / 2;
            if (ChannelCount == 2) SampleCount /= 2;
            LeftChannel = new float[SampleCount];
            if (ChannelCount == 2) RightChannel = new float[SampleCount];
            else RightChannel = null;
            int i = 0;
            while (pos < wav.Length)
            {
                LeftChannel[i] = bytesToFloat(wav[pos], wav[pos + 1]);
                pos += 2;
                if (ChannelCount == 2)
                {
                    RightChannel[i] = bytesToFloat(wav[pos], wav[pos + 1]);
                    pos += 2;
                }
                i++;
            }
        }
        public override string ToString()
        {
            return string.Format("[WAV: LeftChannel={0}, RightChannel={1}, ChannelCount={2}, SampleCount={3}, Frequency={4}]", LeftChannel, RightChannel, ChannelCount, SampleCount, Frequency);
        }
    }
}




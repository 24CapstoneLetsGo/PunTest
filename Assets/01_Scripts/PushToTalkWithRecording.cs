using UnityEngine;
using Photon.Voice.Unity;
using System.IO;
using Photon.Pun; // Photon ���� ���̺귯��

public class PushToTalkWithRecording : MonoBehaviour
{
    private Recorder recorder;
    private AudioClip recordedClip;
    private bool isRecording = false;
    private int recordingStartPosition = 0;

    [Header("fileName")]
    private int speechCount;
    private string playerName;

    void Start()
    {
        // Photon Voice Recorder ������Ʈ ��������
        recorder = GetComponent<Recorder>();

        // �⺻������ ���� ���� ��Ȱ��ȭ
        recorder.TransmitEnabled = false;

        // Photon���� �÷��̾� �г��� ��������
        playerName = PhotonNetwork.NickName;

    }

    void Update()
    {
        // �����̽� Ű�� ������ ���� ���� �� ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRecording();
            recorder.TransmitEnabled = true; // Photon Voice ���� ����
        }

        // �����̽� Ű�� ���� ���� ���� �� ���Ϸ� ����
        if (Input.GetKeyUp(KeyCode.Space))
        {
            recorder.TransmitEnabled = false; // Photon Voice ���� ����
            StopRecordingAndSave();
        }
    }

    // ���� ���� ����
    void StartRecording()
    {
        if (!isRecording)
        {
            // GameManager���� �߾� ��ȣ ��������
            speechCount = GameManager.Instance.GetNextSpeechCount();

            // �ִ� ���� �ð��� ��� ���� (��: 5��)
            recordedClip = Microphone.Start(null, false, 300, 44100);
            isRecording = true;

            // ������ ���۵� ���� ��ġ ����
            recordingStartPosition = Microphone.GetPosition(null);
        }
    }

    // ���� ���� �� ���� ������ ���̸�ŭ AudioClip �ڸ���
    void StopRecordingAndSave()
    {
        if (isRecording)
        {
            // ���� ����
            int recordingEndPosition = Microphone.GetPosition(null);
            Microphone.End(null);

            // ������ ������ ���̸� ���
            int actualSampleLength = recordingEndPosition - recordingStartPosition;

            // ���ο� AudioClip�� �����Ͽ� ���� ������ �κи� ����
            AudioClip trimmedClip = AudioClip.Create(recordedClip.name, actualSampleLength, recordedClip.channels, recordedClip.frequency, false);
            float[] data = new float[actualSampleLength];
            recordedClip.GetData(data, 0);
            trimmedClip.SetData(data, 0);

            // AudioClip�� ���Ϸ� ����
            //string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // ���ϸ� ���� (�߾��ȣ_�г���.wav)
            string fileName = $"{speechCount}_{playerName}.wav";
            SaveAudioClip(trimmedClip, fileName);

            isRecording = false;
        }
    }

    // AudioClip�� WAV ���Ϸ� ����
    void SaveAudioClip(AudioClip clip, string filename)
    {
        var filePath = Path.Combine(Application.persistentDataPath, filename);
        SavWav.Save(filePath, clip);
        Debug.Log("Audio saved to " + filePath);
    }
}

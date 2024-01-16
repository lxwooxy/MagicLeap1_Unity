using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class AudioCaptureSnippet: MonoBehaviour 
{
    public Text TextDisplay;
    private MLInput.Controller _controller;
    private AudioSource _source;
    private bool _bumper = false;

    void Start() 
    {
        MLInput.Start();

        _controller = MLInput.GetController(MLInput.Hand.Left);
        _source = gameObject.GetComponent < AudioSource > ();

        MLInput.OnControllerButtonDown += HandleControlButtonDown;
    }

    void OnDestroy() 
    {
        MLInput.Stop();
        MLInput.OnControllerButtonDown -= HandleControlButtonDown;
    }

    public void CaptureSwitch() 
    {
        if (_bumper) 
        {
            _source.Stop();
            _source.clip = Microphone.Start("", true, 10, 48000);
            TextDisplay.text = "Recording";
        }
        else 
        {
            Microphone.End("");
            _source.Play();
            TextDisplay.text = "Playing";
        }
    }

    private void HandleControlButtonDown(byte controlId, 
                                         MLInput.Controller.Button button) 
    {
        if (button == MLInput.Controller.Button.Bumper) 
        {
            _bumper = !_bumper;
            CaptureSwitch();
        }
    }
}

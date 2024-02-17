using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

public class AudioHapticsController : MonoBehaviour
{
    // Adjust size as needed
    private readonly float[] spectrumData = new float[256]; 

    private void Update()
    {
        // Ensure there's a gamepad connected
        if (Gamepad.current == null) return;

        // Get spectrum data from the AudioListener to capture all audio output
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Split spectrum into high and low frequencies using Linq, assuming cutoff at half
        int cutoffIndex = spectrumData.Length / 2;
        float lowFreqAverage = spectrumData.Take(cutoffIndex).Average();
        float highFreqAverage = spectrumData.Skip(cutoffIndex).Take(cutoffIndex).Average();

        // Normalize and adjust vibration intensity
        float lowFreqVibrationIntensity = Mathf.Clamp(lowFreqAverage * 15, 0, 1);
        float highFreqVibrationIntensity = Mathf.Clamp(highFreqAverage * 100, 0, 1);

        // Average or choose a method to combine the low and high frequencies for vibration
        float combinedVibrationIntensity = (lowFreqVibrationIntensity + highFreqVibrationIntensity) / 2;

        // Apply vibration
        Gamepad.current.SetMotorSpeeds(combinedVibrationIntensity, combinedVibrationIntensity);
    }
}

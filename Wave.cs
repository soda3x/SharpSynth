using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace SharpSynth
{
    class Wave
    {

        public Wave()
        {
            this.amplitude = short.MaxValue;
        }
        public short amplitude { get; set; }
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private bool playing = false;
        private SoundPlayer soundPlayer;
        public float frequency { get; set; }
        public float frequencyOffset { get; set; }
        public void SharpSynth_KeyUp(object sender, KeyEventArgs e)
        {
            if (playing == true)
            {
                soundPlayer.Stop();
                playing = false;
            }
        }

        public void SharpSynth_KeyDown(object sender, KeyEventArgs e)
        {
            IEnumerable<Oscillator> oscillators = Application.OpenForms[0].Controls.OfType<Oscillator>().Where(o => o.OscillatorEnabled);
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            int oscillatorsCount = oscillators.Count();
            // Keyboard Mapped Like Ableton's Keyboard Piano
            switch (e.KeyCode)
            {
                case Keys.A:
                    frequency = 130.8128f + frequencyOffset;
                    break;
                case Keys.W:
                    frequency = 138.5913f + frequencyOffset;
                    break;
                case Keys.S:
                    frequency = 146.8324f + frequencyOffset;
                    break;
                case Keys.E:
                    frequency = 155.5635f + frequencyOffset;
                    break;
                case Keys.D:
                    frequency = 164.8138f + frequencyOffset;
                    break;
                case Keys.F:
                    frequency = 174.6141f + frequencyOffset;
                    break;
                case Keys.T:
                    frequency = 184.9972f + frequencyOffset;
                    break;
                case Keys.G:
                    frequency = 195.9977f + frequencyOffset;
                    break;
                case Keys.Y:
                    frequency = 207.6523f + frequencyOffset;
                    break;
                case Keys.H:
                    frequency = 220.0000f + frequencyOffset;
                    break;
                case Keys.U:
                    frequency = 233.0819f + frequencyOffset;
                    break;
                case Keys.J:
                    frequency = 246.9417f + frequencyOffset;
                    break;
                case Keys.K:
                    frequency = 261.6256f + frequencyOffset;
                    break;
                case Keys.O:
                    frequency = 277.1826f + frequencyOffset;
                    break;
                case Keys.L:
                    frequency = 293.6648f + frequencyOffset;
                    break;
                default:
                    return;
            }

            foreach (Oscillator oscillator in oscillators)
            {
                int samplesPerWavelength = (int)(SAMPLE_RATE / frequency);
                short ampStep = (short)((short.MaxValue * 2) / samplesPerWavelength);
                short tempSample;
                Random random = new Random();

                switch (oscillator.Waveform)
                {
                    case Waveform.Sine:
                        // algorithm for sine wave generation is Sample = Amplitude * sin(t * i)
                        // where t is the angular frequency and i is a unit of time
                        for (int i = 0; i < SAMPLE_RATE; ++i)
                        {
                            wave[i] += Convert.ToInt16((amplitude * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i)) / oscillatorsCount);
                        }
                        break;
                    case Waveform.Square:
                        // algorithm for square wave generation is Sample = Amplitude * sgn(sin(t * i))
                        // sgn tells us whether the value of sine is positive, negative or 0
                        // where t is the angular frequency and i is a unit of time
                        for (int i = 0; i < SAMPLE_RATE; ++i)
                        {
                            wave[i] += Convert.ToInt16((amplitude * Math.Sign(Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i))) / oscillatorsCount);
                        }
                        break;
                    case Waveform.Saw:
                        // algorithm for saw wave generation is y(t) = x – floor(x);
                        // But this is procedurally generated
                        for (int i = 0; i < SAMPLE_RATE; ++i)
                        {
                            tempSample = (short)-amplitude;
                            for (int j = 0; j < samplesPerWavelength && i < SAMPLE_RATE; ++j)
                            {
                                tempSample += ampStep;
                                wave[i++] += Convert.ToInt16(tempSample / oscillatorsCount);
                            }
                        }
                        break;
                    case Waveform.Triangle:
                        // algorithm for triangle uses a fast fourier transform
                        tempSample = (short)-amplitude;
                        for (int i = 0; i < SAMPLE_RATE; ++i)
                        {
                            if (Math.Abs(tempSample + ampStep) > amplitude)
                            {
                                ampStep = (short)-ampStep;
                            }
                            tempSample += ampStep;
                            wave[i] += Convert.ToInt16(tempSample / oscillatorsCount);
                        }
                        break;
                    case Waveform.Noise:
                        // White noise can be generated from negative amplitude to positive amplitude
                        for (int i = 0; i < SAMPLE_RATE; ++i)
                        {
                            wave[i] += Convert.ToInt16(random.Next(-amplitude, amplitude) / oscillatorsCount);
                        }
                        break;
                }

            }

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
            // WAVE File Format
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    short blockAlign = BITS_PER_SAMPLE / 8;
                    int subChunk2Size = SAMPLE_RATE * blockAlign;

                    //RIFF Header
                    binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' }); // ChunkID
                    binaryWriter.Write(36 + subChunk2Size); // ChunkSize
                    binaryWriter.Write(new[] { 'W', 'A', 'V', 'E' }); // Format
                    binaryWriter.Write(new[] { 'f', 'm', 't', ' ' }); // Subchunk1ID
                    binaryWriter.Write(16); // Subchunk1Size
                    binaryWriter.Write(Convert.ToInt16(1)); // AudioFormat (PCM = 1)
                    binaryWriter.Write(Convert.ToInt16(1)); // NumChannels (Mono = 1, Stereo = 2, ... )
                    binaryWriter.Write(SAMPLE_RATE); // SampleRate
                    binaryWriter.Write(SAMPLE_RATE * blockAlign); // ByteRate
                    binaryWriter.Write(blockAlign); // BlockAlign
                    binaryWriter.Write(BITS_PER_SAMPLE); // BitsPerSample
                    binaryWriter.Write(new[] { 'd', 'a', 't', 'a' }); // Subchunk2ID
                    binaryWriter.Write(subChunk2Size); // Subchunk2Size
                    binaryWriter.Write(binaryWave); // Data

                    memoryStream.Position = 0;
                    soundPlayer = new SoundPlayer(memoryStream);
                    soundPlayer.Play();
                    playing = true;
                }
            }

        }
    }
}

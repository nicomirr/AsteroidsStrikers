using SFML.Audio;

namespace SpaceShipGame3
{
    public class SoundEffect
    {
        private SoundBuffer soundBuffer;   
        private Sound sound; 

        public SoundEffect(string soundFilePath)  
        {
            soundBuffer = new SoundBuffer(soundFilePath);   
            sound = new Sound(soundBuffer); 
        }

        public SoundStatus Status => sound.Status;  
        public bool Loop { get => sound.Loop; set => sound.Loop = value; }

        public void Play() => sound.Play();   
        public void Pause() => sound.Pause(); 
        public void Stop() => sound.Stop(); 

    }
}

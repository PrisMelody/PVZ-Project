using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


    public static class SoundManager
    {
        private static Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect>();
        private static Song _backgroundMusic;

        public static void LoadContent(ContentManager content)
        {

            _sounds["PeaHit"] = content.Load<SoundEffect>("Sounds/pea_hit");
            
            _backgroundMusic = content.Load<Song>("Sounds/main_theme");
            
            MediaPlayer.IsRepeating = true;
        }

        public static void PlayMusic()
        {
            if (_backgroundMusic != null && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(_backgroundMusic);
            }
        }

        public static void PlaySound(string name)
        {
            if (_sounds.ContainsKey(name))
            {
                _sounds[name].Play();
            }
        }
    }

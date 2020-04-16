using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Helpers
{
    public class SoundHelper
    {
        public static void PlaySoundEffect(string name)
        {
            SoundEffect effect = Game.GameContent.Load<SoundEffect>(name);
            effect.Play();
        }

        public static void PlaySong(string name)
        {
            Song song = Game.GameContent.Load<Song>(name);
            MediaPlayer.Play(song);
        }
    }
}

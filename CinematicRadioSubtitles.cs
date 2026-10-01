using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using UnityEngine;

namespace Cmd137.CinematicRadioSubtitles
{
    [ItemId("cmd137.cinematic-radio-subtitles")]
    public sealed class CinematicRadioSubtitles : VtolMod
    {
        public static SubtitleService Subtitles { get; private set; }

        public void Awake()
        {
            Subtitles = gameObject.AddComponent<SubtitleService>();
            DontDestroyOnLoad(gameObject);
            Debug.Log("Cinematic Radio Subtitles v1.1 loaded.");
        }

        public override void UnLoad()
        {
            if (Subtitles != null)
            {
                Destroy(Subtitles);
            }

            Debug.Log("Cinematic Radio Subtitles unloaded.");
        }
    }
}

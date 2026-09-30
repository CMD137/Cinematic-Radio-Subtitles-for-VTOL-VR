using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using UnityEngine;

namespace Cmd137.RightCommsSubtitles
{
    [ItemId("cmd137.right-comms-subtitles")]
    public sealed class RightCommsSubtitles : VtolMod
    {
        public static SubtitleService Subtitles { get; private set; }

        public void Awake()
        {
            Subtitles = gameObject.AddComponent<SubtitleService>();
            DontDestroyOnLoad(gameObject);
            Debug.Log("Right Comms Subtitles v1.0 loaded.");
        }

        public override void UnLoad()
        {
            if (Subtitles != null)
            {
                Destroy(Subtitles);
            }

            Debug.Log("Right Comms Subtitles unloaded.");
        }
    }
}

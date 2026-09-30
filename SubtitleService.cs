using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cmd137.CinematicRadioSubtitles
{
    public enum RadioSource
    {
        Tower,
        Lso,
        Awacs
    }

    public sealed class SubtitleService : MonoBehaviour
    {
        // Cinema-style subtitles deliberately show only the current transmission.
        private const int MaxLines = 1;
        private readonly List<ActiveSubtitle> lines = new List<ActiveSubtitle>();
        private Canvas canvas;
        private Text subtitleText;
        private Camera attachedCamera;
        private string displayedText = string.Empty;

        public void Push(RadioSource source, string message, float seconds)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var now = Time.unscaledTime;
            var formatted = Format(source, message);
            if (lines.Count > 0 && lines[0].Text == formatted)
            {
                lines[0].ExpiresAt = now + seconds;
                return;
            }

            lines.Insert(0, new ActiveSubtitle(formatted, now + seconds));
            if (lines.Count > MaxLines)
            {
                lines.RemoveAt(lines.Count - 1);
            }

            RefreshText();
        }

        private void Update()
        {
            EnsureOverlay();

            var now = Time.unscaledTime;
            var changed = false;
            for (var index = lines.Count - 1; index >= 0; index--)
            {
                if (lines[index].ExpiresAt <= now)
                {
                    lines.RemoveAt(index);
                    changed = true;
                }
            }

            if (changed)
            {
                RefreshText();
            }
        }

        private void EnsureOverlay()
        {
            var camera = Camera.main;
            if (camera == null || camera == attachedCamera)
            {
                return;
            }

            if (canvas != null)
            {
                Destroy(canvas.gameObject);
            }

            attachedCamera = camera;
            var root = new GameObject("CinematicRadioSubtitleOverlay", typeof(Canvas));
            DontDestroyOnLoad(root);
            // Screen-space camera UI remains stereo-aware while being placed
            // in front of cockpit geometry rather than behind its depth buffer.
            root.transform.SetParent(null, false);
            root.transform.localPosition = Vector3.zero;
            root.transform.localRotation = Quaternion.identity;
            root.transform.localScale = Vector3.one;

            canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
            canvas.planeDistance = Mathf.Max(camera.nearClipPlane + 0.01f, 0.02f);
            canvas.overrideSorting = true;
            canvas.sortingOrder = short.MaxValue;
            var canvasRect = canvas.GetComponent<RectTransform>();
            canvasRect.anchorMin = Vector2.zero;
            canvasRect.anchorMax = Vector2.one;
            canvasRect.offsetMin = Vector2.zero;
            canvasRect.offsetMax = Vector2.zero;

            var textObject = new GameObject("Text", typeof(Text));
            textObject.transform.SetParent(root.transform, false);
            subtitleText = textObject.GetComponent<Text>();
            subtitleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            subtitleText.fontSize = 30;
            subtitleText.lineSpacing = 0.82f;
            subtitleText.horizontalOverflow = HorizontalWrapMode.Wrap;
            subtitleText.verticalOverflow = VerticalWrapMode.Overflow;
            subtitleText.alignment = TextAnchor.LowerCenter;
            subtitleText.color = new Color(0.9f, 0.96f, 1f, 1f);

            // Keep the view unobstructed. A subtle outline keeps floating text
            // legible over bright sky, terrain, and cockpit glass without a panel.
            var outline = textObject.AddComponent<Outline>();
            outline.effectColor = new Color(0f, 0f, 0f, 0.82f);
            outline.effectDistance = new Vector2(1.25f, -1.25f);
            outline.useGraphicAlpha = false;

            var textRect = subtitleText.rectTransform;
            // Keep the subtitle baseline at 20% of the VR view; when the
            // message wraps, additional lines grow upward into this safe area.
            textRect.anchorMin = new Vector2(0.1f, 0.20f);
            textRect.anchorMax = new Vector2(0.9f, 0.34f);
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            subtitleText.text = displayedText;
        }

        private void RefreshText()
        {
            displayedText = string.Join("\n\n", lines.ConvertAll(line => line.Text).ToArray());
            if (subtitleText != null)
            {
                subtitleText.text = displayedText;
            }
        }

        private static string Format(RadioSource source, string message)
        {
            return string.Format("<b><color=#{0}>{1}</color></b>\n{2}", SourceColor(source), SourceName(source), message);
        }

        private static string SourceName(RadioSource source)
        {
            switch (source)
            {
                case RadioSource.Awacs: return "AWACS";
                case RadioSource.Lso: return "LSO";
                default: return "TOWER";
            }
        }

        private static string SourceColor(RadioSource source)
        {
            switch (source)
            {
                case RadioSource.Awacs: return "83D7FF";
                case RadioSource.Lso: return "FFD27D";
                default: return "B8EFA6";
            }
        }

        private sealed class ActiveSubtitle
        {
            public readonly string Text;
            public float ExpiresAt;

            public ActiveSubtitle(string text, float expiresAt)
            {
                Text = text;
                ExpiresAt = expiresAt;
            }
        }
    }
}

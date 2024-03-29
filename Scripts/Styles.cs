﻿#region

using UnityEditor;
using UnityEngine;

#endregion

namespace MagicLeapSetupTool.Editor
{
    /// <summary>
    /// IMGUI Styles for editor window
    /// </summary>
    public static class Styles
    {
        public static readonly GUIStyle TitleStyle = new GUIStyle(EditorStyles.largeLabel)
            {alignment = TextAnchor.MiddleCenter, fontSize = 20, normal = {textColor = Color.white},font = (Font)Resources.Load("Lomino/Lomino_Md", typeof(Font))};


        public static readonly GUIStyle HelpTitleStyle = new GUIStyle(EditorStyles.largeLabel)
            {fontStyle = FontStyle.Bold};

        public static readonly GUIStyle InfoTitleStyle = new GUIStyle(EditorStyles.helpBox)
            { fontSize = 12};

        public static readonly GUIStyle UrlLabelPersonal = new GUIStyle(EditorStyles.linkLabel)
        {
            name = "url-label", richText = true, alignment = TextAnchor.MiddleLeft
        };

        public static readonly GUIStyle UrlLabelProfessional = new GUIStyle(EditorStyles.linkLabel)
        {
            name = "url-label", richText = true, alignment = TextAnchor.MiddleLeft
        };


        public static readonly GUIStyle FixButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 100
        };

        public static class Utility
        {
            private static GUIStyle style = new GUIStyle();
            private static Texture2D texture = new Texture2D(1, 1);

            public static GUIStyle GetStyleWithColor(Color color)
            {
                texture.SetPixel(0, 0, color);
                texture.Apply();
                style.normal.background = texture;
                return style;
            }

            public static void SetColorForStyle(ref GUIStyle toChange, Color color)
            {
                texture.SetPixel(0, 0, color);
                texture.Apply();
                toChange.normal.background = texture;
            }
        }
    }
}
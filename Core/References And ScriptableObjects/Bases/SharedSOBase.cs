
   //Copyright 2026 Ethan Davis

   //Licensed under the Apache License, Version 2.0 (the "License");
   //you may not use this file except in compliance with the License.
   //You may obtain a copy of the License at
   //  http://www.apache.org/licenses/LICENSE-2.0

   //Unless required by applicable law or agreed to in writing, software
   //distributed under the License is distributed on an "AS IS" BASIS,
   //WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   //See the License for the specific language governing permissions and
   //limitations under the License.
   
   
   
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

namespace SharedValues
{
    public class SharedSOBase : ScriptableObject
    {
        protected const string k_sharedValueFilePath = "Assets\\Scripts\\Shared Values\\";
        protected const string k_editorTextureExtension = "Editor\\Textures\\";
        protected const string k_qMarkTexture = "qMark";

#if UNITY_EDITOR
        void OnEnable()
        {
            SetIcon();
        }

        public void SetIcon()
        {
            string path = GetTexturePath();
            Texture2D icon = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            EditorGUIUtility.SetIconForObject(this, icon);
        }

        public string GetTexturePath()
        {
            return GetFilePath() + k_editorTextureExtension + GetTextureName() + ".png";
        }
#endif

        protected virtual string GetFilePath()
        {
            return k_sharedValueFilePath + "Core\\";
        }

        protected virtual string GetTextureName()
        {
            return k_qMarkTexture;
        }
    }

    public static class SharedSOReserializer
    {
    #if UNITY_EDITOR
            [MenuItem("SO Tools/Values/Reset Icons")]
            public static void ReserializeActions()
            {
                var guids = AssetDatabase.FindAssets("t:SharedSOBase");
                for (short i = 0; i < guids.Length; i++)
                {
                    string guid = guids[i];

                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    SharedSOBase asset = (SharedSOBase)AssetDatabase.LoadAssetAtPath(path, typeof(SharedSOBase));
                
                    asset.SetIcon();
                }
            }
    #endif
    }
}


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
   
   
   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Object = UnityEngine.Object;

namespace SharedValues.Editor
{
    [CustomEditor(typeof(SharedSOBase),true)]
    [CanEditMultipleObjects]
    public class SharedSOEditor : UnityEditor.Editor
    {
        private SharedSOBase item { get { return (target as SharedSOBase); } }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            SharedSOBase so = target as SharedSOBase;
            
            if (so == null)
                return base.RenderStaticPreview(assetPath, subAssets, width, height);
            
            var path = so.GetTexturePath();
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if(icon == null)
            {
                Debug.LogWarning("No texture was found at " + path);
                return base.RenderStaticPreview(assetPath, subAssets, width, height);
            }

            Texture2D cache = new Texture2D(width, height);

            Texture2D preview = AssetPreview.GetAssetPreview(icon);
            EditorUtility.CopySerialized(preview, cache);
            
            return cache;
        }

        private static Type GetType(string TypeName)
        {
            var type = Type.GetType(TypeName);
            if(type!=null)
                return type;

            if(TypeName.Contains("."))
            {
                var assemblyName = TypeName.Substring(0,TypeName.IndexOf('.'));
                var assembly = Assembly.Load(assemblyName);
                if(assembly==null)
                    return null;
                type=assembly.GetType(TypeName);
                if(type!=null)
                    return type;
            }

            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach(var assemblyName in referencedAssemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                if(assembly!=null)
                {
                    type=assembly.GetType(TypeName);
                    if(type!=null)
                        return type;
                }
            }
            return null;
        }
    }
    
}

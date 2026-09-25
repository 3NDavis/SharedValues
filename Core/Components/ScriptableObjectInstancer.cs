
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
   
   
   
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.Linq;
#endif

namespace SharedValues
{
    public interface IMergeScriptableObjectInstacer
    {
        public void MergeInstancer(ScriptableObjectInstancer instancer);
    }

    /// <summary>
    /// A scriptable object instatiation and connection manager
    /// </summary>
    public sealed class ScriptableObjectInstancer : MonoBehaviour
    {
        ///<summary>The instancer that should be used instead of this one</summary>
        [Tooltip("The instancer that should be used instead of this one")]
        public ScriptableObjectInstancer passthroughInstancer;

        /// <summary> The dictionary that returns the instances of the inputted Scriptable Objects </summary>
        private Dictionary<ScriptableObject, ScriptableObject> globalToInstanceMap;
        public Dictionary<ScriptableObject, ScriptableObject> GlobalToInstanceMap => globalToInstanceMap;
#if UNITY_EDITOR
        private List<SOValuePair> instances;
        [System.Serializable]
        private class SOValuePair
        {
            public SOValuePair(ScriptableObject so, string val)
            {
                this.name = so.name;
                this.value = val;
            }
            [HideInInspector]
            public string name;
            public string value;
        }

        void Update()
        {
            //show the current instances in the editor
            if (passthroughInstancer)
            {
                instances = GetPairs(passthroughInstancer.GlobalToInstanceMap.Values.ToList());
            }
            else
            {
               instances = GetPairs(globalToInstanceMap.Values.ToList());
            }
        }

        List<SOValuePair> GetPairs(List<ScriptableObject> SOvalues)
        {
            List<SOValuePair> pairs = new();
            for (int i = 0; i < SOvalues.Count; i++)
            {
                string value = "na";
                if(SOvalues[i] is SharedValue)
                {
                    var sharedVal = (SharedValue)SOvalues[i];
                    var objVal = sharedVal.objValue;
                    if(objVal != null)
                    {
                        value = objVal.ToString();
                    }
                }
                pairs.Add(new SOValuePair(SOvalues[i], value));
            }
            return pairs;
        }
#endif

        void Awake()
        {
            //initialize the globalToInstanceMap for immediate playmode purposes
            globalToInstanceMap = new();

            #if UNITY_EDITOR
            //initialize the instances for debugging
            instances = new();
            #endif
        }

        /// <summary>
        /// Sets the passtrhough instancer allowing for different instance groups at runtime
        /// </summary>
        /// <param name="newPassthroughInstancer"></param>
        public void SetPassthroughInstancer(ScriptableObjectInstancer newPassthroughInstancer)
        {
            passthroughInstancer = newPassthroughInstancer;
        }

/// <summary>
/// Combines the instances from the source into this instancer.
/// </summary>
/// <param name="sourceInstances">The instances to add to the Instancers list</param>
/// <param name="newInstancesOverrideOld">Should new instances replace the current instances</param>
        public void MergeInstancesIntoThisInstancer(Dictionary<ScriptableObject, ScriptableObject> sourceInstances, bool newInstancesOverrideOld)
        {
            foreach(var key in sourceInstances.Keys)
            {
                if (globalToInstanceMap.ContainsKey(key))
                {
                    if (newInstancesOverrideOld)
                    {
                        Destroy(globalToInstanceMap[key]);
                        globalToInstanceMap[key] = sourceInstances[key];
                    }
                    continue;
                }

                globalToInstanceMap.Add(key, sourceInstances[key]);
            }
        }

/// <summary>
/// Sets an instanced <paramref name="globalSharedValue"/> to the value: <paramref name="valueToApply"/>
/// </summary>
/// <typeparam name="TValue">The type of value</typeparam>
/// <typeparam name="TReference">The globalSharedValue of type TValue</typeparam>
/// <param name="instancer">The instancer to get the instance from</param>
/// <param name="valueToApply">the value to apply to the instance</param>
/// <param name="globalSharedValue">the global shared value SO to apply the new value to</param>
        public static void SetInstancedValue<TValue,TReference>(ScriptableObjectInstancer instancer, TValue valueToApply, TReference globalSharedValue)
        where TReference : SharedValue<TValue>
        {
            if (instancer)
                instancer.GetInstance(globalSharedValue).Value = valueToApply;
            else
                globalSharedValue.Value = valueToApply;
        }

/// <summary>
/// Sets an instanced <paramref name="globalSharedValue"/> to the value: <paramref name="valueToApply"/>
/// </summary>
/// <typeparam name="TValue">The type of value</typeparam>
/// <typeparam name="TReference">The globalSharedValue of type TValue</typeparam>
/// <param name="valueToApply">the value to apply to the instance</param>
/// <param name="globalSharedValue">the global shared value SO to apply the new value to</param>
        public void SetInstancedValue<TValue,TReference>(TValue valueToApply, TReference globalSharedValue)
        where TReference : SharedValue<TValue>
        {
            SetInstancedValue(this, valueToApply, globalSharedValue);
        }

/// <summary>
/// Gets the instanced scriptable object, will create one if there isn't one
/// </summary>
/// <param name="sharedValue">the scriptable object to get the instance of</param>
/// <returns>The instanced version of the scriptable object</returns>
        public ScriptableObject GetInstance(ScriptableObject sharedValue)
        {
            if (passthroughInstancer)
            {
                return passthroughInstancer.GetInstance(sharedValue);
            }

            if (globalToInstanceMap.ContainsKey(sharedValue))
            {
                return globalToInstanceMap[sharedValue];
            }
            else
            {
                ScriptableObject newInstance = CreateInstance(sharedValue);
                return newInstance;
            }
        }

/// <summary>
/// Gets the instanced type of the scriptable object, will create one if there isn't one
/// </summary>
/// <param name="sharedValue">the scriptable object to get the instance of</param>
/// <returns>The instanced version of the scriptable object</returns>
        public T GetInstance<T>(T sharedValue) where T : ScriptableObject
        {
            if (passthroughInstancer)
            {
                return passthroughInstancer.GetInstance<T>(sharedValue);
            }

            if(sharedValue == null)
                return null;
            if (globalToInstanceMap.ContainsKey(sharedValue))
            {
                return (T)globalToInstanceMap[sharedValue];
            }
            else
            {
                ScriptableObject newInstance = CreateInstance(sharedValue);
                return (T)newInstance;
            }
        }

/// <summary>
/// Tries to get the instanced shared value, if there is no instance it fails
/// </summary>
/// <param name="sharedValue">The scriptable object to attempt to find instance with</param>
/// <param name="instance">The instanced scriptable object</param>
/// <returns>Returns true if the instance was found</returns>
        public bool TryGetInstance(ScriptableObject sharedValue, out ScriptableObject instance)
        {
            if (passthroughInstancer)
            {
                return passthroughInstancer.TryGetInstance(sharedValue, out instance);
            }

            if (globalToInstanceMap.ContainsKey(sharedValue))
            {
                instance = globalToInstanceMap[sharedValue];
                return true;
            }
            instance = null;
            return false;
        }

/// <summary>
/// Tries to get the instanced shared value, if there is no instance it fails
/// </summary>
/// <param name="sharedValue">The scriptable object to attempt to find instance with</param>
/// <param name="instance">The instanced scriptable object</param>
/// <returns>Returns true if the instance was found</returns>
        public bool TryGetInstance<T>(T sharedValue, out T instance) where T : ScriptableObject
        {
            if (passthroughInstancer)
            {
                return passthroughInstancer.TryGetInstance<T>(sharedValue, out instance);
            }

            if (globalToInstanceMap.ContainsKey(sharedValue))
            {
                instance = (T)globalToInstanceMap[sharedValue];
                return true;
            }
            instance = null;
            return false;
        }


/// <summary>
/// Creates a new scriptable object instance
/// </summary>
/// <param name="sharedValue">The scriptable object to instantiate</param>
/// <param name="overrideCurrentInstance">If there is an old instance should it be overridden</param>
        public void CreateSharedValueInstance(ScriptableObject sharedValue, bool overrideCurrentInstance = false)
        {
            if (passthroughInstancer)
            {
                passthroughInstancer.CreateSharedValueInstance(sharedValue, overrideCurrentInstance);
                return;
            }

            //if there is already an instance
            if(globalToInstanceMap.ContainsKey(sharedValue))
            {
                //if the current instance should be overriden
                if (overrideCurrentInstance)
                {

                    //destroy the old instance
                    Destroy(globalToInstanceMap[sharedValue]);
                    globalToInstanceMap.Remove(sharedValue);

                    CreateInstance(sharedValue);
                }
            }
            //if there isn't already an instance, create it
            else
            {
                CreateInstance(sharedValue);
            }
        }

/// <summary>
/// Instantiates the scriptable object and stores it in the globalToInstance dictionary, there is no null check so will fail if the same SO is attempted to be instantiated more than once
/// </summary>
/// <param name="sharedValue">The scriptable object to instantiate</param>
/// <returns>The instanced scriptable object</returns>
        private ScriptableObject CreateInstance(ScriptableObject sharedValue)
        {
            if (passthroughInstancer)
            {
                return passthroughInstancer.CreateInstance(sharedValue);
            }

            //create the instance
            ScriptableObject newInstance = Instantiate(sharedValue);
            //add the new instance to the dictionary
            globalToInstanceMap.Add(sharedValue, newInstance);
            
            return newInstance;
        }


        private void OnDestroy()
        {
            //destroy all instances when the instancer is destroyed
            foreach(var key in globalToInstanceMap.Keys)
            {
                Destroy(globalToInstanceMap[key]);
            }

            //reset collections
            globalToInstanceMap.Clear();
#if UNITY_EDITOR
            instances.Clear();
#endif
        }
    }
}

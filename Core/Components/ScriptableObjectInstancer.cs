
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
        public ScriptableObjectInstancer passthroughInstancer;

        /// <summary> The dictionary that returns the instances of the inputted Scriptable Objects </summary>
        private Dictionary<ScriptableObject, ScriptableObject> globalToInstanceMap = new();
        public Dictionary<ScriptableObject, ScriptableObject> GlobalToInstanceMap => globalToInstanceMap;
        #if UNITY_EDITOR
        private List<ScriptableObject> instances = new();
        #endif

        public void SetPassthroughInstancer(ScriptableObjectInstancer newPassthroughInstancer)
        {
            passthroughInstancer = newPassthroughInstancer;
        }

        public void MergeInstancesIntoThisInstancer(Dictionary<ScriptableObject, ScriptableObject> sourceInstances, bool newInstancesOverrideOld)
        {
            foreach(var key in sourceInstances.Keys)
            {
                if (globalToInstanceMap.ContainsKey(key))
                {
                    if (newInstancesOverrideOld)
                    {
                        #if UNITY_EDITOR || DEVELOPMENT_BUILD
                        instances.Remove(globalToInstanceMap[key]);
                        instances.Add(sourceInstances[key]);
                        #endif
                        Destroy(globalToInstanceMap[key]);
                        globalToInstanceMap[key] = sourceInstances[key];
                    }
                    continue;
                }

                globalToInstanceMap.Add(key, sourceInstances[key]);
            }
        }

        public static void ApplyValueToInstancer<TValue,TReference>(ScriptableObjectInstancer instancer, TValue value, TReference sharedValue)
        where TValue : struct where TReference : SharedValue<TValue>
        {
            if (instancer)
                instancer.GetInstance(sharedValue).Value = value;
            else
                sharedValue.Value = value;
        }

        public void ApplyValueToInstancer<TValue,TReference>(TValue value, TReference sharedValue)
        where TValue : struct where TReference : SharedValue<TValue>
        {
            ApplyValueToInstancer(this, value, sharedValue);
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
#if UNITY_EDITOR
            instances.Add(newInstance);
#endif
            
            return newInstance;
        }


        private void OnDestroy()
        {
            //destroy all instances when the instancer is destroyed
            foreach(var key in globalToInstanceMap.Keys)
            {
                Destroy(globalToInstanceMap[key]);
            }
#if UNITY_EDITOR
            instances.Clear();
#endif
        }
    }
}

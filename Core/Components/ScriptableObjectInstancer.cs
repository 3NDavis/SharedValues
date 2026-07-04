using UnityEngine;
using System.Collections.Generic;

namespace SharedValues
{
    /// <summary>
    /// A scriptable object instatiation and connection manager
    /// </summary>
    public sealed class ScriptableObjectInstancer : MonoBehaviour
    {
        public ScriptableObjectInstancer passthroughInstancer;

        /// <summary> The dictionary that returns the instances of the inputted Scriptable Objects </summary>
        private Dictionary<ScriptableObject, ScriptableObject> globalToGroupInstanceMap = new();
        #if UNITY_EDITOR
        private List<ScriptableObject> instances = new();
        #endif

        public void SetPassthroughInstancer(ScriptableObjectInstancer newPassthroughInstancer)
        {
            passthroughInstancer = newPassthroughInstancer;
        }

        public void MergeIntoInstancer(Dictionary<ScriptableObject, ScriptableObject> sourceInstances, bool overrideDuplicates)
        {
            foreach(var key in sourceInstances.Keys)
            {
                if (globalToGroupInstanceMap.ContainsKey(key))
                {
                    if (overrideDuplicates)
                    {
                        #if UNITY_EDITOR || DEVELOPMENT_BUILD
                        instances.Remove(globalToGroupInstanceMap[key]);
                        instances.Add(sourceInstances[key]);
                        #endif
                        Destroy(globalToGroupInstanceMap[key]);
                        globalToGroupInstanceMap[key] = sourceInstances[key];
                    }
                    continue;
                }

                globalToGroupInstanceMap.Add(key, sourceInstances[key]);
            }
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

            if (globalToGroupInstanceMap.ContainsKey(sharedValue))
            {
                return globalToGroupInstanceMap[sharedValue];
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
            if (globalToGroupInstanceMap.ContainsKey(sharedValue))
            {
                return (T)globalToGroupInstanceMap[sharedValue];
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

            if (globalToGroupInstanceMap.ContainsKey(sharedValue))
            {
                instance = globalToGroupInstanceMap[sharedValue];
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

            if (globalToGroupInstanceMap.ContainsKey(sharedValue))
            {
                instance = (T)globalToGroupInstanceMap[sharedValue];
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
            if(globalToGroupInstanceMap.ContainsKey(sharedValue))
            {
                //if the current instance should be overriden
                if (overrideCurrentInstance)
                {

                    //destroy the old instance
                    Destroy(globalToGroupInstanceMap[sharedValue]);
                    globalToGroupInstanceMap.Remove(sharedValue);

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
            globalToGroupInstanceMap.Add(sharedValue, newInstance);
#if UNITY_EDITOR
            instances.Add(newInstance);
#endif
            
            return newInstance;
        }


        private void OnDestroy()
        {
            //destroy all instances when the instancer is destroyed
            foreach(var key in globalToGroupInstanceMap.Keys)
            {
                Destroy(globalToGroupInstanceMap[key]);
            }
#if UNITY_EDITOR
            instances.Clear();
#endif
        }
    }
}
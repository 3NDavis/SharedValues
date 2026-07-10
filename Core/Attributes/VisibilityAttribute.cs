
using System;
using UnityEngine;

namespace SharedValues.Attributes
{
    /// <summary>
    /// Sets a criteria for the visibility of a property in the inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class Visibility : PropertyAttribute
    {
        /// <summary>
        /// The property name to validate against
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// The value to validate against
        /// </summary>
        public object Value { get; private set; }


        /// <summary>
        /// Inverts the result of the visibility check
        /// </summary>
        public bool Hide {get; private set;}


        /// <summary>
        /// Sets a criteria for the visibility of a property in the inspector
        /// </summary>
        /// <param name="propertyName">The property name to validate against</param>
        /// <param name="value">The value to compare to</param>
        public Visibility(string propertyName, object value, bool hide = false)
        {
            PropertyName = propertyName;
            Value = value;
            Hide = hide;
        }
    }
}
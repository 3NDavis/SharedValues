
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



using SharedValues.Events.Collections;
using SharedValues.Upgradable;
using UnityEngine;

namespace SharedValues.Samples
{
    [CreateAssetMenu(menuName = "Shared Values/Events/Collections/Mods Domains", fileName = "SharedEvt_Collection_ModsDomainsChange_Name")]
    public class SharedModsDomainsEvent : SharedCollectionChangeEvent<ValueModifierFloat, float>
    {
        
    }

    [System.Serializable]
    public class SharedModsDomainsEventReference : SharedCollectionChangeEventReference<ValueModifierFloat, float>
    {
        
    }
}
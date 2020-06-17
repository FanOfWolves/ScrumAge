using System;
using System.CodeDom;
using ScrumageEngine.Objects.Items;
using System.Collections.Generic;

namespace ScrumageEngine.Objects.Player {
    public class ResourceContainer {

        #region Fields

        private Dictionary<Resource, Int32> resourceDictionary;


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceContainer"/> class.
        /// </summary>
        public ResourceContainer() {
            resourceDictionary = new Dictionary<Resource, Int32>();
        }


        public void AddResource(Resource newResource) {
            Boolean _resourceExists = this.resourceDictionary.ContainsKey(newResource);
            if (_resourceExists)
                this.resourceDictionary[newResource]++;
            else {
                this.resourceDictionary.Add(newResource, 1);
            }
        }

        public Boolean HasResourceAmount(Resource neededResource, Int32 neededAmount) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (!_hasResource) {
                return false;
            }
            else {
                return _amountHere >= neededAmount;
            }
        }

        public Int32 GetResourceAmount(Resource neededResource) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (_hasResource) {
                return _amountHere;
            }
            else {
                return 0;
            }
        }


        public List<Resource> TakeResources(Resource neededResource, Int32 neededAmount) {
            Int32 _amountHere = this.resourceDictionary[neededResource];
            this.resourceDictionary[neededResource] = _amountHere - neededAmount;
            List<Resource> _outputResources = new List<Resource>(neededAmount);
            for (Int32 i = 0; i < neededAmount; i++) {
                _outputResources.Add(neededResource.DeepCopy());
            }
            return _outputResources;
        }

        public Boolean PseudoTakeResources(Resource neededResource, Int32 neededAmount) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (!_hasResource || _amountHere < neededAmount)
                return false;
            this.resourceDictionary[neededResource] = _amountHere - neededAmount;
            return true;
        }



    }
}

using System;
using System.Collections.Generic;
using Cells.Interfaces;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;

namespace Cells.Model.Brain
{
    class BrainDiscoveryManager
    {
        [ImportMany(typeof(IBrain))]
        IEnumerable<IBrain> availableBrains = new List<IBrain>();

        /// <summary>
        /// Brain broker constructor, searches for new MEF IBrain parts
        /// </summary>
        public BrainDiscoveryManager()
        {
            var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            
            var aggregator = new AggregateCatalog();
            aggregator.Catalogs.Add(assemblyCatalog);

            var container = new CompositionContainer(aggregator);
            container.ComposeParts(this);
        }

        /// <summary>
        /// Function returning all the discovered brain types
        /// </summary>
        /// <returns>The list of Braintypes as a list of strings</returns>
        public IEnumerable<String> GetAvailableBrainTypes()
        {
            foreach (IBrain brain in availableBrains)
                yield return brain.GetType().ToString();
        }

        /// <summary>
        /// Function returning a particular brain based on its type
        /// </summary>
        /// <param name="type">The type wished</param>
        /// <returns>The IBrain asked for, null if not found</returns>
        public IBrain GetBrain(Type type)
        { 
            foreach(IBrain brain in availableBrains)
                if (brain.GetType() == type)
                    return brain;

            return null;
        }
    }
}

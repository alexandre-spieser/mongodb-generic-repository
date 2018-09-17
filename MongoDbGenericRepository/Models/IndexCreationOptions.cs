using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbGenericRepository.Models
{
    /// <summary>
    /// Options for creating an index.
    /// </summary>
    public class IndexCreationOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the index is a unique index.
        /// </summary> 
        public bool? Unique { get; set; }
        /// <summary>
        /// Gets or sets the index version for text indexes.
        /// </summary>  
        public int? TextIndexVersion { get; set; }
        /// <summary>
        /// Gets or sets the index version for 2dsphere indexes.
        /// </summary>
        public int? SphereIndexVersion { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the index is a sparse index.
        /// </summary>   
        public bool? Sparse { get; set; }
        /// <summary>
        /// Gets or sets the index name.
        /// </summary>  
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the min value for 2d indexes.
        /// </summary>   
        public double? Min { get; set; }
        /// <summary>
        /// Gets or sets the max value for 2d indexes.
        /// </summary>
        public double? Max { get; set; }
        /// <summary>
        /// Gets or sets the language override.
        /// </summary>   
        public string LanguageOverride { get; set; }
        /// <summary>
        /// Gets or sets when documents expire (used with TTL indexes).
        /// </summary> 
        public TimeSpan? ExpireAfter { get; set; }
        /// <summary>
        /// Gets or sets the default language.
        /// </summary>  
        public string DefaultLanguage { get; set; }
        /// <summary>
        /// Gets or sets the size of a geohash bucket.
        /// </summary>   
        public double? BucketSize { get; set; }
        /// <summary>
        /// Gets or sets the precision, in bits, used with geohash indexes.
        /// </summary>   
        public int? Bits { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to create the index in the background.
        /// </summary> 
        public bool? Background { get; set; }
        /// <summary>
        /// Gets or sets the version of the index.
        /// </summary>   
        public int? Version { get; set; }
    }
}

using MementoMori.AddressableTools.JSON;
using System;
using System.Collections.Generic;
using System.IO;

namespace MementoMori.AddressableTools.Catalog
{
    public class ContentCatalogData
    {
        public string LocatorId { get; set; }
        public ObjectInitializationData InstanceProviderData { get; set; }
        public ObjectInitializationData SceneProviderData { get; set; }
        public ObjectInitializationData[] ResourceProviderData { get; set; }
        public string[] ProviderIds { get; set; }
        public string[] InternalIds { get; set; }
        public SerializedType[] ResourceTypes { get; set; }
        public string[] InternalIdPrefixes { get; set; }

        public Dictionary<object, List<ResourceLocation>> Resources { get; set; }

        internal void Read(ContentCatalogDataJson data)
        {
            LocatorId = data.m_LocatorId;

            InstanceProviderData = new ObjectInitializationData();
            InstanceProviderData.Read(data.m_InstanceProviderData);

            SceneProviderData = new ObjectInitializationData();
            SceneProviderData.Read(data.m_SceneProviderData);

            ResourceProviderData = new ObjectInitializationData[data.m_ResourceProviderData.Length];
            for (var i = 0; i < ResourceProviderData.Length; i++)
            {
                ResourceProviderData[i] = new ObjectInitializationData();
                ResourceProviderData[i].Read(data.m_ResourceProviderData[i]);
            }

            ProviderIds = new string[data.m_ProviderIds.Length];
            for (var i = 0; i < ProviderIds.Length; i++)
            {
                ProviderIds[i] = data.m_ProviderIds[i];
            }

            InternalIds = new string[data.m_InternalIds.Length];
            for (var i = 0; i < InternalIds.Length; i++)
            {
                InternalIds[i] = data.m_InternalIds[i];
            }

            ResourceTypes = new SerializedType[data.m_resourceTypes.Length];
            for (var i = 0; i < ResourceTypes.Length; i++)
            {
                ResourceTypes[i] = new SerializedType();
                ResourceTypes[i].Read(data.m_resourceTypes[i]);
            }

            InternalIdPrefixes = new string[data.m_InternalIdPrefixes.Length];
            for (var i = 0; i < InternalIdPrefixes.Length; i++)
            {
                InternalIdPrefixes[i] = data.m_InternalIdPrefixes[i];
            }

            ReadResources(data);
        }

        private void ReadResources(ContentCatalogDataJson data)
        {
            List<Bucket> buckets;

            var bucketStream = new MemoryStream(Convert.FromBase64String(data.m_BucketDataString));
            using (var bucketReader = new BinaryReader(bucketStream))
            {
                var bucketCount = bucketReader.ReadInt32();
                buckets = new List<Bucket>(bucketCount);

                for (var i = 0; i < bucketCount; i++)
                {
                    var offset = bucketReader.ReadInt32();

                    var entryCount = bucketReader.ReadInt32();
                    var entries = new int[entryCount];
                    for (var j = 0; j < entryCount; j++)
                    {
                        entries[j] = bucketReader.ReadInt32();
                    }

                    buckets.Add(new Bucket(offset, entries));
                }
            }

            List<object> keys;

            var keyDataStream = new MemoryStream(Convert.FromBase64String(data.m_KeyDataString));
            using (var keyReader = new BinaryReader(keyDataStream))
            {
                var keyCount = keyReader.ReadInt32();
                keys = new List<object>(keyCount);

                for (var i = 0; i < keyCount; i++)
                {
                    keyDataStream.Position = buckets[i].offset;
                    keys.Add(SerializedObjectDecoder.Decode(keyReader));
                }
            }

            List<ResourceLocation> locations;

            var entryDataStream = new MemoryStream(Convert.FromBase64String(data.m_EntryDataString));
            var extraDataStream = new MemoryStream(Convert.FromBase64String(data.m_ExtraDataString));
            using (var entryReader = new BinaryReader(entryDataStream))
            using (var extraReader = new BinaryReader(extraDataStream))
            {
                var entryCount = entryReader.ReadInt32();
                locations = new List<ResourceLocation>(entryCount);

                for (var i = 0; i < entryCount; i++)
                {
                    var internalIdIndex = entryReader.ReadInt32();
                    var providerIndex = entryReader.ReadInt32();
                    var dependencyKeyIndex = entryReader.ReadInt32();
                    var depHash = entryReader.ReadInt32();
                    var dataIndex = entryReader.ReadInt32();
                    var primaryKeyIndex = entryReader.ReadInt32();
                    var resourceTypeIndex = entryReader.ReadInt32();

                    var internalId = InternalIds[internalIdIndex];

                    var providerId = ProviderIds[providerIndex];

                    object dependencyKey = null;
                    if (dependencyKeyIndex >= 0)
                    {
                        dependencyKey = keys[dependencyKeyIndex];
                    }

                    object objData = null;
                    if (dataIndex >= 0)
                    {
                        extraDataStream.Position = dataIndex;
                        objData = SerializedObjectDecoder.Decode(extraReader);
                    }

                    var primaryKey = keys[primaryKeyIndex];
                    var resourceType = ResourceTypes[resourceTypeIndex];

                    var loc = new ResourceLocation();
                    loc.ReadCompact(internalId, providerId, dependencyKey, objData, depHash, primaryKey, resourceType);
                    locations.Add(loc);
                }
            }

            Resources = new Dictionary<object, List<ResourceLocation>>(buckets.Count);
            for (var i = 0; i < buckets.Count; i++)
            {
                var bucketEntries = buckets[i].entries;
                var locs = new List<ResourceLocation>(bucketEntries.Length);
                for (var j = 0; j < bucketEntries.Length; j++)
                {
                    locs.Add(locations[bucketEntries[j]]);
                }
                Resources[keys[i]] = locs;
            }
        }

        private struct Bucket
        {
            public int offset;
            public int[] entries;

            public Bucket(int offset, int[] entries)
            {
                this.offset = offset;
                this.entries = entries;
            }
        }
    }
}
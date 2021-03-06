﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FragmentLibrary.Controllers.Viewmodels.Fragment;
using FragmentLibrary.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FragmentLibrary.Repository
{
    public class FragmentImageRepository
    {
        public const string BUCKET_NAME = "fragmentScanImages";

        GridFSBucket _bucket;
        private IMongoCollection<GridFSFileInfo> _filesCollection;


        public FragmentImageRepository(MongoClient dbClient)
        {
            var database = dbClient.GetDatabase("fragmentImages");
            _bucket = new GridFSBucket(database, new GridFSBucketOptions
            {
                BucketName = BUCKET_NAME,
                ChunkSizeBytes = 1048576, // 1MB
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Primary
            });
            _filesCollection = database.GetCollection<GridFSFileInfo>(BUCKET_NAME + ".files");

        }

        public async Task<string> Add(ScanImage image)
        {
            var id = await _bucket.UploadFromBytesAsync(Guid.NewGuid().ToString(), image.Data, new GridFSUploadOptions()
            {
                Metadata = new BsonDocument
                {
                    { nameof(ScanImage.Format), image.Format.Extension }
                }
            });
            return id.ToString();
        }

        public async Task<ScanImage> Find(string imageId)
        {
            var id = new ObjectId(imageId);
            var bytes = await _bucket.DownloadAsBytesAsync(id);
            FilterDefinitionBuilder<GridFSFileInfo> builder = Builders<GridFSFileInfo>.Filter;
            FilterDefinition<GridFSFileInfo> filter = builder.Eq("_id", id);
            var fileInfo = await _filesCollection.Find(filter).SingleAsync();
            string formatStr = fileInfo.Metadata.GetValue(nameof(ScanImage.Format)).AsString;
            return ScanImage.BuidFromRepository(id.ToString(), bytes, ImageFormat.FromExtension(formatStr));
        }
    }
}

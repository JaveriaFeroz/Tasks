using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.Text;


    var connectionString = "DefaultEndpointsProtocol=https;AccountName=softechblobstorage;AccountKey=AuNJWUf9X+utGzqyY8Bm246KvAJzLqMds6SZJc4jl90P2+1MtCCk3us85U2HbZ4bX0H93y3luIbC+AStkx2Pwg==;EndpointSuffix=core.windows.net";
    var containerClient = new BlobContainerClient(connectionString, "test");
    containerClient.CreateIfNotExists();
    var filePath = @"C:\abc\README.txt";

    var blockBlobClient = containerClient.GetBlockBlobClient(filePath);
    int blockSize = 1 * 1024 * 1024;//1 MB Block
    int offset = 0;
    int counter = 0;
    List<string> blockIds = new List<string>();

    using (var fs = File.OpenRead(@"C:\abc\README.txt"))
    {
        var bytesRemaining = fs.Length;
        do
        {
            var dataToRead = Math.Min(bytesRemaining, blockSize);
            byte[] data = new byte[dataToRead];
            var dataRead = fs.Read(data, offset, (int)dataToRead);
            bytesRemaining -= dataRead;
            if (dataRead > 0)
            {
                var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(counter.ToString("d6")));
                blockBlobClient.StageBlock(blockId, new MemoryStream(data));
                Console.WriteLine(string.Format("Block {0} uploaded successfully.", counter.ToString("d6")));
                blockIds.Add(blockId);
                counter++;
            }
        }
        while (bytesRemaining > 0);
        Console.WriteLine("All blocks uploaded. Now committing block list.");
        var headers = new BlobHttpHeaders()
        {
            ContentType = "file"
        };
        blockBlobClient.CommitBlockList(blockIds, headers);
        Console.WriteLine("Blob uploaded successfully!");
    }



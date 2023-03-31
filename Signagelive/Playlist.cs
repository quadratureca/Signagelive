namespace Signagelive
{
    public class Playlist
    {
        public string name { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime lastModified { get; set; }
        public int size { get; set; }
        public PlaylistMediaAsset[] mediaAssets { get; set; }
        public bool includeInProofOfPlay { get; set; }
        public string thumbnailUrl { get; set; }
        public int id { get; set; }
    }

    public class PlaylistMediaAsset
    {
        public int duration { get; set; }
        public int position { get; set; }
        public bool includeInProofOfPlay { get; set; }
        public Mediaasset mediaAsset { get; set; }
        public Validity validity { get; set; }
        public Condition condition { get; set; }
        public MrssPlayOptions mrssPlayOptions { get; set; }
        public int id { get; set; }

        public PlaylistMediaAsset(Mediaasset mediaasset)
        {
            MrssPlayOptions mrssPlayOptions = new MrssPlayOptions
            {
                mode = null
            };
            this.duration = 0;
            this.position = 0;
            this.includeInProofOfPlay = false;
            this.mediaAsset = mediaasset;
            this.validity = null;
            this.condition = null;
            this.mrssPlayOptions = mrssPlayOptions;
        }
    }

    public class Mediaasset
    {
        public string name { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime lastUsed { get; set; }
        public int duration { get; set; }
        public int size { get; set; }
        public bool supported { get; set; }
        public bool includeInProofOfPlay { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string type { get; set; }
        public string hash { get; set; }
        public string hashType { get; set; }
        public int id { get; set; }
        public string url { get; set; }
    }

    public class Validity
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int Days { get; set; }
    }

    public class Condition
    {
        public string tags { get; set; }
        public bool include { get; set; }
    }

    public class MrssPlayOptions
    {
        public string mode { get; set; }
    }

    public class Thumbnails
    {
        public string videoH264 { get; set; }
        public string imageSmall { get; set; }
        public string imageLarge { get; set; }
    }
}

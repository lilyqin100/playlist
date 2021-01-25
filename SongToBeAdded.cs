using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class SongToBeAdded
    {
        [JsonPropertyName("playlist_id")]
        public string PlaylistId  { get; set; }

        [JsonPropertyName("song_id")]
        public string SongIdToBeAdded { get; set; }
    }
}

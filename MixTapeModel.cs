using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class MixTapeModel
    {
        [JsonPropertyName("users")]
        public List<User> users { get; set; }

        [JsonPropertyName("playlists")]
        public List<Playlist> playlists { get; set; }

        [JsonPropertyName("songs")]
        public List<Song> songs { get; set; }

    }
}

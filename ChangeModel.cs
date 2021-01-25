using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class ChangeModel
    {
        [JsonPropertyName("AddPlaylist")]
        public Playlist playlist_toBeAdded { get; set; }

        [JsonPropertyName("RemovePlaylist")]
        public PlaylistToBeRemoved PlayListToBeRemoved{ get; set; }

        [JsonPropertyName("AddSong")]
        public SongToBeAdded songToBeAdded{ get; set; }
    }
}

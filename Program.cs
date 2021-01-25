using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net;
using System.Text.Json;

namespace HighSpot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World 2021!");

            var options = new JsonSerializerOptions
            {
               // PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            // Read input JSON string into myMoel object
            var mixtapeString = File.ReadAllText("C:\\Src\\HighSpot\\mixtape.json");
            var mixTapeModel = JsonSerializer.Deserialize<MixTapeModel>(mixtapeString, options);

            // Take in change file
            var changeString = File.ReadAllText("C:\\Src\\HighSpot\\changes.json");
            var changeModel = JsonSerializer.Deserialize<ChangeModel>(changeString, options);

            // Process models in memory to get the output object model, ASSUMING both JSON file size is not large and can be hold in memory

            //  Step 1: Add new playlist
            AddPlayList(ref mixTapeModel, changeModel.playlist_toBeAdded);

            // Step 2: Remove the existing playlist
            RemovePlayList(ref mixTapeModel, changeModel.PlayListToBeRemoved);

            // Step 3: Add an existing song to an existing playlist
            AddSongToPlayList(ref mixTapeModel, changeModel.songToBeAdded);
            

            // Write the output.json file
            WriteToJsonFile(mixTapeModel, "C:\\Src\\HighSpot\\output.json");

        }

        static void AddPlayList( ref MixTapeModel model, Playlist toBeAddedPlaylist)
        {
            try
            {
                model.playlists.Add(toBeAddedPlaylist);
            }
            catch (Exception e)
            {
                throw new Exception("Error Adding playlist");
            }
        }

        static void RemovePlayList(ref MixTapeModel model, PlaylistToBeRemoved toBeRemovedPlaylist)
        {
            // Scan the existing playlist to find the playlistID to be removed
            foreach (var playlist in model.playlists)
            {
                if (String.Equals(toBeRemovedPlaylist.Id, playlist.Id))
                {
                    model.playlists.Remove(playlist);
                    return;
                }
            }
        }

        static void AddSongToPlayList(ref MixTapeModel model, SongToBeAdded song)
        {
            foreach (var playlist in model.playlists)
            {
                if (String.Equals(song.PlaylistId, playlist.Id))
                {
                    try
                    {
                        playlist.Song_Ids.Add(song.SongIdToBeAdded);
                    }
                    catch
                    {
                        throw new Exception("Error Add Song ID, it may already exists, or other errors");
                    }

                    return;
                }
            }
        }

        static void WriteToJsonFile(MixTapeModel mixTapeModel, string outputFile)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var modelJson = JsonSerializer.Serialize(mixTapeModel, options);
            File.WriteAllText(outputFile, modelJson);
        }
    }
}
